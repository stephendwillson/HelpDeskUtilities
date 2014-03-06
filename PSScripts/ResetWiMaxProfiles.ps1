#Bug in the latest version of Powershell engine prevents StrictMode from being usable if $LASTEXITCODE is used.
#$LASTEXITCODE is wrapped in a script that doesn't declare the variable first, so on execution, if StrictMode 
#is enabled, an exception is thrown. Must choose one or the other--I chose StrictMode.
Set-StrictMode -Version Latest

#region Globals
$autoItExeName = "ResetWiMaxProfiles.exe"
$autoItFolderName = "AutoItScripts"
$argumentList = " "
$resetExitCodeFilename = "C:\reset"
#endregion

$scriptPath = Get-Location

#Fuse script path to folder containing AutoIt .exe, and append .exe onto folder path
$scriptPath = Join-Path  ($scriptPath) $autoItFolderName
$autoItExe = Join-Path ($scriptPath) $autoItExeName

Start-Process $autoItExe -Wait

#Workaround to a bug - exit code from AutoIt executable is written to a file
$exitCode = [System.IO.File]::ReadAllText($resetExitCodeFilename)
If ($exitCode -NotMatch "0") {
    Write-Error "Either exit code file does not exist or profiles were not removed successfully!"
    Remove-Item $resetExitCodeFilename
}