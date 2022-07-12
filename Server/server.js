const app = require('express')();
const http = require('http').Server(app);
const io = require("socket.io")(http);
const port = process.env.PORT || 3000;

var roomIds = new Set();

function getRoomId() {
  var lastRoomId = 1;
  var i = 0;
  do {
    lastRoomId = Math.floor(1000 + Math.random() * (9000 + i));
    i += 10;
  } while (roomIds.has(lastRoomId));
  roomIds.add(lastRoomId);
  return lastRoomId + '';
}

function rmvRoomId(roomId) {
  roomIds.delete(roomId - 0);
}

app.get('/api', (req, res) => {
  res.send('Hi from API');
});

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});

io.on('connection', socket => {

  var query = socket.handshake.query;
  console.log(query.roomId);
  if (query.Rx) {
    if (!query.roomId) {
      query.roomId = getRoomId();
      socket.emit('roomId', query.roomId);

    }
    socket.join(query.roomId);
    socket.on('exit', data => {
      rmvRoomId(data);
      console.log({ data });
    });
  } else {
    socket.on('key', data => {
      socket.to(query.roomId).emit('key', data);
      console.log(query.roomId);
    });
  }

});

http.listen(port, () => {
  console.log(`Socket.IO server running at http://localhost:${port}/`);
});