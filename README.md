![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2022  

## Team name
⟹ GoHorse

## Category
⟹ Extend the Sitecore Command Line interface (CLI) plugin

## Description
⟹ This module extends the Sitecore CLI plugin, adding a new plugin named gohorse

 This plugin comes with a command that has the purpose of running a Sitecore Powershell Script.

 The Sitecore CLI gohorse plugin will decrease the dependency of having an open Sitecore instance, in a browser, to run a Powershell script. Now users will have infinity possibilities of managing the Sitecore CMS by the Sitecore CLI gohorse plugin.

_You can alternately paste a [link here](#docs) to a document within this repo containing the description._

## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Replace this Video link](#video-link)



## Pre-requisites and Dependencies

⟹ The Sitecore CLI gohorse plugin has the following dependencies below:

- Dotnet SDK - 3.1.416 -x64
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-3.1.416-windows-x64-installer

- Dotnet Hosting - 3.1.22
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-3.1.22-windows-hosting-bundle-installer

- Sitecore CLI 4.1.1 (should be installed in the Sitecore instance)
https://sitecoredev.azureedge.net/~/media/2B968036924A4EEB98C2E68641B63A43.ashx?date=20220127T085224

- GoHorse.GraphQL (should be installed in the Sitecore instance)
TODO: [PACKAGE URL]
TODO: [Sitecore.IdentityServer.DevEx.xml]

⟹ Sitecore CLI installation
- Follow the below Sitecore documentation to install the Sitecore CLI plugin:
https://doc.sitecore.com/xp/en/developers/102/developer-tools/install-sitecore-command-line-interface.html


## Installation instructions
⟹Open the project directory in the Powershell command window and run the command below:

-> dotnet tool restore

You must see the message below before continuing:
Restore was successful.

⟹ Sitecore CLI GoHorse Plugin

To Install the GoHorse plugin use command below:

-> dotnet sitecore plugin add -n GoHorse.CLI.Command

You must see the message below before continuing:
Successfully installed version X.X.X of plugin GoHorse.CLI.Command

### Configuration
⟹ If there are any custom configuration that has to be set manually then remember to add all details here.

_Remove this subsection if your entry does not require any configuration that is not fully covered in the installation instructions already_

## Usage instructions
⟹ Before running the new plugin, you must execute the Sitecore CLI login to authenticate in the Sitecore IdentityServer instance:

-> dotnet sitecore login --authority https://<Sitecore identity server> --cm http://<Sitecore instance> --allow-write true

![Sitecore CLI login](docs/images/sitecore-cli-login.png?raw=true "Sitecore CLI login")

Use the command below to execute the command available:

-> dotnet gohorse run-command --command-id "{Sitecore PowerShell script ID}"

If the execution was successful, users will see a message "true" returned in the window.

![Sitecore gohorse execution](docs/images/sitecore-gohorse-execution.png?raw=true "Sitecore gohorse execution")

NOTE:
If you do not have a Powershell script to test the plugin, open your Sitecore instance, and create a new one adding the code below (this script changes the Title field of the Home item, adding its text + "1"):

$item = Get-Item -Path "master:\content\home"

$item.Editing.BeginEdit()

$item.Fields["Title"].Value = $item.Fields["Title"].Value + "1"

$item.Editing.EndEdit()



![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](docs/images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## Comments
If you'd like to make additional comments that is important for your module entry.
