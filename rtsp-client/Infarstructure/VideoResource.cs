using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace rtsp_client.Infarstructure
{
    public class VideoResource : IDisposable
    {
        public string Url { get; private set; }

        public ushort Port { get; private set; }

        public bool IsActive { get; private set; }

        private readonly object _lockObj = new object();

        private Process _process;

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
                    processInfo.FileName = $@"{ConfigurationManager.AppSettings["NodeRtspPath"]}";
                    processInfo.Arguments = $"\"{Url}\" {Port}";
                    //processInfo.CreateNoWindow = true;
                    //processInfo.RedirectStandardError = true;
                    //processInfo.RedirectStandardOutput = true;
                    //processInfo.UseShellExecute = false;

                    Task.Run(() =>
                    {
                        try
                        {
                            using (_process = Process.Start(processInfo))
                            {
                                //_process.EnableRaisingEvents = true;
                                //_process.OutputDataReceived += (sender, e) =>
                                //{

                                //};
                                //_process.ErrorDataReceived += (sender, e) =>
                                //{

                                //};
                                //_process.BeginOutputReadLine();
                                _process.WaitForExit();
                                IsActive = false;
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

        public void Stop()
        {
            try
            {
                _process.Dispose();
            }
            catch(Exception ex)
            {
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}