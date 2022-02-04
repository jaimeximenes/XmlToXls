using System;
using System.IO;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Owin;
using OfficeOpenXml;
using Owin;
using static System.Net.Mime.MediaTypeNames;

namespace XmlProject.services
{
    public class XlsServices : Page
    {
       
        public Byte[] getPlan()
        {
            XmlServices services = new XmlServices();
            var listCampos = services.getDadosBd();
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Relatório");

            worksheet.Cells[1, 1].Value = "Campos";
            worksheet.Cells[2, 1].Value = "Chave NFE";
            worksheet.Cells[2, 2].Value = "CNPJ/ CPF DESTINATÁRIO";
            worksheet.Cells[2, 3].Value = "CNPJ/ CPF EMITENTE";
            worksheet.Cells[2, 4].Value = "iNFORMAÇÕES ADICIONAIS";
            worksheet.Cells[2, 5].Value = "Valor Total da nota";
            int row = 3;
            double temp = 0;
            foreach (var item in listCampos)
            {

                worksheet.Cells[row, 1].Value = item.chaveNfeCte;
                worksheet.Cells[row, 2].Value = item.CnpjDestinatario;
                worksheet.Cells[row, 3].Value = item.cnpjEmitente;
                worksheet.Cells[row, 4].Value = item.infAdic;
                worksheet.Cells[row, 5].Value = item.total;

                temp += Convert.ToDouble(item.total);

                row++;


            }
           
            var headerCells = worksheet.Cells[1, 1, 2, worksheet.Dimension.Columns];
            headerCells.Style.Font.Bold = true;
            headerCells.Style.Font.Italic = true;

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            worksheet.Cells[worksheet.Dimension.Rows, 5].Value = temp;

            return excelPackage.GetAsByteArray();
        }
    }
}
