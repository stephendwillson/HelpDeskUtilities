#Usage: Write-Error "Sample error text"

$adaptorName = "*82579LM*"
$adaptorNameTwist = "*PCIe GBE*"

$adaptor = Get-WmiObject -Class Win32_NetworkAdapter | Where-Object {$_.Name -like $adaptorName}
$adaptorTwist = Get-WmiObject -Class Win32_NetworkAdapter | Where-Object {$_.Name -like $adaptorNameTwist}

If($adaptor) {
    $adaptor.Disable()
    $adaptor.Enable()
    Break
}

ElseIf($adaptorTwist) {
    $adaptorTwist.Disable()
    $adaptorTwist.Enable()
    Break
}

#If for some reason the adaptor can't be found, write an error and break from script
Else {
    Write-Error "Network adaptor matching the pattern " + $adaptorName + " not found."
}

