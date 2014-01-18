#Persistent
#NoTrayIcon
#Singleinstance Force

vError1 := 0
vError2 := 0
vError3 := 0
vError4 := 0
vError5 := 0
vError6 := 0
vError7 := 0
vError8 := 0
vCount := 0
vTeam= 
vTeam2=
vDirectory= 
vDirectoryLoop= 
vCharacterPosistion= 
vToggle= 
vToggleCheck= 
vServerName= 
vConnect= 
vDota := 0
vPixel1= 
vPixel2= 
vPixel3= 
vPixel4= 

FileReadLine, vDirectory, %Working_Dir%\options.ini, 1
if ErrorLevel = 1
    vError1 := 1

FileReadLine, vTeam, %Working_Dir%\options.ini, 2
if ErrorLevel = 1
    vError2 := 1

FileReadLine, vServerName, %Working_Dir%\options.ini, 3
if ErrorLevel = 1
    vError3 := 1

FileReadLine, vConnect, %Working_Dir%\options.ini, 4
if ErrorLevel = 1
    vError4 := 1

if vTeam = radiant
    vTeam2 = jointeam good
if vTeam = dire
    vTeam2 = jointeam bad
if vTeam = spectator
    vTeam2 = jointeam spectator

FileDelete, error.log.txt
vDirectory = %vDirectory%\dota\cfg

loop
{
	FileReadLine, vDirectoryLoop, %vDirectory%\dota\cfg\config.cfg, %A_Index%
	if (ErrorLevel = 1)
	{
		vError5 := 1
		break
	}

	StringGetPos, vCharacterPosistion, vDirectoryLoop, toggleconsole
	if (vPos >= 0)
	{
		StringMid, vToggle, vDirectoryLoop, 7, (vPos - 9)
		if vToggle = uparrow
		    vToggle = Up
		if vToggle = downarrow
		    vToggle = Down
		if vToggle = leftarrow
		    vToggle = Left
		if vToggle = rightarrow
		    vToggle = Right
		StringMid, vToggleCheck, vToggle, 0, 3
		if (vToggleCheck = vKP)
		{
			StringMid, vToggle, vToggle, 4
			vToggle = Numpad%vToggle%
		}
	}
}

Gui, Add, Button, gs0 w1 h1 x-50 y-50 center vv0, .
Gui, Font, s18 cEFEFEF norm
Gui, Add, text, x0 y0 w500 vvText, Launching Dota 2
Gui, Font, s6 cDBDBDB italics
Gui, Add, text, x0 y34 w500 vvText2, %vServerName%
Gui, Font, s18 c1B1B1B bold
Gui, Add, Button, gsButton w280 h70 x30 y90 center vvButton, Connect to Server

Gui, Show, x34 y36 w520 h500
Gui, Font, s8 c1B1B1B norm
Gui, Add, Button, gsXit w44 h26 x15 y180 center vvCancel, Cancel
Gui +LastFound +AlwaysOnTop -Caption +ToolWindow
Gui, Color, 888888
WinSet, TransColor, 888888

