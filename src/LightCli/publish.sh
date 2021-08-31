#!/bin/sh

# PUBLISHING USING DOCKER CONTAINER BECAUSE DOTNET 5 ISN'T SUPPORTED BY MAC M1
# docker container run --env NUGET_API_KEY=$NUGET_API_KEY --rm -v $(pwd):/proj mcr.microsoft.com/dotnet/sdk:5.0 proj/publish.sh

# Install mono
apt install apt-transport-https dirmngr gnupg ca-certificates
apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb https://download.mono-project.com/repo/debian stable-buster main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
apt update
apt install mono-devel -y

# Download the latest stable `nuget.exe` to `/usr/local/bin`
curl -o /usr/local/bin/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

cd proj
dotnet build .
dotnet pack -c Release -o publish
cd publish
mono /usr/local/bin/nuget.exe push *.nupkg -Source https://api.nuget.org/v3/index.json -ApiKey $NUGET_API_KEY