using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAppComercial.Shared.Entities;

namespace GenerarExcel.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {

        //-------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("ExportExcelCategories")]

        public IActionResult ExportExcelCategories([FromBody] List<Category> categories)
        {
            try
            {
                DataTable table = new DataTable();//tabla general

                table.Columns.Add("ID");
                table.Columns.Add("NOMBRE");

                foreach(Category category in categories)
                {
                    DataRow fila = table.NewRow();
                    fila["ID"] = category.Id;
                    fila["NOMBRE"] = category.Name;
                    table.Rows.Add(fila);
                }

                using var libro = new XLWorkbook();
                table.TableName = "Categorias";

                var hoja = libro.Worksheets.Add(table);

                hoja.ColumnsUsed().AdjustToContents();

                using var memoria = new MemoryStream();
                libro.SaveAs(memoria);
                var nombreExcel = "Reporte.xlsx";
                var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                return archivo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("ExportExcelStores")]
        public IActionResult ExportExcelStores([FromBody] List<Store> stores)
        {
            try
            {
                DataTable table = new DataTable();//tabla general

                table.Columns.Add("ID");
                table.Columns.Add("NOMBRE");

                foreach (Store store in stores)
                {
                    DataRow fila = table.NewRow();
                    fila["ID"] = store.Id;
                    fila["NOMBRE"] = store.Name;
                    table.Rows.Add(fila);
                }

                using var libro = new XLWorkbook();
                table.TableName = "Almacenes";

                var hoja = libro.Worksheets.Add(table);

                hoja.ColumnsUsed().AdjustToContents();

                using var memoria = new MemoryStream();
                libro.SaveAs(memoria);
                var nombreExcel = "Reporte.xlsx";
                var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                return archivo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("ExportExcelUsers")]

        public IActionResult ExportExcelUsers([FromBody] List<User> users)
        {
            try
            {
                DataTable table = new DataTable();//tabla general

                table.Columns.Add("ROL");
                table.Columns.Add("NOMBRE");
                table.Columns.Add("APELLIDO");
                table.Columns.Add("EMAIL");
                table.Columns.Add("CONFIRMADO");
                table.Columns.Add("ACTIVO");

                foreach (User user in users)
                {
                    DataRow fila = table.NewRow();
                    fila["ROL"] = user.Rol;
                    fila["NOMBRE"] = user.FirstName;
                    fila["APELLIDO"] = user.LastName;
                    fila["EMAIL"] = user.Email;
                    fila["CONFIRMADO"] = user.EmailConfirmed;
                    fila["ACTIVO"] = user.Active;
                    table.Rows.Add(fila);
                }

                using var libro = new XLWorkbook();
                table.TableName = "Usuarios";

                var hoja = libro.Worksheets.Add(table);

                hoja.ColumnsUsed().AdjustToContents();

                using var memoria = new MemoryStream();
                libro.SaveAs(memoria);
                var nombreExcel = "Reporte.xlsx";
                var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                return archivo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
