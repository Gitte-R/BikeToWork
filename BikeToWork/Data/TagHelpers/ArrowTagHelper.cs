using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BikeToWork.Data.TagHelpers
{
    [HtmlTargetElement("arrow-tag")]
    public class ArrowTagHelper : TagHelper
    {
        public string arrow { get; init; } = " ▼";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.Append(arrow);
        }
    }
}
