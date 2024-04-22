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
    public class StudentController : ApiController
    {
            // GET: Student
            public ActionResult Index()
            {
                return View();
            }
            //GET : /Student/List
            public ActionResult List()
            {
                StudentDataController controller = new StudentDataController();
                IEnumerable<students> students = controller.ListStudents();
                return View(students);
            }

            //GET : /Student/Show/{id}
            public ActionResult Show(int id)
            {
                StudentDataController controller = new StudentDataController();
                students NewStudent = controller.FindStudents(id);


                return View(NewStudent);
            }
        
}
