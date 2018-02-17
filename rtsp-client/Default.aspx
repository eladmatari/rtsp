<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="rtsp_client.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/jsmpg.js"></script>
    <script src="scripts/jsmpg-live.js"></script>
</head>
<body>
    <style>
        <% if (Width != null) { %> canvas { width: <%= Width %>; } <% } %>
        <% if (Height != null) { %> canvas { height: <%= Height %>; } <% } %>                                                              
    </style>
 <div id="vid"></div>
    <script>
        var jsMpgLive = new JsMpgLive('#vid', 'ws://localhost:<%= Port %>');
        jsMpgLive.start();
    </script>
</body>
</html>
