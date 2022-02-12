using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Helpers.Excel
{
    /// <summary>
    /// This method helps that you are working with excel files
    /// You can send Generic List and get excel file of byte type
    /// Or you can save your byte type as excel file in your data store
    /// </summary>
    public static class ExcelExportHelper
    {
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = properties[i].GetValue(item);

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static byte[] ExportExcel(DataTable dataTable, List<string> columns, string heading = "", bool showSrNo = false)
        {
            byte[] result = null;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add($"{heading} Data");
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                    dataTable.Columns[i].ColumnName = columns[i];

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    //int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                    int maxLength = columnCells.Count();
                    if (maxLength < 150)
                        workSheet.Column(columnIndex).AutoFit();

                    columnIndex++;
                }

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }
                result = package.GetAsByteArray();
                package.Dispose();
            }

            return result;
        }

        public static byte[] ExportExcel<T>(List<T> data, string heading = "", bool showSlno = false)
        {
            List<PropertyInfo> listPi = data.FirstOrDefault().GetType().GetProperties().ToList();
            List<string> columns = new List<string>();
            foreach (var item in listPi)
            {
                string name = item?.Name;
                if(item.CustomAttributes.Count() != 0)
                    name = item?.CustomAttributes?.FirstOrDefault().ConstructorArguments?.FirstOrDefault().Value?.ToString();

                columns.Add(name);
            }
            return ExportExcel(ListToDataTable<T>(data), columns, heading, showSlno);
        }

        public static byte[] SaveToExcel(this byte[] bytes, string path, string fileName, out string savedFileName)
        {
            using (ExcelPackage package = new ExcelPackage(new MemoryStream(bytes)))
            {
                savedFileName = $"{fileName}.xlsx";
                package.SaveAs(new FileInfo($"{path}\\{fileName}"));
                package.Dispose();
            }
            return bytes;
        }
    }
}
