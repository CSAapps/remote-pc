# Slide Remote (remote-pc)

A useful tool to control slides when doing a group presentation by looking at somebody else’s screen via 
online conferencing platforms. This tool will instantly allow you to go to the next and previous slides by 
sending keyboard inputs to the PC which is sharing the screen. `socket.io` was used to handle real-time 
communication.

## Tech Stack

+ Backend -  NodeJS 

+ Frontend -  JavaScript , C#

+ Frameworks - socket.io

<br>

<img src="https://user-images.githubusercontent.com/25181517/183568594-85e280a7-0d7e-4d1a-9028-c8c2209e073c.png" alt="icon" width="60" height="60"> &nbsp;
<img src="https://user-images.githubusercontent.com/25181517/117447155-6a868a00-af3d-11eb-9cfe-245df15c9f3f.png" alt="icon" width="60" height="60">&nbsp;
<img src="https://user-images.githubusercontent.com/25181517/121405384-444d7300-c95d-11eb-959f-913020d3bf90.png" alt="icon" width="60" height="60">&nbsp;
<img src="https://socket.io/images/logo-dark.svg" alt="icon" width="60" height="60"> 

## Architecture


+ Client - Tx

    - Runs on the controller's device

    - Sending controlling signals to the server
       
    - Cross Platform
        - Web version - 🌐🔗 [Live Demo](https://pcremote.line.pm/)       

        - Windows Version

+ Client - Rx

    - Runs on the device of the screen-sharing participant.

    - Receiving controlling signals from the server and simulating user inputs

+ Server
    - Generating new `room IDs` when new `Client - Rx` connects

    - Manipulating  `sockets` by assigning them to rooms based on `room ID`
