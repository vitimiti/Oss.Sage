using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Oss.Sage.Launcher.Views;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    [SuppressMessage(
        "csharpsquid",
        "S2325:Methods and properties that don't access instance data should be static",
        Justification = "Accesses instance data through Avalonia XAML."
    )]
    public void Next(object source, RoutedEventArgs args) => Mods.Next();

    [SuppressMessage(
        "csharpsquid",
        "S2325:Methods and properties that don't access instance data should be static",
        Justification = "Accesses instance data through Avalonia XAML."
    )]
    public void Previous(object source, RoutedEventArgs args) => Mods.Previous();
}
