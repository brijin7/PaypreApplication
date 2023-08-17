﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Master_EmpMstr : System.Web.UI.Page
{
    Helper helper = new Helper();
    IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (!IsPostBack)
        {
            BindEmpMaster();
            BindEmployeeType();
            BindDesignation();
            BindDepartment();
            BindShift();
            BindRole();
        }

    }
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindEmployeeType();
        BindDesignation();
        BindDepartment();
        BindShift();
        BindRole();
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion
    #region Bind Employee Type
    public void BindEmployeeType()
    {
        try

        {
            ddlEmployeeType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=8";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlEmployeeType.DataSource = dt;
                            ddlEmployeeType.DataTextField = "configName";
                            ddlEmployeeType.DataValueField = "configId";
                            ddlEmployeeType.DataBind();
                        }
                        else
                        {
                            ddlEmployeeType.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlEmployeeType.Items.Insert(0, new ListItem("Employee Type *", "0"));
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Bind Designation
    public void BindDesignation()
    {
        try
        {
            ddlDesignation.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=9";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDesignation.DataSource = dt;
                            ddlDesignation.DataTextField = "configName";
                            ddlDesignation.DataValueField = "configId";
                            ddlDesignation.DataBind();
                        }
                        else
                        {
                            ddlDesignation.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlDesignation.Items.Insert(0, new ListItem("Designation *", "0"));
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Bind Department
    public void BindDepartment()
    {
        try

        {
            ddlDepartment.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=10";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDepartment.DataSource = dt;
                            ddlDepartment.DataTextField = "configName";
                            ddlDepartment.DataValueField = "configId";
                            ddlDepartment.DataBind();
                        }
                        else
                        {
                            ddlDepartment.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlDepartment.Items.Insert(0, new ListItem("Department *", "0"));
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Bind Role
    public void BindRole()
    {
        try

        {
            ddlRole.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=11";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlRole.DataSource = dt;
                            ddlRole.DataTextField = "configName";
                            ddlRole.DataValueField = "configId";
                            ddlRole.DataBind();
                        }
                        else
                        {
                            ddlRole.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlRole.Items.Insert(0, new ListItem("Role *", "0"));
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Bind Shift
    public void BindShift()
    {
        try
        {
            ddlShift.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "shiftMaster/GetDropDownDetailsShift?branchId="+ Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlShift.DataSource = dt;
                            ddlShift.DataTextField = "shiftName";
                            ddlShift.DataValueField = "shiftId";
                            ddlShift.DataBind();
                        }
                        else
                        {
                            ddlShift.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlShift.Items.Insert(0, new ListItem("Shift *", "0"));
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Bind Emp Master  
    public void BindEmpMaster()
    {
        try

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "employee?gymOwnerId="+Session["gymOwnerId"].ToString() + "&branchId="+Session["branchId"].ToString()+"";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            gvEmpMstr.DataSource = dt;
                            gvEmpMstr.DataBind();
                            divGv.Visible = true;
                            DivForm.Visible = false;
                            btnCancel.Visible = true;
                        }
                        else
                        {
                            gvEmpMstr.DataSource = null;
                            gvEmpMstr.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                            btnCancel.Visible=false;
                        }

                    }
                    else
                    {
                        gvEmpMstr.DataSource = null;
                        gvEmpMstr.DataBind();
                        divGv.Visible = false;
                        DivForm.Visible = true;
                        btnCancel.Visible = false;
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    btnCancel.Visible = false;
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Insert Employee
    public void InsertEmp()
    {
        int StatusCodes;
        string ImageUrl;
        //if (Fuimage.HasFile)
        //{
        //    helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
        //}
        //else
        //{
        //    ImageUrl = "";
        //}

        if (hfImageUrl.Value != "")
        {
            ImageUrl = hfImageUrl.Value;
        }
        else
        {
            if (imgEmpPhotoPrev.ImageUrl != "" && imgEmpPhotoPrev.ImageUrl != "~/img/Defaultupload.png")
            {
                ImageUrl = imgEmpPhotoPrev.ImageUrl;
            }

            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                return;
            }
        }


        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new MstrEmployee()
                {
                   gymOwnerId= Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    empType=ddlEmployeeType.SelectedValue,
                    firstName=txtFirstName.Text,
                    lastName=txtLastName.Text,
                    designation=ddlDesignation.SelectedValue,
                    department=ddlDepartment.SelectedValue,
                    gender=ddlGender.SelectedValue,
                    addressLine1=txtAddress1.Text,
                    addressLine2=txtAddress2.Text,
                    zipcode= txtPincode.Text,
                    city= hfCity.Value,
                    state=hfState.Value,
                    district=hfDistrict.Value,
                    maritalStatus=ddlMaritalStatus.SelectedValue,
                    dob=txtDOB.Text,
                    doj=txtDOJ.Text,
                    aadharId=txtAadharId.Text,
                    photoLink= ImageUrl.Trim(),
                    mobileNo=txtMobileNo.Text,
                    passWord=txtPassWord.Text,
                    shiftId=ddlShift.SelectedValue,
                    roleId= ddlRole.SelectedValue,
                    mailId= txtEmailId.Text,
                    mobileAppAccess="",
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("employee/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        
                        BindEmpMaster();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    clear();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Employee
    public void UpdateEmp()
    {

        int StatusCodes;
        string ImageUrl;
        //if (Fuimage.HasFile)
        //{
        //    helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
        //}
        //else
        //{
        //    ImageUrl = imgEmpPhotoPrev.ImageUrl;
        //}

        if (hfImageUrl.Value != "")
        {
            ImageUrl = hfImageUrl.Value;
        }
        else
        {
            if (imgEmpPhotoPrev.ImageUrl != "" && imgEmpPhotoPrev.ImageUrl != "~/img/Defaultupload.png")
            {
                ImageUrl = imgEmpPhotoPrev.ImageUrl;
            }

            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                return;
            }
        }

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var update = new MstrEmployee()
                {
                    empId=ViewState["empId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    empType = ddlEmployeeType.SelectedValue,
                    firstName = txtFirstName.Text,
                    lastName = txtLastName.Text,
                    designation = ddlDesignation.SelectedValue,
                    department = ddlDepartment.SelectedValue,
                    gender = ddlGender.SelectedValue,
                    addressLine1 = txtAddress1.Text,
                    addressLine2 = txtAddress2.Text,
                    zipcode = txtPincode.Text,
                    city = hfCity.Value,
                    state = hfState.Value,
                    district = hfDistrict.Value,
                    maritalStatus = ddlMaritalStatus.SelectedValue,
                    dob = txtDOB.Text,
                    doj = txtDOJ.Text,
                    aadharId = txtAadharId.Text,
                    photoLink = ImageUrl.Trim(),
                    mobileNo = txtMobileNo.Text,
                    passWord = txtPassWord.Text,
                    shiftId = ddlShift.SelectedValue,
                    roleId = ddlRole.SelectedValue,
                    mailId = txtEmailId.Text,
                    mobileAppAccess = "",
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("employee/update", update).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                      
                        BindEmpMaster();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    clear();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Btnsubmit Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertEmp();
        }
        else
        {
            UpdateEmp();
        }
    }
    #endregion

    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lblempId = (Label)gvrow.FindControl("lblempId");
            ViewState["empId"] = lblempId.Text;
            Label lbllastName = (Label)gvrow.FindControl("lbllastName");
            txtLastName.Text = lbllastName.Text;
            Label lblfirstName = (Label)gvrow.FindControl("lblfirstName");
            txtFirstName.Text = lblfirstName.Text;
            Label lblcity = (Label)gvrow.FindControl("lblcity");
            txtCity.Text = lblcity.Text;
            Label lblstate = (Label)gvrow.FindControl("lblstate");
            txtState.Text = lblstate.Text;
            Label lblgender = (Label)gvrow.FindControl("lblgender");
            ddlGender.SelectedValue = lblgender.Text;
            Label lbladdressLine1 = (Label)gvrow.FindControl("lbladdressLine1");
            txtAddress1.Text = lbladdressLine1.Text;
            Label lbladdressLine2 = (Label)gvrow.FindControl("lbladdressLine2");
            txtAddress2.Text = lbladdressLine2.Text;
            Label lblzipcode = (Label)gvrow.FindControl("lblzipcode");
            txtPincode.Text = lblzipcode.Text;
            Label lbldistrict = (Label)gvrow.FindControl("lbldistrict");
            txtDistrict.Text = lbldistrict.Text;
            Label lblmaritalStatus = (Label)gvrow.FindControl("lblmaritalStatus");
            ddlMaritalStatus.SelectedValue = lblmaritalStatus.Text;
            Label lbldob = (Label)gvrow.FindControl("lbldob");
           // DateTime dob = Convert.ToDateTime(lbldob.Text);
           // DateTime Fromdate = DateTime.Parse(lbldob.Text, objEnglishDate);
           
            txtDOB.Text = lbldob.Text;
            Label lbldoj = (Label)gvrow.FindControl("lbldoj");
           // DateTime doj = Convert.ToDateTime(lbldoj.Text);
            txtDOJ.Text = lbldoj.Text;
           
            Label lblaadharId = (Label)gvrow.FindControl("lblaadharId");
            txtAadharId.Text = lblaadharId.Text;
            
            Label lblmobileNo = (Label)gvrow.FindControl("lblmobileNo");
            txtMobileNo.Text = lblmobileNo.Text;
            Label lblpassWord = (Label)gvrow.FindControl("lblpassWord");
            txtPassWord.Text = lblpassWord.Text;
            Label lblmailId = (Label)gvrow.FindControl("lblmailId");
            txtEmailId.Text = lblmailId.Text;
            BindShift();
            Label lblshiftId = (Label)gvrow.FindControl("lblshiftId");
            ddlShift.SelectedValue = lblshiftId.Text;
            BindRole();
            Label lblroleId = (Label)gvrow.FindControl("lblroleId");
            ddlRole.SelectedValue = lblroleId.Text;
            BindEmployeeType();
            Label lblempType = (Label)gvrow.FindControl("lblempType");
            ddlEmployeeType.SelectedValue = lblempType.Text;
            BindDesignation();
            Label lbldesignation = (Label)gvrow.FindControl("lbldesignation");
            ddlDesignation.SelectedValue = lbldesignation.Text;
            BindDepartment();
            Label lbldepartment = (Label)gvrow.FindControl("lbldepartment");
            ddlDepartment.Text = lbldepartment.Text;
            Label lblphotoLink = (Label)gvrow.FindControl("lblphotoLink");
            imgEmpPhotoPrev.ImageUrl = lblphotoLink.Text.Trim();

            if (imgEmpPhotoPrev.ImageUrl == "")
            {
                imgEmpPhotoPrev.ImageUrl = "~/img/User.png";
            }

            hfCity.Value = lblcity.Text;
            hfState.Value = lblstate.Text;
            hfDistrict.Value = lbldistrict.Text;
            divGv.Visible = false;
            DivForm.Visible = true;
            btnSubmit.Text = "Update";
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }

    }
    #endregion

    #region Active or Inactive  Click Event
    protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblempId = (Label)gvrow.FindControl("lblempId");

            LinkButton lblActiveStatus = (LinkButton)lnkbtn.FindControl("lnkActiveOrInactive");
            string sActiveStatus = lblActiveStatus.Text.Trim() == "Active" ? "A" : "D";
            string QueryType = string.Empty;
            if (sActiveStatus.Trim() == "D")
            {
                QueryType = "active";
            }
            else
            {
                QueryType = "inActive";
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var activeOrInActive = new MstrEmployee()
                {
                    queryType = QueryType.Trim(),
                    empId = lblempId.Text.Trim(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("employee/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindEmpMaster();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                    clear();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion


    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
       
    }
    #endregion

    public void clear()
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        btnSubmit.Text = "Submit";
        ViewState["empId"] = "";
        ddlEmployeeType.ClearSelection();
        txtFirstName.Text = "";
        txtLastName.Text = "";
        ddlDesignation.ClearSelection();
         ddlDepartment.ClearSelection();
        ddlGender.ClearSelection();
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtPincode.Text = "";
        hfCity.Value = "";
        hfState.Value = "";
        hfDistrict.Value = "";
        ddlMaritalStatus.ClearSelection();
        txtDOB.Text = "";
        txtDOJ.Text = "";
        txtAadharId.Text = "";
        txtMobileNo.Text = "";
        txtPassWord.Text = "";
        txtCity.Text = "";
        txtDistrict.Text = "";
        txtState.Text = "";
        ddlShift.ClearSelection();
        ddlRole.ClearSelection();
        txtEmailId.Text = "";
       
    }
    public class MstrEmployee
    {
        public string queryType { get; set; }
        public string empId { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string empType { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string gender { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string maritalStatus { get; set; }
        public string dob { get; set; }
        public string doj { get; set; }
        public string aadharId { get; set; }
        public string photoLink { get; set; }
        public string mobileNo { get; set; }
        public string passWord { get; set; }
        public string shiftId { get; set; }
        public string roleId { get; set; }
        public string mailId { get; set; }
        public string mobileAppAccess { get; set; }
        public string updatedBy { get; set; }
        public string createdBy { get; set; }


    }

}