<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="rtsp_client.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="scripts/WebSocketClient.js"></script>
    <script src="scripts/jsmpg.js"></script>
</head>
<body>
    <canvas id="vid"></canvas>
    <script>
        var vidElm = document.getElementById('vid');
        function loadVideo() {
            
            var wsc = new WebSocketClient();

            wsc.onmessage = function (data, flags, number) {
                //console.log(`WebSocketClient message #${number}: `, data);
            }

            wsc.onopen = function (ws) {
                console.log("WebSocketClient connected:", ws);
                //this.send("Hello World !");
            }
            wsc.onloaded = function (ws) {
                console.log("WebSocketClient loaded:", ws);
                player = new jsmpeg(ws, {
                    canvas: vidElm // Canvas should be a canvas DOM element
                });
            }
            wsc.open('ws://localhost:9999');

            
        }

        window.addEventListener('load', function () {
            loadVideo();
        })
        

    </script>
</body>
</html>
