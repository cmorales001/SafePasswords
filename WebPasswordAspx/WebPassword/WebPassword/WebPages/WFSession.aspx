<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSession.aspx.cs" Inherits="WebPassword.WebPages.WFSession" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Session</title>
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

        <!--Navegador-->
        <nav class="navbar navbar-expand-lg bg-body-tertiary">
            <div class="container-fluid">
                <asp:Label ID="labelNombreUser" runat="server" Text="" Font-Size="20">Hola</asp:Label>
                
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link" href="WFEditar.aspx">Actualizar información</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#" onserverclick="cerrarSesion" runat="server">Cerrar Sesión</a>
                        </li>
                    </ul>
                </div>
            </div>
</nav>
    </header>
    <form id="form1" runat="server">
        <div class="mi-div2">
            <div class="row">
                <div class="col-md-4">
                    <h2 class="text-center">GENERADOR DE CONTRASEÑAS SEGURAS</h2>
                    <div class="form-group">
                        <label for="txtGeneratedPassword">Tu contraseña:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtGeneratedPassword1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                            <div class="input-group-append">
                                <asp:Button ID="btnRegenerate1" runat="server" CssClass="btn btn-outline-secondary" Text="Copiar" OnClick="btnRegenerate1_Click" />

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <hr />
                    </div>
                    <h3 class="text-center">PERSONALIZA TU CONTRASEÑA</h3>
                    <div class="form-group">
                        <label for="txtLength">Longitud:</label>
                        <input type="number" class="form-control" id="txtLength" runat="server" min="8" max="32" value="8" />
                    </div>
                    <div class="form-check">
                        <asp:CheckBox ID="chkSpecialChars1" runat="server" CssClass="form-check-input" Checked="False" />

                        <label class="form-check-label" for="chkSpecialChars1">Incluir caracteres especiales</label>
                    </div>
                    <div class="form-check">
                        <asp:CheckBox ID="chkNumbers" runat="server" CssClass="form-check-input" Checked="False" />
                        <label class="form-check-label" for="chkNumbers">Incluir Números</label>
                    </div>
                    <br />
                    <h4 class="text-center">Genera tu contraseña:</h4>
                    <br />
                    <asp:Button ID="btnGeneratePass" runat="server" CssClass="btn btn-primary" Text="Contraseña personalizada" OnClick="btnGeneratePass_Click" />
                    <asp:Button ID="btnCopy1" runat="server" CssClass="btn btn-primary" Text="Facil de Recordar" OnClick="btnCopy1_Click" />



                </div>
                <div class="col-md-5 col-lg-6 justify-content-end ml-auto">
                    <div class="card p-3">
                        <br />
                        <br />
                        <div class="card p-3">
                            <h2 class="text-center">GESTOR DE CONTRASEÑAS</h2>

                            <div class="form-group">
                                <label for="txtNamePassword">Identificador(nombre):</label>
                                <asp:TextBox ID="txtNamePassword" runat="server" CssClass="form-control" ValidationGroup="RegPassword"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtNamePassword" runat="server" ControlToValidate="txtNamePassword"
                                    ErrorMessage="El campo Identificador es requerido" ValidationGroup="RegPassword"></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group">
                                <label for="txtRegPassword">Contraseña:</label>
                                <asp:TextBox ID="txtRegPassword" runat="server" CssClass="form-control"  ValidationGroup="RegPassword"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword1" runat="server" ControlToValidate="txtRegPassword"
                                    ErrorMessage="El campo Contraseña es requerido" ValidationGroup="RegPassword"></asp:RequiredFieldValidator>
                            </div>

                            <asp:Button ID="btnRegPassword" runat="server" CssClass="btn btn-primary" Text="Añadir" ValidationGroup="RegPassword" OnClick="btnRegPassword_Click" />
                            <br />
                        </div>

                        <div class="card p-3">
                            <h2 class="text-center">TUS CONTRASEÑAS</h2>
                            <div class="form-group">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                    OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="true" />
                                        <asp:BoundField DataField="Password1" HeaderText="Contraseña" />
                                        <asp:ButtonField ButtonType="Button" Text="Copiar" CommandName="CopyRow" />
                                        <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="EditRow" />
                                        <asp:ButtonField ButtonType="Button" Text="Eliminar" CommandName="deleteRow" />
                                    </Columns>
                                </asp:GridView>


                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
