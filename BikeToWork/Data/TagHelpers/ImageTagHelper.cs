using BikeToWork.Data.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Policy;
using System.Text;

namespace BikeToWork.Data.TagHelpers
{
    [HtmlTargetElement("bike-img")]
    public class ImageTagHelper : TagHelper
    {
        public BikeClassEnum ImageBike { get; set; }
        public string ImgUrl { get; set; }

        public string CreateUrl(string _imageBike)
        {
            ImgUrl = $"/images/{_imageBike}.jpg";
            return ImgUrl;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Add("style", "max-width: 70px");
            output.Attributes.Add("style", "max-height: 70px");
            output.Attributes.Add("src", CreateUrl(ImageBike.ToString()));
        }
    }
}
