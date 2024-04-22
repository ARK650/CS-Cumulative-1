using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
   
        public class TeacherController : Controller
        {
            // GET: Teacher
            public ActionResult Index()
            {
                return View();
            }
            //GET : /Teacher/List
            public ActionResult List()
            {
                TeacherDataController controller = new TeacherDataController();
                IEnumerable<teacher> Teacher = controller.ListTeachers();
                return View(Teacher);
            }

            //GET : /Teacher/Show/{id}
            public ActionResult Show(int id)
            {
                TeacherDataController controller = new TeacherDataController();
                teacher NewTeacher = controller.FindTeacher(id);


                return View(NewTeacher);
            }
        }
    }