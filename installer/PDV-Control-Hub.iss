#define MyAppName "PDV Control Hub"
#define MyAppVersion "0.1.0"
#define MyAppPublisher "Samuel"
#define MyAppExeName "PDV.Launcher.exe"

[Setup]
AppId={{8D0B5B2C-6A53-4B25-9C2B-123456789ABC}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}

DefaultDirName={autopf}\PDV Control Hub
DefaultGroupName=PDV Control Hub

OutputDir=..\publish\installer
OutputBaseFilename=PDV-Control-Hub-Setup-V6

Compression=lzma
SolidCompression=yes

ArchitecturesInstallIn64BitMode=x64
ArchitecturesAllowed=x64

PrivilegesRequired=admin
DisableProgramGroupPage=yes

UninstallDisplayName=PDV Control Hub
UninstallDisplayIcon={app}\PDV.Launcher.exe

WizardStyle=modern

[Files]
Source: "..\publish\PDV-Control-Hub\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\PDV Control Hub"; Filename: "{app}\PDV.Launcher.exe"; WorkingDir: "{app}"
Name: "{autodesktop}\PDV Control Hub"; Filename: "{app}\PDV.Launcher.exe"; WorkingDir: "{app}"

[Run]
Filename: "{app}\PDV.Launcher.exe"; Description: "Iniciar PDV Control Hub"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent