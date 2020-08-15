# UniGame.EditorTools

Unity3D Editor Tools Collection

Odin inspector asset is Requaired : https://odininspector.com

## Unity Package Installation

Add to your project manifiest by path [%UnityProject%]/Packages/manifiest.json these lines:

```json
{
  "scopedRegistries": [
    {
      "name": "Unity",
      "url": "https://packages.unity.com",
      "scopes": [
        "com.unity"
      ]
    },
    {
      "name": "UniGame",
      "url": "http://packages.unigame.pro:4873/",
      "scopes": [
        "com.unigame"
      ]
    }
  ],
}
```

![](https://github.com/UniGameTeam/UniGame.EditorTools/blob/master/GitAssets/package_window.png)

![](https://github.com/UniGameTeam/UniGame.EditorTools/blob/master/GitAssets/menu-tools.png)

## Features

### Asset Reference Viewer
=============

- Show Asset Guid by Asset Object
- Show Asset by Guid
- Show All Assets that have dependencies to target asset

![](https://github.com/UniGameTeam/UniGame.EditorTools/blob/master/GitAssets/info-window.png)
