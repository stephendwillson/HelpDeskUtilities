Set-StrictMode -Version Latest

[Environment]::CurrentDirectory = get-location
$scriptPath = Join-Path (Get-Location) “Wi-Fi-NMU.xml"
echo $scriptPath
netsh wlan add profile filename=$scriptPath

netsh wlan show profiles