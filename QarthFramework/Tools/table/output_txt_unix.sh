WORKSPACE=$(cd `dirname $0`; pwd)
SOURCETABLEDIR=$WORKSPACE/../../../Product/TableSources/
TABLERESDIR=$WORKSPACE/../../Assets/StreamingAssets/config

cd $WORKSPACE
printf $WORKSPACE
python ./xlsxconvert/convertxlsx.py -i $SOURCETABLEDIR -o $TABLERESDIR
