using DonationMgrApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using PagedList;

namespace DonationMgrApp.Controllers
{
    public class OrganisationController : Controller
    {
        DonationDB dbcon = new DonationDB();

        // GET: Organisation
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingId = string.IsNullOrEmpty(Sorting_Order) ? "Id" : "";
            ViewBag.SortingName = string.IsNullOrEmpty(Sorting_Order) ? "Name" : "";

            if(Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value; 
            }

            ViewBag.Filter_Value = Search_Data;

            //var orgs = dbcon.Organisations.ToList();
            var orgs = from org in dbcon.Organisations select org;

            if (!string.IsNullOrEmpty(Search_Data))
            {
               orgs = orgs.Where(org => org.Name.ToUpper().Contains(Search_Data.ToUpper() ?? string.Empty));          
            }     

            switch (Sorting_Order)
            {
                case "Id":
                    orgs = orgs.OrderByDescending(org => org.id);
                    break;
                case "Name":
                    orgs = orgs.OrderByDescending(org => org.Name);
                    break;
                default:
                    orgs = orgs.OrderBy(org => org.id);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(orgs.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Organisation org)
        {
            if (ModelState.IsValid)
            {
                dbcon.Organisations.Add(org);
                dbcon.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Organisation ogr = dbcon.Organisations.Find(id);
            return View(ogr);
        }

        [HttpPost]
        public ActionResult Edit(Organisation ogr)
        {
            if (ModelState.IsValid)
            {
                dbcon.Entry(ogr).State = EntityState.Modified;
                dbcon.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ogr);
        }

        public ActionResult Details(int id)
        {
            Organisation ogr = dbcon.Organisations.Find(id);
            return View(ogr);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Organisation ogr = dbcon.Organisations.Find(id);
            dbcon.Organisations.Remove(ogr);
            dbcon.SaveChanges();

            return Json("Success");
        }
    }
}