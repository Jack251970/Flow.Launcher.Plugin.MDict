using MDict.Csharp.Models;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Flow.Launcher.Plugin.MDict;

public partial class PreviewPanel : UserControl
{
    public string KeyText => _word.KeyText;

    private readonly FuzzyWord _word;

    public PreviewPanel(FuzzyWord word)
    {
        _word = word ?? throw new ArgumentNullException(nameof(word));
        InitializeComponent();
        // TODO: Use true dark mode if available
        // TODO: Update ResultWebView2 when the system theme changes
        UpdateResultWebView2(true);
    }

    private async void UpdateResultWebView2(bool isDarkTheme)
    {
        if (Main.Dict is null || string.IsNullOrEmpty(Main.DictDirectory) || _word is null || ResultWebView2 is null) return;

        var (_, Definition) = Main.Dict.Fetch(_word);
        if (Definition is null) return;

        var newDefinition = new StringBuilder(Definition);

        // Replace the light and dark theme CSS based on the system theme
        if (!string.IsNullOrEmpty(Main.Settings.LightThemeCss) && !string.IsNullOrEmpty(Main.Settings.DarkThemeCss))
        {
            if (isDarkTheme)
            {
                newDefinition.Replace(Main.Settings.LightThemeCss, Main.Settings.DarkThemeCss);
            }
            else
            {
                newDefinition.Replace(Main.Settings.DarkThemeCss, Main.Settings.LightThemeCss);
            }
        }

        // Replace relative paths with virtual host urls
        foreach (var kvp in Main.WebView2PathMapping)
        {
            newDefinition.Replace(kvp.Key, kvp.Value);
        }

        ResultWebView2.Visibility = Visibility.Visible;

        await ResultWebView2.EnsureCoreWebView2Async();

        ResultWebView2.CoreWebView2.SetVirtualHostNameToFolderMapping(
            Main.VirtualHost,
            Main.DictDirectory,
            Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow
        );

        ResultWebView2.NavigateToString(newDefinition.ToString());
    }
}
