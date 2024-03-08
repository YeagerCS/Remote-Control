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
The end user runs the c# application, which opens a local WebSocket on the local IPV4 of the device. The c# application then sends a POST request to the python API server with the local ip address as the payload. The python server then immediately emits the local ip, via the socket connection, to the react client. In the clients startpage appear devices that are connected to the application aka every ip that was emitted from the python server. When a user closes the c# application, the ip gets removed. On the client upon selecting a device, the remote user can execute commands on the end users system. The client has a real-time connection with the Websocket of the application and can send commands. After a command is sent, the c# application either executes the command as a cmd command or as a custom command and sends the output back to the client, which then displays it.

## How to run
### C# Application
Go to `/RemoteControl` to find the files or just open the `RemoteControl.sln` solution. In Visual Studio 2022, press CTRL + B to build the app, then run it with CTRL + F5. You'll need to change the HOST variable in `/RemoteControl/Services/WebSocketService.cs:20`, to the host of your API server.

### React Client
Navigate to `/external/client/`, to find the client application. In the terminal, enter: 
```
npm i
npm run dev
```
Here, you'll also need to change the host in `/external/client/src/components/IPReceiver.jsx:11`, to your API server.

### Python API Server
Navigate to `/external/server`, to find `api.py`. In the terminal, enter:
```
pip install flask
pip install flask_socketio
pip install flask_cors
```
Run with
```
python api.py
```
