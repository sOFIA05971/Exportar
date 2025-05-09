using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exportar
{
    internal class Acciones
    {
        private List<Alumno> alumnolist = new List<Alumno>();
        Correo correo = new Correo();


        public List<Alumno> Mostrar()
        {
            try
            {
                return alumnolist;
            }
            catch (Exception ex)
            {
                correo.EnviarCorreo(ex.ToString());
                throw;
            }
        }

        public bool ExportarExcel()
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "Alumnos.xlsx");

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Alumnos");
                    worksheet.Cell(1, 0).Value = "Nombre";
                    worksheet.Cell(1, 2).Value = "Edad";
                    worksheet.Cell(1, 3).Value = "Carrera";
                    worksheet.Cell(1, 4).Value = "Matricula";
                    worksheet.Cell(1, 5).Value = "Fecha Registro";

                    int row = 2;
                    foreach (var alumno in alumnolist)
                    {
                        worksheet.Cell(row, 1).Value = alumno.Nombre;
                        worksheet.Cell(row, 2).Value = alumno.Edad;
                        worksheet.Cell(row, 3).Value = alumno.Carrera;
                        worksheet.Cell(row, 4).Value = alumno.Matricula;
                        worksheet.Cell(row, 5).Value = alumno.Fechanacimiento;
                        row++;
                    }

                    workbook.SaveAs(filePath);
                }

                return true;

            }
            catch (Exception ex)
            {
                correo.EnviarCorreo(ex.ToString());
                throw;
            }
        }

        public bool Importar()
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "Alumnos.xlsx");

                if (!File.Exists(filePath))
                    return false;

                var newList = new List<Alumno>();

                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Alumnos");
                    var rows = worksheet.RowsUsed().Skip(1); // Omitir encabezados

                    foreach (var row in rows)
                    {
                        string nombre = row.Cell(1).GetString();
                        int edad = int.Parse(row.Cell(2).GetValue<string>());
                        string carrera = row.Cell(3).GetString();
                        int matricula = int.Parse(row.Cell(4).GetValue<string>());
                        DateTime fechaIngreso = DateTime.Parse(row.Cell(5).GetString());

                        newList.Add(new Alumno(nombre, edad, carrera, matricula, fechaIngreso));
                    }
                }

                alumnolist = newList;
                return true;
            }
            catch (Exception ex)
            {
                correo.EnviarCorreo(ex.ToString());
                throw;
            }

        }
    }

}


