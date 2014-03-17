Set-StrictMode -Version Latest

#region Globals
$expectedOutputString = ".exe=exefile"
#endregion

$cmdOutputString = cmd /c assoc .exe=exefile

If($cmdOutputString -eq $expectedOutputString) {
    $outputString = "Successfully reset .exe association."
    $color = 'Black'
    [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,0)
}
Else {
    $outputString = "Failed to reset .exe association: " + $cmdOutputString
    $color = 'Red'
    [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,1)
}