<%@ Page Title="Relatório XML" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioXml.aspx.cs" Inherits="XmlProject.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Enviar XML</h3>
    <p>
        <asp:FileUpload ID="FileUploadControl" runat="server" />
        <h4>
            <asp:Button ID="UploadButton" class="btn btn-primary btn-lg" runat="server" OnClick="UploadButton_Click" Text=" Enviar XML" />
            <asp:Button ID="DownBtn" class="btn btn-primary btn-lg" runat="server" OnClick="DownBtn_Click" Text="Baixar Relatório" />
    </p>
    </h4>
     <%--      <asp:DropDownList ID="ddlFiltro" runat="server" Width="150px" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
               <asp:ListItem Text="Cnpj Emitente" Value="Cnpj Emitente"></asp:ListItem>
               <asp:ListItem Text="Cnpj Destinatário" Value="Cnpj Destinatário"></asp:ListItem>
           </asp:DropDownList>--%>
    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
        <HeaderTemplate>
            <table>
                <tr>
                    <td>Chave NFE</td>
                    <td>cnpj Destinatário</td>
                    <td>cnpj Emitente<</td>
                    <td>Valor total da nota<</td>
                    <td>Informações Adicionais</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("chaveNfeCte") %> </td>
                <td><%# Eval("cnpjDestinatario") %> </td>
                <td><%# Eval("cnpjEmitente") %> </td>
                <td><%# Eval("total").ToString() %> </td>
                <td><%# Eval("infAdic") %> </td>
                <td>
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary mb-3" runat="server" CommandArgument='<%# Eval("chaveNfeCte") %>'
                        CommandName="xml" OnClick="LinkButton1_Click">XML</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-primary mb-3" runat="server" CommandArgument='<%# Eval("chaveNfeCte") %>'
                        CommandName="json" OnClick="LinkButton2_Click">Json</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" CssClass="btn btn-primary mb-3" runat="server" CommandArgument='<%# Eval("chaveNfeCte") %>'
                        CommandName="del" OnClick="LinkButton3_Click">Delete</asp:LinkButton>
                </td>

        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
