# IME Control for LoupixDeck

[English](#english) | [日本語](#日本語)

A [LoupixDeck](https://github.com/RadiatorTwo/LoupixDeck) plugin that turns the
IME (Input Method Editor) of the active window on or off from a button.

[LoupixDeck](https://github.com/RadiatorTwo/LoupixDeck) のボタンから、
アクティブなウィンドウの IME を ON / OFF するプラグインです。

---

## English

### Features

| Command | What it does |
|---|---|
| **IME Control → IME OFF** (`ImeControl.Off`) | Turns the IME of the active window off. Does nothing if it is already off. |
| **IME Control → IME ON** (`ImeControl.On`) | Turns the IME of the active window on. Does nothing if it is already on. |

Both commands can be assigned to touch buttons, physical keys and dials.

Typical uses:

- Put **IME OFF** before a text macro so commands such as `/gs change リーパー`
  are typed in half-width characters, without IME conversion getting in the way.
- Switch to IME OFF before a game or a terminal and back to IME ON for chat.

### Requirements

- Windows 10 / 11
- LoupixDeck v1.38.0 or later (Plugin SDK 1.28.0)

### Installation

**From the Plugin Store** (once the plugin is listed): open LoupixDeck's Plugin
Store, find **IME Control** and install it.

**From a zip file:**

1. Download `imecontrol-<version>-windows.zip` from the
   [Releases](../../releases) page. Do not extract it.
2. In LoupixDeck, open the **Plugins** window and choose **Install from Zip…**.
3. Select the downloaded zip.

### Usage

1. Open the command menu of a button.
2. Select **IME Control → IME OFF** or **IME Control → IME ON**.
3. Press the button while the window you want to change is in front.

Each press writes either `changed` or `no change` to the LoupixDeck log.

### Limitations

- The plugin uses the Win32 `WM_IME_CONTROL` message. Some applications ignore
  it, such as some UWP / Store apps and windows running as administrator.
- If text macros lose Japanese characters (for example only `/gs change ` arrives
  from `/gs change リーパー`), check whether the **Interception** driver is
  enabled in LoupixDeck's settings. Interception can only type characters that
  exist on the keyboard and silently skips the rest. This is not caused by this
  plugin.

### Building from source

```powershell
dotnet build -c Release
```

The output is in `bin/Release/net10.0/`. To install it, zip
`LoupixDeck.Plugin.ImeControl.dll` and `plugin.json` (both at the root of the
zip) and use **Install from Zip…**. Do not include `LoupixDeck.PluginSdk.dll`;
the host provides it.

### License

[MIT](LICENSE)

---

## 日本語

### 機能

| コマンド | 内容 |
|---|---|
| **IME Control → IME OFF**（`ImeControl.Off`） | アクティブなウィンドウの IME を OFF にします。すでに OFF なら何もしません。 |
| **IME Control → IME ON**（`ImeControl.On`） | アクティブなウィンドウの IME を ON にします。すでに ON なら何もしません。 |

どちらのコマンドも、タッチボタン・物理キー・ダイヤルに割り当てられます。

使い方の例：

- テキスト送信マクロの前に **IME OFF** を入れておくと、`/gs change リーパー`
  のような日本語を含むコマンドを、IME の変換に邪魔されずに半角で入力できます。
- ゲームやターミナルを使う前に IME OFF、チャットのときに IME ON に切り替えます。

### 動作環境

- Windows 10 / 11
- LoupixDeck v1.38.0 以降（Plugin SDK 1.28.0）

### インストール

**プラグインストアから**（掲載後）：LoupixDeck のプラグインストアを開き、
**IME Control** を探してインストールします。

**zip ファイルから：**

1. [Releases](../../releases) ページから `imecontrol-<バージョン>-windows.zip`
   をダウンロードします。展開する必要はありません。
2. LoupixDeck の **Plugins**（プラグイン）画面を開き、**Install from Zip…**（zip からインストール）を選びます。
3. ダウンロードした zip を選びます。

### 使い方

1. ボタンのコマンドメニューを開きます。
2. **IME Control → IME OFF** または **IME Control → IME ON** を選びます。
3. IME を切り替えたいウィンドウを前面に出した状態で、ボタンを押します。

押すたびに、LoupixDeck のログに `changed`（切り替えた）または
`no change`（変更なし）が記録されます。

### 制限事項

- Win32 の `WM_IME_CONTROL` メッセージで IME を切り替えています。
  一部の UWP / ストアアプリや、管理者権限で動いているウィンドウでは効かないことがあります。
- テキスト送信で日本語が消える場合（例：`/gs change リーパー` が `/gs change `
  までしか届かない）は、LoupixDeck の設定で **Interception** ドライバーが
  有効になっていないか確認してください。Interception はキーボードで打てる文字しか
  送れず、それ以外の文字は何も言わずに飛ばします。このプラグインが原因ではありません。

### ソースからビルドする

```powershell
dotnet build -c Release
```

`bin/Release/net10.0/` にできた `LoupixDeck.Plugin.ImeControl.dll` と `plugin.json`
を zip にまとめ（どちらも zip の直下に置きます）、**Install from Zip…** でインストールします。
`LoupixDeck.PluginSdk.dll` は zip に入れないでください（LoupixDeck 本体に含まれています）。

### ライセンス

[MIT ライセンス](LICENSE)
