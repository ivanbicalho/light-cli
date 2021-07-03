using System;
using System.Threading.Tasks;
using LightCli.Args;

namespace LightCli.Commands
{
    public class HelpCommand : Command<NoArgs>
    {
        public override string CommandName => "--help";

        private readonly string _description = "Show an explanation about this CLI";
        public override string Description => _description;

        public override string ExampleUsage => CommandName;

        private readonly string _helpMessage;

        public HelpCommand(string description = null, string helpMessage = null)
        {
            if (description != null)
                _description = description;

            _helpMessage = helpMessage;
        }

        protected override async Task Run(NoArgs args)
        {
            if (_helpMessage != null)
            {
                Console.WriteLine(_helpMessage);
                return;
            }


            
            await Task.FromResult(0);
        }
    }
}