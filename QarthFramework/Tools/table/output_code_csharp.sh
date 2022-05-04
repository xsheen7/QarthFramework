WORKSPACE=$(cd `dirname $0`; pwd)
SOURCETABLEDIR=$WORKSPACE/../../../Product/TableSources/
PROJECTTABLEDIR=$WORKSPACE/../../Assets/Scripts/TableModule/Tables/

cd $WORKSPACE
./outputcode -i $SOURCETABLEDIR -o $PROJECTTABLEDIR