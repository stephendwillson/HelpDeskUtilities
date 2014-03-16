Set-StrictMode -Version Latest

#region Globals
$color = 'Black'
$logDirectoryCutoffMB = 1000
$wimaxLogDir = "C:\Program Files\Intel\WiMAX\Bin\Trace"
$wrtLogDir = "C:\Program Files\Intel\WRT\Plugins\WiFiPlugins\Collected Data"
#endregion

Function CheckWiMaxLogDir {

    #If folder doesn't exist, end the function. User likely does not have WRT installed.
    If(!(Test-Path $wimaxLogDir)) {
        $outputString = "Directory " + $wimaxLogDir + " does not exist. Continuing to next task."
        [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,0)
        break
    }

    #Find total size of WiMax trace directory + all subdirectories
    $colItems = (Get-ChildItem $wimaxLogDir -recurse | Measure-Object -property length -sum)
    $directorySize = "{0:N2}" -f ($colItems.sum / 1MB)

    #If the directory size is greater than the cutoff size (in MB), delete all subdirectories.
    If($directorySize -ge $logDirectoryCutoffMB) {
        $outputString = "Directory " + $wimaxLogDir + " is greater than " + $logDirectoryCutoffMB + "MB. Deleting all subdirectories and files."
        [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,0)
        Get-ChildItem -Force $logDirectoryCutoffMB | Remove-Item -Recurse -Force
    }
    Else {
        $outputString = "Directory " + $wimaxLogDir + " is less than " + $logDirectoryCutoffMB + "MB. Continuing to next task."
        [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,0)
    }
}

Function CheckWRTLogDir {

    #If folder doesn't exist, end the function. User likely does not have WRT installed.
    If(!(Test-Path $wrtLogDir)) {
        $outputString = "Directory " + $wrtLogDir + " does not exist. Continuing to next task."
        [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,0)
        break
    }

    #Find total size of WRT log directory + all subdirectories
    $colItems = (Get-ChildItem $wrtLogDir -recurse | Measure-Object -property length -sum)
    $directorySize = "{0:N2}" -f ($colItems.sum / 1MB)

    #If the directory size is greater than the cutoff size (in MB), delete all subdirectories.
    If($directorySize -ge $logDirectoryCutoffMB) {
        $outputString = "Directory " + $wrtLogDir + " is greater than " + $logDirectoryCutoffMB + "MB. Deleting all subdirectories and files."
        [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,0)
        Get-ChildItem -Force $logDirectoryCutoffMB | Remove-Item -Recurse -Force
    }
    Else {
        $outputString = "Directory " + $wrtLogDir + " is less than " + $logDirectoryCutoffMB + "MB. Continuing to next task."
        [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,0)
    }
}

CheckWiMaxLogDir
CheckWRTLogDir