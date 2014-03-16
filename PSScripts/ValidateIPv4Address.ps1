Set-StrictMode -Version Latest

#Script will need to be updated when IPv6 is rolled out at NMU.

#region Globals
$unregisteredOctet = 10
$validOctets = 35,172,198,204,207
$invalidOctet = 69
$outputString = ""
$firstOctet = @()
#endregion

$netAddress = ipconfig | find "IPv4 Address"

#Find all IPv4 address with regex below
$addresses = $netAddress | Select-String "(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})" -AllMatches

#No matches = no IPv4 addresses at all
If($addresses -eq $null) {

    $color = 'Red'
    $outputString = "Machine has no IP address."
    [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,1)
    break
}

#Load an array called $firstOctet with each beginning octet
Foreach ($address in $addresses) {
    $dummyValue = $address -match "(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})"
    $firstOctet = $firstOctet + $Matches[1]
}

If($firstOctet -match $unregisteredOctet) {

    $color = 'Red'
    $outputString = "Machine needs to be registered. First octet is " + $unregisteredOctet + ". Launching IE with registration page now."
    [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,1)

    $ie = New-Object -ComObject InternetExplorer.Application
    $ie.Navigate("register.nmu.edu")
    $ie.Visible = $true
}
Else {
    $color = 'Black'
    $outputString = "Machine appears to have at least one valid IPv4 address."
    [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,0)
}