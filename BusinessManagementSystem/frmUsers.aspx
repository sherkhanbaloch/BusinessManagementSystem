<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmUsers.aspx.cs" Inherits="BusinessManagementSystem.frmUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Users</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Default.aspx">Dashboard</a></li>
                        <li class="breadcrumb-item active">Users</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <!-- content-header -->

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-10">
                </div>
                <div class="col-md-2 my-2">
                    <button type="button" class="btn btn-dark btn-block" data-toggle="modal" data-target="#myModal">
                        Add User
                    </button>
                </div>
                <!-- Modal Start -->
                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title text-center">Add New User</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6 my-2">
                                        <asp:TextBox ID="fullNameTxt" CssClass="form-control" placeholder="Full Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 my-2">
                                        <asp:TextBox ID="userNameTxt" CssClass="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 my-2">
                                        <asp:DropDownList ID="roleDDL" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Select Role"></asp:ListItem>
                                            <asp:ListItem Text="Administrator"></asp:ListItem>
                                            <asp:ListItem Text="User"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6 my-2">
                                        <asp:TextBox ID="passwordTxt" CssClass="form-control" placeholder="Password" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 my-2">
                                        <asp:Label Text="User Image" CssClass="col-form-label" runat="server" />
                                        <asp:Label ID="lblMessage" Text="" CssClass="col-form-label" runat="server" />
                                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                    </div>
                                      <div class="col-md-6 my-2 text-center">
                                        <asp:Image ID="ImageBox" CssClass="border border-dark" Height="70px" Width="70px" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-between">
                                <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save User" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClear" CssClass="btn btn-info" runat="server" Text="Clear Data" OnClick="btnClear_Click" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                        <!-- modal-content -->
                    </div>
                    <!-- modal-dialog -->
                </div>
                <!-- modal end -->
            </div>

            <!-- Repeater and Data Tabel Starts -->
            <div class="row">
                <div class="col-md-12 my-2">
                    <div class="card card-block table-border-style">
                        <div class="card-body table-responsive">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>Full Name</th>
                                                <th>User Name</th>
                                                <th>Role</th>
                                                <th>Password</th>
                                                <th>Image</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelID" Text='<%# Eval("ID") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelFullName" Text='<%# Eval("Full Name") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelUserName" Text='<%# Eval("User Name") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelRole" Text='<%# Eval("Role") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPassword" Text='<%# Eval("Password") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Image ID="Image1" runat="server" Width="50" ImageUrl='<%# Eval("Image URL") %>' />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="editLink" Text="Edit" CssClass="text-primary mx-2" CommandName="cmdEdit" CommandArgument='<%# Eval("ID") %>' runat="server"> <i class="fa fa-pen"></i> </asp:LinkButton>
                                            <asp:LinkButton ID="deleteLink" Text="Delete" CssClass="text-danger mx-2" CommandName="cmdDelete" CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirmDeleteRecord(this);"> <i class="fa fa-trash"></i> </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>    
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Repeater and data table ends -->

            <!-- Update Modal starts -->
            <div class="modal fade" id="updateModal" tabindex="-1">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title text-center">Update User</h4>
                            <button type="button" class="close" onclick="closeModal()">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="fullNameTxt2" CssClass="form-control" placeholder="Full Name" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="userNameTxt2" CssClass="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:DropDownList ID="roleDDL2" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="Select Role"></asp:ListItem>
                                        <asp:ListItem Text="Administrator"></asp:ListItem>
                                        <asp:ListItem Text="User"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="passwordTxt2" CssClass="form-control" placeholder="Password" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                    <div class="col-md-6 my-2">
                                        <asp:Label Text="User Image" CssClass="col-form-label" runat="server" />
                                        <asp:Label ID="lblMessage2" Text="" CssClass="col-form-label" runat="server" />
                                        <asp:FileUpload ID="FileUpload2" CssClass="form-control" runat="server" />
                                    </div>
                                      <div class="col-md-6 my-2 text-center">
                                        <asp:Image ID="ImageBox2" CssClass="border border-dark" Height="70px" Width="70px" runat="server" />
                                    </div>
                                </div>
                        </div>
                        <div class="modal-footer justify-content-between">
                            <asp:Button ID="btnUpdate" CssClass="btn btn-success" runat="server" Text="Update User" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnReset2" CssClass="btn btn-info" runat="server" Text="Clear Data" />
                            <button type="button" class="btn btn-danger" onclick="closeModal()">Close</button>
                        </div>
                    </div>
                    <!-- modal-content -->
                </div>
                <!-- modal-dialog -->
            </div>
            <!-- modal -->
        </div>
    </section>

    <script src="JS/MyJavaScript.js"></script>

</asp:Content>
