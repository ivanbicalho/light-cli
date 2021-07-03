using System.Threading.Tasks;
using LightCli.Commands;
using LightCli.Tests.ConfigurationTests.ValidArgs;
using Xunit;

namespace LightCli.Tests.ConfigurationTests
{
    public class ValidConfigurationTests
    {
        [Fact]
        public async Task ValidDefaultIndexArgs_Valid()
        {
            var args = new[] { "generic" };
            var command = new GenericCommand<ValidDefaultIndexArgs>();
            await Util.Run(args, command);

            Assert.Equal(1, command.Args.Arg1);
            Assert.Equal("test", command.Args.Arg2);
        }

        [Fact]
        public async Task ValidDefaultNamedArgs_Valid()
        {
            var args = new[] { "generic" };
            var command = new GenericCommand<ValidDefaultNamedArgs>();
            await Util.Run(args, command);

            Assert.Equal(1, command.Args.Arg1);
            Assert.Equal("test", command.Args.Arg2);
        }

        [Fact]
        public async Task ValidIndexArgs_Valid()
        {
            var args = new[] { "generic", "1", "test" };
            var command = new GenericCommand<ValidIndexArgs>();
            await Util.Run(args, command);

            Assert.Equal(1, command.Args.Arg1);
            Assert.Equal("test", command.Args.Arg2);
        }

        [Fact]
        public async Task ValidNamedArgs_Valid()
        {
            var args = new[] { "generic", "-a", "1", "-a2", "test2", "--arg3", "test3" };
            var command = new GenericCommand<ValidNamedArgs>();
            await Util.Run(args, command);

            Assert.Equal(1, command.Args.Arg1);
            Assert.Equal("test2", command.Args.Arg2);
            Assert.Equal("test3", command.Args.Arg3);
        }

        [Fact]
        public async Task ValidRequiredIndexArgs_Valid()
        {
            var args = new[] { "generic", "1" };
            var command = new GenericCommand<ValidRequiredIndexArgsA>();
            await Util.Run(args, command);

            Assert.Equal(1, command.Args.Arg1);
            Assert.Equal(0, command.Args.Arg2);

            args = new[] { "generic" };
            var commandB = new GenericCommand<ValidRequiredIndexArgsB>();
            await Util.Run(args, commandB);

            Assert.Equal(0, commandB.Args.Arg1);
            Assert.Equal(0, commandB.Args.Arg2);
            Assert.Equal(0, commandB.Args.Arg3);
        }
    }
}
