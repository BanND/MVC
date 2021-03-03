using EPiServer.Core;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using MVC.Models.Pages;
using MVC.Models.ViewModels;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class StandardPageCopyController : PageControllerBase<StandardPageCopy>
    {
        public ActionResult Index(StandardPageCopy currentPage)
        {
            var model = PageViewModel.Create(currentPage);

            //Using a property multiple times
            PropertyData property = currentPage.Property["TestProp1Description"];
            if (property != null && !property.IsNull)
            {
                var editHints = ViewData.GetEditHints<PageViewModel<StandardPageCopy>, StandardPageCopy>();
                editHints.AddConnection(m => m.CurrentPage.TestProp1, p => p.TestProp1);
                editHints.AddConnection<LinkItemCollection>(m => m.Layout.CustomerZonePages, p => new LinkItemCollection());

            }

            var propertyValueAsString = currentPage["TestProp1Description"] as string;
            if (!string.IsNullOrEmpty(propertyValueAsString))
            {
                //DoSomething();
            }

            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink)) // Check if it is the StartPage or just a page of the StartPage type.
            {
                //Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<PageViewModel<StandardPageCopy>, StandardPageCopy>();
                editHints.AddConnection(m => m.CurrentPage.TestProp1, p => p.TestProp1);
                editHints.AddConnection<LinkItemCollection>(m => m.Layout.CustomerZonePages, p => new LinkItemCollection());
            }

            return View(model);
        }

    }
}
