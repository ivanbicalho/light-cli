using System;
using System.Reflection;
using System.Security;
using System.Threading.Tasks;
using LightCli.Args;

namespace LightCli.Commands
{
    public class VersionCommand : Command<NoArgs>
    {
        public override string CommandName => "--version";
        public override string Description => "Show current CLI version";
        public override string ExampleUsage => "--version";

        private bool _showRevision;

        public VersionCommand(bool showRevision = false)
        {
            _showRevision = showRevision;
        }
        
        protected override Task Run(NoArgs args)
        {
            var v = Assembly.GetCallingAssembly().GetName().Version;
            
            if (v == null)
                return Task.CompletedTask;

            Console.WriteLine(string.Concat($"{v.Major}.{v.Minor}.{v.Build}", 
                _showRevision ? $".{v.Revision}" : string.Empty));

            return Task.CompletedTask;
        }
    }
}