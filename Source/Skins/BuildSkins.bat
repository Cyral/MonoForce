@ECHO OFF
ECHO Building Skin Files (WIN/XNA)...

REM Project file passes solution directory as first parameter.
set SKIN_BASE="%1Skins"
set SKIN_DIR="%1Skins"
set CONTENT_DIR="%1Skins\bin\x86\Content\Skins"
set Z_DIR="%1Tools\7zip"

ECHO +7ZIP: %Z_DIR%
ECHO +SKIN: %SKIN_BASE%
ECHO +PLAT: %SKIN_DIR%
ECHO +CONT: %CONTENT_DIR%

SET SKINS=Default,Bricklayer

FOR %%A in (%SKINS%) do "%Z_DIR%\7za.exe" a -tzip -mx9 -r -x!Addons "%SKIN_DIR%\%%A.skin" "%CONTENT_DIR%\%%A\*.*"