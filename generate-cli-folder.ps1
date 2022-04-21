$Folder = "sitecore-cli-folder"

if (Test-Path -Path $Folder) {
    Write-Host "Folder '$Folder' exist - removing"
    Remove-Item $Folder -Recurse
} 

Write-Host "Creating folder '$Folder'"
New-Item -Path $Folder -ItemType Directory

Write-Host "Copy files to folder '$Folder':"
Copy-Item -Path "nuget.config" -Destination $Folder
Copy-Item -Path "sitecore.json" -Destination $Folder
Copy-Item -Path ".config" -Destination $Folder -Recurse

Write-Host "Finished!"
