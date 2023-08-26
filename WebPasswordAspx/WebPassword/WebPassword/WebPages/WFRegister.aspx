<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFRegister.aspx.cs" Inherits="WebPassword.WebPages.WFRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro</title>
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
                    <h2 class="text-center">REGISTRARSE</h2>

                    <div class="form-group">
                        <label for="txtRegFirstName">Nuevos Nombres</label>
                        <asp:TextBox ID="txtRegFirstName" runat="server" CssClass="form-control"  ValidationGroup="Registro"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredtxtRegFirstName" runat="server" ControlToValidate="txtRegFirstName"
                            ErrorMessage="El campo Nombre es requerido"  ValidationGroup="Registro"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="txtRegLastName">Nuevos Apellidos</label>
                        <asp:TextBox ID="txtRegLastName" runat="server" CssClass="form-control"  ValidationGroup="Registro"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredtxtRegLastName" runat="server" ControlToValidate="txtRegLastName"
                            ErrorMessage="El campo Apellido es requerido"  ValidationGroup="Registro"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="txtRegUsernam">Nuevo Correo Electrónico:</label>
                        <asp:TextBox ID="txtRegUsernam" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredtxtRegUsernam" runat="server" ControlToValidate="txtRegUsernam"
                            ErrorMessage="El campo Correo Electrónico es requerido"  ValidationGroup="Registro"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revRegUsernam" runat="server" ControlToValidate="txtRegUsernam"
                            ErrorMessage="Ingrese un correo electrónico válido" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                             ValidationGroup="Registro"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label for="txtRegPassword"> Contraseña:</label>
                        <asp:TextBox ID="txtRegPassword" runat="server" CssClass="form-control" TextMode="Password"  ValidationGroup="Registro"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredtxtRegPassword" runat="server" ControlToValidate="txtRegPassword"
                            ErrorMessage="El campo Contraseña es requerido"  ValidationGroup="Registro"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="txtRegConfPassword">Confirmar Contraseña:</label>
                        <asp:TextBox ID="txtRegConfPassword" runat="server" CssClass="form-control" TextMode="Password"  ValidationGroup="Registro"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredtxtRegConfPassword" runat="server" ControlToValidate="txtRegConfPassword"
                            ErrorMessage="El campo Contraseña es requerido"  ValidationGroup="Registro"></asp:RequiredFieldValidator>
                    </div>

                    
                    
                    <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-primary" Text="Registrarme"  ValidationGroup="Registro" OnClick="btnRegistrar_Click" />
                   <br />
                    <br />

                    <div class="text-center">
                        <asp:Button ID="BtnRegresar" runat="server" CssClass="btn btn-primary" Text="Regresar"  ValidationGroup="Regresar" OnClick="BtnRegresar_Click" />
                    </div>

                </div>

            </div>
            <!-- fin div inicio de sesión -->
        </div>
        <!--fin div para centrar-->
    </form>

    
</body>
</html>
