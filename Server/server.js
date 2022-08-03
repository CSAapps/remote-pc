const app = require('express')();
const http = require('http').Server(app);
const io = require("socket.io")(http);
const port = process.env.PORT || 3000;

app.get('/', (req, res) => {
  res.send('normal version is deactivated. use quick version');
});

app.get('/q', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});

app.get('/qd', (req, res) => {
  return res.redirect("https://github.com/CSAapps/remote-pc/releases/download/q/RemotePC.zip");
});

io.on('connection', (socket) => {
  console.log(Date.now());
  socket.on('key', data => {
    console.log(data);
    socket.broadcast.emit('key', data);
  });
});

http.listen(port, () => {
  console.log(`Socket.IO server running at http://localhost:${port}/`);
});