using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FleetMgtSolution.BLL;

namespace FleetMgtSolution.BackOffice
{
    public partial class ManageDept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindList();
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> An error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
                 
            }
        }
        private void BindGrid()
        {
            gvDept.DataSource = DepartmentBLL.GetDeptList();
            gvDept.DataBind();
        }
        private void BindList()
        {
            ddlDir.DataTextField = "DataField";
            ddlDir.DataValueField = "DataValue";
            ddlDir.DataSource = Utility.DirectorateList();
            ddlDir.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    Department dpt =null;bool rst=false;
                    dpt  = DepartmentBLL.GetDepartment(Convert.ToInt32(txtID.Text));
                    if (dpt != null)
                    {
                        dpt.Name = txtDept.Text; dpt.Directorate = ddlDir.SelectedValue;
                        rst = DepartmentBLL.Update(dpt);
                        if (rst != false)
                        {
                            BindGrid();
                            success.Visible = true;
                            success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record updated successfully!!.";
                            return;
                        }
                    }
                    
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button>Record could Not updated. Kindly try again. If error persist contact Administrator!!.";
                    }
                }
                else
                {
                    string dept = ""; string dir = "";
                    dept = txtDept.Text;
                    dir = ddlDir.SelectedValue;
                    bool result = false;
                    Department dp = new Department();
                    dp.Name = dept; dp.Directorate = dir;
                    result = DepartmentBLL.AddDepartment(dp);
                    if (result)
                    {
                        BindGrid();
                        txtDept.Text = "";
                        ddlDir.SelectedValue = "";
                        success.Visible = true;
                        success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record added successfully!!.";
                        return;
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button>Record could Not added. Kindly try again. If error persist contact Administrator!!.";
                    }
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void gvDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "Update";
                dvID.Visible = true;
                dvMsg.InnerText = "Update Department :";
                btnSubmit.Text = "Update";
                 
                GridViewRow row = gvDept.SelectedRow;
                txtID.Text = row.Cells[0].Text;
                txtDept.Text = row.Cells[2].Text.ToLower();
                //ddlDir.SelectedValue=row.Cells[2].Text.ToLower();
                //ddlDir.SelectedItem.Text = HttpUtility.HtmlDecode(row.Cells[2].Text).ToLower();
            } 
            catch (Exception ex)
            {  
                error.Visible = true;
                error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }

        protected void gvDept_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDept.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch
            {
            }
        }
    }
}