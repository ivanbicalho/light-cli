using LightCli.Exceptions;
using LightCli.Tests.ConfigurationTests.InvalidArgs;
using System.Threading.Tasks;
using Xunit;

namespace LightCli.Tests.ConfigurationTests
{
    public class InvalidConfigurationTests
    {
        [Fact]
        public async Task InvalidDefaultIndexArgs_Invalid()
        {
            var args = new[] { "generic", "1" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<InvalidDefaultIndexArgs>(args));
        }

        [Fact]
        public async Task InvalidDefaultNamedArgs_Invalid()
        {
            var args = new[] { "generic", "1" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<InvalidDefaultNamedArgs>(args));
        }

        [Fact]
        public async Task InvalidNoArgs_Invalid()
        {
            var args = new[] { "generic" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<InvalidNoArgs>(args));
        }

        [Fact]
        public async Task InvalidRequiredIndexArgs_Invalid()
        {
            var args = new[] { "generic", "1", "2" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<InvalidRequiredIndexArgsA>(args));

            args = new[] { "generic", "1", "2", "3" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<InvalidRequiredIndexArgsB>(args));

            args = new[] { "generic", "1" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<InvalidRequiredIndexArgsC>(args));
        }

        [Fact]
        public async Task InvalidRequiredNamedArgs_Invalid()
        {
            var args = new[] { "generic", "1" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<InvalidRequiredNamedArgs>(args));
        }

        [Fact]
        public async Task NamedIndexArgs_Invalid()
        {
            var args = new[] { "generic", "1" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<NamedIndexArgs>(args));
        }

        [Fact]
        public async Task NamelessNamedArgs_Invalid()
        {
            var args = new[] { "generic", "1" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<NamelessNamedArgs>(args));
        }

        [Fact]
        public async Task NegativeIndexArgs_Invalid()
        {
            var args = new[] { "generic", "1" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<NegativeIndexArgs>(args));
        }

        [Fact]
        public async Task SameIndexArgs_Invalid()
        {
            var args = new[] { "generic", "1", "2" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<SameIndexArgs>(args));
        }

        [Fact]
        public async Task SameNamedArgs_Invalid()
        {
            var args = new[] { "generic", "1", "2" };
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<SameNamedArgsA>(args));
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<SameNamedArgsB>(args));
            await Assert.ThrowsAsync<InvalidConfigurationException>(async () => await Util.Run<SameNamedArgsC>(args));
        }
    }
}