$REGEX = '_\("(.*)"\)' # Regex to detect _("localized strings")

$build = ([Xml](Get-Content ./build.xml)).root
$appSettings = ([Xml](Get-Content ./App.config)).configuration.appSettings.add

$BLUEPRINT_FOLDER = $build.BLUEPRINT_FOLDER
$BLUEPRINT_POT_PATH = $build.BLUEPRINT_POT_PATH
$APP_DISPLAY_NAME = ($appSettings | Where-Object {$_.key -eq 'APP_DISPLAY_NAME'}).value

$hashTable = @{}

if (Test-Path $BLUEPRINT_POT_PATH) 
{
    Remove-Item $BLUEPRINT_POT_PATH
}

$labels = Get-ChildItem -Path $BLUEPRINT_FOLDER -Filter *.blp -Recurse | Select-String -Pattern $REGEX | Select-Object -Property Filename,LineNumber,Matches

foreach ($label in $labels) 
{ 
    $key = $label.Matches.Groups[1].Value

    if (!$hashTable.ContainsKey($key))
    {
        $hashTable.Add($key, [System.Collections.ArrayList]::new())
    }

    $hashTable[$key].Add($label.Filename + ":" + $label.LineNumber)
}

[string]$file = 'msgid ""
msgstr ""
"Project-Id-Version: {0}\n"
"POT-Creation-Date: {1}\n"
"PO-Revision-Date: {1}\n"
"Last-Translator: \n"
"Language-Team: \n"
"MIME-Version: 1.0\n"
"Content-Type: text/plain; charset=utf-8\n"
"Content-Transfer-Encoding: 8bit\n"
"X-Generator: Powershell script\n"' -f $APP_DISPLAY_NAME, (Get-Date -Format "yyyy-MM-dd HH:mm:ssK")

Add-Content $BLUEPRINT_POT_PATH ($file + "`n")

foreach($element in $hashTable.GetEnumerator())
{
    foreach($item in $element.Value)
    {
        Add-Content $BLUEPRINT_POT_PATH ("#: " + $item)
    }

    Add-Content $BLUEPRINT_POT_PATH ('msgid "' + $element.Name + '"')
    Add-Content $BLUEPRINT_POT_PATH ('msgstr ""' + "`n")
}



