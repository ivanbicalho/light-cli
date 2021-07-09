using System.Threading.Tasks;
using Xunit;

namespace LightCli.Tests.ParametersTests
{
    public class ValidParametersTests
    {
        [Fact]
        public async Task ValidShortNames()
        {
            var args = new[] { "generic", "-id", "1", "-n", "test" };
            var result = await Util.Run<NamedArgs>(args);

            Assert.True(result.Success);
            Assert.NotNull(result.Command);
            Assert.Equal("generic", result.Command.CommandName);
            Assert.Null(result.Message);
        }

        [Fact]
        public async Task ValidFullNames()
        {
            var args = new[] { "generic", "--identifier", "1", "--name", "test" };
            var result = await Util.Run<NamedArgs>(args);

            Assert.True(result.Success);
            Assert.NotNull(result.Command);
            Assert.Equal("generic", result.Command.CommandName);
            Assert.Null(result.Message);
        }
    }
}