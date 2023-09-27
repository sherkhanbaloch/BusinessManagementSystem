﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmProducts.aspx.cs" Inherits="BusinessManagementSystem.frmProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Products</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Default.aspx">Dashboard</a></li>
                        <li class="breadcrumb-item active">Products</li>
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
                        Add Product
                    </button>
                </div>
                <!-- Modal Start -->
                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title text-center">Add New Product</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6 my-2">
                                        <asp:TextBox ID="nameTxt" CssClass="form-control" placeholder="Product Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 my-2">
                                        <asp:TextBox ID="barcodeTxt" CssClass="form-control" placeholder="Bar Code" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 my-2">
                                        <asp:DropDownList ID="categoryDDL" CssClass="form-control" AppendDataBoundItems="true" runat="server">
                                           <asp:ListItem Value="0" Text="Select Category"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6 my-2">
                                        <asp:TextBox ID="purchaseTxt" CssClass="form-control" placeholder="Purchase Rate" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 my-2">
                                        <asp:TextBox ID="saleTxt" CssClass="form-control" placeholder="Sale Rate" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 my-2">
                                        <asp:TextBox ID="openingTxt" CssClass="form-control" placeholder="Opening Stock" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-between">
                                <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save Product" OnClick="btnSave_Click" />
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
                                                <th>Name</th>
                                                <th>Bar Code</th>
                                                <th>Category</th>
                                                <th>Purchase Rate</th>
                                                <th>Sale Rate</th>
                                                <th>Opening Stock</th>
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
                                            <asp:Label ID="LabelName" Text='<%# Eval("Name") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelBarCode" Text='<%# Eval("Bar Code") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelCategory" Text='<%# Eval("Category") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPurchaseRate" Text='<%# Eval("Purchase Rate") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelSaleRate" Text='<%# Eval("Sale Rate") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelOpeningStock" Text='<%# Eval("Opening Stock") %>' runat="server" />
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
                            <h4 class="modal-title text-center">Update Product</h4>
                            <button type="button" class="close" onclick="closeModal()">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="nameTxt2" CssClass="form-control" placeholder="Product Name" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="barcodeTxt2" CssClass="form-control" placeholder="Bar Code" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:DropDownList ID="categoryDDL2" CssClass="form-control" AppendDataBoundItems="true" runat="server">
                                        <asp:ListItem Value="0" Text="Select Category"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="purchaseTxt2" CssClass="form-control" placeholder="Purchase Rate" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="saleTxt2" CssClass="form-control" placeholder="Sale Rate" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="openingTxt2" CssClass="form-control" placeholder="Opening Stock" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-between">
                            <asp:Button ID="btnUpdate" CssClass="btn btn-success" runat="server" Text="Update Product" OnClick="btnUpdate_Click" />
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
