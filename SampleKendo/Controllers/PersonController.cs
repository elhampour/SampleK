using System.Linq;
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

    public class AddressController : Controller
    {
        public ActionResult Index(int id)
        {
            return View(id);
        }

        public ActionResult Get([DataSourceRequest] DataSourceRequest request, int personId)
        {
            var database = new ApplicationDbContext();
            var result = database.Addresses.Where(a => a.PersonId == personId).Select(a => new AddressViewModel()
            {
                AddressDesc = a.AddressDesc,
                Id = a.Id
            });
            var data = result.ToDataSourceResult(request);
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, AddressViewModel address, int personId)
        {
            if (address != null && ModelState.IsValid)
            {
                var addressNew = new Address()
                {
                    AddressDesc = address.AddressDesc,
                    PersonId = personId
                };
                var db = new ApplicationDbContext();
                db.Addresses.Add(addressNew);
                db.SaveChanges();
            }

            return Json(new[] { address }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, AddressViewModel address)
        {
            if (address != null && ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                var toeditpeson = db.Addresses.Find(address.Id);
                toeditpeson.AddressDesc = address.AddressDesc;
                db.SaveChanges();
            }

            return Json(new[] { address }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove([DataSourceRequest] DataSourceRequest request, AddressViewModel address)
        {
            if (address != null)
            {
                var db = new ApplicationDbContext();
                var addresstodel = db.Addresses.Find(address.Id);
                db.Addresses.Remove(addresstodel);
                db.SaveChanges();
            }

            return Json(new[] { address }.ToDataSourceResult(request, ModelState));
        }
    }

    public class AddressViewModel
    {
        public int Id { get; set; }

        public string AddressDesc { get; set; }
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
            var result = database.Persons.Select(a => new PersonViewModel()
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName
            });
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