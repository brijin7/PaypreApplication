﻿<%@ Page Title="Branch Gallery" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="BranchGalleryMstr.aspx.cs" Inherits="Master_Branch_BranchGalleryMstr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="CntBranchGallery" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            margin-left: 1rem;
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
            <asp:Label ID="Label1" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Branch Gallery"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Branch <span>Gallery</span></h5>
            </div>
            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlGalleryType" CssClass="form-select" runat="server" TabIndex="17">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvPaymentType" InitialValue="0" ValidationGroup="UserEnroll" ControlToValidate="ddlGalleryType" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Gallery  Type">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtdescription" TabIndex="2" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Description</asp:Label>
                            <asp:RequiredFieldValidator ID="RfvWorkOutType" ValidationGroup="UserEnroll" ControlToValidate="txtdescription" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Description">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="profile">Gallery Image <span class="reqiredstar">*</span></div>
                        <asp:Image ID="imgGalleryPhotoPrev" class="imgpreview" TabIndex="1" runat="server" ImageUrl="~/img/Defaultupload.png"
                            ClientIDMode="Static"  />
                      
                        <asp:FileUpload ID="Fuimage" CssClass="mx-4" TabIndex="2" runat="server" onchange="ShowImagePreview(this);" />
                    </div>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" TabIndex="3" CssClass="btnSubmit" runat="server"
                    Text="Submit" CausesValidation="true" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" TabIndex="4" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Branch <span>Gallery</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                              <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvBranchGalleryMstr"
                    runat="server" DataKeyNames="imageId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.no.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Gallery Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgalleryname" runat="server" Text='<%#Bind("galleryname") %>' Visible="true"></asp:Label>
                                <asp:Label ID="lblgalleryId" runat="server" Text='<%#Bind("galleryId") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblimageId" runat="server" Text='<%#Bind("imageId") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>' Visible="false"></asp:Label>
                                <asp:Image ID="previewimageUrl" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Width="100" Height="100" />
                                <asp:Label ID="lblactiveStatus" runat="server" Text='<%#Bind("activeStatus") %>' Visible="false"></asp:Label>
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

                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactive_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
     <asp:HiddenField ID="hfImageUrl" EnableViewState="true" runat="server" />
 
    <script type="text/javascript">
        function ShowImagePreview(input) {
            debugger;
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
                            $('#<%=imgGalleryPhotoPrev.ClientID%>').prop('src', e.target.result);

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


    </script>
    
</asp:Content>

