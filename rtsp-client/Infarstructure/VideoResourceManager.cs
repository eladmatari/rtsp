using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsp_client.Infarstructure
{
    public class VideoResourceManager
    {
        private static List<VideoResource> _resources = new List<VideoResource>();
        private static ushort _port = 60000;

        public static void AddResource(string url)
        {
            var resource = new VideoResource(url, _port++);
            _resources.Add(resource);
        }

        public static ushort ActivateVideo(byte index)
        {
            _resources[index].Start();
            return _resources[index].Port;
        }

        internal static void StopAll()
        {
            foreach (var resource in _resources)
            {
                resource.Stop();
            }
        }
    }
}