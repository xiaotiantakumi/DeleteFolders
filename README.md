# DeleteFolders
npm installで増えたモジュールを消し去るために作った。

## appsettings.jsonの設定について
BasePath：起点となるパスを指定する。このパスから再帰的に指定されたフォルダを削除する。
Folders：削除したいフォルダを指定する。複数指定可能。配列で指定する。

```
  "BasePath": "C:\\code\\npm templates",
  "Folders": [ "node_modules", "dist" ]
```
