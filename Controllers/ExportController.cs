using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EmployeeDemo.Data;
using EmployeeDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace EmpApiEntityFramework.Controllers
{
    public class ExportEmployeeController : ApiController
    {
        private readonly EmployeeContext context;
        ExportEmployeeController() => context = new EmployeeContext();
        public HttpResponseMessage GetExcelFile()
        {
            List<Employee> ed = context.Employees.ToList();
            MemoryStream mem = new MemoryStream();
            SpreadsheetDocument doc = SpreadsheetDocument.Create(mem, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
            // SpreadsheetDocument doc = SpreadsheetDocument.Create("E://office work//specktasystems//exceltry//new1.xls", DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);

            WorkbookPart wbp = doc.AddWorkbookPart();
            wbp.Workbook = new Workbook();

            Sheets sheet = doc.WorkbookPart.Workbook.AppendChild(new Sheets());
            //first sheet
            WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
            SheetData sd1 = new SheetData();
            Worksheet w1 = new Worksheet();
            String[] employeesColumnHeader = { "First Name", "Date Of Birth", "Age", "Phone", "Department" };
            Row detailheader = new Row();
            for (int i = 0; i < employeesColumnHeader.Count(); i++)
            {
                Cell cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(employeesColumnHeader[i])
                };
                detailheader.Append(cell);
            }
            sd1.Append(detailheader);
            w1.AppendChild(sd1);
            wsp.Worksheet = w1;
            Sheet sh = new Sheet() { Id = wbp.GetIdOfPart(wsp), SheetId = 1, Name = "Employee Details" };

            sheet.Append(sh);


            //second sheet
            WorksheetPart wsp2 = wbp.AddNewPart<WorksheetPart>();
            SheetData sd2 = new SheetData();
            Worksheet w2 = new Worksheet();
            String[] addressesColumnHeader = { "Employee Name", "House Number", "Street" };
            //data sheet 2
            Row addressHeader = new Row();
            for (int i = 0; i < addressesColumnHeader.Count(); i++)
            {
                Cell cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(addressesColumnHeader[i])
                };
                addressHeader.Append(cell);
            }
            sd2.Append(addressHeader);
            w2.AppendChild(sd2);
            wsp2.Worksheet = w2;
            Sheet sh2 = new Sheet() { Id = wbp.GetIdOfPart(wsp2), SheetId = 2, Name = "Employee Addresses" };
            sheet.Append(sh2);
            //database mapping with excel
            foreach (Employee employee in ed)
            {
                Row detail = new Row();
                employee.Addresses.FirstOrDefault(d => d.AddressType);
                detail.Append(CreateCell(employee.FirstName));
                detail.Append(CreateCell(employee.DateOfBirth.ToString()));
                detail.Append(CreateCell(employee.Age.ToString()));
                detail.Append(CreateCell(employee.Phone.ToString()));
                detail.Append(CreateCell(employee.Department));

                sd1.Append(detail);
                foreach (Address eda in employee.Addresses)
                {
                    Row addresses = new Row();
                    addresses.Append(CreateCell(employee.FirstName));
                    addresses.Append(CreateCell(eda.HouseNo));
                    addresses.Append(CreateCell(eda.Street));
                    sd2.Append(addresses);
                }

            }
            wbp.Workbook.Save();
            doc.Close();

            var resultdata = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(mem.ToArray())
            };
            resultdata.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "EmployeeDetails.xls"
                };
            resultdata.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return resultdata;

        }
        private Cell CreateCell(string text)
        {
            Cell cell = new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(text)
            };
            return cell;
        }
    }
}
