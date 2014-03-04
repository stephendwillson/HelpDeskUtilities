Set-StrictMode -Version Latest

$rKillExeName = 'rkill.exe'
$rKillExePath = 'Programs and Files\Cleaning and Virus Removal\rKill'
$rKillLocal = 'C:\rkill.com'
$argumentList = '-s'

#Get script path minus script name
$scriptPath = Get-Location

#Get top level root directory for script--just drive letter (D:, C:, etc.)
$scriptRoot = ($scriptPath -split '\\')[0]

#Build file path for rKill executable ("DriveLetter:\path")
$rKillFullPath = Join-Path  ($scriptRoot) $rKillExePath

#Find rKill executable
$rKill = Get-ChildItem -Path $rKillFullPath -Recurse -Filter $rKillExeName

#Build file path for rKill executable ("DriverLetter:\path\executable.exe")
$rKill = Join-Path ($rKillFullPath) $rKill

#Move rKill to local machine, rename "rkill.com"
Copy-Item $rKill $rKillLocal

#Start rKill and wait for exit before continuing
$p = [System.Diagnostics.Process]::Start($rKillLocal,$argumentList)
$p.WaitForExit()

Start-Sleep -s 4

#Delete the local copy of RKill
Remove-Item $rKillLocal