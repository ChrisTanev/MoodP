﻿using System.Collections.Generic;

namespace MP.Data.Implementation
{
    public class Lyrics
    {
        public int lyrics_id { get; set; }
        public int can_edit { get; set; }
        public int locked { get; set; }
        public string action_requested { get; set; }
        public int verified { get; set; }
        public int restricted { get; set; }
        public int instrumental { get; set; }
        public int @explicit { get; set; }
        public string lyrics_body { get; set; }
        public string lyrics_language { get; set; }
        public string lyrics_language_description { get; set; }
        public string script_tracking_url { get; set; }
        public string pixel_tracking_url { get; set; }
        public string html_tracking_url { get; set; }
        public string lyrics_copyright { get; set; }
        public List<object> writer_list { get; set; }
        public List<object> publisher_list { get; set; }
        public string updated_time { get; set; }
    }

}
