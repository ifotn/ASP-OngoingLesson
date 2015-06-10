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
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //fill the grid
            if (!IsPostBack)
            {
                GetDepartments();
            }
        }

        protected void GetDepartments()
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //use link to query the Departments model
                var deps = from d in conn.Departments
                           select d;

                //bind to the gridview
                grdDepartments.DataSource = deps.ToList();
                grdDepartments.DataBind();
            }
        }

        protected void grdDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //get the Department Id
                Int32 DepartmentID = Convert.ToInt32(grdDepartments.DataKeys[e.RowIndex].Values["DepartmentID"]);

                var d = (from dep in conn.Departments
                         where dep.DepartmentID == DepartmentID
                         select dep).FirstOrDefault();

                //process the delete
                conn.Departments.Remove(d);
                conn.SaveChanges();

                //update the grid
                GetDepartments();
            }
        }
    }
}