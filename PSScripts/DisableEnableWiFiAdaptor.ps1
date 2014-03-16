Set-StrictMode -Version Latest

#region Globals
$adaptorName = "*6250 AGN*"
#endregion

$adaptor = Get-WmiObject -Class Win32_NetworkAdapter | Where-Object {$_.Name -like $adaptorName}

#If for some reason the adaptor can't be found, write an error and break from script
If(!$adaptor) {
    $outputString = "Network adaptor matching the pattern " + $adaptorName + " not found."
    $color = 'Red'
    [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,1)
    Break
}

$adaptor.Disable()
$adaptor.Enable()