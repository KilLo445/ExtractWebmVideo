<a name="readme-top"></a>
<div align="center">
    <h1>ExtractWebmVideo</h1>
    <br />
    A fork of <a href="https://github.com/csabor/ExtractWebmVideo">csabor/ExtractWebmVideo</a> that extracts the embedded video from Just Dance 4 AutoDance files by locating the WebM header and writing the video data to a separate file. Along with drag-and-drop support, MP4 conversion using <a href="https://ffmpeg.org/">FFmpeg</a>, and additional quality-of-life features.
    <br />
  </p>
  
  [![Forks][forks-shield]][forks-url]
  [![Stargazers][stars-shield]][stars-url]
  [![Issues][issues-shield]][issues-url]
  <br />
  [![Downloads][dl-shield]][latest]
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-fork">About The Fork</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#usage">Usage</a></li>
        <li><a href="#examples">Examples</a></li>
        <li><a href="#arguments">Arguments</a></li>
      </ul>
    </li>
        <li>
      <a href="#ffmpeg">FFmpeg</a>
      <ul>
        <li><a href="#path">PATH Information</a></li>
        <li><a href="#installing-ffmpeg">Installing FFmpeg</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

## About The Fork
![Example GIF](https://github.com/KilLo445/ExtractWebmVideo/blob/master/.github/Example.gif?raw=true)

The whole reason of this fork was to add some new features, so here's the list!
* Updated Framework
    * Updated __.NET Framework__ from __v4.6.1__ to __v4.8__
* Drag & Drop
    * Drag your AutoDance file onto `ExtractWebmVideo.exe` for your WebM.
* Convert to MP4
    * Easily convert the WebM into MP4 using FFmpeg with a command line argument. _(FFmpeg not included)_
* Delete original AutoDance
    * Delete the original AutoDance file with a command line argument.

_I also made the README nicer, but I don't really count this as a new feature._

_All the original code to convert the AutoDance file into the WebM was written by [csabor](https://github.com/csabor) and is mostly unchanged. I do not take any credit for it, all I have done is add some new QOL features and provided a built version as the original does not contain a build, only source._

### Built With

* [![.NET][.NET]][framework-url]

## Getting Started

### Prerequisites

.NET Framework Runtime 4.8 is required to run ExtractWebmVideo.  
Windows versions that support .NET Framework 4.8 may already have it installed.
  - [Download page](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
  - [Direct (Web)](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net48-web-installer)
  - [Direct (Offline)](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net48-offline-installer)

### Installation

1. Head over to the [latest release](https://github.com/KilLo445/ExtractWebmVideo/releases/latest)
2. Download `ExtractWebmVideo.exe`

### Usage

```
ExtractWebmVideo.exe <File or Directory> <Extra arguments (-mp4, etc)>
```
_Psst! You can also drag and drop your AutoDance file onto ExtractWebmVideo.exe_

### Examples
Extract to WebM  

    ExtractWebmVideo.exe "ugc-0"
    -> ugc-0-converted.webm

Extract to MP4  

    ExtractWebmVideo.exe "ugc-0" -mp4
    -> ugc-0-converted.mp4

_You can specify a folder instead of a file to batch convert all files within the directory!_

### Arguments
    -help                      Print help text and exit
    -mp4                       Convert WebM to MP4 (FFmpeg required)
    -delete                    Permanently remove original AutoDance file (cannot be recovered; confirmation is prompted)
    -github                    Open GitHub page and exit
    -version                   Print installed version and exit

## FFmpeg
[FFmpeg](https://ffmpeg.org/) is required to convert the extracted WebM to MP4. By default, the extracted video will be a WebM file.

### PATH
#### __General Information__
_The __PATH__ is the system variable that Windows uses to locate needed executables from the command line._

The program will attempt to search for FFmpeg in __PATH__, if it cannot find it, it will resort to looking for `ffmpeg.exe` next to the main exe in the root directory.

### Installing FFmpeg
#### Install to PATH (Recommended)
_I personally recommend installing FFmpeg to PATH, as this is usable at anytime across your system by you or any program that may need to use it._
1. Download FFmpeg from any mirror available [here](https://ffmpeg.org/download.html#build-windows).

    _FFmpeg Essentials is sufficient for ExtractWebmVideo, though FFmpeg Full is recommended for a PATH install._
2. Extract the `bin` folder from your download.
3. Place the files inside the `bin` folder somewhere safe, such as `C:\ffmpeg`

    It should look something like this  

        C:\   
        └─ ffmpeg  
            ├── ffprobe.exe  
            ├── ffplay.exe  
            └── ffmpeg.exe
4. Open the __Start Menu__ and search for __Advanced system settings__.
5. Select __Environment Variables__.
6. Under __System variables__ find the __Path__ variable and select __Edit__.
7. Add a new entry with the value of the path you extracted FFmpeg into, such as `C:\ffmpeg`
8. Done! Open __Command Prompt__ and type `ffmpeg` to verify it installed correctly.

    _You may need to restart your PC for these changes to take effect_

#### One Time Use
1. Download FFmpeg from any mirror available [here](https://ffmpeg.org/download.html#build-windows).

    _FFmpeg Essentials is sufficient for ExtractWebmVideo_
2. Extract `ffmpeg.exe` from your download.
3. Place `ffmpeg.exe` next to `ExtractWebmVideo.exe`
4. Done!

## License

Distributed under the MIT License. See `LICENSE` for more information.

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[forks-shield]: https://img.shields.io/github/forks/KilLo445/ExtractWebmVideo.svg?style=for-the-badge
[forks-url]: https://github.com/KilLo445/ExtractWebmVideo/network/members
[stars-shield]: https://img.shields.io/github/stars/KilLo445/ExtractWebmVideo.svg?style=for-the-badge
[stars-url]: https://github.com/KilLo445/ExtractWebmVideo/stargazers
[issues-shield]: https://img.shields.io/github/issues/KilLo445/ExtractWebmVideo.svg?style=for-the-badge
[issues-url]: https://github.com/KilLo445/ExtractWebmVideo/issues
[.NET]: https://img.shields.io/badge/.NET_Framework-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[framework-url]: https://dotnet.microsoft.com/en-us/download/dotnet-framework
[dl-shield]: https://img.shields.io/github/downloads/KilLo445/ExtractWebmVideo/ExtractWebmVideo.exe?displayAssetName=false&style=for-the-badge&label=Downloads&color=2E3440
[latest]: https://github.com/KilLo445/ExtractWebmVideo/releases/latest