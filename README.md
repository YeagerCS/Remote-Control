# Remote Control

## Description
Remote Control is a distributed application which allows external users to remotely control windows devices in the same network via the web. 

## Functionality
The end user runs a c# application on his device, enabling another user (remote user) within the same network to find the end users device on the web client. The remote user can then execute any CMD command on the end users device with a few additional commands from the c# application. These commands include the following functionalities:
- `create <filename.ext>` => creates any file
- `lock` => locks the device
- `wallpaper https://<url>` => sets a wallpaper on the device
- `write <file.ext> "<text>"` => writes to the specified file

## How it works
### Structure
<img src="/gitImages/diagram.png"/>
