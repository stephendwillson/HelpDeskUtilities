Set-StrictMode -Version Latest

#Script will need to be updated when IPv6 is rolled out at NMU.

#region Globals
$unregisteredOctet = 10
$validOctets = 35,172,198,204,207
$validOutputString = ""
$invalidOutputString = ""
#endregion

#Use a regular expression to find the 4 octets of IPv4 address.
#Values found to match automatically get added to $matches.
#$matches[0] is full IP address, $matches[1] is first octet, $matches[2] is second octet, etc.
$netAddress = ipconfig | find "IPv4 Address"
If(!($netAddress -match "(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})")) {
    Write-Error "Machine has no IP address."
}

#If first octet is 10, machine needs to registered, so launch IE to the registration page.
If($matches[1] -eq $unregisteredOctet) {
    $invalidOutputString = "Machine needs to be registered. First octet is " + $matches[1] + ". Launching IE with registration page now."
    Write-Error $invalidOutputString
    $ie = New-Object -ComObject InternetExplorer.Application
    $ie.Navigate("register.nmu.edu")
    $ie.Visible = $true
}
ElseIf($validOctets -match $matches[1]) {
    $validOutputString = "Machine has a valid IPv4 address. First octet is " + $matches[1] + "."
    Write-Output $validOutputString
}