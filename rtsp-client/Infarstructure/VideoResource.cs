using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace rtsp_client.Infarstructure
{
    public class VideoResource
    {
        public string Url { get; private set; }

        public ushort Port { get; private set; }

        public bool IsActive { get; private set; }

        private readonly object _lockObj = new object();

        public VideoResource(string url, ushort port)
        {
            Url = url;
            Port = port;
        }

        public void Start()
        {
            if (IsActive)
                return;

            lock (_lockObj)
            {
                if (IsActive)
                    return;

                try
                {
                    IsActive = true;
                    var processInfo = new ProcessStartInfo();
                    processInfo.FileName = @"C:\Program Files\nodejs\node.exe";
                    processInfo.Arguments = $"\"{ConfigurationManager.AppSettings["NodeRtspPath"]}\" \"{Url}\" {Port}";
                    processInfo.CreateNoWindow = true;
                    processInfo.RedirectStandardError = true;
                    processInfo.RedirectStandardOutput = true;
                    processInfo.UseShellExecute = false;

                    Task.Run(() =>
                    {
                        try
                        {
                            using (var process = Process.Start(processInfo))
                            {
                                process.EnableRaisingEvents = true;
                                process.OutputDataReceived += (sender, e) =>
                                {

                                };
                                process.ErrorDataReceived += (sender, e) =>
                                {

                                };
                                process.BeginOutputReadLine();
                                process.Exited += (sender, e) =>
                                {
                                    IsActive = false;
                                };

                                process.WaitForExit();
                            }
                        }
                        catch (Exception ex)
                        {
                            IsActive = false;
                        }
                    });

                }
                catch (Exception ex)
                {
                    IsActive = false;
                }
            }
        }
    }
}