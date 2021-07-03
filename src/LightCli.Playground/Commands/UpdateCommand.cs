using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LightCli.Commands;

namespace LightCli.Playground.Commands
{
    public class UpdateCommand : Command<UpdateCommandArgs>
    {
        public override string CommandName => "update";
        public override string Description => "Update a customer information";
        public override string ExampleUsage => $"{CommandName} -id 1 -n John";
        protected override async Task Run(UpdateCommandArgs args)
        {
            // your code here...
        }
    }
}
