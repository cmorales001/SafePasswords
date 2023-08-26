<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFLogin.aspx.cs" Inherits="WebPassword.WebPages.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link rel="stylesheet" href="Estilo.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</head>
<body>
    <header>
        <div class="container">
            <div class="row">
                <div class="col-md-3">

                    <img src="https://blog.udlap.mx/wp-content/uploads/2020/08/contrasena-.jpg"
                        alt="texto_alternativo" width="150px" height="100px" />

                </div>
                <div class="col-md-6">
                    <h1 class="text-center">SAFE PASSWORD</h1>
                    <br />
                <br />
                    <h5 class="text-center">Contraseñas Seguras para tu tranquilidad</h5>
                    
                </div>
                <br />
                <br />
                
            </div>
            <div class="text-center">
                    
                </div>
        </div>

        
    </header>
    <form id="form1" runat="server" class="mi-formulario">
        <!-- div para centrar el contenido-->
        <div class="d-flex justify-content-center">
            <!-- Div que contiene en inicio de sesion -->
            <div class="mi-div">
                <div class="card p-3">
                    <br />
                    <br />

                    <h2 class="text-center">Inicio de Sesión</h2>
                    <div class="form-group">
                        <label for="txtRegUsernam">Correo Electrónico:</label>
                        <asp:TextBox ID="txtRegUsernam" runat="server" CssClass="form-control" ValidationGroup="Login"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRegUsernam" runat="server" ControlToValidate="txtRegUsernam"
                            ErrorMessage="El campo Correo Electrónico es requerido" ValidationGroup="Login"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revRegUsernam" runat="server" ControlToValidate="txtRegUsernam"
                            ErrorMessage="Ingrese un correo electrónico válido" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                            ValidationGroup="Login"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label for="txtRegPassword">Contraseña:</label>
                        <asp:TextBox ID="txtRegPassword1" runat="server" CssClass="form-control" TextMode="Password" ValidationGroup="Login"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword1" runat="server" ControlToValidate="txtRegPassword1"
                            ErrorMessage="El campo Contraseña es requerido" ValidationGroup="Login"></asp:RequiredFieldValidator>
                    </div>
                    
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Ingresar" OnClick="btnLogin_Click" ValidationGroup="Login" />
                    <br />
                    <br />

                    <div class="text-center">
                        <h6>Si no tiene una cuenta, puedes registrarte presionando "Registrarse".</h6>
                        <asp:Button ID="BtnNewRegistro" runat="server" CssClass="btn btn-primary" Text="Registrarse" OnClick="BtnNewRegistro_Click" ValidationGroup="Registro" />
                    </div>
                </div>
                    
                </div>

            
            <!-- fin div inicio de sesión -->
        </div>
        <!--fin div para centrar-->
    </form>

    
</body>
</html>
