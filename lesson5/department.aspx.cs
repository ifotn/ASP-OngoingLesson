﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference models
using lesson5.Models;
using System.Web.ModelBinding;

namespace lesson5
{
    public partial class department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if the page isn't posted back, check the url for an id to see know add or edit
            if (!IsPostBack)
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                    //we have a url parameter if the count is > 0 so populate the form
                    GetDepartment();
                }
            }
        }

        protected void GetDepartment()
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //get id from url parameter and store in a variable
                Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                var d = (from dep in conn.Departments
                         where dep.DepartmentID == DepartmentID
                         select dep).FirstOrDefault();

                //populate the form from our department object
                txtName.Text = d.Name;
                txtBudget.Text = d.Budget.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //instantiate a new deparment object in memory
                Department d = new Department();

                //decide if updating and then re-query the updated departments
                if (Request.QueryString.Count > 0)
                {
                    Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    d = (from dep in conn.Departments
                         where dep.DepartmentID == DepartmentID
                         select dep).FirstOrDefault();
                }

                //fill in the values
                d.Name = txtName.Text;
                d.Budget = Convert.ToDecimal(txtBudget.Text);

                //Only add a new record if it's not updating an existing one
                if (Request.QueryString.Count == 0)
                {
                    conn.Departments.Add(d);
                }

                conn.SaveChanges();

                //redirect to the departments page
                Response.Redirect("departments.aspx");
            }
        }
    }
}