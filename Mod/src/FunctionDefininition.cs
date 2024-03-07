using System.Text.Json.Serialization;
using UndertaleModLib.Models;

public class FunctionDefinition
{
    public string Name { get; set; }
    public UndertaleExtensionVarType ReturnType { get; set; }
    public UndertaleExtensionVarType[] ArgumentTypes { get; set; }
}