# Running on Linux via Proton

This tool is a Windows WPF application, but it can be used on Linux through Steam Proton.

### Recommended Proton versions

- Proton Experimental

## Step 1 — Enable Proton for Sekiro

1. Open Steam
2. Right-click **Sekiro: Shadows Die Twice**
3. Click **Properties**
4. Go to **Compatibility**
5. Enable: ✔ "Force the use of a compatibility tool"
6. Select: **Proton Experimental**

## Step 2 — Install / Run the Helper

### Option A: Run from EXE (recommended)

Download the latest release:

👉 Go to the [Releases page](https://github.com/ajilenakh/SimpleSekiroSavegameHelper/releases) and download `SimpleSekiroSavegameHelper.exe`.

Then run it using Proton:

```bash
STEAM_COMPAT_CLIENT_INSTALL_PATH="$HOME/.steam/steam" \
STEAM_COMPAT_DATA_PATH="$HOME/.steam/steam/steamapps/compatdata/814380" \
"$HOME/.steam/steam/steamapps/common/Proton - Experimental/proton" run \
"path/to/SimpleSekiroSavegameHelper.exe"
```

## Step 3 — Make sure Sekiro is running

The tool requires Sekiro to be running in the background.

1. Start the game from Steam first.
2. Wait until you are in-game (or at least at the main menu)
3. Then launch the helper
4. Import the saves
5. Then restart the game
