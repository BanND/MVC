using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Pages
{
    /// <summary>
    /// Used for the pages mainly consisting of manually created content such as text, images, and blocks
    /// </summary>
    [SiteContentType(GUID = "9CCC8A41-5C8C-4BE0-8E73-520FF3DE8268")]
    [SiteImageUrl(Global.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
    public class StandardPageCopy : SitePageData
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        [CultureSpecific]
        public virtual XhtmlString MainBody { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        public virtual ContentArea MainContentArea { get; set; }

        public string TestProp1 { get; set; }

        public int TestProp2 { get; set; }

        [CultureSpecific]
        public virtual string TestProp1Description
        {
            get
            {
                var propertyValue = this["TestProp1"] as string;

                return string.IsNullOrWhiteSpace(propertyValue) ? (string)this["TestProp2"] : propertyValue;
            }
            set => this["TestProp1"] = value;
        }
    }
}