if (vError1 = 1)
{
	FileAppend, er001 - dota.exe directory not specified`n, error.log.txt
	sleep, 50
	Gui, Destroy
	ExitApp
}
if (vError2 = 1)
{
	FileAppend, er002 - Team to join was not specified`n, error.log.txt
	sleep, 50
	Gui, Destroy
	ExitApp
}
if (vError3 = 1)
{
	FileAppend, er003 - Server Name was not Specified`n, error.log.txt
	sleep, 50
	Gui, Destroy
	ExitApp
}
if (vError4 = 1)
{
	FileAppend, er004 - Connection IP was not specified`n, error.log.txt
	sleep, 50
	Gui, Destroy
	ExitApp
}
if (vError5 = 1)
{
	FileAppend, er005 - ToggleConsole hotkey not found within config file`n, error.log.txt
	sleep, 50
	Gui, Destroy
	ExitApp
}

Loop
{
	if (A_Index = 46)
	{
		vError6 := 1
		break
	}
	IfWinExist, DOTA 2
	    vDota := 1
	IfWinExist, DOTA 2
	    vCount := A_Index
	IfWinExist, DOTA 2
	    break
	Sleep, 1000
}

If vCount < 0
    vCount := 0

Sleep, 14000 - vCount

GuiControl Text, vText, Connecting to Server
GuiControl Text, vText2, %vServerName%
GuiControl Hide, vText
GuiControl Show, vText
GuiControl Hide, vText2
GuiControl Show, vText2

if vDota != 1
    vError6 := 1
Loop
{
	if (A_Index = 31)
	{
		GuiControl Show, vButton
		vError7 := 1
		break
	}
	IfWinExist, DOTA 2
	    vDota := 1
	if (vDota = 1)
	{
		IfWinExist, DOTA 2
		    WinActivate
		Sleep, 400
		IfWinExist, DOTA 2
		    WinActivate
		Sleep, 100
		PixelGetColor, vPixel1, 2, 2
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vPixel2, 2, 5
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vPixel3, 5, 2
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vPixel4, 5, 5
		if ErrorLevel = 1
		    vError8 := 1
		if (vPixel1 != 0x000000) and (vPixel2 != 0x000000) and (vPixel3 != 0x000000) and (vPixel4 != 0x000000)
		{
			vPixel1= 
			vPixel2= 
			vPixel3= 
			vPixel4= 
			Sleep, 1000
			IfWinExist, DOTA 2
			    WinActivate
			Sleep, 100
			PixelGetColor, vPixel1, 2, 2
			PixelGetColor, vPixel2, 2, 5
			PixelGetColor, vPixel3, 5, 2
			PixelGetColor, vPixel4, 5, 5
			if (vPixel1 != 0x000000) and (vPixel2 != 0x000000) and (vPixel3 != 0x000000) and (vPixel4 != 0x000000)
			{
				GuiControl Hide, vCancel
				GuiControl Text, vText, Joining Team
				GuiControl Text, vText2, %vTeam2%
				GuiControl Hide, vText
				GuiControl Show, vText
				GuiControl Hide, vText2
				GuiControl Show, vText2
				sleep, 800
				IfWinExist, DOTA 2
				    WinActivate
				sleep, 50
				IfWinExist, DOTA 2
				    SendInput {%vToggle%}				
				sleep, 50
				IfWinExist, DOTA 2
				    SendInput %vTeam%				
				sleep, 50
				IfWinExist, DOTA 2
				    SendInput {enter}
				sleep, 100
				IfWinExist, DOTA 2
				    WinActivate
				sleep, 10
				IfWinExist, DOTA 2
				    SendInput {%vToggle%}			
				sleep, 10
				break
			}
		}

	}
	else
	{
		Sleep, 1000
	}
}


s0:
;
Return

sButton:
IfWinExist, DOTA 2
    WinActivate				
sleep, 50
IfWinExist, DOTA 2
    SendInput %vConnect%				
sleep, 50
IfWinExist, DOTA 2
    SendInput {enter}
sleep, 100
IfWinExist, DOTA 2
    WinActivate
sleep, 5000
Loop
{
	if (A_Index = 121)
	{
		vError9 := 1
		break
	}
	IfWinExist, DOTA 2
	    vDota := 1
	if (vDota = 1)
	{
		IfWinExist, DOTA 2
		    WinActivate
		Sleep, 400
		IfWinExist, DOTA 2
		    WinActivate
		Sleep, 100
		PixelGetColor, vPixel1, 2, 2
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vPixel2, 2, 5
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vPixel3, 5, 2
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vPixel4, 5, 5
		if ErrorLevel = 1
		    vError8 := 1
		if (vPixel1 != 0x000000) and (vPixel2 != 0x000000) and (vPixel3 != 0x000000) and (vPixel4 != 0x000000)
		{
			vPixel1= 
			vPixel2= 
			vPixel3= 
			vPixel4= 
			Sleep, 1000
			IfWinExist, DOTA 2
			    WinActivate
			Sleep, 100
			PixelGetColor, vPixel1, 2, 2
			PixelGetColor, vPixel2, 2, 5
			PixelGetColor, vPixel3, 5, 2
			PixelGetColor, vPixel4, 5, 5
			if (vPixel1 != 0x000000) and (vPixel2 != 0x000000) and (vPixel3 != 0x000000) and (vPixel4 != 0x000000)
			{
				GuiControl Hide, vCancel
				GuiControl Text, vText, Joining Team
				GuiControl Text, vText2, %vTeam2%
				GuiControl Hide, vText
				GuiControl Show, vText
				GuiControl Hide, vText2
				GuiControl Show, vText2
				sleep, 800
				IfWinExist, DOTA 2
				    WinActivate
				sleep, 50
				IfWinExist, DOTA 2
				    SendInput {%vToggle%}				
				sleep, 50
				IfWinExist, DOTA 2
				    SendInput %vTeam%				
				sleep, 50
				IfWinExist, DOTA 2
				    SendInput {enter}
				sleep, 100
				IfWinExist, DOTA 2
				    WinActivate
				sleep, 10
				IfWinExist, DOTA 2
				    SendInput {%vToggle%}			
				sleep, 10
				break
			}
		}

	}
	else
	{
		Sleep, 1000
	}
}
Return

sXit:
sleep, 50
Gui, Destroy
ExitApp
Return

~$F15::
;
Return

if (vError6 = 1)
{
	FileAppend, er006 - Dota 2 was not launched`n, error.log.txt
}
if (vError7 = 1)
{
	FileAppend, er007 - Could not connect to the server`n, error.log.txt
}
if (vError8 = 1)
{
	FileAppend, er008 - Pixels could not be determined`n, error.log.txt
}
if (vError9 = 1)
{
	FileAppend, er009 - CONNECT button activated & Could not connect to IP after pressing that button`n, error.log.txt
}

if (vError1 = 1) or (vError2 = 1) or (vError3 = 1) or (vError4 = 1) or (vError5 = 1) or (vError6 = 1) or (vError7 = 1) or (vError8 = 1) or (vError9 = 1)
{
	FileAppend, `n`n----ERROR LOG----`n`nvError1 = %vError1%`nvError2 = %vError2%`nvError3 = %vError3%`nvError4 = %vError4%`nvError5 = %vError5%`nvError6 = %vError6%`nvError7 = %vError7%`nvError8 = %vError8%`nvCount = %vCount%`nvTeam = %vTeam%`nvTeam2 = %vTeam2%`nvDirectory = %vDirectory%`nvDirectoryLoop = %vDirectoryLoop%`nvCharacterPosistion = %vCharacterPosistion%`nvToggle = %vToggle%`nvToggleCheck = %vToggleCheck%`nvServerName = %vServerName%`nvConnect = %vConnect%`nvDota = %vDota%`nvPixel1 = %vPixel1%`nvPixel2 = %vPixel2%`nvPixel3 = %vPixel3%`nvPixel4 = %vPixel4%`n, error.log.txt
	sleep, 50
	Gui, Destroy
	ExitApp
}