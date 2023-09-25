$PO_PATH = "Data/po"
$APP_POT_PATH = $PO_PATH + "/application.pot"
$BLUEPRINT_POT_PATH = $PO_PATH + "/blueprint.pot"
$POT_PATH = $PO_PATH + "/strings.pot"

# Remove existing POT file
if (Test-Path $POT_PATH) 
{
    Remove-Item $POT_PATH
}

# Extract strings from CS source files
GetText.Extractor -t $APP_POT_PATH

# Extract strings from Blueprint UI files
./ExtractorStringsBlueprint.ps1

# Merge the two POT files together
xgettext $PO_PATH/*.pot -o $POT_PATH

# Update or create PO files for each lang specified in LINGUAS file
foreach($lang in Get-Content $PO_PATH/LINGUAS) 
{
    if(Test-Path $PO_PATH/$lang.po -PathType leaf)
    {
        msgmerge --update $PO_PATH/$lang.po $POT_PATH
    }
    else 
    {
        msginit --input=$POT_PATH --locale=$lang --output=$PO_PATH/$lang.po --no-translator
    }
}

# Cleanup intermediate files
if (Test-Path $BLUEPRINT_POT_PATH) 
{
    Remove-Item $BLUEPRINT_POT_PATH
}
if (Test-Path $APP_POT_PATH) 
{
    Remove-Item $APP_POT_PATH
}