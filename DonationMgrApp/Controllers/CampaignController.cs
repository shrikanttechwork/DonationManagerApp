using DonationMgrApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using PagedList;

namespace DonationMgrApp.Controllers
{
    public class CampaignController : Controller
    {
        DonationDB dbcon = new DonationDB();

        // GET: Campaign
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingId = string.IsNullOrEmpty(Sorting_Order) ? "Id" : "";
            ViewBag.SortingName = string.IsNullOrEmpty(Sorting_Order) ? "Name" : "";
            ViewBag.SortingDate = string.IsNullOrEmpty(Sorting_Order) ? "Date" : "";
            ViewBag.SortingOrgName = string.IsNullOrEmpty(Sorting_Order) ? "OrgName" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.Filter_Value = Search_Data;

            var camps = from camp in dbcon.Campaigns.Include(a => a.Organisation) select camp;

            if (!string.IsNullOrEmpty(Search_Data))
            {
                camps = camps.Where(camp => camp.CampaignName.ToUpper().Contains(Search_Data.ToUpper() ?? string.Empty));
            }

            switch (Sorting_Order)
            {
                case "Id":
                    camps = camps.OrderByDescending(camp => camp.Campid);
                    break;
                case "Name":
                    camps = camps.OrderByDescending(camp => camp.CampaignName);
                    break;
                case "Date":
                    camps = camps.OrderByDescending(camp => camp.StartDate);
                    break;
                case "OrgName":
                    camps = camps.OrderBy(camp => camp.Organisation.Name);
                    break;
                default:
                    camps = camps.OrderBy(camp => camp.Campid);
                    break;
            }

            int Size_Of_Page = 5;
            int No_Of_Page = (Page_No ?? 1);
            return View(camps.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        // GET - Create method
        public ActionResult Create()
        {
            ViewBag.OrgId = new SelectList(dbcon.Organisations, "id", "Name");
            return View();
        }

        // POST - Cerate method
        [HttpPost]
        public ActionResult Create(Campaign camp)
        {
            if (ModelState.IsValid)
            {
                dbcon.Campaigns.Add(camp);
                dbcon.SaveChanges();
                return RedirectToAction("Index");
            }

            var campList = dbcon.Campaigns.Include(a => a.Organisation);

            return View(campList.OrderByDescending(p => p.Campid).ToList());
        }

        // GET - Edit method
        public ActionResult Edit(int id)
        {
            Campaign camp = dbcon.Campaigns.Find(id);
            ViewBag.OrgId = new SelectList(dbcon.Organisations, "id", "Name", camp.OrgId); 
            return View(camp);
        }

        // POST - Edit method
        [HttpPost]
        public ActionResult Edit(Campaign camp)
        {
            if (ModelState.IsValid)
            {
                dbcon.Entry(camp).State = EntityState.Modified;
                dbcon.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrgId = new SelectList(dbcon.Organisations, "id", "Name", camp.OrgId);
            return View(camp);
        }

        // GET - Details method
        public ActionResult Details(int id)
        {
            Campaign camp = dbcon.Campaigns.Find(id);
            return View(camp);
        }

        // POST - Delete method
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Campaign camp = dbcon.Campaigns.Find(id);
            dbcon.Campaigns.Remove(camp);
            dbcon.SaveChanges();

            return Json("Success");
        }

    }
}