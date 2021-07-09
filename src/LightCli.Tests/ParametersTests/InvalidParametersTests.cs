using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LightCli.Tests.ParametersTests
{
    public class InvalidParametersTests
    {
        [Fact]
        public async Task Empty_Args()
        {
            var args = new List<string>().ToArray();
            var result = await Util.Run<NamedArgs>(args);

            Assert.False(result.Success);
            Assert.Null(result.Command);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task Null_Args()
        {
            var result = await Util.Run<NamedArgs>(null);

            Assert.False(result.Success);
            Assert.Null(result.Command);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task Command_Not_Found()
        {
            var args = new[] { "invalid_command_name", "-id", "1", "-n", "test" };
            var result = await Util.Run<NamedArgs>(args);

            Assert.False(result.Success);
            Assert.Null(result.Command);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task DoNot_Send_Require_Parameter()
        {
            var args = new[] { "generic", "-n", "test" };
            var result = await Util.Run<NamedArgs>(args);

            Assert.False(result.Success);
            Assert.NotNull(result.Command);
            Assert.Equal("generic", result.Command.CommandName);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task Send_Invalid_Parameter_Type()
        {
            var args = new[] { "generic", "-id", "test", "-n", "test" };
            var result = await Util.Run<NamedArgs>(args);

            Assert.False(result.Success);
            Assert.NotNull(result.Command);
            Assert.Equal("generic", result.Command.CommandName);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task Send_More_Than_One_Parameter_Same_Name()
        {
            var args = new[] { "generic", "-id", "1", "-id", "2", "-n", "test" };
            var result = await Util.Run<NamedArgs>(args);

            Assert.False(result.Success);
            Assert.NotNull(result.Command);
            Assert.Equal("generic", result.Command.CommandName);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task Send_More_Than_One_Parameter_Diff_Name()
        {
            var args = new[] { "generic", "-id", "1", "--identifier", "2", "-n", "test" };
            var result = await Util.Run<NamedArgs>(args);

            Assert.False(result.Success);
            Assert.NotNull(result.Command);
            Assert.Equal("generic", result.Command.CommandName);
            Assert.NotNull(result.Message);
        }
    }
}