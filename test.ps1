$item = Get-Item -Path "master:\content\home"

$test = "abcTEST"
Write-Host $test

$item.Editing.BeginEdit()
$item.Fields["Title"].Value = $item.Fields["Title"].Value + "!?"
$item.Editing.EndEdit()