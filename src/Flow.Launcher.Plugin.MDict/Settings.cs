using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Flow.Launcher.Plugin.MDict;

public class Settings : BaseModel
{
    private string _dictPath = string.Empty;
    public string DictPath
    {
        get => _dictPath;
        set
        {
            if (_dictPath != value)
            {
                _dictPath = value;
                OnPropertyChanged();
            }
        }
    }

    private string _lightThemeCss = string.Empty;
    public string LightThemeCss
    {
        get => _lightThemeCss;
        set
        {
            if (_lightThemeCss != value)
            {
                _lightThemeCss = value;
                OnPropertyChanged();
            }
        }
    }

    private string _darkThemeCss = string.Empty;
    public string DarkThemeCss
    {
        get => _darkThemeCss;
        set
        {
            if (_darkThemeCss != value)
            {
                _darkThemeCss = value;
                OnPropertyChanged();
            }
        }
    }

    private int _fuzzySize = 99;
    public int FuzzySize
    {
        get => _fuzzySize;
        set
        {
            if (_fuzzySize != value)
            {
                _fuzzySize = value;
                OnPropertyChanged();
            }
        }
    }

    private int _distance = 5;
    public int Distance
    {
        get => _distance;
        set
        {
            if (_distance != value)
            {
                _distance = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _enableCheck = false;
    public bool EnableCheck
    {
        get => _enableCheck;
        set
        {
            if (_enableCheck != value)
            {
                _enableCheck = value;
                OnPropertyChanged();
            }
        }
    }

    public void RestoreToDefault()
    {
        var defaultSettings = new Settings();
        var type = GetType();
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (CheckJsonIgnoredOrKeyAttribute(prop))
            {
                continue;
            }
            var defaultValue = prop.GetValue(defaultSettings);
            prop.SetValue(this, defaultValue);
        }
    }

    public override string ToString()
    {
        var type = GetType();
        var props = type.GetProperties();
        var s = props.Aggregate(
            "Settings(\n",
            (current, prop) =>
            {
                if (CheckJsonIgnoredOrKeyAttribute(prop))
                {
                    return current;
                }
                return current + $"\t{prop.Name}: {prop.GetValue(this)}\n";
            }
        );
        s += ")";
        return s;
    }

    private static bool CheckJsonIgnoredOrKeyAttribute(PropertyInfo prop)
    {
        return prop.GetCustomAttribute<JsonIgnoreAttribute>() != null;
    }
}
