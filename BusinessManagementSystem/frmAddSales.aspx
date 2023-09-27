<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmAddSales.aspx.cs" Inherits="BusinessManagementSystem.frmAddSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Create Sale Invoice</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Default.aspx">Dashboard</a></li>
                        <li class="breadcrumb-item active">Create Invoice</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <!-- content-header -->

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="invoice p-3 mb-3">
                <p class="lead">Invoice Details:</p>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label Text="Invoice ID:" runat="server" />
                        <asp:TextBox ID="invoiceIDTxt" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label Text="Invoice Date:" runat="server" />
                        <asp:TextBox ID="dateTxt" CssClass="form-control" TextMode="Date" runat="server" />
                    </div>
                </div>

                <hr />

                <asp:ScriptManager ID="ScriptManager1" runat="server" />

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <p class="lead">Customers Details:</p>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label Text="Customer:" runat="server" />
                                <asp:DropDownList ID="customerDDL" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" runat="server" OnSelectedIndexChanged="customerDDL_SelectedIndexChanged">
                                    <asp:ListItem Text="Select Customer" Value="0" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Label Text="Phone:" runat="server" />
                                <asp:TextBox ID="phoneTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label Text="Address:" runat="server" />
                                <asp:TextBox ID="addressTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label Text="Balance:" runat="server" />
                                <asp:TextBox ID="balanceTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label Text="Max Credit:" runat="server" />
                                <asp:TextBox ID="maxcreditTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <hr />

                        <p class="lead">Products Details:</p>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label Text="Product:" runat="server" />
                                <asp:DropDownList ID="productDDL" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" runat="server" OnSelectedIndexChanged="productDDL_SelectedIndexChanged">
                                    <asp:ListItem Text="Select Product" Value="0" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label Text="Bar Code:" runat="server" />
                                <asp:TextBox ID="barcodeTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label Text="Category:" runat="server" />
                                <asp:TextBox ID="categoryTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label Text="Purchase Rate:" runat="server" />
                                <asp:TextBox ID="prateTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-3">
                                <asp:Label Text="Sale Rate:" runat="server" />
                                <asp:TextBox ID="srateTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label Text="Available Stock:" runat="server" />
                                <asp:TextBox ID="stockTxt" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label Text="Quantity:" runat="server" />
                                <asp:TextBox ID="quantityTxt" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-3">
                                <br />
                                <asp:Button ID="btnAddToCart" CssClass="btn btn-dark btn-block" Text="Add To Cart" runat="server" OnClick="btnAddToCart_Click" />
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-12 table-responsive">
                                <asp:GridView ID="GridView1" CssClass="table table-striped" runat="server"></asp:GridView>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-6">
                                <p class="lead">Invoice Description:</p>
                                <asp:TextBox ID="descriptionTxt" CssClass="form-control w-100" placeholder="Enter Invoice Description (Optional)" TextMode="MultiLine" Rows="8" runat="server" />
                            </div>
                            <div class="col-6">
                                <p class="lead">Invoice Total:</p>
                                <div class="table-responsive">
                                    <table class="table">
                                        <tr>
                                            <th style="width: 50%">Subtotal:</th>
                                            <td>
                                                <asp:TextBox ID="subtotalTxt" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>Discount:</th>
                                            <td>
                                                <asp:TextBox ID="discountTxt" CssClass="form-control" AutoPostBack="true" Text="0" runat="server" OnTextChanged="discountTxt_TextChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>Total Amount:</th>
                                            <td>
                                                <asp:TextBox ID="totalTxt" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>Cash Receiving:</th>
                                            <td>
                                                <asp:TextBox ID="cashTxt" CssClass="form-control" Text="0" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row no-print justify-content-around">
                    <div class="col-3">
                        <asp:LinkButton ID="btnSave" CssClass="btn btn-success btn-block" runat="server" OnClick="btnSave_Click"><i class="fas fa-download">&nbsp;</i>Save Invoice</asp:LinkButton>
                    </div>
                    <div class="col-3">
                        <asp:LinkButton ID="btnReset" CssClass="btn btn-danger btn-block" runat="server"><i class="fas fa-solid fa-rotate"></i>Reset Data</asp:LinkButton>
                    </div>
                    <div class="col-3">
                        <asp:LinkButton ID="btnPrint" CssClass="btn btn-primary btn-block" runat="server"> <i class="fas fa-print">&nbsp;</i>Print Invoice</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

    </section>

</asp:Content>
