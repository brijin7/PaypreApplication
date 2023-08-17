using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;

public partial class Master_Tools_LiveConfigMstr : System.Web.UI.Page
{
    IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (!IsPostBack)
        {
            ddlSpecialist.Items.Insert(0, new ListItem("Specialist *", "0"));
            GetConfig();
            GetTrainer();
            GetTrainingType();
            GetSlot();
        }

    }
    #endregion
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        ConfigClear();
    }
    #endregion
  
    #region Bind Config
    public void GetConfig()
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
                string sUrl = Session["BaseUrl"].ToString().Trim() + "liveConfig/LiveBranchowner?branchId=" + Session["branchId"].ToString() + "&gymOwnerId="+ Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvLiveConfigMstr.DataSource = dt;
                        gvLiveConfigMstr.DataBind();
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {
            InsertConfig();
        }
        else
        {
            UpdateConfigType();
        }

    }
    #endregion
    #region Clear
    public void ConfigClear()
    {
        txtLiveUrl.Text = string.Empty;
        txtLiveDate.Text = string.Empty;
        txtPurPoseName.Text = string.Empty;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
        ddltrainingType.ClearSelection();
        ddlTrainer.ClearSelection();
        ddlSpecialist.ClearSelection();
        chkSlotList.ClearSelection();
    }
    #endregion
    #region Insert Function 
    public void InsertConfig()
    {
        try
        {
            string slotId = string.Empty;

            int Count = 0;

            for (int i = 0; i < chkSlotList.Items.Count; i++)
            {

                if (chkSlotList.Items[i].Selected == true)
                {
                    slotId += chkSlotList.Items[i].Value + ",";
                    Count = 1;
                }

            }
            if (Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Slot');", true);
                return;
            }

            DateTime Fromdate = DateTime.Parse(txtLiveDate.Text, objEnglishDate);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new LiveConfig()
                {
                    gymownerId = Session["gymownerId"].ToString(),
                    branchId= Session["branchId"].ToString(),
                    purposename = txtPurPoseName.Text,
                    liveurl = txtLiveUrl.Text,
                    livedate = txtLiveDate.Text,
                    createdBy = Session["userId"].ToString(),
                    trainerId=ddlTrainer.SelectedValue,
                    categoryId=ddlSpecialist.SelectedValue,
                    trainingTypeId=ddltrainingType.SelectedValue,
                    slotId=chkSlotList.SelectedValue

                };
                HttpResponseMessage response = client.PostAsJsonAsync("liveConfig/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        GetConfig();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                       
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ConfigClear();
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Update Function 
    public void UpdateConfigType()
    {
        try
        {
            string slotId = string.Empty;

            int Count = 0;

            for (int i = 0; i < chkSlotList.Items.Count; i++)
            {

                if (chkSlotList.Items[i].Selected == true)
                {
                    slotId += chkSlotList.Items[i].Value + ",";
                    Count = 1;
                }

            }
            if (Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Slot');", true);
                return;
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
              
                var Insert = new LiveConfig()
                {
                    gymownerId = Session["gymownerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    uniqueId = ViewState["uniqueId"].ToString(),
                    purposename = txtPurPoseName.Text,
                    liveurl = txtLiveUrl.Text,
                    livedate = txtLiveDate.Text,
                    updatedBy = Session["userId"].ToString(),
                    trainerId = ddlTrainer.SelectedValue,
                    categoryId = ddlSpecialist.SelectedValue,
                    trainingTypeId = ddltrainingType.SelectedValue,
                    slotId = chkSlotList.SelectedValue

                };
                HttpResponseMessage response = client.PostAsJsonAsync("liveConfig/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        GetConfig();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                      
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ConfigClear();
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
    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");
            Label lblpurposename = (Label)gvrow.FindControl("lblpurposename");
            Label lbllivedate = (Label)gvrow.FindControl("lbllivedate");
            Label lblliveurl = (Label)gvrow.FindControl("lblliveurl");
            Label lbltrainerId = (Label)gvrow.FindControl("lbltrainerId");
            Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
            Label lbltrainingTypeId = (Label)gvrow.FindControl("lbltrainingTypeId");
          
            txtPurPoseName.Text = lblpurposename.Text.Trim();
            txtLiveUrl.Text = lblliveurl.Text.Trim();
            txtLiveDate.Text = lbllivedate.Text.Trim();
            ViewState["uniqueId"] = lbluniqueId.Text.Trim();
            divGv.Visible = false;
            DivForm.Visible = true;
            btnSubmit.Text = "Update";
            ddlTrainer.SelectedValue = lbltrainerId.Text;
            GetSpecialIst();
            ddlSpecialist.SelectedValue = lblcategoryId.Text;
            ddltrainingType.SelectedValue = lbltrainingTypeId.Text;

            DataList dtlSlotDetails = gvLiveConfigMstr.FindControl("dtlSlotDetails") as DataList;
            DataTable dt = new DataTable();
            dt.Columns.Add("slotId", typeof(Int64));
            dt.Columns.Add("categorySlotId", typeof(Int64));
            dt.Columns.Add("activeStatus", typeof(String));
            for (int i = 0; i < dtlSlotDetails.Items.Count; i++)
            {
                Label lblcategorySlotId = dtlSlotDetails.Items[i].FindControl("lblcategorySlotId") as Label;
                Label lblslotId = dtlSlotDetails.Items[i].FindControl("lblslotId") as Label;
                Label lblactiveStatus = dtlSlotDetails.Items[i].FindControl("lblactiveStatus") as Label;
                for (int j = 0; j < chkSlotList.Items.Count; j++)
                {
                    if (chkSlotList.Items[j].Value == lblslotId.Text)
                    {
                        if (lblactiveStatus.Text == "A")
                        {
                            chkSlotList.Items[j].Selected = true;
                        }
                    }

                }
                dt.Rows.Add(lblslotId.Text, lblcategorySlotId.Text, lblactiveStatus.Text);
            }
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
            Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");
         
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
                var activeOrInActive = new LiveConfig()
                {
                    queryType = QueryType.Trim(),
                    uniqueId = lbluniqueId.Text.Trim(),
                   
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("liveConfig/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {

                        GetConfig();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Config Class

    public class LiveConfig
    {
        public string queryType { get; set; }
        public string gymownerId { get; set; }
        public string branchId { get; set; }
        public string liveurl { get; set; }
        public string livedate { get; set; }
        public string purposename { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string uniqueId { get; set; }
        public string trainerId { get; set; }
        public string categoryId { get; set; }
        public string trainingTypeId { get; set; }
        public string slotId { get; set; }
    }

    #endregion

    #region Get Trainer
    public void GetTrainer()
    {
        try
        {
            ddlTrainer.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainer/ddlTrainerList?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["TrainerDetails"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlTrainer.DataSource = dt;
                            ddlTrainer.DataTextField = "trainerName";
                            ddlTrainer.DataValueField = "trainerId";
                            ddlTrainer.DataBind();
                        }
                        else
                        {
                            ddlTrainer.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlTrainer.Items.Insert(0, new ListItem("Trainer *", "0"));
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion

    #region Get CategorySpecialist
    protected void ddlTrainer_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSpecialIst();
    }
    public void GetSpecialIst()
    {
        try
        {
            ddlSpecialist.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerDetails/getTrainerSpecialist?trainerId=" + ddlTrainer.SelectedValue + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlSpecialist.DataSource = dt;
                            ddlSpecialist.DataTextField = "specialistTypeName";
                            ddlSpecialist.DataValueField = "specialistTypeId";
                            ddlSpecialist.DataBind();
                        }
                        else
                        {
                            ddlSpecialist.DataBind();
                        }

                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlSpecialist.Items.Insert(0, new ListItem("Specialist *", "0"));
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    ddlSpecialist.Items.Insert(0, new ListItem("Specialist *", "0"));
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Get TrainingType
    public void GetTrainingType()
    {
        try
        {
            ddltrainingType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainingTypeMaster/getDropDownDeatils?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddltrainingType.DataSource = dt;
                            ddltrainingType.DataTextField = "trainingTypeName";
                            ddltrainingType.DataValueField = "trainingTypeId";
                            ddltrainingType.DataBind();
                        }
                        else
                        {
                            ddltrainingType.DataBind();
                        }

                    }
                    else
                    {
                        ///ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddltrainingType.Items.Insert(0, new ListItem("Training Type *", "0"));
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    ddltrainingType.Items.Insert(0, new ListItem("Training Type *", "0"));
                    ///ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion

    #region Get Slot
    public void GetSlot()
    {
        try
        {
            chkSlotList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "slotMaster/GetSlotstoActivate?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            chkSlotList.DataSource = dt;
                            chkSlotList.DataTextField = "SlotTime";
                            chkSlotList.DataValueField = "slotId";
                            chkSlotList.DataBind();
                        }
                        else
                        {
                            chkSlotList.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
}