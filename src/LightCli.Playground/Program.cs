using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using LightCli.Args;
using LightCli.Attributes;
using LightCli.Commands;
using LightCli.Playground.Commands;
using LightCli.Printers;
using LightCli.Printers.Custom;

namespace LightCli.Playground
{
    class Program
    {
        public class MyArgs : IArgs
        {
            [IndexArg(0)]
            public int Id { get; set; }
        }

        public class Customer : ICustomColor<Customer>
        {
            [Print(1)]
            public int Id { get; set; }

            [Print(2, title: "Full Name", maxSize: 10, postTextWhenBreak: "..", color: ConsoleColor.DarkBlue)]
            public string Name { get; set; }

            [Print(3, title: "R$", color: ConsoleColor.Green)]
            public double Money { get; set; }

            public ConsoleColor? CustomColor(string propertyName, Customer customer)
            {
                if (propertyName == "Money" && customer.Money < 0)
                    return ConsoleColor.Red;

                return null;
            }

            public string CustomTextFormat(string propertyName, Customer customer)
            {
                return "";
            }
        }


        static async Task Main(string[] args)
        {
            await Print();
            //await BasicCommand();
            //await AdvancedCommand();
            //await NoCommand();
        }

        private static async Task Print()
        {
            var items = new List<Customer>
            {
                new Customer {Id = 1, Name = "John", Money = 100.33},
                new Customer {Id = 789, Name = "Silva Custom Name", Money = 2311.21},
                new Customer {Id = 1500, Name = "Maria", Money = -1.12}
            };

            TablePrinter.Print(items);

            await Task.FromResult(0);
        }

        private static async Task BasicCommand()
        {
            var args = new[] { "update", "-id", "1", "-n", "John" };

            var runner = new CliRunner();
            runner.AddCommand(new UpdateCommand());
            // add all the others commands

            await runner.Run(args);
        }

        private static async Task AdvancedCommand()
        {
            var args = new[] { "update", "-id", "1", "-n", "John" };

            var runner = new CliRunner();
            runner.AddCommand(new UpdateCommand());
            // add all the others commands

            var result = await runner.Run(args);

            // all good, then break
            if (result.Success)
                return;

            // write the message indicating the problem
            Console.WriteLine(result.Message);

            // if the command wasn't found, show default message with available commands
            if (result.Command == null)
            {
                runner.ShowDefaultAvailableCommandsMessage();
                return;
            }

            // if the command was found, show the help message for this specific command
            result.Command.ShowDefaultHelp();
        }

        private static async Task NoCommand()
        {
            var args = new[] { "1", "2" };

            var runner = new CliRunner();

            await runner.RunWithoutCommand<MyArgs>(args, async myArgs =>
            {
                // your code here...
            });
        }
    }
}
