using GMSL;
using UndertaleModLib;
using UndertaleModLib.Models;

namespace ImGUIGM;

public class Mod : IGMSLMod
{

    private UndertaleExtensionFile _extension;
    private uint _currentId = 1;
    private UndertaleData _data;

    public void Load(UndertaleData data)
    {
        _data = data;

        SetupExtension();

        CreateFunction("imgui_init", 
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String, 
                UndertaleExtensionVarType.String, 
                UndertaleExtensionVarType.String 
        });

        CreateFunction("imgui_destroy",
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String, 
        });

        CreateFunction("imgui_newframe", new UndertaleExtensionVarType[] {});
        CreateFunction("imgui_render", new UndertaleExtensionVarType[] {});
        
        CreateFunction("imgui_begin",
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String, 
        });

        CreateFunction("imgui_end", new UndertaleExtensionVarType[] {});

        CreateFunction("imgui_text",
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String, 
        });

        CreateFunction("imgui_separator", new UndertaleExtensionVarType[] {});

        CreateFunction("imgui_separator_text",
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String, 
        });

        CreateFunction("imgui_input_real", 
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String, 
                UndertaleExtensionVarType.Double, 
                UndertaleExtensionVarType.String 
        });

        CreateFunction("imgui_input_text", 
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String,
                UndertaleExtensionVarType.String 
        });

        CreateFunction("imgui_button",
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String, 
        });

        CreateFunction("imgui_checkbox",
            new UndertaleExtensionVarType[] { 
                UndertaleExtensionVarType.String, 
        });
    }

    private void CreateFunction(string name, UndertaleExtensionVarType[] args)
    {
        var fn = new UndertaleExtensionFunction()
        {
            Name = _data.Strings.MakeString(name),
            ExtName = _data.Strings.MakeString(name),
            Kind = 11,
            ID = _currentId
        };

        foreach (var arg in args)
        {
            fn.Arguments.Add(new(arg));
        }

        _extension.Functions.Add(fn);
        _currentId++;
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
