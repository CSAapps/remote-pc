<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA==" crossorigin="anonymous" referrerpolicy="no-referrer" />


# Slide Remote (remote-pc)

A useful tool to control slides when doing a group presentation by looking at somebody else’s screen via 
online conferencing platforms. This tool will instantly allow you to go to the next and previous slides by 
sending keyboard inputs to the PC which is sharing the screen. `socket.io` was used to handle real-time 
communication.

## Tech Stack

+ Backend - <i class="fab fa-node-js"></i> NodeJS 

+ Frontend - <i class="fab fa-js"></i> JavaScript &nbsp;&nbsp;  <i class="fab fa-windows"></i> C#

+ Frameworks - <i class="fas fa-plug"></i> socket.io

## Architecture


+ Client - Tx

    - Runs on the controller's device

    - Sending controlling signals to the server
       
    - Cross Platform
        - Web version - <i class="fas fa-external-link-alt"></i> [Live Demo](https://pcremote.line.pm/)       

        - Windows Version

+ Client - Rx

    - Runs on the device of the screen-sharing participant.

    - Receiving controlling signals from the server and simulating user inputs

+ Server
    - Generating new `room IDs` when new `Client - Rx` connects

    - Manipulating  `sockets` by assigning them to rooms based on `room ID`



