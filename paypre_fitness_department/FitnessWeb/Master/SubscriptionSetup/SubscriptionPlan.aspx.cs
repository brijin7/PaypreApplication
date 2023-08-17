using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_SubscriptionPlan : System.Web.UI.Page
{
    Helper helper = new Helper();
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
            GetTax();
            GetSubscriptionPlan();
            GetOfferDetails();
        }
    }
    #endregion
    #region Get SubscriptionPlan
    public void GetSubscriptionPlan()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "subscriptionPlanMaster?gymOwnerId="
                             + Session["gymOwnerId"].ToString().Trim() + "&branchId="
                             + Session["branchId"].ToString().Trim() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvSubscriptionPlan.DataSource = dt;
                        gvSubscriptionPlan.DataBind();
                        DivForm.Visible = false;
                        divGv.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        DivForm.Visible = true;
                        divGv.Visible = false;
                        btnCancel.Visible = false;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Ddl Tax
    protected void ddlTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlTax.SelectedValue != "0")
        {
            // txtNetAmount.ReadOnly = false;
            string[] Tp;
            string[] Percentage;
            Tp = ddlTax.SelectedItem.Text.Split(',');
            for (int i = 0; i < Tp.Count(); i++)
            {
                Percentage = Tp[i].Split('-');
                ViewState["TaxPercentage"] = Percentage[1];
            }
            ViewState["TaxCount"] = Tp.Count();


          
            if (txtNetAmount.Text != "" && txtNetAmount.Text != "0.00" && txtNetAmount.Text != "0")
            {
                GetNetAmount();
            }
            else
            {
                txtNetAmount.Text = "";
                txtprice.Text = "";
                txttax.Text = "";
            }
        }
    }
    #endregion
    #region Get Tax
    public void GetTax()
    {
        try
        {
            ddlTax.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "taxMaster/GetddlTax?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                            ddlTax.DataSource = dt;
                            ddlTax.DataTextField = "taxDetails";
                            ddlTax.DataValueField = "taxId";
                            ddlTax.DataBind();
                        }
                        else
                        {
                            ddlTax.DataBind();
                        }                  
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlTax.Items.Insert(0, new ListItem("Tax  *", "0"));
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
    #region Get NetAmount
    protected void txtNetAmount_TextChanged(object sender, EventArgs e)
    {
        if (ViewState["TaxPercentage"] != null && ViewState["TaxPercentage"].ToString() != "" && txtNetAmount.Text != "")
        {
            if (txtNetAmount.Text != "0.00" && txtNetAmount.Text != "0")
            {
                GetNetAmount();
            }
            else
            {
                txtNetAmount.Text = "";
                txtprice.Text = "";
                txttax.Text = "";
            }
        }

    }

    public void GetNetAmount()
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
                string Endpoint = "fitnessCategoryPrice/GetTax?netAmount=" + txtNetAmount.Text + "" +
                   "&taxPercentage=" + ViewState["TaxPercentage"].ToString() + "";

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
                            txtprice.Text = dt.Rows[0]["netAmount"].ToString();
                            txttax.Text = dt.Rows[0]["tax"].ToString();
                        }
                    }
                    else
                    {
                        ddlTax.Items.Clear();
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
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Planclear();
        divGv.Visible = false;
        DivForm.Visible = true;
        AddBenefits.Visible = false;
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        AddBenefits.Visible = false;
    }
    #endregion
    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            AddBenefits.Visible = false;
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lblsubscriptionPlanId = (Label)gvrow.FindControl("lblsubscriptionPlanId");
            Label lblpackageName = (Label)gvrow.FindControl("lblpackageName");
            Label lbldescription = (Label)gvrow.FindControl("lbldescription");
            Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");
            Label lbltaxId = (Label)gvrow.FindControl("lbltaxId");
            Label lblnetAmount = (Label)gvrow.FindControl("lblnetAmount");
            Label lblactualAmount = (Label)gvrow.FindControl("lblactualAmount");
            Label lbldisplayAmount = (Label)gvrow.FindControl("lbldisplayAmount");
            Label lblamount = (Label)gvrow.FindControl("lblamount");
            Label lblnoOfDays = (Label)gvrow.FindControl("lblnoOfDays");
            Label lbltax = (Label)gvrow.FindControl("lbltax");
            Label lbltaxName = (Label)gvrow.FindControl("lbltaxName");
            Label lblcredits = (Label)gvrow.FindControl("lblcredits");
            Label lblcgstTax = (Label)gvrow.FindControl("lblcgstTax");
            Label lblsgstTax = (Label)gvrow.FindControl("lblsgstTax");
            Label lblisTrialAvailable = (Label)gvrow.FindControl("lblisTrialAvailable");
            Label lblnoOfTrialDays = (Label)gvrow.FindControl("lblnoOfTrialDays");
            Label lblofferId = (Label)gvrow.FindControl("lblofferId");
            Label lblofferName = (Label)gvrow.FindControl("lblofferName");
            Label lblfromDate = (Label)gvrow.FindControl("lblfromDate");
            Label lbltoDate = (Label)gvrow.FindControl("lbltoDate");

            txtDescription.Text = lbldescription.Text.Trim();
            txtPackageName.Text = lblpackageName.Text.Trim();
            txtNoOfDays.Text = lblnoOfDays.Text.Trim();
            txtCredits.Text = lblcredits.Text.Trim();
            txtNoOfDays.Text = lblnoOfDays.Text.Trim();
            txtNoofTrailDays.Text = lblnoOfTrialDays.Text.Trim();
            imgpreview.ImageUrl = lblimageUrl.Text.Trim();
            ddlTax.SelectedValue = lbltaxId.Text.Trim();
            txtprice.Text = lblamount.Text.Trim();
            txttax.Text = lbltax.Text.Trim();
            txtNetAmount.Text = lblnetAmount.Text;
            txtactutalAmount.Text = lblactualAmount.Text;
            txtdisplayAmount.Text = lbldisplayAmount.Text;

            if (lblisTrialAvailable.Text.Trim() == "N")
            {
                RbtnlTrailAvail.SelectedValue = "No";
            }
            else
            {
                RbtnlTrailAvail.SelectedValue = "Yes";
            }
            if (RbtnlTrailAvail.SelectedValue == "Yes")
            {
                txtNoofTrailDays.Visible = true;
                lblNoofTrailDays.Visible = true;
            }
            else
            {
                txtNoofTrailDays.Visible = false;
                lblNoofTrailDays.Visible = false;
            }
            if (lblofferId.Text != "")
            {
                GetOfferDetails();
                ddlOffer.SelectedValue = lblofferId.Text;

            }
            txtFromdate.Text = lblfromDate.Text;
            txtTodate.Text = lbltoDate.Text;

            ViewState["subscriptionPlanId"] = lblsubscriptionPlanId.Text.Trim();
            btnPlanSubmit.Text = "Update";
            divGv.Visible = false;
            DivForm.Visible = true;

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
            Label lblsubscriptionPlanId = (Label)gvrow.FindControl("lblsubscriptionPlanId");

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
                var activeOrInActive = new subscriptionPlanActive()
                {
                    queryType = QueryType.Trim(),
                    subscriptionPlanId = lblsubscriptionPlanId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionPlanMaster/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        GetSubscriptionPlan();
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
    #region Plan PlanSubmit 
    protected void btnPlanSubmit_Click(object sender, EventArgs e)
    {
        if (btnPlanSubmit.Text == "Submit")
        {
            InsertPlan();
        }
        else
        {
            UpdatePlan();
        }
    }
    #endregion
    #region Insert Plan
    public void InsertPlan()
    {
        try
        {
            string TrailAvail;
            string NoofTrailDays;
            if (RbtnlTrailAvail.SelectedValue == "No")
            {
                TrailAvail = "N";
                NoofTrailDays = "0";
            }
            else
            {
                TrailAvail = "Y";
                NoofTrailDays = txtNoofTrailDays.Text;
            }


            string offerName = string.Empty;
            string[] offerNames;


            offerNames = ddlOffer.SelectedItem.Text.Split('~');
            offerName = offerNames[0];

            int StatusCodes;
            string ImageUrl;
       
            if (hfImageUrl.Value != "")
            {
                ImageUrl = hfImageUrl.Value;
            }
            else
            {
                if (imgpreview.ImageUrl != "" && imgpreview.ImageUrl != "~/img/Defaultupload.png")
                {
                    ImageUrl = imgpreview.ImageUrl;
                }

                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                    return;
                }

            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new subscriptionPlan()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    packageName = txtPackageName.Text,
                    noOfDays = txtNoOfDays.Text,
                    description = txtDescription.Text,
                    imageUrl = ImageUrl,
                    tax = txttax.Text,
                    taxId = ddlTax.SelectedValue,
                    actualAmount = txtactutalAmount.Text,
                    displayAmount = txtdisplayAmount.Text,
                    amount = txtprice.Text,
                    netAmount = txtNetAmount.Text,
                    offerId = ddlOffer.SelectedValue,
                    offerName = offerName,
                    fromDate = txtFromdate.Text,
                    toDate = txtTodate.Text,
                    credits = txtCredits.Text,
                    isTrialAvailable = TrailAvail,
                    noOfTrialDays = NoofTrailDays,
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionPlanMaster/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Planclear();
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        Planclear();
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Update Plan
    public void UpdatePlan()
    {
        try
        {
            string TrailAvail;
            string NoofTrailDays;
            if (RbtnlTrailAvail.SelectedValue == "No")
            {
                TrailAvail = "N";
                NoofTrailDays = "0";
            }
            else
            {
                TrailAvail = "Y";
                NoofTrailDays = txtNoofTrailDays.Text;
            }

            string offerid = string.Empty;
            string offerName = string.Empty;
            string[] offerNames;
            string fromdate = string.Empty;
            string todate = string.Empty;
            if (ddlOffer.SelectedIndex != 0)
            {
                fromdate = txtFromdate.Text;
                todate = txtTodate.Text;
                offerid = ddlOffer.SelectedValue;
                offerNames = ddlOffer.SelectedItem.Text.Split('~');
                offerName = offerNames[0];
            }
            else
            {
                fromdate = txtFromdateGV.Text;
                todate = txtTodateGV.Text;
                offerid = ddlOfferGv.SelectedValue;
                offerNames = ddlOfferGv.SelectedItem.Text.Split('~');
                offerName = offerNames[0];
            }

            int StatusCodes;
            string ImageUrl;
          
            if (hfImageUrl.Value != "")
            {
                ImageUrl = hfImageUrl.Value;
            }
            else
            {
                if (imgpreview.ImageUrl != "" && imgpreview.ImageUrl != "~/img/Defaultupload.png")
                {
                    ImageUrl = imgpreview.ImageUrl;
                }

                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                    return;
                }
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new subscriptionPlan()
                {
                    subscriptionPlanId = ViewState["subscriptionPlanId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    packageName = txtPackageName.Text,
                    noOfDays = txtNoOfDays.Text,
                    description = txtDescription.Text,
                    imageUrl = ImageUrl,
                    tax = txttax.Text,
                    taxId = ddlTax.SelectedValue,
                    amount = txtprice.Text,
                    netAmount = txtNetAmount.Text,
                    actualAmount = txtactutalAmount.Text,
                    displayAmount = txtdisplayAmount.Text,
                    offerId = offerid,
                    offerName = offerName,
                    fromDate = fromdate,
                    toDate = todate,
                    credits = txtCredits.Text,
                    isTrialAvailable = TrailAvail,
                    noOfTrialDays = NoofTrailDays,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionPlanMaster/update", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Planclear();
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        Planclear();
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Clear
    public void Planclear()
    {
        txtDescription.Text = "";
        txtactutalAmount.Text = "";
        txtdisplayAmount.Text = "";
        txtNoofTrailDays.Text = "";
        txtPackageName.Text = "";
        txtNetAmount.Text = "";
        ddlOffer.ClearSelection();
        txtCredits.Text = "";
        txtNoOfDays.Text = "";
        imgpreview.ImageUrl = "~/img/Defaultupload.png";
        btnPlanSubmit.Text = "Submit";
        ddlTax.SelectedValue = "0";
        txtprice.Text = "";
        txttax.Text = "";
        RbtnlTrailAvail.SelectedValue = "Yes";
        txtNoofTrailDays.Visible = true;
        lblNoofTrailDays.Visible = true;
    }
    public void Benefiteclear()
    {
        txtsubDescription.Text = "";
        imgpreviewSub.ImageUrl = "~/img/Defaultupload.png";
        btnSubSubmit.Text = "Submit";
    }
    #endregion
    #region subscriptionPlan Insert & Update Classes
    public class subscriptionPlan
    {
        public string subscriptionPlanId { get; set; }
        public string packageName { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string noOfDays { get; set; }
        public string tax { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string taxId { get; set; }
        public string amount { get; set; }
        public string netAmount { get; set; }
        public string actualAmount { get; set; }
        public string displayAmount { get; set; }
        public string offerId { get; set; }
        public string offerName { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string credits { get; set; }
        public string isTrialAvailable { get; set; }
        public string noOfTrialDays { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    public class subscriptionPlanActive
    {
        public string queryType { get; set; }
        public string subscriptionPlanId { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    #region Get SubBenefit
    public void GetSubBenefit()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "subscriptionBenefits?subscriptionPlanId="
                             + ViewState["subscriptionPlanId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvSubBenefit.DataSource = dt;
                        gvSubBenefit.DataBind();
                        divBenefits.Visible = true;
                    }
                    else
                    {
                        divBenefits.Visible = false;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Add Benefits  Click Event
    protected void linkAddDetails_Click(object sender, EventArgs e)
    {

        txtsubDescription.Text = string.Empty;
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblsubscriptionPlanId = (Label)gvrow.FindControl("lblsubscriptionPlanId");
        Label lblpackageName = (Label)gvrow.FindControl("lblpackageName");
        BenefitplanName.InnerHtml = "Plan Name : " + lblpackageName.Text.Trim();
        ViewState["subscriptionPlanId"] = lblsubscriptionPlanId.Text.Trim();
        imgpreviewSub.ImageUrl = "~/img/Defaultupload.png";
        GetSubBenefit();
        AddBenefits.Visible = true;
        btnSubSubmit.Text = "Submit";
    }
    #endregion
    #region Benefits Cancel Click Event
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        txtsubDescription.Text = string.Empty;
        AddBenefits.Visible = false;

    }
    #endregion
    #region Benefits  Submit Click Event
    protected void btnSubSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubSubmit.Text == "Submit")
        {
            InsertBenefit();
        }
        else
        {
            UpdateBenefit();
        }
    }
    #endregion
    #region SubBenefit Insert & Update Classes
    public class SubBenefit
    {
        public string uniqueId { get; set; }
        public string subscriptionPlanId { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    public class SubBenefitActive
    {
        public string queryType { get; set; }
        public string uniqueId { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    #region Benefeits Edit  Click Event
    protected void LnkEditBenefeits_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

        Label lblSubBenefitId = (Label)gvrow.FindControl("lblSubBenefitId");
        Label lblsubscriptionPlanId = (Label)gvrow.FindControl("lblsubscriptionPlanId");
        Label lbldescription = (Label)gvrow.FindControl("lbldescription");
        Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");

        txtsubDescription.Text = lbldescription.Text.Trim();
        imgpreviewSub.ImageUrl = lblimageUrl.Text.Trim();


        ViewState["SubBenefitId"] = lblSubBenefitId.Text.Trim();
        btnSubSubmit.Text = "Update";
    }
    #endregion
    #region Benefeits Active or Inactive  Click Event
    protected void lnkActiveOrInactiveBenefeits_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblSubBenefitId = (Label)gvrow.FindControl("lblSubBenefitId");

        LinkButton lblActiveStatus = (LinkButton)lnkbtn.FindControl("lnkActiveOrInactiveBenefeits");
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
            var activeOrInActive = new SubBenefitActive()
            {
                queryType = QueryType.Trim(),
                uniqueId = lblSubBenefitId.Text.Trim(),
                updatedBy = Session["userId"].ToString()
            };
            HttpResponseMessage response = client.PostAsJsonAsync("subscriptionBenefits/activeOrInActive", activeOrInActive).Result;
            if (response.IsSuccessStatusCode)
            {
                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                if (StatusCode == 1)
                {
                    GetSubBenefit();
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
    #endregion
    #region Insert Benefit
    public void InsertBenefit()
    {
        try
        {
            int SCode;
            string Response;
            //helper.UploadImage(FileUpload1, Session["BaseUrl"].ToString().Trim() + "UploadImage", out SCode, out Response);
            //if (SCode == 0)
            //{
            //    Response = null;
            //}

            if (hfImageUrlsub.Value != "")
            {
                Response = hfImageUrlsub.Value;
            }
            else
            {
                if (imgpreviewSub.ImageUrl != "" && imgpreviewSub.ImageUrl != "~/img/Defaultupload.png")
                {
                    Response = imgpreviewSub.ImageUrl;
                }

                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                    return;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new SubBenefit()
                {
                    subscriptionPlanId = ViewState["subscriptionPlanId"].ToString(),
                    description = txtsubDescription.Text,
                    imageUrl = Response,
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionBenefits/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Benefiteclear();
                        GetSubBenefit();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Update Benefit
    public void UpdateBenefit()
    {
        try
        {
            int SCode;
            string Response;
            //if (FileUpload1.HasFile)
            //{

            //    helper.UploadImage(FileUpload1, Session["BaseUrl"].ToString().Trim() + "UploadImage", out SCode, out Response);
            //    if (SCode == 0)
            //    {
            //        Response = null;
            //    }
            //}
            //else
            //{
            //    Response = imgpreviewSub.Src;
            //}
            if (hfImageUrlsub.Value != "")
            {
                Response = hfImageUrlsub.Value;
            }
            else
            {
                if (imgpreviewSub.ImageUrl != "" && imgpreviewSub.ImageUrl != "~/img/Defaultupload.png")
                {
                    Response = imgpreviewSub.ImageUrl;
                }

                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                    return;
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new SubBenefit()
                {
                    uniqueId = ViewState["SubBenefitId"].ToString(),
                    subscriptionPlanId = ViewState["subscriptionPlanId"].ToString(),
                    description = txtsubDescription.Text,
                    imageUrl = Response,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionBenefits/update", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Benefiteclear();
                        GetSubBenefit();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Selected Index Changed
    protected void RbtnlTrailAvail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbtnlTrailAvail.SelectedValue == "Yes")
        {
            txtNoofTrailDays.Visible = true;
            lblNoofTrailDays.Visible = true;
            RfvNoofTailDays.Enabled = true;
        }
        else
        {
            txtNoofTrailDays.Visible = false;
            lblNoofTrailDays.Visible = false;
            RfvNoofTailDays.Enabled = false;
        }
        if (hfImageUrl.Value != "")
        {
            imgpreview.ImageUrl = hfImageUrl.Value;
        }
        else
        {
            if (imgpreview.ImageUrl != "" && imgpreview.ImageUrl != "~/img/Defaultupload.png")
            {
                imgpreview.ImageUrl = imgpreview.ImageUrl;
            }
        }
    }

    #endregion
    #region Get OfferDetails
    public void GetOfferDetails()
    {
        try
        {
            ddlOffer.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string sUrl = Session["BaseUrl"].ToString().Trim() + "offerMapping/GetMstrOfferMappingUser?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                     "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
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
                            ddlOffer.DataSource = dt;
                            ddlOffer.DataTextField = "OfferValueType";
                            ddlOffer.DataValueField = "offerId";
                            ddlOffer.DataBind();
                            ddlOfferGv.DataSource = dt;
                            ddlOfferGv.DataTextField = "OfferValueType";
                            ddlOfferGv.DataValueField = "offerId";
                            ddlOfferGv.DataBind();

                            Session["offer"] = dt;
                        }
                        else
                        {
                            ddlOffer.DataBind();
                            ddlOfferGv.DataBind();
                        }

                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlOffer.Items.Insert(0, new ListItem("Offer *", "0"));
                    ddlOfferGv.Items.Insert(0, new ListItem("Offer *", "0"));
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    ddlOffer.Items.Insert(0, new ListItem("Offer *", "0"));
                    ddlOfferGv.Items.Insert(0, new ListItem("Offer *", "0"));
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
    #region Offer selected indexx changed 
    protected void ddlOffer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOffer.SelectedIndex != 0)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["offer"];
            var offerdetails = dt.AsEnumerable().Where(x => x.Field<Int64>("offerId") == Convert.ToInt64(ddlOffer.SelectedValue)).CopyToDataTable();
            decimal actual = Convert.ToDecimal(txtactutalAmount.Text);
            decimal display;
            decimal offervalue;
            offervalue = Convert.ToDecimal(offerdetails.Rows[0]["offerValue"].ToString());
            if (offerdetails.Rows[0]["offerType"].ToString() == "F")
            {
                display = actual - offervalue - 1;
            }
            else
            {
                display = (actual * offervalue) / 100 - 1;
            }

            txtdisplayAmount.Text = display.ToString();
            txtNetAmount.Text = display.ToString();
            if (ViewState["TaxPercentage"] != null && ViewState["TaxPercentage"].ToString() != "" && txtNetAmount.Text != "")
            {
                if (txtNetAmount.Text != "0.00" && txtNetAmount.Text != "0")
                {
                    GetNetAmount();
                }
                else
                {
                    txtNetAmount.Text = "";
                    txtprice.Text = "";
                    txttax.Text = "";
                }
            }
        }
        else
        {
            txtdisplayAmount.Text = "";
            txtNetAmount.Text = "";
        }

    }
    #endregion


    #region btn Submit Event Gridview
    protected void btnSubGV_Click(object sender, EventArgs e)
    {
        if (txtFromdate.Text != "" && txtTodate.Text != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter FromDate And ToDate');", true);
        }

        if (ddlOfferGv.SelectedIndex != 0)
        {
            int select = 0;

            foreach (GridViewRow item in gvSubscriptionPlan.Rows)
            {
                CheckBox chkItem = item.FindControl("chkItem") as CheckBox;
                Label lblactiveStatusgv = (Label)item.FindControl("lblactiveStatusgv");
                if (lblactiveStatusgv.Text == "A")
                {
                    if (chkItem.Checked == true)
                    {
                        select = 1;
                        Label lblsubscriptionPlanId = (Label)item.FindControl("lblsubscriptionPlanId");
                        Label lblpackageName = (Label)item.FindControl("lblpackageName");
                        Label lbldescription = (Label)item.FindControl("lbldescription");
                        Label lblimageUrl = (Label)item.FindControl("lblimageUrl");
                        Label lbltaxId = (Label)item.FindControl("lbltaxId");
                        Label lblnetAmount = (Label)item.FindControl("lblnetAmount");
                        Label lblactualAmount = (Label)item.FindControl("lblactualAmount");
                        Label lbldisplayAmount = (Label)item.FindControl("lbldisplayAmount");
                        Label lblamount = (Label)item.FindControl("lblamount");
                        Label lblnoOfDays = (Label)item.FindControl("lblnoOfDays");
                        Label lbltax = (Label)item.FindControl("lbltax");
                        Label lbltaxName = (Label)item.FindControl("lbltaxName");
                        Label lblcredits = (Label)item.FindControl("lblcredits");
                        Label lblcgstTax = (Label)item.FindControl("lblcgstTax");
                        Label lblsgstTax = (Label)item.FindControl("lblsgstTax");
                        Label lblisTrialAvailable = (Label)item.FindControl("lblisTrialAvailable");
                        Label lblnoOfTrialDays = (Label)item.FindControl("lblnoOfTrialDays");
                        Label lblofferId = (Label)item.FindControl("lblofferId");
                        Label lblofferName = (Label)item.FindControl("lblofferName");
                        Label lblfromDate = (Label)item.FindControl("lblfromDate");
                        Label lbltoDate = (Label)item.FindControl("lbltoDate");

                        txtDescription.Text = lbldescription.Text.Trim();
                        txtPackageName.Text = lblpackageName.Text.Trim();
                        txtNoOfDays.Text = lblnoOfDays.Text.Trim();
                        txtCredits.Text = lblcredits.Text.Trim();
                        txtNoOfDays.Text = lblnoOfDays.Text.Trim();
                        txtNoofTrailDays.Text = lblnoOfTrialDays.Text.Trim();
                        imgpreview.ImageUrl = lblimageUrl.Text.Trim();
                        ddlTax.SelectedValue = lbltaxId.Text.Trim();
                        txtprice.Text = lblamount.Text.Trim();
                        txttax.Text = lbltax.Text.Trim();
                        txtNetAmount.Text = lblnetAmount.Text;
                        txtactutalAmount.Text = lblactualAmount.Text;
                        txtdisplayAmount.Text = lbldisplayAmount.Text;

                        if (lblisTrialAvailable.Text.Trim() == "N")
                        {
                            RbtnlTrailAvail.SelectedValue = "No";
                        }
                        else
                        {
                            RbtnlTrailAvail.SelectedValue = "Yes";
                        }
                        if (RbtnlTrailAvail.SelectedValue == "Yes")
                        {
                            txtNoofTrailDays.Visible = true;
                            lblNoofTrailDays.Visible = true;
                        }
                        else
                        {
                            txtNoofTrailDays.Visible = false;
                            lblNoofTrailDays.Visible = false;
                        }
                 
                        txtFromdate.Text = lblfromDate.Text;
                        txtTodate.Text = lbltoDate.Text;

                        ViewState["subscriptionPlanId"] = lblsubscriptionPlanId.Text.Trim();
                        DataTable dt = new DataTable();
                        dt = (DataTable)Session["offer"];
                        var offerdetails = dt.AsEnumerable().Where(x => x.Field<Int64>("offerId") == Convert.ToInt64(ddlOfferGv.SelectedValue)).CopyToDataTable();
                        decimal actual = Convert.ToDecimal(txtactutalAmount.Text);
                        decimal display;
                        decimal offervalue;
                        offervalue = Convert.ToDecimal(offerdetails.Rows[0]["offerValue"].ToString());
                        if (offerdetails.Rows[0]["offerType"].ToString() == "F")
                        {
                            display = actual - offervalue - 1;
                        }
                        else
                        {
                            display = (actual * offervalue) / 100 - 1;
                        }

                        txtdisplayAmount.Text = display.ToString();
                        txtNetAmount.Text = display.ToString();
                        string[] Tp;
                        string[] Percentage;
                        Tp = ddlTax.SelectedItem.Text.Split('-');
                        Percentage = Tp[1].Split(',');
                        ViewState["TaxPercentage"] = Percentage[0];

                        if (ViewState["TaxPercentage"] != null && ViewState["TaxPercentage"].ToString() != "" && txtNetAmount.Text != "")
                        {
                            if (txtNetAmount.Text != "0.00" && txtNetAmount.Text != "0")
                            {
                                GetNetAmount();
                            }
                            else
                            {
                                txtNetAmount.Text = "";
                                txtprice.Text = "";
                                txttax.Text = "";
                            }
                        }
                        UpdatePlan();
                    }
                    else
                    {

                    }

                }

                else
                {

                }

            }
            ddlOfferGv.ClearSelection();
            if (select == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Plan');", true);
            }

        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Offer');", true);
        }
    }
    #region Btn cancel EVent
    protected void btnCancelGV_Click(object sender, EventArgs e)
    {
        ddlOfferGv.ClearSelection();
        GetSubscriptionPlan();

        txtFromdateGV.Text = string.Empty;
        txtTodateGV.Text = string.Empty;
    }
    #endregion
    #endregion
}