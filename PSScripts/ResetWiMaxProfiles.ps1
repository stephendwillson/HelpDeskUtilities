Set-StrictMode -Version Latest

#Needs work on getting exit code from AutoIt script correctly

#region Globals
$autoItExeName = "ResetWiMaxProfiles.exe"
$autoItFolderName = "AutoItScripts"
$argumentList = " "
#endregion

$scriptPath = Get-Location

#Fuse script path to folder containing AutoIt .exe, and append .exe onto folder path
$scriptPath = Join-Path  ($scriptPath) $autoItFolderName
$autoItExe = Join-Path ($scriptPath) $autoItExeName

Start-Process $autoItExe -Wait

echo .
echo .
echo .
echo $LASTEXITCODE
If ($LASTEXITCODE -eq 1) {
    Write-Error "Failed to reset profiles!"
    break
}
Else {
}