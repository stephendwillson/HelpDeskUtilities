#include "MsgBoxConstants.au3"
#include <File.au3>

Opt("MustDeclareVars", 1)
Opt("WinTitleMatchMode", 2) ;will search for substrings ANYWHERE in the title, rather than the default behavior (substring from the START of file)

#Region Globals
Global $wimaxFullPath = "C:\Program Files\Intel\WiMAX\Bin\WiMAXCU.exe"
Global $resetExitCodeFilename = "C:\reset"
Global $wimaxWindowTitle = "WiMAX"
Global $restoreCompleteTitle = "Intel® PROSet/Wireless WiMAX"
Global $restoreCompleteText = "restored"
Global $optionsButtonText = "Options"
Global $advancedWindowText = "Advanced"
Global $restoreButtonText = "Restore Settings"
Global $defaultTimeout = 20
#EndRegion

;Notify user to leave keyboard/mouse alone, just in case a poorly-timed click/keypress breaks this script
MsgBox($MB_ICONWARNING,"HelpDesk Utilities","Please do not interact with the mouse/keyboard until notified! Click ""OK"" to continue, or wait for this notification to close on its own.",10)
Sleep(3000)

;Open the WiMax application
Local $pid = Run($wimaxFullPath)
If $pid == 0 Then
   ExitScript(1)
EndIf

;Wait for window to open for $defaultTimeout seconds
Local $hWnd = WinWait($wimaxWindowTitle,"",$defaultTimeout)
If $hWnd == 0 Then
   ExitScript(1)
EndIf
Sleep(500)

;Activate the window (bring to foreground)
WinActivate($hWnd)
Sleep(500)

;Open up Options menu, select third item down (hopefully "Advanced"!) and hit Enter
ControlFocus($wimaxWindowTitle,"Options","[NAME:BtnBoxOptionsWiFiHelp]")
Sleep(500)
Send("{SPACE}")
Sleep(500)

For $i = 1 To 3 Step 1
   Send("{DOWN}")
   Sleep(500)
Next
Send("{ENTER}")
Sleep(500)

;Grab a handle on the newly opened Advanced options window
ControlSetText("","","[NAME:AdvancedPanel]","HelpDeskTest") ;Since the window has "no" title/text, we force it to have some text
Local $hWndAdvanced = WinGetHandle("","HelpDeskTest")
If $hWndAdvanced == 0 Then
   ExitScript(1)
EndIf

;Move to "Settings" tab
Send("{RIGHT}")
Sleep(500)

;Click "Restore settings for all networks" radio button
Send("{TAB}")
Sleep(500)
Send("{UP}")
Send("{UP}") ;this second Up keypress isn't necessary, but I'm leaving it just in case
Sleep(500)
ControlClick("","",$restoreButtonText)

;Wait for settings to finish restoring
Local $hWndRestore = WinWait($restoreCompleteTitle,$restoreCompleteText,$defaultTimeout)
Sleep(5000)
If $hWndRestore == 0 Then
   ExitScript(1)
EndIf

;Close all windows
WinClose($hWndRestore)
Sleep(500)
WinClose($hWndAdvanced)
Sleep(500)
WinClose($hWnd)
Sleep(500)

ExitScript(0)

;Notify user all GUI automation is complete and set exit code accordingly
Func ExitScript($exitCode)

   If Not _FileCreate($resetExitCodeFilename) Then
	  $exitCode = 1
   EndIf

   If Not FileWriteLine($resetExitCodeFilename,String($exitCode)) Then
	  $exitCode = 1
   EndIf

   If $exitCode == 1 Then
	  MsgBox($MB_ICONWARNING,"HelpDesk Utilities","An error has occurred! You may now interact with the mouse/keyboard. Thank you!",10)
	  Exit(1)
   Else
	  MsgBox($MB_ICONWARNING,"HelpDesk Utilities","You may now interact with the mouse/keyboard. Thank you!",10)
	  Exit(0)
   EndIf

EndFunc