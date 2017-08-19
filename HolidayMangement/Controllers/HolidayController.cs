using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolidayMangement.Controllers
{
    public class HolidayController : Controller
    {
        // GET: Holiday
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetHolidays()
        {
            using (var dc = new EmployeesDbEntities1())
            {
                var holidays = dc.Holidays.OrderBy(h => h.HolidayDate).ToList();

                var dtoHoliday = new List<HolidayMetadata>();

                foreach (var item in holidays)
                {
                    dtoHoliday.Add(
                        new HolidayMetadata
                        {
                            Id = item.Id,
                            HolidayType = item.HolidayType,
                            HolidayName = item.HolidayName,
                            HolidayDate = item.HolidayDate.ToShortDateString()
                        }
                        );
                }

                return Json(new { data = dtoHoliday }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Save(long id)
        {
            using (var dc = new EmployeesDbEntities1())
            {
                var holiday = dc.Holidays.Where(h => h.Id == id).FirstOrDefault();
                return View(holiday);
            }
        }
        [HttpPost]
        public ActionResult Save(Holiday holiday)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var dc = new EmployeesDbEntities1())
                {
                    if (holiday.Id > 0)
                    {
                        //Edit
                        var fetchedHoliday = dc.Holidays.Where(h => h.Id == holiday.Id).FirstOrDefault();
                        if (fetchedHoliday != null)
                        {
                            fetchedHoliday.HolidayType = holiday.HolidayType;
                            fetchedHoliday.HolidayName = holiday.HolidayName;
                            fetchedHoliday.HolidayDate = holiday.HolidayDate;
                        }
                    }
                    else
                    {
                        //Save
                        dc.Holidays.Add(holiday);
                    }
                    dc.SaveChanges();
                    status = true;

                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            using (var dc = new EmployeesDbEntities1())
            {
                var holiday = dc.Holidays.Where(h => h.Id == id).FirstOrDefault();
                if (holiday != null)
                {
                    return View(holiday);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteHoliday(long id)
        {
            bool status = false;
            using (var dc = new EmployeesDbEntities1())
            {
                var holiday = dc.Holidays.Where(h => h.Id == id).FirstOrDefault();
                if (holiday != null)
                {
                    dc.Holidays.Remove(holiday);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}