const app = require('express')();
const http = require('http').Server(app);
const io = require("socket.io")(http);
const port = process.env.PORT || 3000;

app.get('/api', (req, res) => {
  res.send('Hi from API');
});

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});

io.on('connection', (socket) => {
  socket.on('key', data => {
    socket.broadcast.emit('key', data);
  });
});

http.listen(port, () => {
  console.log(process.env)

  console.log(`Socket.IO server running at http://localhost:${port}/`);
});