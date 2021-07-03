using System.Threading.Tasks;
using LightCli.Args;

namespace LightCli.Commands
{
    public interface ICommand
    {
        int CommandIndex { get; }
        string CommandName { get; }
        string Description { get; }
        string ExampleUsage { get; }
        void ShowDefaultHelp();
        Task Run(string[] args);
    }
}
