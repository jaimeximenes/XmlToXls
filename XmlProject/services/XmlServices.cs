using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace XmlProject.services
{
    public class XmlServices : ContextDefault
    {
        ReaderXmlEntities1 context = new ReaderXmlEntities1();
        Campos campos = new Campos();

        public void getDadosXml(XmlDocument xml)
        {

            byte[] xmlComp = Encoding.ASCII.GetBytes(xml.OuterXml);
            campos.Xml = xmlComp;

            var ns = new XmlNamespaceManager(xml.NameTable);
            ns.AddNamespace("nfeProc", "http://www.portalfiscal.inf.br/nfe");

            var infNFe = xml.SelectSingleNode("//nfeProc:infNFe", ns);
            var chave = infNFe.Attributes["Id"].InnerText.Replace("NFe", "");
            campos.chaveNfeCte = chave;

            var cnpjEmitente = xml.SelectSingleNode("//nfeProc:emit//nfeProc:CNPJ", ns);

            if (cnpjEmitente == null)
            {
                XmlNode dadoEmitenteCPf = xml.SelectSingleNode("//nfeProc:emit//nfeProc:CPF", ns);
                campos.cnpjEmitente = dadoEmitenteCPf.InnerText;
            }
            else
            {
                campos.cnpjEmitente = cnpjEmitente.InnerText;
            }

            XmlNode cnpjDest = xml.SelectSingleNode("//nfeProc:dest//nfeProc:CNPJ", ns);
            if (cnpjDest == null)
            {
                XmlNode cnpjDestCPF = xml.SelectSingleNode("//nfeProc:dest//nfeProc:CPF", ns);
                campos.CnpjDestinatario = cnpjDest.InnerText;

            }
            else
            {
                campos.CnpjDestinatario = cnpjDest.InnerText;
            }
            XmlNode valorTotalNfe = xml.SelectSingleNode("//nfeProc:vNF", ns);
            var valor = valorTotalNfe.InnerText.Replace(".", ",");
            campos.total = valor;
            XmlNode infAdic = xml.SelectSingleNode("//nfeProc:infAdic", ns);
            campos.infAdic = infAdic.InnerText;


            context.Campos.Add(campos);
            context.SaveChanges();

        }

        public List<Campos> getDadosBd()
        {
            return context.Campos.ToList();
        }
        public string getXmlById(string id)
        {
            var temp = context.Campos.Where(i => i.chaveNfeCte == id).FirstOrDefault();

            var xmlString = Encoding.ASCII.GetString(temp.Xml);
            return xmlString;
        }
        public void deleteXml(string id)
        {
            //var temp = context.Campos.Where(i => i.chaveNfeCte == id).FirstOrDefault();
            //var delXml = Encoding.ASCII.GetString(temp.Xml);
            //return delXml.Remove(Convert.ToInt32(temp.Xml));

            var dbxml = context.Campos.Where(i => i.chaveNfeCte == id).FirstOrDefault();
            context.Campos.Remove(dbxml);
            context.SaveChanges();

        }
    }
}