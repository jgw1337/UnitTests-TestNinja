﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public class VideoServiceDIConstructorInjection
    {
        private IFileReader _fileReader;

        public VideoServiceDIConstructorInjection(IFileReader fileReader = null)
        {
            _fileReader = fileReader ?? new FileReader();
        }

        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            using (var context = new VideoContext())
            {
                var videos =
                    (from video in context.Videos
                     where !video.IsProcessed
                     select video).ToList();

                foreach (var v in videos)
                    videoIds.Add(v.Id);

                return String.Join(",", videoIds);
            }
        }
    }
}