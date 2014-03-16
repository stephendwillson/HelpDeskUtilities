Set-StrictMode -Version Latest

#Script needs to be made more robust to handle file name/path changes in the future.
#It's not sustainable to expect someone to edit each of these scripts to reflect every change in
#file name or path.

#region Globals
#Call correct version of CCleaner directly - otherwise, waiting for process to exist does not function correctly (see CCleaner documentation)
If (($ENV:Processor_Architecture -eq "x86" -and (Test-Path env:PROCESSOR_ARCHITEW6432)) -or ($ENV:Processor_Architecture -eq "AMD64")) {
    $ccleanerExeName = 'CCleaner64.exe'
}
ElseIf ($ENV:Processor_Architecture -eq 'x86')
{
    $ccleanerExeName = 'CCleaner.exe'
}
Else {
    $color = 'Red'
    $outputString = "Unable to determine CPU architecture! Exiting script."
    [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,1)
    Break
}

$ccleanerExePath = 'Programs and Files\Cleaning and Virus Removal\CCleaner'
$argumentList = '/auto'
#endregion

#Get script path minus script name
$scriptPath = Get-Location

#Get top level root directory for script--just drive letter (D:, C:, etc.)
$scriptRoot = ($scriptPath -split '\\')[0]

#Build file path for CCleaner executable ("DriveLetter:\path")
$ccleanerFullPath = Join-Path  ($scriptRoot) $ccleanerExePath

#Find CCleaner executable
$ccleaner = Get-ChildItem -Path $ccleanerFullPath -Recurse -Filter $ccleanerExeName

#Build file path for CCleaner executable ("DriverLetter:\path\executable.exe")
$ccleaner = Join-Path ($ccleanerFullPath) $ccleaner

#Start CCleaner and wait for exit before continuing
$p = [System.Diagnostics.Process]::Start($ccleaner,$argumentList)
$p.WaitForExit()