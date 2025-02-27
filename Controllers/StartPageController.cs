using System;
using System.Web.Mvc;
using EPiServer.Core;
using MVC.Models.Pages;
using MVC.Models.ViewModels;
using EPiServer.Web;
using EPiServer.Web.Mvc;

namespace MVC.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            var model = PageViewModel.Create(currentPage);

            //Using a property multiple times
            PropertyData property = currentPage.Property["TestProp1Description"];
            if (property != null && !property.IsNull)
            {
                var editHints = ViewData.GetEditHints<PageViewModel<StartPage>, StartPage>();
            }

            var propertyValueAsString = currentPage["TestProp1Description"] as string;
            if (!string.IsNullOrEmpty(propertyValueAsString))
            {
                //DoSomething();
            }

            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink)) // Check if it is the StartPage or just a page of the StartPage type.
            {
                //Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<PageViewModel<StartPage>, StartPage>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.ProductPages, p => p.ProductPageLinks);
                editHints.AddConnection(m => m.Layout.CompanyInformationPages, p => p.CompanyInformationPageLinks);
                editHints.AddConnection(m => m.Layout.NewsPages, p => p.NewsPageLinks);
                editHints.AddConnection(m => m.Layout.CustomerZonePages, p => p.CustomerZonePageLinks);
            }

            return View(model);
        }

    }
}
