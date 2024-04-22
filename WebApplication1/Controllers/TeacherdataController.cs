using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Resultset;

namespace WebApplication1.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private schoolDBcontext School = new schoolDBcontext();

        //This Controller Will access the Teachers table of our  database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teacher (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<teacher> Teachers = new List<teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string Employeenumber = ResultSet["employeenumber"].ToString();
                string Hiredate = ResultSet["hiredate"].ToString();
                decimal Salary = (decimal)ResultSet["salary"];


                teacher Newteacher = new teacher();
                Newteacher.teacherId = TeacherId;
                Newteacher.teacherFname = TeacherFname;
                Newteacher.teacherLname = TeacherLname;
                Newteacher.employeenumber = Employeenumber;
                Newteacher.hiredate = Hiredate;
                Newteacher.salary = Salary;

                //Add the Teacher Name to the List
                Teachers.Add(Newteacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }


        /// <summary>
        /// Finds an teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>An teacher object</returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public teacher FindTeacher(int id)
        {
            teacher NewTeacher = new teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = " + id;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string Employeenumber = ResultSet["employeenumber"].ToString();
                string Hiredate = ResultSet["hiredate"].ToString();
                decimal Salary = (decimal)ResultSet["salary"];


                teacher Newteacher = new teacher();
                Newteacher.teacherId = TeacherId;
                Newteacher.teacherFname = TeacherFname;
                Newteacher.teacherLname = TeacherLname;
                Newteacher.employeenumber = Employeenumber;
                Newteacher.hiredate = Hiredate;
                Newteacher.salary = Salary;
            }


            return NewTeacher;
        }

        /// <summary>
        /// Deletes an Teacher from the connected MySQL Database if the ID of that author exists. Does NOT maintain relational integrity. 
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Teacher from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        /// <summary>
        /// Adds an Author to the MySQL Database. 
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. </param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Likes Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Insert into teachers (teacherfname, teacherlname, hiredate, employeenumber, salary) values (@teacherfname, @teacherlname, @hiredate, @employeenumber, @salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.teacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.teacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.hiredate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();



        }

        /// <summary>
        /// Updates an Author on the MySQL Database. 
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teacher's table.</param>
        /// <example>
        /// POST api/AuthorData/UpdateAuthor/208 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Likes Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Debug.WriteLine(TeacherInfo.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL QUERY
            cmd.CommandText = "update authors set teacherfname=@TeacherFname, teacherlname=@TeacherLname where authorid=@AuthorId";
            cmd.Parameters.AddWithValue("@AuthorFname", TeacherInfo.teacherFname);
            cmd.Parameters.AddWithValue("@AuthorLname", TeacherInfo.teacherLname);
            cmd.Parameters.AddWithValue("@AuthorId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }


    }
}