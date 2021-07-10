namespace LightCli.Printers.Custom
{
    /// <summary>
    /// Format customizations
    /// </summary>
    /// <typeparam name="T">Type of the class that has [Print] attributes</typeparam>
    public interface ICustomFormat<in T>
    {
        string CustomFormat(string propertyName, T customer);
    }
}