using LightCli.Args;
using LightCli.Attributes;
using LightCli.Playground.Commands;
using LightCli.Printers;
using LightCli.Printers.Custom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LightCli.Commands;

namespace LightCli.Playground
{
    internal class Program
    {
        public class MyArgs : IArgs
        {
            [IndexArg(0, "test item")]
            public Uri Link { get; set; }

            [IndexArg(1)]
            public DateTime Date { get; set; }
        }

        public class Customer : ICustomColor<Customer>, ICustomFormat<Customer>
        {
            [Print(order: 4)]
            public int Id { get; set; }

            [Print(order: 2, title: "Full Name", maxSize: 13, postTextWhenBreak: "...", color: ConsoleColor.Yellow)]
            public string Name { get; set; }

            [Print(order: 1, title: "R$", color: ConsoleColor.Green)]
            public double Money { get; set; }

            public ConsoleColor? CustomColor(string propertyName, Customer customer)
            {
                // customizing the color for the "Money" property when less than zero
                if (propertyName == "Money" && customer.Money < 0)
                    return ConsoleColor.Red;

                // return null if you don't want to do others customizations
                return null;
            }

            public string CustomFormat(string propertyName, Customer customer)
            {
                // customizing the formatting for the "Name" property
                if (propertyName == "Name")
                    return customer.Name.ToUpper();

                // return null if you don't want to do others customizations
                return null;
            }
        }

        private static async Task Main(string[] args)
        {
            //await Print();
            await BasicCommand();
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
            //var args = new[] { "update", "-id", "1", "-n", "John" };
            var args = new[] { "--version" };

            var runner = new CliRunner();
            runner
                .AddVersionCommand()
                .AddCommand(new UpdateCommand());
            // add all the others commands

            var result = await runner.Run(args);

            //runner.ShowDefaultAvailableCommandsMessage();
            //Console.WriteLine(result.Message);
            //result.Command.ShowDefaultHelp();
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
            var args = new[] { "http://google.com", "20/10/2021" };

            var runner = new CliRunner();

            var result = await runner.RunWithoutCommand<MyArgs>(args, async myArgs =>
            {
                Console.WriteLine(myArgs.Link);
                Console.WriteLine(myArgs.Date);
            });

            Console.WriteLine(result.Message);

            result.ShowDefaultArgsHelp();
        }
    }
}