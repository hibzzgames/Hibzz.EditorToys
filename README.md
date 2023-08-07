# Hibzz.EditorToys
![LICENSE](https://img.shields.io/badge/LICENSE-CC--BY--4.0-ee5b32?style=for-the-badge) [![Twitter Follow](https://img.shields.io/badge/follow-%40hibzzgames-1DA1f2?logo=twitter&style=for-the-badge)](https://twitter.com/hibzzgames) [![Discord](https://img.shields.io/discord/695898694083412048?color=788bd9&label=DIscord&style=for-the-badge)](https://discord.gg/YXdJ8cZngB) ![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

***This package contains a small set of helpful editor utilities to improve the game development workflow***

> This package is in it's alpha stage and in active development. This repo has been made public as a preview.

<br>

## Installation
**Via Github**
This package can be installed in the Unity Package Manager using the following git URL.
```
https://github.com/hibzzgames/Hibzz.EditorToys.git
```

Alternatively, you can download the latest release from the [releases page](https://github.com/hibzzgames/Hibzz.EditorToys/releases) and manually import the package into your project.

<br>

## Tools
The following tools are currently available:
- [Print To Screen](https://github.com/hibzzgames/Hibzz.EditorToys/#print-to-screen)
- [Release Incrementor](https://github.com/hibzzgames/Hibzz.EditorToys/#release-incrementor)
- [Scriptable Object Creator](https://github.com/hibzzgames/Hibzz.EditorToys/#scriptable-object-creator)

<br>

### Print To Screen

![printtoscreen](https://github.com/hibzzgames/Hibzz.EditorToys/assets/37605842/0e6ff149-803d-48c5-901f-1cb11b9c02d2)


`PrintToScreen` is a simple tool that allows users to print a message to the screen for a specified duration. This is very useful for real-time debugging and testing. The tool can be further customized by specifying the color of the text.


```csharp
using Hibzz.EditorToys;

// print message to the screen
EditorToys.PrintToScreen("Hello World!"); // prints "Hello World!" to the screen for 1 frame
EditorToys.PrintToScreen("Hello World!", 5f); // prints "Hello World!" to the screen for 5 seconds
EditorToys.PrintToScreen("Hello World!", Color.red); // prints "Hello World!" to the screen for 1 frame in red
EditorToys.PrintToScreen("Hello World!", 5f, Color.red); // prints "Hello World!" to the screen for 5 seconds in red
```

<br>

### Release Incrementor

![release-incrementor](https://github.com/hibzzgames/Hibzz.EditorToys/assets/37605842/6ada618e-7f0a-418b-938d-9023a972fc36)

The Release Incrementor prompts the users to increment the version number of the project when creating a new build. With a press of a button, users will be able to increment the patch or minor version number of the project. This tool can be enabled from the `Hibzz > Editor Toys > Release Incrementor` menu.


<br>

### Scriptable Object Creator
<img src="https://github.com/hibzzgames/Hibzz.EditorToys/assets/37605842/b0dbbb77-4dc2-4a51-aece-f85e2133a84d" width="70%">


The Scriptable Object Creator allows users to create a new instance of a scriptable object directly by opening the context menu on a script file that inherits from `ScriptableObject`. This lets users avoid having to create a unique menu item for each scriptable object type they create reducing the clutter in the Unity Editor and improving the workflow. 

<br>

## Disabling Tools
Since this package contains a wide variety of tools, it is completely understandable if you don't want to use all of them. The Editor Toys package adds support for disabling tools using scripting define symbols. 

This package has tight integration with the [Hibzz.DefineManager](https://github.com/hibzzgames/Hibzz.DefineManager) package which allows users to visually interact with the defines from the Unity Editor. The users can read what each of the scripting define symbols do in a neat interface and enable/disable the tools with a click of a button.

The following symbols can be used to disable specific tools:

- `DISABLE_PRINT_TO_SCREEN`
- `DISABLE_RELEASE_INCREMENTOR`
- `DISABLE_SCRIPTABLE_OBJECT_CREATOR`

## Have a question or want to contribute?
If you have any questions or want to contribute, feel free to join the [Discord server](https://discord.gg/YXdJ8cZngB) or [Twitter](https://twitter.com/hibzzgames). I'm always looking for feedback and ways to improve this tool. Thanks!

Additionally, you can support the development of these open-source projects via [GitHub Sponsors](https://github.com/sponsors/sliptrixx) and gain early access to the projects.

