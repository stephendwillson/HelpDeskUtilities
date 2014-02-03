#Need checks for no adaptor installed
#Usage: Write-Error "Sample error text"



$adaptorName = "*6250 AGN*"

$adaptor = Get-WmiObject -Class Win32_NetworkAdapter | Where-Object {$_.Name -like $adaptorName}
$adaptor.Disable()
$adaptor.Enable()