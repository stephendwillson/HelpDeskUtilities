#Usage: Write-Error "Sample error text"



$adaptorName = "*6250 AGN*"

$adaptor = Get-WmiObject -Class Win32_NetworkAdapter | Where-Object {$_.Name -like $adaptorName}

#If for some reason the adaptor can't be found, write an error and break from script
If(!$adaptor) {
    Write-Error "Network adaptor matching the pattern " + $adaptorName + " not found."
    Break
}

$adaptor.Disable()
$adaptor.Enable()