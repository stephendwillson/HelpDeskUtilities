Set-StrictMode -Version Latest

#Script needs to be made more robust to handle file name/path changes in the future.
#It's not sustainable to expect someone to edit each of these scripts to reflect every change in
#file name or path.



$ccleanerExeName = "CCleaner.exe"
$ccleanerExePath = "Program Files\CCleaner"

#Get script path minus script name
$scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition

#Get top level root directory for script--just drive letter (D:, C:, etc.)
$scriptRoot = ($scriptPath -split '\\')[0]

#Build file path for CCleaner executable ("DriveLetter:\path")
$ccleanerFullPath = Join-Path  ($scriptRoot) $ccleanerExePath

#Find CCleaner executable
$ccleaner = Get-ChildItem -Path $ccleanerFullPath -Recurse -Filter $ccleanerExeName

#Build file path for CCleaner executable ("DriverLetter:\path\executable.exe")
$ccleaner = Join-Path ($ccleanerFullPath) $ccleaner

start $ccleaner /auto