; <COMPILER: v1.0.48.5>
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
vError9 := 0
vError11 := 0
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
vXP1=
vXP2=
vXP3=
vXP4=
vConCheck= v v

FileReadLine, vDirectory, options.ini, 1
if ErrorLevel = 1
    vError1 := 1

FileReadLine, vTeam, options.ini, 2
if ErrorLevel = 1
    vError2 := 1

FileReadLine, vServerName, options.ini, 3
if ErrorLevel = 1
    vError3 := 1

FileReadLine, vConnect, options.ini, 4
if ErrorLevel = 1
    vError4 := 1

if vTeam = radiant
    vTeam2 = jointeam good
if vTeam = dire
    vTeam2 = jointeam bad
if vTeam = spectator
    vTeam2 = jointeam spectator
if (vTeam = R)
{
	vTeam2 = jointeam good
	vTeam = Radiant
}
if vTeam = D
{
	vTeam2 = jointeam bad
	vTeam = Dire
}
if vTeam = S
{
	vTeam2 = jointeam spectator
	vTeam = Spectator
}


FileDelete, error.log.txt

loop
{
	FileReadLine, vDirectoryLoop, %vDirectory%\dota\cfg\config.cfg, %A_Index%
	if (ErrorLevel = 1)
	{
		vError5 := 1
		break
	}

	StringGetPos, vCharacterPosistion, vDirectoryLoop, toggleconsole
	if (vCharacterPosistion >= 0)
	{
		StringMid, vToggle, vDirectoryLoop, 7, (vCharacterPosistion - 9)
		if vToggle = uparrow
		    vToggle = Up
		if vToggle = downarrow
		    vToggle = Down
		if vToggle = leftarrow
		    vToggle = Left
		if vToggle = rightarrow
		    vToggle = Right
		if vToggle = KP_MINUS
		    vToggle = NumpadSub
		if vToggle = KP_PLUS
		    vToggle = NumpadAdd
		if vToggle = KP_SLASH
		    vToggle = NumpadDiv
		if vToggle = KP_MULTIPLY
		    vToggle = NumpadMult
		if vToggle = KP_Enter
		    vToggle = NumpadEnter
		if vToggle = KP_DEL
		    vToggle = NumpadDel
		if vToggle = KP_INS
		    vToggle = NumpadIns
		if vToggle = KP_DOWNARROW
		    vToggle = NumpadDown
		if vToggle = KP_LEFTARROW
		    vToggle = NumpadLeft
		if vToggle = KP_RIGHTARROW
		    vToggle = NumpadRight
		if vToggle = KP_UPARROR
		    vToggle = NumpadUp
		if vToggle = KP_HOME
		    vToggle = NumpadHome
		if vToggle = KP_PGUP
		    vToggle = NumpadPgUp
		if vToggle = KP_PGDN
		    vToggle = NumpadPgDn
		if vToggle = KP_0
		    vToggle = Numpad0
		if vToggle = KP_1
		    vToggle = Numpad1
		if vToggle = KP_2
		    vToggle = Numpad2
		if vToggle = KP_3
		    vToggle = Numpad3
		if vToggle = KP_4
		    vToggle = Numpad4
		if vToggle = KP_5
		    vToggle = Numpad5
		if vToggle = KP_6
		    vToggle = Numpad6
		if vToggle = KP_7
		    vToggle = Numpad7
		if vToggle = KP_8
		    vToggle = Numpad8
		if vToggle = KP_9
		    vToggle = Numpad9
		break
	}
}

Gui, Add, Button, gs0 w1 h1 x-50 y-50 center vv0, .
Gui, Font, s6 cDBDBDB italics
Gui, Add, text, x0 y34 w500 vvText2, %vServerName%
Gui, Add, text, x70 y180 w500 vvText3, 1.2.7D hotfix 5
Gui, Font, s18 cEFEFEF norm
Gui, Add, text, x0 y0 w500 vvText, Launching Dota 2
Gui, Font, s12 c1B1B1B bold
Gui, Add, Button, gsConnect w140 h70 x20 y90 center vvButtonConnect, Connect to Server
GuiControl Hide, vButtonConnect
Gui, Add, Button, gsJoinTeam w140 h70 x170 y90 center vvButtonJoinTeam, Join Team
GuiControl Hide, vButtonJoinTeam

