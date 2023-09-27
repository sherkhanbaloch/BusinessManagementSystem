<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="BusinessManagementSystem.frmLogin" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Sign In | Business Management System</title>

    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="TemplateFiles/plugins/fontawesome-free/css/all.min.css">
    <!-- icheck bootstrap -->
    <link rel="stylesheet" href="TemplateFiles/plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="TemplateFiles/dist/css/adminlte.min.css">

    <!-- Sweet Alert 2 -->
    <link href="TemplateFiles/plugins/sweetalert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="TemplateFiles/plugins/sweetalert2/sweetalert2.min.js"></script>

    <!-- Fav Icon -->
    <link rel="shortcut icon" href="Images/favicon.png" type="image/x-icon">

</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server">
        <div class="login-logo">
            <a href="#" class="text-center"><b>Business </b>Management System</a>
        </div>

        <div class="login-box">
            <div class="card">
                <div class="card-body login-card-body">
                    <p class="login-box-msg">Sign In To Start Your Session</p>
                    <div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="User Name Required." ControlToValidate="usernameTxt" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="input-group mb-3">
                        <asp:TextBox ID="usernameTxt" class="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-user"></span>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password Required." ControlToValidate="passwordTxt" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="input-group mb-3">
                        <asp:TextBox ID="passwordTxt" class="form-control" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-lock"></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-8">
                            <div class="icheck-primary">
                                <asp:CheckBox ID="remember" runat="server" />
                                <label for="remember">
                                    Remember Me
                                </label>
                            </div>
                        </div>
                        <div class="col-4">
                            <asp:Button ID="btnSignIn" class="btn btn-primary btn-block" runat="server" Text="Sign In" OnClick="btnSignIn_Click" />
                        </div>
                    </div>

                    <div class="social-auth-links text-center mb-3">
                        <p>- OR -</p>
                        <a href="#" class="btn btn-block btn-primary">
                            <i class="fab fa-facebook mr-2"></i>Sign In Using Facebook
                        </a>
                        <a href="#" class="btn btn-block btn-danger">
                            <i class="fab fa-google mr-2"></i>Sign In Using Google
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- jQuery -->
    <script src="TemplateFiles/plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap 4 -->
    <script src="TemplateFiles/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- AdminLTE App -->
    <script src="TemplateFiles/dist/js/adminlte.min.js"></script>

</body>
</html>
