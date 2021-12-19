const app = require('express')();
const http = require('http').Server(app);
const io = require("socket.io")(http, {
  cors: {
    origin: "*",
    methods: ["GET", "POST"]
  }
});
const port = process.env.PORT || 3000;

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});

app.get('/port', (req, res) => {
  res.send(port+"");
});

app.get('/org', (req, res) => {
  
  var origin = req.get('origin');
  res.send(origin+"");
});

app.get('/req', (req, res) => {
  
  
  res.send(JSON.stringify(req));
});

io.on('connection', (socket) => {
  socket.on('chat message', msg => {
    io.emit('chat message', msg);
  });
});

http.listen(port, () => {
  console.log(`Socket.IO server running at http://localhost:${port}/`);
});