# VRChat Cache Relocator

VRChat Cache Relocator is a C# tool that moves the VRChat cache folder (%LocalAppData%\..\LocalLow\VRChat\VRChat) to a user-specified directory (e.g., another drive) and creates a symbolic link to maintain game functionality. Ideal for freeing up space on your system drive or using a faster/larger disk.

## Requirements
    Windows OS (uses cmd.exe and mklink).
    .NET SDK 7.0 or newer (for building).
    Admin privileges for creating symbolic links.
    VRChat installed.
    
## Example
    Enter the new path for the VRChat cache folder:
    F:\
    symbolic link created for C:\Users\Harmony\AppData\LocalLow\VRChat\VRChat <<===>> F:\VRChatCache\
    VRChat cache successfully moved and linked.

## How I Can Do It By Myself

To manually relocate the VRChat cache folder without using this tool, follow these steps:

1. **Open Command Prompt as Administrator**:
   - Press `Win + S`, type `cmd` or `Command Prompt`.
   - Right-click on "Command Prompt" and select **Run as administrator**.

2. **Move the VRChat Cache Folder**:
   - Identify the current VRChat cache folder, typically at `C:\Users\<YourUsername>\AppData\LocalLow\VRChat\VRChat`.
   - Move your VRChat cache folder to your target location
   - Rename the VRChat cache folder to VRChatCache and delete the old one if it still exists.
   

3. **Create a Symbolic Link**:
   - In the same Command Prompt (running as administrator), create a symbolic link to point the original path to the new location:
     ```cmd
     mklink /D "C:\Users\<YourUsername>\AppData\LocalLow\VRChat\VRChat" "TargetLocation\VRChatCache"
     ```
   - Ensure the paths match exactly, including the folder name.

4. **Verify**:
   - Check that the symbolic link works by navigating to `C:\Users\<YourUsername>\AppData\LocalLow\VRChat\VRChat` in File Explorer; it should redirect to `TargetLocation\VRChatCache`.
   - Launch VRChat to confirm the cache is accessed correctly.

## Special Thanks
    AI for README and some comments in code


