namespace LightCli.Printers.Custom
{
    public interface ICustomFormat<in T>
    {
        string CustomFormat(string propertyName, T customer);
    }
}