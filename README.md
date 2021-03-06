# Light CLI

---

## Overview

LightCli is a helper for creating memorable command line interfaces for .NET. Never worry about the args[] parameter again! :)

This main goal of the light cli is to help developers to manipulate the **args** on the **program Main**, besides other functionalities like printing a table.

Deal with **args** is hard and easy to break your code:

```csharp
static async Task Main(string[] args)
{ 
    var id = Convert.ToInt32(args[0]); // don't do this
}
```

Light Cli is extremely simple to use and there are some ways to use depending your goal.

## Named arguments

Name arguments are represented as a class with **NamedArg** configuration attributes, as showed below:

```csharp
public class MyArgs : IArgs
{
    [NamedArg(shortName:"-id", fullName:"--identifier", description:"Customer's ID")]
    public int Id { get; set; }

    [NamedArg(shortName:"-n", description: "Customer's name", required:false, defaultValue:"Unknown")]
    public string Name { get; set; }
}
```

## Index arguments

Index arguments are also represented as a class, but with **IndexArg** configuration attributes, as showed below:

```csharp
public class MyArgs : IArgs
{
    [IndexArg(0, description: "Customer's ID")]
    public int Id { get; set; }

    [IndexArg(1, description: "Customer's name", required: false, defaultValue: "Unknown")]
    public string Name { get; set; }
}
```

## Commands usage

You can configure your CLI with as many commands you want. First, define your command:

```csharp
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
```

And then, you just have to configure the **Runner** and run:

```csharp
static async Task Main(string[] args)
{ 
    var runner = new CliRunner();
    runner.AddCommand(new UpdateCommand());
    // add all the others commands

    await runner.Run(args);
}
```

## No Commands usage

There is another simple option to use the CLI without commands. For it, just call the operation **RunWithoutCommand**:

```csharp
static async Task Main(string[] args)
{ 
    var runner = new CliRunner();

    var result = await runner.RunWithoutCommand<MyArgs>(args, async myArgs =>
    {
        // your code here...
    });
}
```

## Manipulating the command result

You can define custom logics with the your command result. We have some ready operations to help you with:

```csharp
static async Task Main(string[] args)
{ 
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
```

You can manipulate results from an operation without commands, like below:

```csharp
static async Task Main(string[] args)
{ 
    var runner = new CliRunner();

    var result = await runner.RunWithoutCommand<MyArgs>(args, async myArgs =>
    {
        // your code here...
    });

    // all good, then break
    if (result.Success)
        return;
    
    // write the message indicating the problem
    Console.WriteLine(result.Message);

    // show the help message for the args (in this case, MyArgs)
    result.ShowDefaultArgsHelp();
}
```

## Printing a table

Now is quite easy to print a pretty table. Everything you have to do is decorate your members class with the Print attribute and use our **TablePrinter**.

```csharp
public class Customer
{
    [Print(order: 1)]
    public int Id { get; set; }

    [Print(order: 2, title: "Full Name")]
    public string Name { get; set; }

    [Print(order: 3, title: "R$")]
    public double Money { get; set; }
}

static async Task Main(string[] args)
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
```

The result is like this:

![table1.png](img/table1.png)

## Printing a table - Basic customizations

You can do basic customizations directly on the attribute [Print], as showed below:

```csharp
public class Customer
{
    [Print(order: 1)]
    public int Id { get; set; }

    [Print(order: 2, title: "Full Name", maxSize: 13, postTextWhenBreak: "...", color: ConsoleColor.Yellow)]
    public string Name { get; set; }

    [Print(order: 3, title: "R$", color: ConsoleColor.Green)]
    public double Money { get; set; }            
}
```

The result should be:

![table2.png](img/table2.png)

## Printing a table - Advanced customizations

Advanced customizations allows you change de format and color from a specific item. You can implement it as you want:

```csharp
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
```

The result:

![table3.png](img/table3.png)
