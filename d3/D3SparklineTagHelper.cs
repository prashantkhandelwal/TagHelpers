using Microsoft.AspNet.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelpers
{
    [HtmlTargetElement("d3-sparkline")]
    public class D3SparklineTagHelper : TagHelper
    {
        [HtmlAttributeName("stroke-color")]
        public string stroke { get; set; } = "steelblue";
        [HtmlAttributeName("stroke-width")]
        public string strokewidth { get; set; } = "1";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            TagHelperContent data = await output.GetChildContentAsync();

            if (!data.IsEmpty)
            {
                stroke = context.AllAttributes["stroke-color"] != null ? context.AllAttributes["stroke-color"].Value.ToString() : stroke;
                strokewidth = context.AllAttributes["stroke-width"] != null ? context.AllAttributes["stroke-width"].Value.ToString() : strokewidth;

                output.PreContent.SetHtmlContent("<style>path{stroke:" + stroke + ";stroke-width:" + strokewidth + "; fill:none;}</style>");
                output.Content.SetHtmlContent("<span id=\"spark\"></span>");
                output.Content.AppendHtml("<script>var spark = d3.select(\"#spark\").append(\"svg:svg\").attr(\"width\", \"100%\").attr(\"height\", \"100%\");" +
                "var data = [" + data.GetContent() + "]; " +
                "var x = d3.scale.linear().domain([0, 10]).range([0, 50]);" +
                "var y = d3.scale.linear().domain([0, 10]).range([0, 30]);" +
                "var line = d3.svg.line()" +
                "    .x(function(d, i) {" +
                "    return x(i);" +
                "})" +
                ".y(function(d) {" +
                "    return y(d);" +
                "});" +
                "spark.append(\"svg:path\").attr(\"d\", line(data));</script>");
            }
        }
    }
}
