﻿using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using GMSL;
using UndertaleModLib;
using UndertaleModLib.Models;

namespace ImGUIGM;

public class Mod : IGMSLMod
{

    private UndertaleExtensionFile _extension;
    private UndertaleData _data;

    public void Load(UndertaleData data, ModInfo info)
    {
        _data = data;
        
        SetupExtension();
        LoadFunctions(Path.Combine(info.ModDir, "defs.json")); 
    }

    private void LoadFunctions(string file)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());

        foreach (var definition in JsonSerializer.Deserialize<List<FunctionDefinition>>(File.ReadAllText(file), options))
        {
            CreateFunction(definition.Name, definition.ArgumentTypes, definition.ReturnType);
        }
    }

    private void CreateFunction(string name, UndertaleExtensionVarType[] args, UndertaleExtensionVarType ret)
    {
        var fn = new UndertaleExtensionFunction()
        {
            Name = _data.Strings.MakeString(name),
            ExtName = _data.Strings.MakeString(name),
            Kind = 11,
            ID = Extension.NextId(),
            RetType = ret
        };

        foreach (var arg in args)
        {
            fn.Arguments.Add(new(arg));
        }

        _extension.Functions.Add(fn);
    }

    private void SetupExtension()
    {
        _extension = new()
        {
            Kind = UndertaleExtensionKind.Dll,
            Filename = _data.Strings.MakeString(Path.Combine(Environment.CurrentDirectory, "libExtension.dll")),
            InitScript = _data.Strings.MakeString(""),
            CleanupScript = _data.Strings.MakeString("")
        };

        UndertaleExtension extension = new()
        {
            Name = _data.Strings.MakeString("ImGUIGM"),
            ClassName = _data.Strings.MakeString(""),
            Version = _data.Strings.MakeString("1.0.0"),
            FolderName = _data.Strings.MakeString("")
        };
        extension.Files.Add(_extension);
        _data.Extensions.Add(extension);
    }
}
