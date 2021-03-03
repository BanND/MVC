using System;
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

        public virtual string TestProp1 { get; set; }

        public virtual int TestProp2 { get; set; }

        [CultureSpecific]
        public virtual string TestProp1Description
        {
            get
            {
                var propertyValue = this["TestProp1"] as string;

                return string.IsNullOrWhiteSpace(propertyValue) ? (string)this["TestProp2"] : propertyValue;
            }
            set => this["TestProp1"] = value;
            //set { this.SetPropertyValue(p => p.TestProp1, value); }
        }

        [CultureSpecific]
        public virtual string TestProp2Description
        {
            get
            {
                var propertyValue = this["TestProp2"] as string;

                return string.IsNullOrWhiteSpace(propertyValue) ? (string)this["TestProp2"] : propertyValue;
            }
            set => this["TestProp1"] = value;
        }

        public virtual string GetPropertyValueTest
        {
            get
            {
                // 1. Get as string, defaults to null and otherwise calls ToString for the value
                string mainBody = this.GetPropertyValue("MainBody");

                // 2. Specify a fallback value
                string mainBodyFallback = this.GetPropertyValue("MainBody", string.Empty);

                // 3. Which is equivalent to
                string mainBodyFallback2 = this.GetPropertyValue("MainBody") ?? string.Empty;

                // 4. So a common usage is probably
                string pageHeading = this.GetPropertyValue("PageHeading", this.PageName);

                // 5. Get typed value, defaults to null if property is not set or defined
                XhtmlString xhtml = this.GetPropertyValue<XhtmlString>("MainBody");

                // 6. Which supports fallback in the same way as the "ToString" overload
                XhtmlString xhtmlWithFallback =
                    this.GetPropertyValue<XhtmlString>("MainBody", new XhtmlString());

                // 7. Advanced: Specify a conversion for the value
                // Note: this example is very similar to 1. since we just call ToString, EXCEPT
                // if MainBody is something else than an Xhtml property in which case 1. would still
                // return the ToString representation while this would throw an exception since
                // it tries to cast the value to an XhtmlString before handing it over to the
                // conversion lambda
                string convertedXhtml =
                    this.GetPropertyValue<XhtmlString, string>("MainBody", x => x.ToString());

                // 8. This is equivalent to 1. since it treats the property value as object
                string mainBody2 =
                    this.GetPropertyValue<object, string>("MainBody", x => x.ToString());

                return string.Empty;
            }

            set { this.SetPropertyValue(p => p.GetPropertyValueTest, value); }
        }
    }
}
