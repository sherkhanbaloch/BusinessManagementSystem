<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BusinessManagementSystem.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Dashboard</h1>
                </div>
                <%--<div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Dashboard</a></li>
                    </ol>
                </div>--%>
            </div>
        </div>
    </div>
    <!-- Content Header -->

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-info">
                        <div class="inner">
                            <asp:Label ID="lblCustomers" Text="0" runat="server" />
                            <p>Customers</p>
                        </div>
                        <div class="icon">
                            <i class="fas fa-users"></i>
                        </div>
                        <%-- <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>--%>
                    </div>
                </div>
                <!-- ./col -->
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-success">
                        <div class="inner">
                            <asp:Label ID="lblProducts" Text="0" runat="server" />
                            <p>Products</p>
                        </div>
                        <div class="icon">
                            <i class="fas fa-cubes"></i>
                        </div>
                        <%--<a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>--%>
                    </div>
                </div>
                <!-- ./col -->
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-warning">
                        <div class="inner">
                            <asp:Label ID="lblSuppliers" Text="0" runat="server" />
                            <p>Suppliers</p>
                        </div>
                        <div class="icon">
                            <i class="fas fa-truck"></i>
                        </div>
                        <%--<a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>--%>
                    </div>
                </div>
                <!-- ./col -->
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-danger">
                        <div class="inner">
                            <asp:Label ID="lblUsers" Text="0" runat="server" />
                            <p>Users</p>
                        </div>
                        <div class="icon">
                            <i class="fas fa-user-shield"></i>
                        </div>
                        <%--<a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>--%>
                    </div>
                </div>
                <!-- ./col -->
            </div>
            <!-- /.row -->
            <!-- Main row -->

            <!-- Info boxes -->
            <div class="row">
                <div class="col-12 col-sm-6 col-md-3">
                    <div class="info-box">
                        <span class="info-box-icon bg-danger elevation-1"><i class="fas fa-shopping-cart"></i></span>
                        <div class="info-box-content">
                            <span class="info-box-text">Sales</span>
                            <span class="info-box-number">
                                <asp:Label ID="lblSale" Text="0" runat="server" />
                            </span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <!-- /.info-box -->
                </div>
                <!-- /.col -->
                <div class="col-12 col-sm-6 col-md-3">
                    <div class="info-box mb-3">
                        <span class="info-box-icon bg-warning elevation-1"><i class="fas fa-cart-arrow-down"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">Purchases</span>
                            <span class="info-box-number">
                                <asp:Label ID="lblPurchase" Text="0" runat="server" /></span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <!-- /.info-box -->
                </div>
                <!-- /.col -->

                <!-- fix for small devices only -->
                <div class="clearfix hidden-md-up"></div>

                <div class="col-12 col-sm-6 col-md-3">
                    <div class="info-box mb-3">
                        <span class="info-box-icon bg-success elevation-1"><i class="fas fa-coins"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">Received Balances</span>
                            <span class="info-box-number">
                                <asp:Label ID="lblReceived" Text="0" runat="server" /></span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <!-- /.info-box -->
                </div>
                <!-- /.col -->
                <div class="col-12 col-sm-6 col-md-3">
                    <div class="info-box mb-3">
                        <span class="info-box-icon bg-info elevation-1"><i class="fas fa-credit-card"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">Paid Balances</span>
                            <span class="info-box-number">
                                <asp:Label ID="lblPaid" Text="0" runat="server" /></span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <!-- /.info-box -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->

            <hr />

            <div class="row">
                <div class="col-sm-3 col-6">
                    <div class="description-block border-dark border-right">
                        <%--<span class="description-percentage text-success"><i class="fas fa-caret-up"></i>0%</span>--%>
                        <h5 class="description-header text-primary"><i class="fas fa-caret-right">&nbsp;</i><asp:Label ID="lblRevenue" Text="0" runat="server" /></h5>
                        <span class="description-text text-capitalize">Total Revenue</span>
                    </div>
                    <!-- /.description-block -->
                </div>
                <!-- /.col -->
                <div class="col-sm-3 col-6">
                    <div class="description-block border-dark border-right">
                        <%--<span class="description-percentage text-warning"><i class="fas fa-caret-left"></i>0%</span>--%>
                        <h5 class="description-header text-warning"><i class="fas fa-caret-left">&nbsp;</i><asp:Label ID="lblCost" Text="0" runat="server" /></h5>
                        <span class="description-text text-capitalize">Total Cost</span>
                    </div>
                    <!-- /.description-block -->
                </div>
                <!-- /.col -->
                <div class="col-sm-3 col-6">
                    <div class="description-block border-dark border-right">
                        <%--<span class="description-percentage text-success"><i class="fas fa-caret-up"></i>0%</span>--%>
                        <h5 class="description-header text-success"><i class="fas fa-caret-up">&nbsp;</i><asp:Label ID="lblProfit" Text="0" runat="server" /></h5>
                        <span class="description-text text-capitalize">Total Profit</span>
                    </div>
                    <!-- /.description-block -->
                </div>
                <!-- /.col -->
                <div class="col-sm-3 col-6">
                    <div class="description-block">
                        <%--<span class="description-percentage text-danger"><i class="fas fa-caret-down"></i>0%</span>--%>
                        <h5 class="description-header text-danger"><i class="fas fa-caret-down">&nbsp;</i><asp:Label ID="lblLoss" Text="0" runat="server" /></h5>
                        <span class="description-text text-capitalize">Total Loss</span>
                    </div>
                    <!-- /.description-block -->
                </div>
            </div>
            <!-- /.row -->

            <hr />

            <div class="row">
                <div class="col-6">
                    <!-- Info Boxes Style 2 -->
                    <div class="info-box mb-3 bg-danger col-sm-12">
                        <span class="info-box-icon"><i class="fas fa-users"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">Remaining Balance To Customers</span>
                            <span class="info-box-number">
                                <asp:Label ID="lblCustomerRemaining" Text="0" runat="server" /></span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <!-- /.info-box -->
                    <div class="info-box mb-3 bg-warning col-sm-12">
                        <span class="info-box-icon"><i class="fas fa-truck"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">Remaining Balance To Suppliers</span>
                            <span class="info-box-number">
                                <asp:Label ID="lblSupplierRemaining" Text="0" runat="server" /></span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <!-- /.info-box -->
                </div>
                <div class="col-6 d-flex flex-column align-items-center">
                    <img src="Images/AWB-Softwares.png" class="w-25 d-flex jus" />
                   <b><p>Sher Khan Baloch - AWB Software and Business Solutions</p></b>
                </div>
            </div>
            <!-- /.row (main row) -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- /.content -->
</asp:Content>
