SET WORKSPACE=%~dp0
SET SOURCETABLEDIR=%~dp0/../../../Product/TableSources/
SET TABLERESDIR=%~dp0/../../Assets/StreamingAssets/config/

cd %WORKSPACE%
%~dp0/convertxlsx.exe -i %SOURCETABLEDIR% -o %TABLERESDIR%

pause