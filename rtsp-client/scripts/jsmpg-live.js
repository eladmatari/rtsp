function WebSocketClient() {
    this.number = 0;	// Message number
    this.autoReconnectInterval = 5 * 1000;	// ms
}
WebSocketClient.prototype.open = function (url) {
    var self = this;

    self.url = url;
    self.instance = new WebSocket(self.url);

    self.instance.addEventListener('open', function () {
        self.onopen();
    });
    self.instance.addEventListener('message', function (data, flags) {
        self.number++;
        self.onmessage(data, flags, self.number);
    });
    self.instance.addEventListener('close', function (e) {
        switch (e) {
            case 1000:	// CLOSE_NORMAL
                console.log("WebSocket: closed");
                break;
            default:	// Abnormal closure
                self.reconnect(e);
                break;
        }
        self.onclose(e);
    });
    self.instance.addEventListener('error', function (e) {
        switch (e.code) {
            case 'ECONNREFUSED':
                self.reconnect(e);
                break;
            default:
                self.onerror(e);
                break;
        }
    });

    if (self.onloaded)
        self.onloaded(self.instance);
}
WebSocketClient.prototype.send = function (data, option) {
    try {
        this.instance.send(data, option);
    } catch (e) {
        this.instance.emit('error', e);
    }
}
WebSocketClient.prototype.reconnect = function (e) {
    console.log('WebSocketClient: retry in ' + this.autoReconnectInterval + 'ms', e);

    var self = this;
    try {
        self.instance.removeEventListener('open');
        self.instance.removeEventListener('message');
        self.instance.removeEventListener('close');
        self.instance.removeEventListener('error');
    }
    catch (e) { }

    setTimeout(function () {
        //console.log("WebSocketClient: reconnecting...");
        //self.open(self.url);
        location.reload(true);
    }, this.autoReconnectInterval);
}
WebSocketClient.prototype.onopen = function (e) { console.log("WebSocketClient: open", arguments); }
WebSocketClient.prototype.onmessage = function (data, flags, number) { console.log("WebSocketClient: message", arguments); }
WebSocketClient.prototype.onerror = function (e) { console.log("WebSocketClient: error", arguments); }
WebSocketClient.prototype.onclose = function (e) { console.log("WebSocketClient: closed", arguments); }

function JsMpgLive(selector, url) {
    var self = this;
    var canvasElm = null;

    var setCanvas = function () {

        if (canvasElm) {
            canvasElm.remove();
        }

        $(selector).append('<canvas></canvas>');
        canvasElm = $(selector).find('canvas');
    }

    this.start = function () {
        var wsc = new WebSocketClient();

        wsc.onmessage = function (data, flags, number) {
            //console.log(`WebSocketClient message #${number}: `, data);
        }

        wsc.onopen = function (ws) {
            console.log("WebSocketClient connected:", ws);
            isConnected = true; 
        }

        wsc.onloaded = function (ws) {
            console.log("WebSocketClient loaded:", ws);
            setCanvas();
            var player = new jsmpeg(ws, {
                canvas: canvasElm[0] // Canvas should be a canvas DOM element
            });

            setTimeout(function () {
                if (!$('canvas').width())
                    location.reload(true);
            }, 5000);
        }

        wsc.open(url);
    }
}