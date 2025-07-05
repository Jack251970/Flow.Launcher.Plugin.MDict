using System.Windows;
using System.Windows.Controls;

namespace Flow.Launcher.Plugin.MDict;

public partial class SettingsPanel : UserControl
{
    public Settings Settings { get; }

    public SettingsPanel(Settings settings)
    {
        Settings = settings;
        DataContext = settings;
        InitializeComponent();
    }

    private void DictPathSelectButton_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new Microsoft.Win32.OpenFileDialog
        {
            Filter = Localize.flowlauncher_plugin_mdict_plugin_md_files()
        };

        if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.FileName))
        {
            Settings.DictPath = dlg.FileName;
        }
    }
}
