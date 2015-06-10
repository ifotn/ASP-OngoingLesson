using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference our entity framework models
using lesson5.Models;
using System.Web.ModelBinding;

namespace lesson5
{
    public partial class student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if the page isn't posted back, check the url for an id to see know add or edit
            if (!IsPostBack)
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                    //we have a url parameter if the count is > 0 so populate the form
                    GetStudent();
                }
            }
        }

        protected void GetStudent()
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //get id from url parameter and store in a variable
                Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                var s = (from stud in conn.Students
                         where stud.StudentID == StudentID
                         select stud).FirstOrDefault();

                //populate the form from our Student object
                txtFirstMidName.Text = s.FirstMidName;
                txtLastName.Text = s.LastName;
                txtEnrollmentDate.Text = s.EnrollmentDate.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //instantiate a new deparment object in memory
                Student s = new Student();

                //decide if updating and then re-query the updated Students
                if (Request.QueryString.Count > 0)
                {
                    Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    s = (from stud in conn.Students
                         where stud.StudentID == StudentID
                         select stud).FirstOrDefault();
                }

                //fill in the values
                s.FirstMidName = txtFirstMidName.Text;
                s.LastName = txtLastName.Text;
                s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

                //Only add a new record if it's not updating an existing one
                if (Request.QueryString.Count == 0)
                {
                    conn.Students.Add(s);
                }

                conn.SaveChanges();

                //redirect to the Students page
                Response.Redirect("students.aspx");
            }
        }
    }
}