using System;
using System.Collections.Generic;
using System.Text;
using LightCli.Args;
using LightCli.Attributes;

namespace LightCli.Playground.Commands
{
    public class UpdateCommandArgs : IArgs
    {
        [NamedArg(shortName:"-id", fullName:"--identifier", description:"Customer's ID")]
        public int Id { get; set; }

        [NamedArg(shortName:"-n", description: "Customer's name", required:false, defaultValue:"Unknown")]
        public string Name { get; set; }
    }

    public class MyArgs : IArgs
    {
        [IndexArg(0, description: "Customer's ID")]
        public int Id { get; set; }

        [IndexArg(1, description: "Customer's name", required: false, defaultValue: "Unknown")]
        public string Name { get; set; }
    }
}
