﻿<%@ Page Title="Food Diet Time" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="FoodDietTime.aspx.cs" Inherits="Master_DeitSetup_FoodDietTime" %>

<asp:Content ID="CntFoodDietTime" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .CheckBoxList {
            font-size: 0.7rem;
            white-space: nowrap;
            display: flex;
            justify-content: space-between;
            font-weight: 900;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Common Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Food Menu Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Food Diet Time Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Food Diet Time <span>Master</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlmealType" CssClass="form-select" runat="server" TabIndex="1" >
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvmealType" InitialValue="0" ValidationGroup="DietTime" ControlToValidate="ddlmealType" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Meal Type">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:CheckBoxList ID="chckfoodItem" CssClass="CheckBoxList" TabIndex="2"  runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                    </asp:CheckBoxList>                  

                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" TabIndex="3"  CssClass="btnSubmit" ValidationGroup="DietTime" runat="server" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="4" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Food Diet Time <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvFoodDietTimeMstr" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter" DataKeyNames="mealType" OnRowDataBound="gvFoodDietTimeMstr_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.no.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Meal Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblmealTypeName" runat="server" Text='<%#Bind("mealTypeName") %>' Visible="true"></asp:Label>
                                <asp:Label ID="lblmealType" runat="server" Text='<%#Bind("mealType") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Food Diet List" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:DataList runat="server" ID="dlBreakFast" RepeatDirection="Vertical" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfoodItemId" runat="server" Text='<%# Bind("foodItemId") %>' Font-Bold="true" Visible="false" Width="100px"></asp:Label>
                                        <asp:Label ID="lblgvfoodItemName" runat="server" Text='<%# Bind("foodItemName") %>' Font-Bold="true" Width="100px"></asp:Label>
                                        <asp:Label ID="lblgvruleType" runat="server" Text='<%# Bind("mealType") %>' Font-Bold="true" Visible="false" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    OnClick="LnkEdit_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>

