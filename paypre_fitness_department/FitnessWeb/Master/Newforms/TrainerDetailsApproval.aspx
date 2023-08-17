﻿<%@ Page Title="Trainer Details" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="TrainerDetailsApproval.aspx.cs" Inherits="Master_Newforms_TrainerDetailsApproval" %>

<asp:Content ID="CntTrainerDetails" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
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
            margin-top: -1rem;
        }

        .lblCertificate {
            font-size: 0.8rem;
            color: black;
            margin-top: -1rem;
            margin-bottom: 1.5rem;
            margin-left: -4rem;
        }

        .C1 {
            position: fixed !important;
            z-index: 2 !important;
            margin-left: 20rem !important;
            opacity: 5;
            top: 15%;
            left: 30%;
            bottom: 0%;
            transform: translateX(-50%);
            background: #000000d6;
            height: 36rem !important;
            transition: all 0.3s ease-in-out !important;
            width: 60% !important;
        }

        .CClose {
            color: black;
            font-size: 2rem;
            position: fixed;
            z-index: 3;
            top: 18%;
            left: 80%;
            font-weight: 500;
        }

        .imgC1 {
            margin-left: 15rem !important;
        }

        .clickable-row {
            cursor: pointer;
            background-color: #f5f5f5;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Trainer Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Trainer Details"></asp:Label>
        </div>
    </div>

    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Trainer Details</h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlSpecialistType" TabIndex="2" CssClass="form-select" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        ControlToValidate="ddlSpecialistType" runat="server" CssClass="rfvStyle"
                        ValidationGroup="TrainerDetails" InitialValue="0" ErrorMessage="Select Specialist Type">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtExperience" AutoComplete="off" MaxLength="50" TabIndex="3" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Experience <span class="reqiredstar">*</span></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="TrainerDetails"
                            ControlToValidate="txtExperience" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Experience">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtQualification" AutoComplete="off" MaxLength="50" TabIndex="4" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Qualification <span class="reqiredstar">*</span></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="TrainerDetails"
                            ControlToValidate="txtQualification" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Qualification">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="text-center">
                        <label class="lblCertificate">Certificates <span class="reqiredstar">*</span></label><br />
                    </div>
                    <%-- <label id="CCloseBtn" onclick="CClose()" class="CClose d-none">X</label>--%>
                    <asp:Image ID="imgEmpPhotoPrev" class="imgpreview" TabIndex="5" ClientIDMode="Static" runat="server" ImageUrl="~/img/User.png" />
                    <asp:FileUpload ID="Fuimage" CssClass="mx-4" TabIndex="6" runat="server" onchange="ShowImagePreview(this);" />
                </div>
            </div>

            <div class="float-end">
                <asp:Button ID="btnSubmit" TabIndex="21" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="TrainerDetails" />
                <asp:Button ID="btnCancel" TabIndex="22" CssClass="btnCancel" CausesValidation="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Trainer Details</h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd" Visible="false">
                         <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">

                <asp:GridView ID="gvTrainerDetails" runat="server" DataKeyNames="trainerId" AutoGenerateColumns="false" OnRowCreated="gvTrainerDetails_RowCreated"
                    OnRowDataBound="gvTrainerDetails_RowDataBound" CssClass="table table-striped table-hover border"  PageSize="2500">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.no.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Trainer Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltrainerId" runat="server" Text='<%#Bind("trainerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltrainerName" runat="server" Text='<%#Bind("trainerName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specialist Type" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgvspecialistTypeId" runat="server" Text='<%#Bind("specialistTypeId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblgvspecialistTypeName" runat="server" Text='<%#Bind("specialistTypeName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Experience" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblexperience" runat="server" Text='<%#Bind("experience") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblqualification" runat="server" Text='<%#Bind("qualification") %>' Visible="true"></asp:Label>
                                <%--<asp:Label ID="lblcertificates" runat="server" Text='<%#Bind("certificates") %>' Visible="false"></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <label id="CCloseBtn" onclick="CClose()" class="CClose d-none">X</label>
                                <%--<asp:Image ID="lblcertificates" runat="server" onclick="C1()" ClientIDMode="Static" ImageUrl='<%# Eval("certificates") %>' Width="50" Height="50" />--%>
                                <asp:Image ID="lblcertificates" runat="server" ClientIDMode="Static" ImageUrl='<%# Eval("certificates") %>' Width="50" Height="50" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Approved Details" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("approvedStatus").ToString() =="Y"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("approvedStatus").ToString() =="Y"?"Approved":"Not-Approved"%>' OnClick="lnkActiveOrInactive_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    OnClick="LnkEdit_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                    </Columns>
                    <RowStyle CssClass="clickable-row" />
                </asp:GridView>
            </div>
        </div>

    </div>

    <asp:HiddenField ID="gridcount" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">
        function ShowImagePreview(input) {

            var fup = document.getElementById("<%=Fuimage.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgEmpPhotoPrev.ClientID%>').prop('src', e.target.result);

                        };
                        reader.readAsDataURL(input.files[0]);

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
    </script>

    <script>
        var close = 0;
    
        function CClose() {
            debugger
            close = 1;
            document.getElementsByClassName('C1')[0].classList.remove("C1");
            document.getElementById('CCloseBtn').classList.add('d-none')


        }
        window.onload = function () {
            debugger
            var rows = document.querySelectorAll('.clickable-row');
            var gridrows = document.getElementById("gridcount").value;
            for (var i = 0; i < rows.length; i++) {
                rows[i].onclick = function () {
                    debugger
                    if (close != 1) {
                        var index = this.rowIndex - 1; // subtract 1 to account for the header row
                        // alert("Clicked row index: " + index);
                        document.getElementById('lblcertificates_' + index).classList.add("C1");
                        document.getElementById('CCloseBtn').classList.remove('d-none')
                    }
                    else {
                        close = 0;
                    }
                }
            }
        }
    </script>
</asp:Content>

