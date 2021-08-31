#!/bin/sh

# PUBLISHING USING DOCKER CONTAINER BECAUSE DOTNET 5 ISN'T SUPPORTED BY MAC M1
docker container run \
    --env NUGET_API_KEY=$NUGET_API_KEY \
    --rm \
    -v $(pwd):/proj mcr.microsoft.com/dotnet/sdk:5.0 \
    proj/publish-steps.sh