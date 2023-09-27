<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmPayBalance.aspx.cs" Inherits="BusinessManagementSystem.frmPayBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Suppliers Payments</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Default.aspx">Dashboard</a></li>
                        <li class="breadcrumb-item active">Suppliers Payments</li>
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
                        Add Payment
                    </button>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <!-- Modal Start -->
                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title text-center">Add New Payment</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-6 my-2">
                                                <asp:DropDownList ID="supplierDDL" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="supplierDDL_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem>Select Supplier</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6 my-2">
                                                <asp:TextBox ID="dateTxt" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 my-2">
                                                <asp:TextBox ID="previousTxt" CssClass="form-control" placeholder="Previous Balance" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 my-2">
                                                <asp:TextBox ID="amountTxt" CssClass="form-control" placeholder="Paid Balance" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 my-2">
                                                <asp:TextBox ID="descriptionTxt" CssClass="form-control" placeholder="Description" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 my-2">
                                                <asp:DropDownList ID="methodDDL" CssClass="form-control" runat="server">
                                                    <asp:ListItem>Cash</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>Bank Transfer</asp:ListItem>
                                                    <asp:ListItem>Card</asp:ListItem>
                                                    <asp:ListItem>Mobile Wallet</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer justify-content-between">
                                <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save Payment" OnClick="btnSave_Click" />
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
                                                <th>Supplier</th>
                                                <th>Date</th>
                                                <th>Amount</th>
                                                <th>Description</th>
                                                <th>Mode</th>
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
                                            <asp:Label ID="LabelName" Text='<%# Eval("Supplier") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPhone" Text='<%# Convert.ToDateTime(Eval("Date")).ToString("dd-MM-yyyy") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelAddress" Text='<%# Eval("Amount") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelMaxCredit" Text='<%# Eval("Description") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" Text='<%# Eval("Mode") %>' runat="server" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="editLink" Text="Edit" CssClass="text-primary mx-2" CommandName="cmdEdit" CommandArgument='<%# Eval("ID") %>' runat="server"> <i class="fa fa-pen"></i> </asp:LinkButton>
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
                            <h4 class="modal-title text-center">Update Payment</h4>
                            <button type="button" class="close" onclick="closeModal()">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:DropDownList ID="supplilerDDL2" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                                        <asp:ListItem>Select Supplier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="dateTxt2" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="previousTxt2" CssClass="form-control" placeholder="Previous Balance" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="amountTxt2" CssClass="form-control" placeholder="Paid Balance" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:TextBox ID="descriptionTxt2" CssClass="form-control" placeholder="Description" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:DropDownList ID="methodDDL2" CssClass="form-control" runat="server">
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>Bank Transfer</asp:ListItem>
                                        <asp:ListItem>Card</asp:ListItem>
                                        <asp:ListItem>Mobile Wallet</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-between">
                            <asp:Button ID="btnUpdate" CssClass="btn btn-success" runat="server" Text="Update Payment" OnClick="btnUpdate_Click" />
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
