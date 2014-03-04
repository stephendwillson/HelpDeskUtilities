Set-StrictMode -Version Latest

# This script is poorly written, but after so much testing/tweaking, I can't make it work better.
# Grabs the location HelpDesk Utilities was launched from, appends \PSScripts onto it, then appends \Wi-Fi-NMU.xml onto it.

netsh wlan delete profile name="NMU"

$scriptPath = Get-Location

$scriptPath = Join-Path  ($scriptPath) "PSScripts"
$scriptPath = Join-Path ($scriptPath) “Wi-Fi-NMU.xml"

netsh wlan add profile filename=$scriptPath

netsh wlan show profiles