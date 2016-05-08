using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Text;

namespace TagHelpers
{
    [HtmlTargetElement("youtube", Attributes = "video-id")]
    public class YouTubeTagHelper : TagHelper
    {
        private const string YOUTUBE_EMBED_URL = "http://www.youtube.com/embed/";

        [HtmlAttributeName("video-id")]
        public string videoid { get; set; }
        public int width { get; set; } = 560;
        public int height { get; set; } = 315;
        [HtmlAttributeName("allow-fullscreen")]
        public bool allowfullscreen { get; set; }
        [HtmlAttributeName("auto-play")]
        public bool autoplay { get; set; }
        [HtmlAttributeName("disable-related-videos")]
        public bool disablerelatedvideos { get; set; }
        [HtmlAttributeName("show-info")]
        public bool showinfo { get; set; }
        [HtmlAttributeName("show-controls")]
        public bool showcontrols { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            videoid = context.AllAttributes["video-id"] != null ? context.AllAttributes["video-id"].Value.ToString() : videoid;
            width = context.AllAttributes["width"] != null ? Convert.ToInt32(context.AllAttributes["width"].Value) : width;
            height = context.AllAttributes["height"] != null ? Convert.ToInt32(context.AllAttributes["height"].Value) : height;
            allowfullscreen = context.AllAttributes["allow-fullscreen"] != null ? allowfullscreen : false;
            autoplay = context.AllAttributes["auto-play"] != null ? autoplay : false;
            disablerelatedvideos = context.AllAttributes["disable-related-videos"] != null ? disablerelatedvideos : false;
            showinfo = context.AllAttributes["show-info"] != null ? showinfo : true;
            showcontrols = context.AllAttributes["show-controls"] != null ? showcontrols : true;

            StringBuilder embedString = new StringBuilder("<iframe width=\"" + width + "\" height=\"" + height + "\" src=\"" + YOUTUBE_EMBED_URL + videoid);

            embedString.Append("/?");
            embedString.Append("autoplay=" + (autoplay ? "1" : "0"));
            embedString.Append("&controls=" + (showcontrols ? "1" : "0"));
            embedString.Append("&showinfo=" + (showinfo ? "1" : "0"));

            embedString.Append("\"");

            if (allowfullscreen)
            {
                embedString.Append(" allowfullscreen");
            }

            embedString.Append(" frameborder=\"0\"></iframe>");

            output.Content.SetHtmlContent(embedString.ToString());
        }
    }
}
