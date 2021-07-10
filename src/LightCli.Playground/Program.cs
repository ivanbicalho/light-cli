﻿using LightCli.Args;
using LightCli.Attributes;
using LightCli.Playground.Commands;
using LightCli.Printers;
using LightCli.Printers.Custom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightCli.Playground
{
    internal class Program
    {
        public class MyArgs : IArgs
        {
            [IndexArg(0)]
            public int Id { get; set; }
        }

        //public class Customer : ICustomColor<Customer>
        //{
        //    [Print(1)]
        //    public int Id { get; set; }

        //    [Print(2, title: "Full Name", maxSize: 10, postTextWhenBreak: "..", color: ConsoleColor.DarkBlue)]
        //    public string Name { get; set; }

        //    [Print(3, title: "R$", color: ConsoleColor.Green)]
        //    public double Money { get; set; }

        //    public ConsoleColor? CustomColor(string propertyName, Customer customer)
        //    {
        //        if (propertyName == "Money" && customer.Money < 0)
        //            return ConsoleColor.Red;

        //        return null;
        //    }

        //    public string CustomTextFormat(string propertyName, Customer customer)
        //    {
        //        return "";
        //    }
        //}

        //public ConsoleColor? CustomColor(string propertyName, Customer customer)
        //{
        //    if (propertyName == "Money" && customer.Money < 0)
        //        return ConsoleColor.Red;

        //    return null;
        //}

        public class Customer : ICustomColor<Customer>, ICustomFormat<Customer>
        {
            [Print(order: 1)]
            public int Id { get; set; }

            [Print(order: 2, title: "Full Name", maxSize: 13, postTextWhenBreak: "...", color: ConsoleColor.Yellow)]
            public string Name { get; set; }

            [Print(order: 3, title: "R$", color: ConsoleColor.Green)]
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
            var items = new List<Customer>
            {
                new Customer {Id = 1, Name = "John", Money = 100.33},
                new Customer {Id = 789, Name = "Silva Custom Name", Money = 2311.21},
                new Customer {Id = 1500, Name = "Maria", Money = -1.12}
            };

            TablePrinter.Print(items);

            await Task.FromResult(0);

            //await Print();
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