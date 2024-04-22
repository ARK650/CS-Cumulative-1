using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClassesController : ApiController
    {
         
        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }
        //GET : /Classes/List
        public ActionResult List()
        {
            ClassesDataController controller = new ClassesDataController();
            IEnumerable<classes> classes = controller.ListClasses();
            return View(classes);
        }

        //GET : /Classes/Show/{id}
        public ActionResult Show(int id)
        {
            ClassesDataController controller = new ClassesDataController();
            classes NewClasses = controller.FindClasses(id);


            return View(NewClasses);
        }
    }
}
