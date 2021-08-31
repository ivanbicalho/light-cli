using System;
using System.Reflection;
using System.Security;
using System.Threading.Tasks;
using LightCli.Args;

namespace LightCli.Commands
{
    public static class CliRunnerExtension
    {
        /// <summary>
        /// Add the default version command to show current version: --version
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="showRevision">Shows or not the latest node from version, ex: 1.0.0.453432</param>
        /// <returns></returns>
        public static CliRunner AddVersionCommand(this CliRunner runner, bool showRevision = false)
        {
            return runner.AddCommand(new VersionCommand(showRevision));
        }
    }
}