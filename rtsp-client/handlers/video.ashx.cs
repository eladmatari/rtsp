using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace rtsp_client.handlers
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class video : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //// ffmpeg -rtsp_transport tcp -i rtsp://184.72.239.149/vod/mp4:BigBuckBunny_115k.mov -f hls -b:v 800k -r 30 -

            //var processInfo = new ProcessStartInfo();
            //processInfo.FileName = @"D:\Projects\node-rtsp\node-rtsp\ffmpeg.exe";
            //processInfo.Arguments = "-rtsp_transport tcp -i rtsp://184.72.239.149/vod/mp4:BigBuckBunny_115k.mov -f hls -b:v 800k -r 30 -";
            //processInfo.CreateNoWindow = true;
            //processInfo.RedirectStandardError = true;
            //processInfo.RedirectStandardOutput = true;
            //processInfo.UseShellExecute = false;


            //using (var process = Process.Start(processInfo))
            //{
            //    process.EnableRaisingEvents = true;
            //    process.OutputDataReceived += (sender, e) =>
            //    {
            //        if (!context.Response.IsClientConnected)
            //        {
            //            process.Kill();
            //        }

            //        string str = e.Data;
            //        byte[] bytes = new byte[str.Length * sizeof(char)];
            //        Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            //        context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            //        context.Response.Flush();
            //    };
            //    process.ErrorDataReceived += (sender, e) =>
            //    {

            //    };
            //    process.BeginOutputReadLine();

            //    process.WaitForExit();
            //}
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}