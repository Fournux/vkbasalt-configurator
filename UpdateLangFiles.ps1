$build = ([Xml](Get-Content ./build.xml)).root

$PO_FOLDER = $build.PO_FOLDER
$APP_POT_PATH = $build.APP_POT_PATH
$BLUEPRINT_POT_PATH = $build.BLUEPRINT_POT_PATH
$POT_PATH = $build.POT_PATH

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
xgettext $PO_FOLDER/*.pot -o $POT_PATH

# Update or create PO files for each lang specified in LINGUAS file
foreach($lang in Get-Content $PO_FOLDER/LINGUAS) 
{
    if(Test-Path $PO_FOLDER/$lang.po -PathType leaf)
    {
        msgmerge --update $PO_FOLDER/$lang.po $POT_PATH
    }
    else 
    {
        msginit --input=$POT_PATH --locale=$lang --output=$PO_FOLDER/$lang.po --no-translator
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