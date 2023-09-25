$PROJECT_NAME = 'vkbasalt-configurator'
$BLUEPRINT_FOLDER = './UI/Blueprint'
$POT_FILE = './Data/po/blueprint.pot'

$REGEX = '_\("(.*)"\)' # Regex to detect _("localized strings")

$hashTable = @{}

if (Test-Path $POT_FILE) 
{
    Remove-Item $POT_FILE
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
"X-Generator: Powershell script\n"' -f $PROJECT_NAME, (Get-Date -Format "yyyy-MM-dd HH:mm:ssK")

Add-Content $POT_FILE ($file + "`n")

foreach($element in $hashTable.GetEnumerator())
{
    foreach($item in $element.Value)
    {
        Add-Content $POT_FILE ("#: " + $item)
    }

    Add-Content $POT_FILE ('msgid "' + $element.Name + '"')
    Add-Content $POT_FILE ('msgstr ""' + "`n")
}



