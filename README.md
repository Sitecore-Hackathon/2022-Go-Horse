![SPE CLI SpeShell](docs/images/GoHorse%20SpeShell.png?raw=true)
# SPE CLI SpeShell

This module was originally an entry at the Sitecore Hackathon 2022 ([Click here to browse the original submission](https://github.com/Sitecore-Hackathon/2022-Go-Horse/tree/Hackathon-submission))  

## Authors
⟹ **Go Horse**
- José Neto
- Roberto de Almeida Jr.
- Rodrigo Peplau 

## Description
⟹ This module extends the Sitecore CLI, adding a new command named "spe" (Sitecore Powershell Extensions)
From the command prompt, you can now run SPE scripts on Sitecore instances.

## TOC

* [Pre-requisites and Dependencies](#pre-requisites-and-dependencies)
* [Installation](#installation-instructions)
* [Usage instructions](#usage-instructions)
* [Login to your Sitecore instance](#login-to-your-sitecore-instance)
* [CASE 1 - Run a Powershell Script stored in Sitecore](#case-1---run-a-powershell-script-stored-in-sitecore)
* [CASE 2 - Run a Powershell Script file from your local](#case-2---run-a-powershell-script-file-from-your-local)
* [CASE 3 - Run Inline Powershell commands](#case-3---run-inline-powershell-commands)
* [CASE 4 - Control multiple Powershell sessions](#case-4---control-multiple-powershell-sessions)

## Pre-requisites and Dependencies
[<< Back to TOC](#toc)

⟹ Sitecore 10.2 with Identity Server fully functional, with the following modules installed:

* Sitecore PowerShell Extensions:  
https://doc.sitecorepowershell.com/installation

* Sitecore CLI 4.1.1 (using installation wizard):  
https://sitecoredev.azureedge.net/~/media/2B968036924A4EEB98C2E68641B63A43.ashx?date=20220127T085224

* GoHorse.GraphQL.SpeShell (using installation wizard):  
[GoHorse GraphQL SpeShell-1.0.zip](/sc-packages/GoHorse%20GraphQL%20SpeShell-1.0.zip?raw=true)

⟹ Install the following dependencies where you want to run the CLI:

* Dotnet SDK - 3.1.416 -x64  
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-3.1.416-windows-x64-installer  

* Dotnet Hosting - 3.1.22  
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-3.1.22-windows-hosting-bundle-installer

## Installation instructions
[<< Back to TOC](#toc)

**STEP 1** ⟹ Get your CLI folder prepared:

> The quickest way to get a valid CLI folder is to clone or download this git repository and use it's root folder
> 
> For a cleaner CLI folder, follow the steps below:

 1. Create an empty folder
 2. Download and copy the following files to the root of your folder:
 - [nuget.config](/nuget.config?raw=true)
 - [sitecore.json](/sitecore.json?raw=true)
 3. Inside your folder, create a new folder called ".config"
 4. Download and copy the following files to the ".config" folder: 
 - [/.config/dotnet-tools.json](/.config/dotnet-tools.json?raw=true)
 
 In the end you folder will look like the following:
 
 ![CLI folder](docs/images/CLIfolder.png?raw=true)

<br>

**STEP 2** ⟹ Restore Sitecore CLI

Open your CLI folder in the Powershell command window and run the command below:

```powershell
dotnet tool restore
```

If any errors occurred during the command above, use the Sitecore official CLI documentation:  https://doc.sitecore.com/xp/en/developers/102/developer-tools/install-sitecore-command-line-interface.html

You must see the message below before continuing:

* *Restore was successful.*

<br>

**STEP 3** ⟹ Install the Sitecore CLI SpeShell Plugin

```powershell
dotnet sitecore plugin add -n GoHorse.CLI.SpeShell
```

You must see the message below before continuing:

* *Successfully installed version X.Y.Z of plugin GoHorse.CLI.SpeShell*

<br>

## Usage instructions

### Login to your Sitecore instance

⟹ Before running the new plugin, you must execute the Sitecore CLI login to authenticate in the Sitecore IdentityServer instance:

```powershell
dotnet sitecore login --authority https://<Sitecore identity server> --cm http://<Sitecore instance> --allow-write true
```

![Sitecore CLI login](docs/images/sitecore-cli-login.png?raw=true "Sitecore CLI login")

[<< Back to TOC](#toc)

<br>

### CASE 1 - Run a Powershell Script stored in Sitecore

The module comes with a test script that can be executed by PATH or ID:

```powershell
dotnet sitecore spe --script-id "/sitecore/system/Modules/PowerShell/Script Library/GoHorse SpeShell Test Script"
```

```powershell
dotnet sitecore spe --script-id "{11CE538E-5EA9-481A-8506-30F7DB03F308}"
```

Will result as below:

```powershell
Results: PowerShell script successful executed /sitecore/system/Modules/PowerShell/Script Library/GoHorse SpeShell Test Script
abc
True
```

[<< Back to TOC](#toc)

<br>

### CASE 2 - Run a Powershell Script file from your local

Local PS1 files can also be executed inside the Sitecore CM instance:

```powershell
dotnet sitecore spe --file .\test.ps1
```

Will result as below:

```powershell
Results: PowerShell script successful executed .\test.ps1
abcTEST
True
```

[<< Back to TOC](#toc)

<br>

### CASE 3 - Run Inline Powershell commands

Local PS1 files can also be executed inside the Sitecore CM instance (Make sure you escape the strings accordingly):

```powershell
dotnet sitecore spe --script "`$test='ABC'; Write-Host `$test;"
```

Will result as below:

```powershell
Results: PowerShell script successful executed $test='ABC'; Write-Host $test;
ABC
```

[<< Back to TOC](#toc)

<br>

### CASE 4 - Control multiple Powershell sessions

Multiple Powershell sessions can be controlled with the *--session* parameter.

For instance, you can instantiate the same variable with different values, using different sessions:

```powershell
dotnet sitecore spe --session "session1" --script "`$test='ABC';"
dotnet sitecore spe --session "session2" --script "`$test='XYZ';"
```
Then you print the variable in different sessions:

```powershell
dotnet sitecore spe --session "session1" --script 'Write-Host $test'
dotnet sitecore spe --session "session2" --script 'Write-Host $test'
```

Will result as below:

```powershell
Results: PowerShell script successful executed Write-Host $test
ABC
Results: PowerShell script successful executed Write-Host $test
XYZ
```

[<< Back to TOC](#toc)