Gui, Show, x34 y36 w520 h500
Gui, Font, s8 c1B1B1B norm
Gui, Add, Button, gsClose w44 h26 x15 y180 center vvClose, Close
GuiControl Hide, vClose
Gui, Add, Button, gsXit w44 h26 x15 y180 center vvCancel, Cancel
Gui +LastFound +AlwaysOnTop -Caption
Gui, Color, 888888
WinSet, TransColor, 888888

if (vError1 = 1)
{
	FileAppend, er001 - dota.exe directory is incorrect`n, error.log.txt
}
if (vError2 = 1)
{
	FileAppend, er002 - Team to join was not specified`n, error.log.txt
}
if (vError3 = 1)
{
	FileAppend, er003 - Server Name was not Specified`n, error.log.txt
}
if (vError4 = 1)
{
	FileAppend, er004 - Connection IP was not specified`n, error.log.txt
}
if (vError5 = 1)
{
	FileAppend, er005 - ToggleConsole hotkey not found within config file`n, error.log.txt
}
if (vError1 = 1) or (vError2 = 1) or (vError3 = 1) or (vError4 = 1) or (vError5 = 1) or (vError6 = 1) or (vError7 = 1) or (vError8 = 1) or (vError9 = 1) or (vError11 = 1) or (vError9 = 1)
{
	FileAppend, `n`n----ERROR LOG----`n`nvError1 = %vError1%`nvError2 = %vError2%`nvError3 = %vError3%`nvError4 = %vError4%`nvError5 = %vError5%`nvError6 = %vError6%`nvError7 = %vError7%`nvError8 = %vError8%`nvError9 = %vError9%`nvError11 = %vError11%`nvCount = %vCount%`nvTeam = %vTeam%`nvTeam2 = %vTeam2%`nvDirectory = %vDirectory%`nvDirectoryLoop = %vDirectoryLoop%`nvCharacterPosistion = %vCharacterPosistion%`nvToggle = %vToggle%`nvToggleCheck = %vToggleCheck%`nvServerName = %vServerName%`nvConnect = %vConnect%`nvDota = %vDota%`nvPixel1 = %vPixel1%`nvPixel2 = %vPixel2%`nvPixel3 = %vPixel3%`nvPixel4 = %vPixel4%`nvXP1 = %vXP1%`nvXP2 = %vXP2%`nvXP3 = %vXP3%`nvXP4 = %vXP4%`n, error.log.txt
	sleep, 50
	IfWinExist, ActivateConsole
	    WinActivate
	IfWinExist, ActivateConsole
	    ExitApp
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

loop
{
	if (A_Index = 601)
	{
		GuiControl Text, vText, Manual Connection Override
		GuiControl Text, vText2, connection failed
		GuiControl Text, vText3, close when done
		GuiControl Hide, vText
		GuiControl Show, vText
		GuiControl Hide, vText2
		GuiControl Show, vText2
		GuiControl Hide, vText3
		GuiControl Show, vText3
		GuiControl Show, vButtonConnect
		GuiControl Show, vButtonJoinTeam
		break
	}

	vClipboard = Clipboard
	Clipboard=
	BlockInput, On
	IfWinExist, DOTA 2
	    WinActivate
	IfWinExist, DOTA 2
	    SendInput %A_Space%
	IfWinExist, DOTA 2
	    Send {Shift Down}
	IfWinExist, DOTA 2
	    Send {Left}
	IfWinExist, DOTA 2
	    Send {Shift Up}
	IfWinExist, DOTA 2
	    Send ^x
	BlockInput, Off
	BlockInput, Off
	sleep, 150
	BlockInput, Off
	Clipboard = v%Clipboard%v
	if (clipboard = vConCheck)
	{
		IfWinExist, DOTA 2
		    WinActivate
		PixelGetColor, vXP1, 2, 2
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vXP2, 2, 5
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vXP3, 5, 2
		if ErrorLevel = 1
		    vError8 := 1
		PixelGetColor, vXP4, 5, 5
		if ErrorLevel = 1
		    vError8 := 1
		Clipboard = vClipboard
		IfWinExist, DOTA 2
		    WinActivate
		BlockInput, On
		IfWinExist, DOTA 2
		    send {enter}
		sleep, 100
		IfWinExist, DOTA 2
		    sendInput %vConnect%
		IfWinExist, DOTA 2
		    sendInput {enter}
		BlockInput, Off
		BlockInput, Off
		break
	}
	else
	{
		Sleep, 100
	}
}
BlockInput, Off
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
	if (A_Index = 61)
	{
		GuiControl Text, vText, Manual Connection Override
		GuiControl Text, vText2, connection failed
		GuiControl Text, vText3, close when done
		GuiControl Hide, vText
		GuiControl Show, vText
		GuiControl Hide, vText2
		GuiControl Show, vText2
		GuiControl Hide, vText3
		GuiControl Show, vText3
		GuiControl Show, vButtonConnect
		GuiControl Show, vButtonJoinTeam
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
		if (A_Index < 3)
		{
			sleep, 2000
		}
		if (vPixel1 = vXP1) and (vPixel2 = vXP2) and (vPixel3 = vXP3) and (vPixel4 = vXP4)
		{
			GuiControl Text, vText, Manual Connection Override
			GuiControl Text, vText2, connection failed
			GuiControl Text, vText3, close when done
			GuiControl Hide, vText
			GuiControl Show, vText
			GuiControl Hide, vText2
			GuiControl Show, vText2
			GuiControl Hide, vText3
			GuiControl Show, vText3
			GuiControl Show, vButtonConnect
			GuiControl Show, vButtonJoinTeam
			vError7 := 1
			break
		}
		else
		{
			if (vPixel1 != 0x000000) or (vPixel2 != 0x000000) or (vPixel3 != 0x000000) or (vPixel4 != 0x000000)
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
				if (vPixel1 != 0x000000) or (vPixel2 != 0x000000) or (vPixel3 != 0x000000) or (vPixel4 != 0x000000)
				{
					GuiControl Show, vClose
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
					BlockInput, On
					IfWinExist, DOTA 2
					    SendInput %vTeam2%
					sleep, 50
					IfWinExist, DOTA 2
					    SendInput {enter}
					BlockInput, Off
					sleep, 100
					BlockInput, Off
					IfWinExist, DOTA 2
					    WinActivate
					sleep, 10
					IfWinExist, DOTA 2
					    SendInput {%vToggle%}
					sleep, 10
					vDota := 3
				}
			}
			else
			{
				Sleep, 1000
			}
		}
	}
	if (vDota = 3)
	{
		BlockInput, Off
		FileAppend, Successful Launch`n`n----ERROR LOG----`n`nvError1 = %vError1%`nvError2 = %vError2%`nvError3 = %vError3%`nvError4 = %vError4%`nvError5 = %vError5%`nvError6 = %vError6%`nvError7 = %vError7%`nvError8 = %vError8%`nvError9 = %vError9%`nvError11 = %vError11%`nvCount = %vCount%`nvTeam = %vTeam%`nvTeam2 = %vTeam2%`nvDirectory = %vDirectory%`nvDirectoryLoop = %vDirectoryLoop%`nvCharacterPosistion = %vCharacterPosistion%`nvToggle = %vToggle%`nvToggleCheck = %vToggleCheck%`nvServerName = %vServerName%`nvConnect = %vConnect%`nvDota = %vDota%`nvPixel1 = %vPixel1%`nvPixel2 = %vPixel2%`nvPixel3 = %vPixel3%`nvPixel4 = %vPixel4%`nvXP1 = %vXP1%`nvXP2 = %vXP2%`nvXP3 = %vXP3%`nvXP4 = %vXP4%`n, error.log.txt
		sleep, 50
		IfWinExist, ActivateConsole
		    WinActivate
		IfWinExist, ActivateConsole
		    ExitApp
	ExitApp
		break
	}
}

s0:

Return

sConnect:
sleep, 50
vClipboard = Clipboard
Clipboard=
BlockInput, On
IfWinExist, DOTA 2
    WinActivate
IfWinExist, DOTA 2
    SendInput %A_Space%
IfWinExist, DOTA 2
    Send {Shift Down}
IfWinExist, DOTA 2
    Send {Left}
IfWinExist, DOTA 2
    Send {Shift Up}
IfWinExist, DOTA 2
    Send ^x
sleep, 150
Clipboard = v%Clipboard%v
if (clipboard != vConCheck)
{
    SendInput {%vToggle%}
}
IfWinExist, DOTA 2
    WinActivate
PixelGetColor, vXP1, 2, 2
PixelGetColor, vXP2, 2, 5
PixelGetColor, vXP3, 5, 2
PixelGetColor, vXP4, 5, 5

Clipboard = vClipboard
IfWinExist, DOTA 2
    WinActivate
IfWinExist, DOTA 2
    send {enter}
sleep, 100
IfWinExist, DOTA 2
    sendInput %vConnect%
IfWinExist, DOTA 2
    sendInput {enter}
BlockInput, Off
BlockInput, Off
IfWinExist, DOTA 2
    WinActivate
IfWinExist, ActivateConsole
    WinActivate
IfWinExist, DOTA 2
    WinActivate
sleep, 5000
BlockInput, Off
Loop
{
	if (A_Index = 121)
	{
		vError11 := 1
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
		if (A_Index < 3)
		{
			sleep, 2000
		}
		if (vPixel1 = vXP1) and (vPixel2 = vXP2) and (vPixel3 = vXP3) and (vPixel4 = vXP4)
		{
			vError11 := 1
			break
		}
	}
}
IfWinExist, DOTA 2
    WinActivate
IfWinExist, ActivateConsole
    WinActivate
IfWinExist, DOTA 2
    WinActivate
Return

sJoinTeam:
sleep, 800
IfWinExist, DOTA 2
    WinActivate
sleep, 50
IfWinExist, DOTA 2
    SendInput {%vToggle%}
sleep, 50
BlockInput, On
IfWinExist, DOTA 2
    SendInput %vTeam2%
sleep, 50
IfWinExist, DOTA 2
    SendInput {enter}
BlockInput, Off
sleep, 100
IfWinExist, DOTA 2
    WinActivate
sleep, 10
IfWinExist, DOTA 2
    SendInput {%vToggle%}
sleep, 10
FileAppend, Connected to Server successful, did not join team Automatically.`nPressed the button to Join Team`n`n----ERROR LOG----`n`nvError1 = %vError1%`nvError2 = %vError2%`nvError3 = %vError3%`nvError4 = %vError4%`nvError5 = %vError5%`nvError6 = %vError6%`nvError7 = %vError7%`nvError8 = %vError8%`nvError9 = %vError9%`nvError11 = %vError11%`nvCount = %vCount%`nvTeam = %vTeam%`nvTeam2 = %vTeam2%`nvDirectory = %vDirectory%`nvDirectoryLoop = %vDirectoryLoop%`nvCharacterPosistion = %vCharacterPosistion%`nvToggle = %vToggle%`nvToggleCheck = %vToggleCheck%`nvServerName = %vServerName%`nvConnect = %vConnect%`nvDota = %vDota%`nvPixel1 = %vPixel1%`nvPixel2 = %vPixel2%`nvPixel3 = %vPixel3%`nvPixel4 = %vPixel4%`nvXP1 = %vXP1%`nvXP2 = %vXP2%`nvXP3 = %vXP3%`nvXP4 = %vXP4%`n, error.log.txt
BlockInput, Off
sleep, 50
BlockInput, Off
IfWinExist, DOTA 2
    WinActivate
IfWinExist, ActivateConsole
    WinActivate
IfWinExist, DOTA 2
    WinActivate
Return

sXit:
sleep, 50
BlockInput, Off
FileAppend, User pressed Cancel`n`n----ERROR LOG----`n`nvError1 = %vError1%`nvError2 = %vError2%`nvError3 = %vError3%`nvError4 = %vError4%`nvError5 = %vError5%`nvError6 = %vError6%`nvError7 = %vError7%`nvError8 = %vError8%`nvError9 = %vError9%`nvError11 = %vError11%`nvCount = %vCount%`nvTeam = %vTeam%`nvTeam2 = %vTeam2%`nvDirectory = %vDirectory%`nvDirectoryLoop = %vDirectoryLoop%`nvCharacterPosistion = %vCharacterPosistion%`nvToggle = %vToggle%`nvToggleCheck = %vToggleCheck%`nvServerName = %vServerName%`nvConnect = %vConnect%`nvDota = %vDota%`nvPixel1 = %vPixel1%`nvPixel2 = %vPixel2%`nvPixel3 = %vPixel3%`nvPixel4 = %vPixel4%`nvXP1 = %vXP1%`nvXP2 = %vXP2%`nvXP3 = %vXP3%`nvXP4 = %vXP4%`n, error.log.txt
IfWinExist, ActivateConsole
    WinActivate
IfWinExist, ActivateConsole
    ExitApp
ExitApp
Return

sClose:
sleep, 50
BlockInput, Off
FileDelete, error.log.txt
FileAppend, `n`n----VAR LOG----`n`nvError1 = %vError1%`nvError2 = %vError2%`nvError3 = %vError3%`nvError4 = %vError4%`nvError5 = %vError5%`nvError6 = %vError6%`nvError7 = %vError7%`nvError8 = %vError8%`nvError9 = %vError9%`nvError11 = %vError11%`nvCount = %vCount%`nvTeam = %vTeam%`nvTeam2 = %vTeam2%`nvDirectory = %vDirectory%`nvDirectoryLoop = %vDirectoryLoop%`nvCharacterPosistion = %vCharacterPosistion%`nvToggle = %vToggle%`nvToggleCheck = %vToggleCheck%`nvServerName = %vServerName%`nvConnect = %vConnect%`nvDota = %vDota%`nvPixel1 = %vPixel1%`nvPixel2 = %vPixel2%`nvPixel3 = %vPixel3%`nvPixel4 = %vPixel4%`nvXP1 = %vXP1%`nvXP2 = %vXP2%`nvXP3 = %vXP3%`nvXP4 = %vXP4%`n, error.log.txt
IfWinExist, ActivateConsole
    WinActivate
IfWinExist, ActivateConsole
    ExitApp
ExitApp
Return

~$F15::

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
if (vError11 = 1)
{
	FileAppend, er011 - Connection timed out. Cannot connect to server`n, error.log.txt
}

