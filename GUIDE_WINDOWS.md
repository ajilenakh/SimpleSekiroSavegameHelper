# Simple Sekiro Savegame Helper — Windows Guide

## What you need

- **Sekiro: Shadows Die Twice** installed via Steam
- **Administrator privileges** — the app patches the game's memory and modifies save files. Right-click the `.exe` and select **"Run as administrator"**.

---

## Quick Start

1. Start **Sekiro** and load into the game (main menu is fine).
2. **Run this tool as administrator** — it will automatically find your save file.
3. Use the buttons below to back up, import, or restore saves.

---

## What you see

| UI Element                           | What it does                                                           |
| ------------------------------------ | ---------------------------------------------------------------------- |
| Path box (top)                       | Shows where your save file is located                                  |
| Checkbox: _Patch game..._            | NOPs Sekiro's checksum/SteamID checks in RAM so it loads any save file |
| Checkbox: _Enable hotkeys_           | Turns on keyboard shortcuts                                            |
| **Import foreign savegame**          | Copy save slots from another player's file into yours                  |
| _Backup name..._ text box            | Give your next backup a name                                           |
| **Backup savegame**                  | Make a copy of your current save                                       |
| **Set new name for selected backup** | Rename the backup you've selected (highlighted) in the dropdown        |
| **Revert to selected backup**        | Replace your current save with the selected backup                     |
| **Delete selected savegame**         | Delete the selected backup                                             |
| Dropdown list                        | Shows all your backups with date/time and any names you've set         |
| Status bar                           | Shows what just happened ("14:32:15 BACKUP SAVED")                     |

---

## Features

### 1. Backup your save (do this first!)

1. (Optional) Type a name in the _Backup name..._ box, e.g. "Before Genichiro"
2. Click **Backup savegame** (or press `Ctrl + B`)
3. You'll see it appear in the dropdown with the name you gave it

The backup is saved right next to your save file as `S0000.sl2_backup_2024-01-01-143000.bak`.

### 2. Give backups a friendly name

1. Click a backup in the dropdown to select it
2. Type a name in the _Backup name..._ box
3. Click **Set new name for selected backup** (or `Ctrl + N`)

### 3. Restore a backup

1. Select a backup from the dropdown
2. Click **Revert to selected backup** (or `Ctrl + R`)
3. Your current save is replaced — restart Sekiro to load it

### 4. Delete a backup

1. Select a backup from the dropdown
2. Click **Delete selected savegame** (or `Ctrl + D`)

### 5. Load a foreign save (from another Steam user)

Two ways:

**A) With your own settings preserved (recommended):**

1. Click **Import foreign savegame**
2. Select the source file (the foreign `.sl2` or `.bak`)
3. In the next dialog, select your OWN save file as the destination
4. Confirm — the foreign save slots are copied in, but your game settings stay

**B) Direct import (foreign settings will overwrite yours):**

1. Click **Import foreign savegame**
2. Select the source file
3. Click **Cancel** on the destination dialog
4. Read the warning and click **Yes**
5. The foreign file is modified directly — restart Sekiro to load it

### 6. Patch the game to load any save file

Check **"Patch game to load unimported foreign/modified save files"**.

This patches Sekiro's memory (not game files) to skip:

- Checksum verification
- SteamID checks on the save file
- SteamID checks on individual save slots

**You must re-enable this every time you restart Sekiro** — RAM patches don't persist.

Use this if you're editing your save file with a hex editor or if "Self protection failed" errors appear.

### 7. Hotkeys

| Shortcut   | Action                    |
| ---------- | ------------------------- |
| `Ctrl + B` | Backup current save       |
| `Ctrl + N` | Name selected backup      |
| `Ctrl + R` | Revert to selected backup |
| `Ctrl + D` | Delete selected backup    |

Enable them by checking **"Enable hotkey shortcuts"**.

---

## Troubleshooting

| Error / Problem                                                         | Why                                                                                                            | Fix                                                                                                                                                      |
| ----------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **"Could not find default save file!"**                                 | App couldn't find `S0000.sl2` in `%APPDATA%\Sekiro\`. Either you haven't played yet, or the save is elsewhere. | Click the path box at top to browse and select your save file manually.                                                                                  |
| **"Game not running!"**                                                 | Sekiro needs to be running for the memory patch to work.                                                       | Start Sekiro first, then use the tool.                                                                                                                   |
| **"No access to game!"**                                                | The tool can't open Sekiro's process.                                                                          | **Run the tool as administrator.**                                                                                                                       |
| **"Could not find checksum check in memory!"** / similar pattern errors | The game version doesn't match the patterns this tool knows.                                                   | Make sure Sekiro is fully updated. The tool supports versions up to 1.4.0.0.                                                                             |
| **"Self protection failed error code: 4"** (in-game)                    | Sekiro's DRM rejected the save file.                                                                           | Enable the **"Patch game..."** checkbox, then reload the save. If that fails, use **Import foreign savegame** with your own save as destination instead. |
| **"Source file invalid!"**                                              | The file you selected isn't a valid Sekiro save.                                                               | Make sure you're selecting an `S0000.sl2` or `.bak` file.                                                                                                |
| **"Destination file invalid!"**                                         | The destination file isn't a valid Sekiro save.                                                                | Same as above — pick a real save file.                                                                                                                   |
| **Checkbox won't stay checked**                                         | The patch failed (game not running, no access, or pattern not found).                                          | Check that Sekiro is running and you're running as admin.                                                                                                |
| **Backup dropdown is empty**                                            | You haven't made any backups yet, or all have been deleted.                                                    | Click **Backup savegame** to create one.                                                                                                                 |
| **"Another instance is already running!"**                              | You already have the tool open.                                                                                | Close the other instance, or check your system tray.                                                                                                     |
| **"Error while loading configuration file"**                            | The settings XML is corrupt.                                                                                   | The app creates a new one automatically. If problems persist, delete `SimpleSekiroSavegameHelper.xml` next to your save file.                            |

---

## Save file location

```
%APPDATA%\Sekiro\<SteamID64>\S0000.sl2
```

Which expands to something like:

```
C:\Users\<YourName>\AppData\Roaming\Sekiro\76561197960287930\S0000.sl2
```

Backups are stored in the same folder with a `.bak` extension.

---

## Administrator permissions

The tool needs admin rights because:

- It calls `OpenProcess` with `PROCESS_ALL_ACCESS` to read/write Sekiro's memory
- It overwrites save files in `%APPDATA%`

If you skip admin mode, the memory patch will fail and you'll see **"No access to game!"**.
