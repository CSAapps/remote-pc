const app = require('express')();
const http = require('http').Server(app);
const io = require("socket.io")(http);
const port = process.env.PORT || 3000;

var roomIds = new Set();

function getRoomId() {
  var lastRoomId = 0;
  while (roomIds.has(lastRoomId)) lastRoomId++;
  roomIds.add(lastRoomId);
  return lastRoomId;
}
app.get('/api', (req, res) => {
  res.send('Hi from API');
});

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});

io.on('connection', socket => {

  var query = socket.handshake.query;
  if (query.Rx && !query.roomId) {
    var roomId = getRoomId();
    socket.join(roomId);
    socket.Rx = 1;
    socket.emit('roomId', roomId + '');
    console.log({ roomId });
    console.log({ roomIds });
  }


  socket.on('key', data => {
    socket.broadcast.emit('key', data);
  });

  socket.on('exit', data => {
    if (socket.Rx) roomIds.delete(data - 0);
    console.log({ data });
  });

});

http.listen(port, () => {
  console.log(`Socket.IO server running at http://localhost:${port}/`);
});