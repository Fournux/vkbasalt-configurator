$PO_PATH = "Data/po"
$APP_POT_PATH = $PO_PATH + "/application.pot"
$BLUEPRINT_POT_PATH = $PO_PATH + "/blueprint.pot"
$POT_PATH = $PO_PATH + "/strings.pot"

if (Test-Path $POT_PATH) 
{
    Remove-Item $POT_PATH
}

GetText.Extractor -t $APP_POT_PATH
./ExtractStringBlueprint.ps1
xgettext $PO_PATH/*.pot -o $POT_PATH

foreach($lang in Get-Content $PO_PATH/LINGUAS) {
    if(Test-Path $PO_PATH/$lang.po -PathType leaf){
        msgmerge --update $PO_PATH/$lang.po $POT_PATH
    }
    else {
        msginit --input=$POT_PATH --locale=$lang --output=$PO_PATH/$lang.po --no-translator
    }
}

if (Test-Path $BLUEPRINT_POT_PATH) 
{
    Remove-Item $BLUEPRINT_POT_PATH
}

if (Test-Path $APP_POT_PATH) 
{
    Remove-Item $APP_POT_PATH
}