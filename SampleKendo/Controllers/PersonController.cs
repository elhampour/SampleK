using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SampleKendo.Models;

namespace SampleKendo.Controllers
{
    public class PersonViewModel
    {
        public int Id { get; set; }   
         
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get([DataSourceRequest] DataSourceRequest request)
        {
            var database = new ApplicationDbContext();
            var result = database.Persons;
            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, PersonViewModel person)
        {
            if (person != null && ModelState.IsValid)
            {
                var personnew = new Person()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName
                };
                var db = new ApplicationDbContext();
                db.Persons.Add(personnew);
                db.SaveChanges();
            }

            return Json(new[] { person }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, PersonViewModel person)
        {
            if (person != null && ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                var toeditpeson = db.Persons.Find(person.Id);
                toeditpeson.FirstName = person.FirstName;
                toeditpeson.LastName = person.LastName;
                db.SaveChanges();
            }

            return Json(new[] { person }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove([DataSourceRequest] DataSourceRequest request, PersonViewModel person)
        {
            if (person != null)
            {
                var db = new ApplicationDbContext();
                var toeditpeson = db.Persons.Find(person.Id);
                db.Persons.Remove(toeditpeson);
                db.SaveChanges();
            }

            return Json(new[] { person }.ToDataSourceResult(request, ModelState));
        }
    }
}