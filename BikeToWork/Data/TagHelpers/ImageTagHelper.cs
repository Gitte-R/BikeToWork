using BikeToWork.Data.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Policy;
using System.Text;

namespace BikeToWork.Data.TagHelpers
{
    [HtmlTargetElement("bike-img")]
    public class ImageTagHelper : TagHelper
    {
        public BikeClassEnum imageBike { get; set; }
        public string imgUrl { get; set; }

        public string CreateUrl(string imageBike)
        {
            imgUrl = $"/images/{imageBike}.jpg";
            return imgUrl;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Add("style", "max-width: 70px");
            output.Attributes.Add("style", "max-height: 70px");
            output.Attributes.Add("src", CreateUrl(imageBike.ToString()));
        }
    }
}
