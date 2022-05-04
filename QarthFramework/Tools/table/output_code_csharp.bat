SET WORKSPACE=%~dp0
SET SOURCETABLEDIR=%~dp0/../../../Product/TableSources/
SET PROJECTTABLEDIR=%~dp0/../../Assets/Scripts/TableModule/Tables/

cd %WORKSPACE%
%~dp0/outputcode -i %SOURCETABLEDIR% -o %PROJECTTABLEDIR%

pause