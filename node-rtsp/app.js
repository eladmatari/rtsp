Stream = require('node-rtsp-stream');

stream = new Stream({
    name: 'name',
    streamUrl: process.argv[2], //'rtsp://184.72.239.149/vod/mp4:BigBuckBunny_115k.mov',
    wsPort: process.argv[3] //9999
});

