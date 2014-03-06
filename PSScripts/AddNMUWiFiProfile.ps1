Set-StrictMode -Version Latest

# This script is poorly written, but after so much testing/tweaking, I can't make it work better.
# Grabs the location HelpDesk Utilities was launched from, appends \PSScripts onto it, then appends \Wi-Fi-NMU.xml onto it.

#region Globals
$profileName = "NMU"
$psFolderName = "PSScripts"
$profileXmlFilename = “Wi-Fi-NMU.xml"
#endregion


netsh wlan delete profile name=$profileName

$scriptPath = Get-Location

$scriptPath = Join-Path  ($scriptPath) $psFolderName
$profileXml = Join-Path ($scriptPath) $profileXmlFilename

netsh wlan add profile filename=$profileXml

netsh wlan show profiles