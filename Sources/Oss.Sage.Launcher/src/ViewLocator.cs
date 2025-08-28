using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Oss.Sage.Launcher.ViewModels;

namespace Oss.Sage.Launcher;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
        {
            return null;
        }

        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type is not null)
        {
            return Activator.CreateInstance(type) as Control;
        }

        return new TextBlock { Text = $"Not Found: {name}" };
    }

    public bool Match(object? data) => data is ViewModelBase;
}
