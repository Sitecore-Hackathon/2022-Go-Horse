![SPE CLI SpeShell](docs/images/GoHorse%20SpeShell.png?raw=true)
# SPE CLI SpeShell

This module was originally an entry at the Sitecore Hackathon 2022

* [Click here to browse the original submission](https://github.com/Sitecore-Hackathon/2022-Go-Horse/tree/Hackathon-submission)

Or continue down for the updated version

## Team name
⟹ **Go Horse**
- José Neto
- Roberto de Almeida Jr.
- Rodrigo Peplau 

## Category
⟹ **Extend the Sitecore Command Line interface (CLI) plugin**

## Description
⟹ This module extends the Sitecore CLI plugin, adding a new plugin named "gohorse"

 This plugin comes with a command that has the purpose of running a Sitecore Powershell Script.

 The Sitecore CLI gohorse plugin will decrease the dependency of having an open Sitecore instance, in a browser, to run a Powershell script. Now there are endless possibilities of managing the Sitecore XP with the Sitecore CLI gohorse plugin.


## Video link
[![Sitecore Hackathon 2022 - Go Horse Team](docs/images/image-thumbnail.jpg?raw=true)](https://www.youtube.com/watch?v=v2I7RFLJduc "Sitecore Hackathon 2022 - Go Horse Team")



## Pre-requisites and Dependencies
⟹ Sitecore 10.2 with Identity Server fully functional and PowerShell module

⟹ The Sitecore CLI gohorse plugin has the following dependencies below:

* Dotnet SDK - 3.1.416 -x64
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-3.1.416-windows-x64-installer  

* Dotnet Hosting - 3.1.22 
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-3.1.22-windows-hosting-bundle-installer

* Sitecore CLI 4.1.1 (using installation wizard)
https://sitecoredev.azureedge.net/~/media/2B968036924A4EEB98C2E68641B63A43.ashx?date=20220127T085224

* GoHorse.GraphQL (using installation wizard)   
[GoHorse-GraphQL-Ext-1.0.zip](/sc-packages/GoHorse-GraphQL-Ext-1.0.zip?raw=true)


## Installation instructions
⟹ Open the git repository root in the Powershell command window and run the command below:

```powershell
dotnet tool restore
```
If any errors occurred during the command above, use the Sitecore official CLI documentation:  https://doc.sitecore.com/xp/en/developers/102/developer-tools/install-sitecore-command-line-interface.html

You must see the message below before continuing:

* Restore was successful.

⟹ Sitecore CLI GoHorse Plugin

To Install the GoHorse plugin use command below:

```powershell
dotnet sitecore plugin add -n GoHorse.CLI.Command
```

You must see the message below before continuing:

* Successfully installed version X.X.X of plugin GoHorse.CLI.Command


## Usage instructions
⟹ Before running the new plugin, you must execute the Sitecore CLI login to authenticate in the Sitecore IdentityServer instance:

```powershell
dotnet sitecore login --authority https://<Sitecore identity server> --cm http://<Sitecore instance> --allow-write true
```

![Sitecore CLI login](docs/images/sitecore-cli-login.png?raw=true "Sitecore CLI login")

The module comes with a test script that can be executed by the command below:

```powershell
dotnet sitecore gohorse run-command --command-id "{11CE538E-5EA9-481A-8506-30F7DB03F308}"
```

If the execution was successful, users will see a message "true" returned in the window.

![Sitecore gohorse execution](docs/images/sitecore-gohorse-execution.png?raw=true "Sitecore gohorse execution")

When execution finishes you will be able to see that the PowerShell script has updated the home item, title field, adding the text "1" to its value.
![Sitecore Home Updated](docs/images/Sitecore-Home-updated.png?raw=true "Sitecore Home Updated")

#GoHorse


![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
