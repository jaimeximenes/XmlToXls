using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using XmlProject.services;

namespace XmlProject
{
    public partial class About : Page
    {

        XmlServices services = new XmlServices();
        XmlDocument xml = new XmlDocument();
        XlsServices xlsServices = new XlsServices();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataTable();
            }
        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {

                string filename = Path.GetFileName(FileUploadControl.FileName);
                FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                xml.Load(Server.MapPath("~/") + filename);
                services.getDadosXml(xml);

            }
        }

        //Gerar Planilha com os Campos de interesse capturados do XML

        //Salvar Planilha Gerada em algum lugar no computador e armazenar o Caminho


        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            services.getDadosBd();
        }
        public void BindDataTable()
        {
            Repeater1.DataSource = services.getDadosBd();
            Repeater1.DataBind();
        }
        protected void DownBtn_Click(object sender, EventArgs e)
        {
            var file = xlsServices.getPlan();

            Response.ClearContent();
            Response.AddHeader("content-length", file.Length.ToString());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.OutputStream.Write(file, 0, file.Length);
            //Response.Write(file);
            Response.End();
            BindDataTable();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            var xml = services.getXmlById(btn.CommandArgument);
            Response.ContentType = "text/xml";
            Response.AppendHeader("Content-Disposition", "attachment; filename=xmlNota.xml");
            Response.Write(xml);
            Response.Flush();
            Response.End();

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            var xml = services.getXmlById(btn.CommandArgument);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string json = JsonConvert.SerializeXmlNode(doc);
            Response.ContentType = "text/xml";
            Response.AppendHeader("Content-Disposition", "attachment; filename=xml.Json");
            Response.Write(json);
            Response.Flush();
            Response.End();


        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            LinkButton del = (LinkButton)(sender);
            //string delPlan = services.getXmlById(del.CommandArgument);
            //xml.RemoveAll();
            services.delXml(del.CommandArgument);
            BindDataTable();

        }
    }
}