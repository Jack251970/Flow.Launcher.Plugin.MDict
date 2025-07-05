<p align="center">
  <img src="./src/Flow.Launcher.Plugin.MDict/Images/icon.png" width="90">
</p>

# Flow Launcher MDict Plugin

<p>
  <img src="https://img.shields.io/maintenance/yes/3000">
  <a href="https://github.com/Flow-Launcher/Flow.Launcher"><img src="https://img.shields.io/badge/Flow%20Launcher-1.20.0+-blue"></a>
  <img src="https://img.shields.io/github/release-date/Jack251970/Flow.Launcher.Plugin.MDict">
  <a href="https://github.com/Jack251970/Flow.Launcher.Plugin.MDict/releases/latest"><img src="https://img.shields.io/github/v/release/Jack251970/Flow.Launcher.Plugin.MDict"></a>
  <img src="https://img.shields.io/github/license/Jack251970/Flow.Launcher.Plugin.MDict">
</p>

**This plugin is a MDict dictionary searcher for [Flow Launcher](https://github.com/Flow-Launcher/Flow.Launcher). It can help you search entries in MDict dictionaries.**

## ⭐ Features

- Search words in MDict dictionary files
- Dictionary file light / dark theme support

## 🖼️ Screenshots

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="./images/screenshot1_dark.png">
  <source media="(prefers-color-scheme: light)" srcset="./images/screenshot1_light.png">
  <img alt="Screenshot 1" src="./images/screenshot1_light.png">
</picture>

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="./images/screenshot2_dark.png">
  <source media="(prefers-color-scheme: light)" srcset="./images/screenshot2_light.png">
  <img alt="Screenshot 2" src="./images/screenshot2_light.png">
</picture>

## 🚀 Installation

* Plugin Store (Recommended)

  1. Search `MDict` in Flow Launcher Plugin Store and install

* Manually Release

  1. Downlaod zip file from [Release](https://github.com/Jack251970/Flow.Launcher.Plugin.MDict/releases)
  2. Unzip the release zip file
  3. Place the released contents in your `%appdata%/FlowLauncher/Plugins` folder and **restart** Flow Launcher

* Manually Build

  1. Clone the repository
  2. Run `build.ps1` or `build.sh` to publish the plugin in `.dist` folder
  3. Unzip the release zip file
  4. Place the released contents in your `%appdata%/FlowLauncher/Plugins` folder and **restart** Flow Launcher

## 📝 Usage

### 0. Set Action Keyword

The default action keyword is `md`, you can change it in the Flow Launcher.

### 1. Setup MDict Dictionary

Firstly, open the plugin settings in Flow Launcher.

#### 1. Setup Dictionary File

By clicking the `Select` button in the settings, you can add MDict dictionary file.

Select the mdx file you want to use, for example, `C:\MDict\mydict.mdx`.

#### 2. Setup Css Files

Add css files for light and dark theme support.
These css files should use the relative path to the folder of the dictionary file.

For example, if your dictionary file is `C:\MDict\mydict.mdx` and the css files are `C:\MDict\css\light.css` and `C:\MDict\css\dark.css`,
and then you should set the light css to `css\light.css` and dark css to `css\dark.css` in the settings.

### 2. Search MDict Dictionary

For Windows 10 and Windows 11, if you want to merge clipboard history records from Windows Clipboard History, you can enable `Merge Windows Clipboard history into data source` option. (v2.0.0+)

If you want to make query records fully match the Windows clipboard history, you can enable `Use only Windows clipboard history as data source option`. Under this mode, records of files format will not shown in the list. Records from the database will no longer be loaded, and records cannot be saved or pinned to the database. (v2.1.1+)

## 📚 Reference

- [ICONS](https://icons8.com/icons)
- [MDict.Csharp](https://github.com/Jack251970/MDict.Csharp)
- [WindowManager](https://github.com/Jack251970/Flow.Launcher.Plugin.WindowManager)

## 📄 License

[MIT License](LICENSE)

## ❤️ Thank You

If you are enjoying this plugin, then please support my work and enthusiasm by buying me a coffee on
[https://ko-fi/jackye](https://ko-fi.com/jackye).

[<img style="float:left" src="https://user-images.githubusercontent.com/14358394/115450238-f39e8100-a21b-11eb-89d0-fa4b82cdbce8.png" width="200">](https://ko-fi.com/jackye)
