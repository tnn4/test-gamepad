Requirements:
- Dotnet 8


YOu should already have dotnet working

## Install Template

install monogame template
```
dotnet new install Monogame.Templates.CSharp
```

Create a new monogame project with simplified output
```
mkmg.sh <project-name>
```

Install mgcb tool
```
dotnet tool -g dotnet-mgcb

#usage( while you're inside project):
mgcb
```

## Development
```
# Build
dotnet build
//

# Watch for file changes
dotnet watch
//

# Package
# create a backupa archive to out/ directory
./build.sh
//

# Publish
dotnet publish -c release
//
```

## Deployment
type | command
--- | ---
framework dependent executablefor current platform     | `dotnet publish`
framework dependent executable for specific platform   | `dotnet publish -r <RID>`
framework dependent binary                             | `dotnet publish`
self-contained executable                              | `dotnet publish -r <RID> --self-contained`

Note: for release builds add `-c Release`
- e.g. `dotnet publish -c Release`

see: [RIDs](https://github.com/dotnet/sdk/blob/main/src/Layout/redist/PortableRuntimeIdentifierGraph.json)
see: https://learn.microsoft.com/en-us/dotnet/core/deploying/

e.g. osx-x64
```
# debug
dotnet publish -r osx-x64 --self-contained
# release build
dotnet publish -c Release -r osx-x64 --self-contained
```

## Content

Install mgcb-GUI
```
dotnet mgcb-editor


# Find file: Content.mgcb
$ dotnet mgcb-editor Content.mgcb

Right-click Content > Add > New Item...
> SpriteFont Description 
> monogram.spritefont

Put `monogram.ttf` inside Content/data folder

ERR? https://stackoverflow.com/questions/73583464/monogame-could-not-find-font-file-when-running

# Usage in code:

In Game1.cs
    SpriteFont font;

    LoadContent()
        font = Content.Load<SpriteFont>("monogram");
    Draw()
        this._spriteBatch.Begin();

        this._spriteBatch.DrawString(
            font, 
            $"{this.elapsedTime}(timeout at {this.timeOut})",
            new Vector2(this.preferredW*(1/8), this.preferredH*(1/8)),
            Color.White
        );

        this._spriteBatch.End();
    //
//

```

```
You can use the mgcb tool instead of the GUI

If mgcb --build fails:
    mgcb --clean Content.mgcb
//
```

GamePad Testing

This application can be used to test and do diagnostics for a controller with:
- ABXY, 
- DPad (arrow buttons)
- Left Shoulder, Left Trigger
- Right Shoulder, Right Trigger

# Change project name

Find app.manifest:
<assemblyIdentity version="1.0.0.0" name="<project-name"/>

LICENSE:
Code - MIT
