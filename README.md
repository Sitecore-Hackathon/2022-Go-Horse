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

## Pre-requisites and Dependencies

⟹ Sitecore 10.2 with Identity Server fully functional the following modules installed:

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
 
 ![SPE CLI SpeShell](docs/images/GoHorse%20SpeShell.png?raw=true)


**STEP 2** ⟹ Open your CLI folder in the Powershell command window and run the command below:

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