if (vError1 = 1) or (vError2 = 1) or (vError3 = 1) or (vError4 = 1) or (vError5 = 1) or (vError6 = 1) or (vError7 = 1) or (vError8 = 1) or (vError9 = 1) or (vError11 = 1) or (vError9 = 1) or (vError11 = 1)
{
	FileAppend, `n`n----ERROR LOG----`n`nvError1 = %vError1%`nvError2 = %vError2%`nvError3 = %vError3%`nvError4 = %vError4%`nvError5 = %vError5%`nvError6 = %vError6%`nvError7 = %vError7%`nvError8 = %vError8%`nvError9 = %vError9%`nvError11 = %vError11%`nvError9 = %vError9%`nvError11 = %vError11%`nvCount = %vCount%`nvTeam = %vTeam%`nvTeam2 = %vTeam2%`nvDirectory = %vDirectory%`nvDirectoryLoop = %vDirectoryLoop%`nvCharacterPosistion = %vCharacterPosistion%`nvToggle = %vToggle%`nvToggleCheck = %vToggleCheck%`nvServerName = %vServerName%`nvConnect = %vConnect%`nvDota = %vDota%`nvPixel1 = %vPixel1%`nvPixel2 = %vPixel2%`nvPixel3 = %vPixel3%`nvPixel4 = %vPixel4%`nvXP1 = %vXP1%`nvXP2 = %vXP2%`nvXP3 = %vXP3%`nvXP4 = %vXP4%`n, error.log.txt
	sleep, 50
	BlockInput, Off
	IfWinExist, ActivateConsole
	    WinActivate
	IfWinExist, ActivateConsole
	    ExitApp
ExitApp
}
else if (vError1 != 1) and (vError2 != 1) and (vError3 != 1) and (vError4 != 1) and (vError5 != 1) and (vError6 != 1) and (vError7 != 1) and (vError8 != 1) and (vError9 != 1)
{
	FileAppend, `n`n----VAR LOG----`n`nvError1 = %vError1%`nvError2 = %vError2%`nvError3 = %vError3%`nvError4 = %vError4%`nvError5 = %vError5%`nvError6 = %vError6%`nvError7 = %vError7%`nvError8 = %vError8%`nvError9 = %vError9%`nvError11 = %vError11%`nvCount = %vCount%`nvTeam = %vTeam%`nvTeam2 = %vTeam2%`nvDirectory = %vDirectory%`nvDirectoryLoop = %vDirectoryLoop%`nvCharacterPosistion = %vCharacterPosistion%`nvToggle = %vToggle%`nvToggleCheck = %vToggleCheck%`nvServerName = %vServerName%`nvConnect = %vConnect%`nvDota = %vDota%`nvPixel1 = %vPixel1%`nvPixel2 = %vPixel2%`nvPixel3 = %vPixel3%`nvPixel4 = %vPixel4%`nvXP1 = %vXP1%`nvXP2 = %vXP2%`nvXP3 = %vXP3%`nvXP4 = %vXP4%`n, error.log.txt
}
