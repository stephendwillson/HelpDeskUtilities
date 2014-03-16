Set-StrictMode -Version Latest

#region Globals
$adaptorName = "*82579LM*"
$adaptorNameTwist = "*PCIe GBE*"
#endregion

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
    $outputString = "Network adaptor matching the pattern " + $adaptorName + " or " + $adaptorNameTwist + " not found."
    $color = 'Red'
    [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,1)
}