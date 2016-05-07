using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace TagHelpers
{
    public class MarkdownTagHelper : TagHelper
    {
        //Attribute for our custom markdown
        public string Url { get; set; }

        private string parse_content = string.Empty;

        private bool isValidURL(string URL)
        {
            Uri uriResult;
            return Uri.TryCreate(URL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme.ToLowerInvariant() == "http" || uriResult.Scheme.ToLowerInvariant() == "https");
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context.AllAttributes["url"] != null)
            {
                string url = context.AllAttributes["url"].Value.ToString();
                string webContent = string.Empty;
                if (url.Trim().Length > 0)
                {
                    if (isValidURL(url))
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            webContent = await client.GetStringAsync(new Uri(url));
                            parse_content = CommonMark.CommonMarkConverter.Convert(webContent);
                            output.Content.SetHtmlContent(parse_content);
                        }
                    }
                }
            }
            else
            {
                //Gets the content inside the markdown element
                var content = await output.GetChildContentAsync();

                //Read the content as a string and parse it.
                parse_content = CommonMark.CommonMarkConverter.Convert(content.GetContent());

                //Render the parsed markdown inside the tags.
                output.Content.SetHtmlContent(parse_content);
            }
        }
    }
}