Set-StrictMode -Version Latest

#Seems to be an issue with printing like usual (Write-Output $outputString). It looks like there's a way to print directly to the RichTextBox from the C# WinForm,
#but it's not working quite correctly. Print to textbox is working, but text is always bolded, no matter what.
#Usage: [HelpDesk_Utilities.Logger]::Log($outputString,[System.Drawing.Color]$color,[bool]'false') where $color = 'Black'

#region Globals
$wimaxLogDir = "C:\Program Files\Intel\WiMAX\Bin\Trace"
$wrtLogDir = "C:\Program Files\Intel\WRT\Plugins\WiFiPlugins\Collected Data"
$logDirectoryCutoffMB = 1000
#endregion

Function CheckWiMaxLogDir {

    #If folder doesn't exist, end the function. User likely does not have WRT installed.
    If(!(Test-Path $wimaxLogDir)) {
        $outputString = "Directory " + $wimaxLogDir + " does not exist. Continuing to next task."
        Write-Output $outputString
        break
    }

    #Find total size of WiMax trace directory + all subdirectories
    $colItems = (Get-ChildItem $wimaxLogDir -recurse | Measure-Object -property length -sum)
    $directorySize = "{0:N2}" -f ($colItems.sum / 1MB)

    #If the directory size is greater than the cutoff size (in MB), delete all subdirectories.
    If($directorySize -ge $logDirectoryCutoffMB) {
        $outputString = "Directory " + $wimaxLogDir + " is greater than " + $logDirectoryCutoffMB + "MB. Deleting all subdirectories and files."
        Write-Output $outputString
        Get-ChildItem -Force $logDirectoryCutoffMB | Remove-Item -Recurse -Force
    }
    Else {
        $outputString = "Directory " + $wimaxLogDir + " is less than " + $logDirectoryCutoffMB + "MB. Continuing to next task."
        Write-Output $outputString
    }
}

Function CheckWRTLogDir {

    #If folder doesn't exist, end the function. User likely does not have WRT installed.
    If(!(Test-Path $wrtLogDir)) {
        $outputString = "Directory " + $wrtLogDir + " does not exist. Continuing to next task."
        Write-Output $outputString
        break
    }

    #Find total size of WRT log directory + all subdirectories
    $colItems = (Get-ChildItem $wrtLogDir -recurse | Measure-Object -property length -sum)
    $directorySize = "{0:N2}" -f ($colItems.sum / 1MB)

    #If the directory size is greater than the cutoff size (in MB), delete all subdirectories.
    If($directorySize -ge $logDirectoryCutoffMB) {
        $outputString = "Directory " + $wrtLogDir + " is greater than " + $logDirectoryCutoffMB + "MB. Deleting all subdirectories and files."
        Write-Output $outputString
        Get-ChildItem -Force $logDirectoryCutoffMB | Remove-Item -Recurse -Force
    }
    Else {
        $outputString = "Directory " + $wrtLogDir + " is less than " + $logDirectoryCutoffMB + "MB. Continuing to next task."
        [console]::WriteLine($outputString)
    }
}

CheckWiMaxLogDir
CheckWRTLogDir