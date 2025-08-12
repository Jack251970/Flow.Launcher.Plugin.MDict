using MDict.Csharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace Flow.Launcher.Plugin.MDict;

public class Main : IPlugin, IPluginI18n, ISettingProvider, IDisposable
{
    internal static PluginInitContext Context { get; private set; } = null!;

    internal static Settings Settings { get; private set; } = null!;

    internal const string VirtualHost = "appassets";

    internal static MdxDict? Dict { get; private set; }
    internal static string? DictDirectory { get; private set; }

    internal static Dictionary<string, string> WebView2PathMapping { get; } = new();

    private readonly static string ClassName = nameof(Main);

    #region IPlugin Interface

    public List<Result> Query(Query query)
    {
        if (Dict == null)
        {
            return new List<Result>
            {
                new()
                {
                    Title = Localize.flowlauncher_plugin_mdict_plugin_please_select_dictionary(),
                    IcoPath = Context.CurrentPluginMetadata.IcoPath,
                    Action = _ =>
                    {
                        Context.API.OpenSettingDialog();
                        return true;
                    }
                }
            };
        }

        var querySearch = query.Search;
        if (string.IsNullOrEmpty(querySearch))
        {
            return new List<Result>
            {
                new()
                {
                    Title = Localize.flowlauncher_plugin_mdict_plugin_please_type_query(),
                    IcoPath = Context.CurrentPluginMetadata.IcoPath
                }
            };
        }
        else
        {
            var results = new List<Result>();
            var score = Settings.FuzzySize;
            foreach (var word in Dict.FuzzySearch(querySearch, Settings.FuzzySize, (uint)Settings.Distance))
            {
                results.Add(new Result
                {
                    Title = querySearch == word.KeyText ? $"{word.KeyText} √" : word.KeyText,
                    AutoCompleteText = word.KeyText,
                    IcoPath = Context.CurrentPluginMetadata.IcoPath,
                    Action = _ =>
                    {
                        Context.API.CopyToClipboard(word.KeyText);
                        return false;
                    },
                    CopyText = word.KeyText,
                    Score = score,
                    AddSelectedCount = false,
                    PreviewPanel = new Lazy<UserControl>(() => new PreviewPanel(word))
                });
                score--;
            }
            return results;
        }
    }

    public void Init(PluginInitContext context)
    {
        Context = context;

        // Init settings
        Settings = context.API.LoadSettingJsonStorage<Settings>();
        Settings.PropertyChanged += Settings_PropertyChanged;
        Context.API.LogDebug(ClassName, $"Init: {Settings}");

        // Load dictionary
        LoadDictionary(Settings.DictPath);
    }

    private void Settings_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Settings.DictPath):
                if (File.Exists(Settings.DictPath))
                {
                    LoadDictionary(Settings.DictPath);
                }
                break;
        }
    }

    #endregion

    #region Dictionary Management

    private static void LoadDictionary(string path)
    {
        // Check path exists
        if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;

        // Close the previous dictionary if it exists
        Dict?.Close();

        // Load the new dictionary
        try
        {
            Dict = new MdxDict(path);
        }
        catch (Exception ex)
        {
            Context.API.ShowMsg(Localize.flowlauncher_plugin_mdict_plugin_error_loading_dictionary());
            Context.API.LogException(ClassName, $"Failed to load dictionary from {path}", ex);
            Dict = null;
            return;
        }

        // Load the web view paths
        DictDirectory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(DictDirectory))
        {
            WebView2PathMapping.Clear();
            foreach (var file in Directory.EnumerateFiles(DictDirectory))
            {
                var relatedPath = Path.GetRelativePath(DictDirectory, file);
                if (!string.IsNullOrEmpty(relatedPath))
                {
                    // If related path contains backslashes or forward slashes, we need to add two versions of replacement rules
                    if (relatedPath.Contains('\\'))
                    {
                        var unixRelatedPath = relatedPath.Replace('\\', '/');
                        WebView2PathMapping[relatedPath] = $"https://{VirtualHost}/{unixRelatedPath}";
                        WebView2PathMapping[unixRelatedPath] = $"https://{VirtualHost}/{unixRelatedPath}";
                    }
                    else if (relatedPath.Contains('/'))
                    {
                        var winRelatedPath = relatedPath.Replace('/', '\\');
                        WebView2PathMapping[relatedPath] = $"https://{VirtualHost}/{relatedPath}";
                        WebView2PathMapping[winRelatedPath] = $"https://{VirtualHost}/{relatedPath}";
                    }
                    // Else just add the related path as it is
                    else
                    {
                        WebView2PathMapping[relatedPath] = $"https://{VirtualHost}/{relatedPath}";
                    }
                }
            }
        }
    }

    #endregion

    #region IPluginI18n Interface

    public string GetTranslatedPluginTitle()
    {
        return Localize.flowlauncher_plugin_mdict_plugin_name();
    }

    public string GetTranslatedPluginDescription()
    {
        return Localize.flowlauncher_plugin_mdict_plugin_description();
    }

    public void OnCultureInfoChanged(CultureInfo cultureInfo)
    {

    }

    #endregion

    #region ISettingProvider Interface

    public Control CreateSettingPanel()
    {
        Context.API.LogDebug(ClassName, $"Settings Panel: {Settings}");
        return new SettingsPanel(Settings);
    }

    #endregion

    #region IDisposable

    private bool _disposed;

    public void Dispose()
    {
        if (_disposed)
        {
            Dict?.Close();
            return;
        }
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposed = true;
        }
    }

    #endregion
}
