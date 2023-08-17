<%@ Page Title="Subscription Plan" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="SubscriptionPlan.aspx.cs" Inherits="Master_SubscriptionPlan" %>

<asp:Content ID="CtnSubscriptionPlan" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function SelectAllCheckboxes(chk, selector) {
            $('#<%=gvSubscriptionPlan.ClientID%>').find(selector + " input:checkbox").each(function () {
                ;
                $(this).prop("checked", $(chk).prop("checked"));

            });
        }
    </script>
    <style>
        .ddl {
            padding: 1rem;
            border: 1px dashed;
            margin-bottom: 0.5rem;
            border-radius: 1rem;
        }

        .profile {
            margin-bottom: 0.5rem;
            color: #202124;
            font-size: 1rem;
            margin-left: 2rem;
        }

        .imgpreview {
            width: 100px;
            height: 100px;
            margin-bottom: 0.2rem;
            border-radius: 4rem;
            margin-left: 1rem;
            border: 1px solid;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="Label1" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Subscription Plan"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Subscription <span>Plan</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9">
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtPackageName" MaxLength="50" AutoComplete="off" TabIndex="1" CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Package Name <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvShortName" ValidationGroup="Subscription" ControlToValidate="txtPackageName" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Package Name">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" AutoComplete="off" TabIndex="2" CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Description <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvDescription" ValidationGroup="Subscription" ControlToValidate="txtDescription" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Description">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtNoOfDays" MaxLength="12" TabIndex="3" AutoComplete="off" onkeypress="return isNumber(event);" CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">No Of Days <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvNoOfDays" ValidationGroup="Subscription" ControlToValidate="txtNoOfDays" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter No Of Days">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <asp:DropDownList ID="ddlTax" AutoPostBack="true" OnSelectedIndexChanged="ddlTax_SelectedIndexChanged" TabIndex="4"
                                CssClass="form-select" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="Rfvtax" ValidationGroup="Subscription"
                                ControlToValidate="ddlTax" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Select Tax" InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtactutalAmount" runat="server" AutoComplete="off" TabIndex="6"
                                    CssClass="txtbox" placeholder=" " MaxLength="7" onkeypress="return AllowOnlyAmountAndDot(this.id);" />
                                <asp:Label CssClass="txtlabel" runat="server">Actutal Amount  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Subscription" ControlToValidate="txtactutalAmount" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Actutal Amount">
                            </asp:RequiredFieldValidator>
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <asp:DropDownList ID="ddlOffer" AutoPostBack="true" OnSelectedIndexChanged="ddlOffer_SelectedIndexChanged"
                                CssClass="form-select" runat="server" TabIndex="5">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="FCPrice"
                        ControlToValidate="ddlOffer" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Offer" InitialValue="0">
                    </asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtFromdate" TabIndex="1" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">From Date</asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="FCPrice"
                                    ControlToValidate="txtFromdate" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter From Date">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <div class="txtboxdiv">

                                <asp:TextBox ID="txtTodate" TabIndex="2" CssClass="txtbox ConverttoDate" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">To Date</asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="FCPrice"
                                    ControlToValidate="txtTodate" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter To Date">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>



                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtdisplayAmount" runat="server" TabIndex="6" AutoComplete="off"
                                    CssClass="txtbox" placeholder=" " MaxLength="7" onkeypress="return AllowOnlyAmountAndDot(this.id);" />
                                <asp:Label CssClass="txtlabel" runat="server">Display Amount  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Subscription" ControlToValidate="txtdisplayAmount" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Display Amount">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtNetAmount" runat="server" TabIndex="6" AutoComplete="off" AutoPostBack="true"
                                    OnTextChanged="txtNetAmount_TextChanged"
                                    CssClass="txtbox" placeholder=" " MaxLength="7" onkeypress="return AllowOnlyAmountAndDot(this.id);" />
                                <asp:Label CssClass="txtlabel" runat="server">Net Amount  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvnetAmount" ValidationGroup="Subscription" ControlToValidate="txtNetAmount" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Net Amount">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtprice" MaxLength="12" CssClass="txtbox" runat="server" AutoComplete="off" TabIndex="6"
                                    onkeypress="return AllowOnlyAmountAndDot(this.id);" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Price  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Subscription" ControlToValidate="txtprice" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Price">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txttax" MaxLength="12" CssClass="txtbox" AutoComplete="off" runat="server" TabIndex="7"
                                    onkeypress="return AllowOnlyAmountAndDot(this.id);" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Tax Amount  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Subscription" ControlToValidate="txttax" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Tax Amount">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtCredits" MaxLength="12" onkeypress="return isNumber(event);" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="8" />
                                <asp:Label CssClass="txtlabel" runat="server">Credits  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Subscription" ControlToValidate="txtCredits" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Credits">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="lblstyle">Is Trail Available <span class="reqiredstar">*</span></asp:Label>
                            <asp:RadioButtonList ID="RbtnlTrailAvail" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RbtnlTrailAvail_SelectedIndexChanged" CssClass="frmcheckbox" TabIndex="9" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Subscription" ControlToValidate="RbtnlTrailAvail" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Select Is Trail Available">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtNoofTrailDays" MaxLength="12" onkeypress="return isNumber(event);" AutoComplete="off" TabIndex="10"
                                    CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" ID="lblNoofTrailDays" runat="server">No Of Trail Days  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvNoofTailDays" ValidationGroup="Subscription" ControlToValidate="txtNoofTrailDays" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter No of Trail Days">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-3 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-3 mb-12">
                            <div class="profile">Image</div>
                            <asp:Image ID="imgpreview" class="imgpreview" TabIndex="1"
                                ClientIDMode="Static" runat="server" ImageUrl="~/img/Defaultupload.png" />
                            <%--<img id="imgpreview" class="imgpreview" clientidmode="Static" tabindex="11" runat="server" src="~/img/Defaultupload.png" />--%>
                            <asp:FileUpload ID="fuimage" CssClass="mx-4" runat="server" TabIndex="12" onchange="showpreview(this);" />
                        </div>
                    </div>
                </div>
            </div>


            <div class="float-end">
                <asp:Button CssClass="btnSubmit" ID="btnPlanSubmit" ValidationGroup="Subscription" TabIndex="13"
                    runat="server" Text="Submit" OnClick="btnPlanSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" TabIndex="14"
                    Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Subscription <span>Plan</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlOfferGv"
                        CssClass="form-select" runat="server" TabIndex="5">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="FCPriceGV"
                        ControlToValidate="ddlOfferGv" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Offer" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFromdateGV" TabIndex="1" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">From Date</asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="FCPriceGV"
                            ControlToValidate="txtFromdateGV" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter From Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">

                        <asp:TextBox ID="txtTodateGV" TabIndex="2" CssClass="txtbox ConverttoDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">To Date</asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="FCPriceGV"
                            ControlToValidate="txtTodateGV" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter To Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvSubscriptionPlan" runat="server" DataKeyNames="subscriptionPlanId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.no.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="subscriptionPlanId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblsubscriptionPlanId" runat="server" Text='<%#Bind("subscriptionPlanId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Package Name" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblpackageName" runat="server" Text='<%#Bind("packageName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="imageUrl" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Days" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblnoOfDays" runat="server" Text='<%#Bind("noOfDays") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="taxId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltaxId" runat="server" Text='<%#Bind("taxId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblnetAmount" runat="server" Text='<%#Bind("netAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblactualAmount" runat="server" Text='<%#Bind("actualAmount") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldisplayAmount" runat="server" Text='<%#Bind("displayAmount") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblofferId" runat="server" Text='<%#Bind("offerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblofferName" runat="server" Text='<%#Bind("offerName") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblactiveStatusgv" runat="server" Text='<%#Bind("activeStatus") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblfromDate" runat="server" Text='<%#Bind("fromDate") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltoDate" runat="server" Text='<%#Bind("toDate") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="amount" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblamount" runat="server" Text='<%#Bind("amount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="tax" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltax" runat="server" Text='<%#Bind("tax") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="taxName" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltaxName" runat="server" Text='<%#Bind("taxName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="credits" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcredits" runat="server" Text='<%#Bind("credits") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="cgstTax" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcgstTax" runat="server" Text='<%#Bind("cgstTax") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="sgstTax" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblsgstTax" runat="server" Text='<%#Bind("sgstTax") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="isTrialAvailable" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblisTrialAvailable" runat="server" Text='<%#Bind("isTrialAvailable") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="noOfTrialDays" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblnoOfTrialDays" runat="server" Text='<%#Bind("noOfTrialDays") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkEdit_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactive_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Add Benefits" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkAddDetails" Text="Add"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>'
                                    OnClick="linkAddDetails_Click" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Offer" HeaderStyle-CssClass="gvHeader">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAllCheckboxes(this,'.FormRights');" />
                                <br />
                                Offer
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" CssClass="FormRights" Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubGV" CssClass="btnSubmit" TabIndex="9" ValidationGroup="FCPriceGV" runat="server"
                    Text="Submit" OnClick="btnSubGV_Click" OnClientClick="return confirm('Are sure you want to continue');" />
                <asp:Button ID="btnCancelGV" CssClass="btnCancel" CausesValidation="false" runat="server" TabIndex="10"
                    Text="Cancel" OnClick="btnCancelGV_Click" />
            </div>
        </div>
    </div>
    <div id="AddBenefits" runat="server" class="DisplyCard" visible="false">
        <div class="DisplyCardPostion table-responsive">
            <div class="PageHeader" style="margin-top: -25px">
                <h5>Subscription <span>Benefits</span>
                    <a onclick="btnClose()" class="float-end btnclose">
                        <i class="fa-solid fa-x"></i></a>
                </h5>
                <div class="text-start">
                    <a class="addlblHead" id="BenefitplanName" runat="server"></a>
                </div>
            </div>
            <div class="row" style="margin-top: -20px">
                <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtsubDescription" TextMode="MultiLine" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Description <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="SubBenefits" ControlToValidate="txtsubDescription" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Description">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="profile">Image</div>
                    <asp:Image ID="imgpreviewSub" class="imgpreview" TabIndex="1"
                        ClientIDMode="Static" runat="server" ImageUrl="~/img/Defaultupload.png" />
                    <%-- <img id="imgpreviewSub" class="imgpreview" clientidmode="Static" runat="server" src="~/img/Defaultupload.png" />--%>
                    <asp:FileUpload ID="FileUpload1" CssClass="mx-4" runat="server" onchange="showpreviewsub(this);" />
                </div>
            </div>
            <div class="text-end">
                <asp:Button ID="btnSubSubmit" CssClass="btnSubmit" ValidationGroup="SubBenefits" OnClick="btnSubSubmit_Click" runat="server" Text="Submit" />
                <asp:Button ID="btnSubCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnSubCancel_Click" />
            </div>

            <hr />
            <div id="divBenefits" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvSubBenefit" Style="font-size: 0.8rem" runat="server" DataKeyNames="SubBenefitId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.no.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SubBenefitId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblSubBenefitId" runat="server" Text='<%#Bind("SubBenefitId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="subscriptionPlanId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblsubscriptionPlanId" runat="server" Text='<%#Bind("subscriptionPlanId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="imageUrl" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEditBenefeits"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkEditBenefeits_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactiveBenefeits"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactiveBenefeits_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfImageUrl" EnableViewState="true" runat="server" />
    <asp:HiddenField ID="hfImageUrlsub" EnableViewState="true" runat="server" />
    <script type="text/javascript">

        function showpreview(input) {
            debugger;
            var fup = document.getElementById("<%=fuimage.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgpreview.ClientID%>').prop('src', e.target.result);

                        };
                        reader.readAsDataURL(input.files[0]);
                        var formData = new FormData()
                        formData.append("file", $('input[type=file]')[0].files[0])
                        $.ajax({
                            url: '<%= Session["ImageUrl"].ToString() %>',
                            type: 'POST',
                            data: formData,
                            contentType: false,
                            processData: false,
                            success: function (image) {
                                $('#<%=hfImageUrl.ClientID%>').val(image.image);
                                console.log(image);
                            },
                            error: function (image) {
                                alert('Error');
                            }
                        });
                    }
                }
                else {
                    swal("Please, Upload image file less than or equal to 1 MB !!!");
                    fup.focus();
                    return false;
                }
            }
            else {
                swal("Please, Upload Gif, Jpg, Jpeg or Bmp Images only !!!");
                fup.focus();
                return false;
            }
        }

        function showpreviewsub(input) {
            debugger;
            var fup = document.getElementById("<%=FileUpload1.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgpreviewSub.ClientID%>').prop('src', e.target.result);

                        };
                        reader.readAsDataURL(input.files[0]);
                        var formData = new FormData()
                        formData.append("file", $('input[type=file]')[0].files[0])
                        $.ajax({
                            url: '<%= Session["ImageUrl"].ToString() %>',
                            type: 'POST',
                            data: formData,
                            contentType: false,
                            processData: false,
                            success: function (image) {
                                $('#<%=hfImageUrlsub.ClientID%>').val(image.image);
                                console.log(image);
                            },
                            error: function (image) {
                                alert('Error');
                            }
                        });
                    }
                }
                else {
                    swal("Please, Upload image file less than or equal to 1 MB !!!");
                    fup.focus();
                    return false;
                }
            }
            else {
                swal("Please, Upload Gif, Jpg, Jpeg or Bmp Images only !!!");
                fup.focus();
                return false;
            }
        }
        function btnClose() {
            $('#<%= AddBenefits.ClientID %>').css("display", "none");
        }
    </script>
</asp:Content>

