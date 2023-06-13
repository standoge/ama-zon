using ama_zon.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace ama_zon.Controllers
{
    public class UploadTxtFileController : Controller
    {
        private readonly EmpleoadoService _empleoadoService;

        public UploadTxtFileController(EmpleoadoService empleoadoService)
        {
            _empleoadoService = empleoadoService;
        }

        public ActionResult UploadForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadForm(IFormCollection collection)
        {
            try
            {
                var file = collection.Files.GetFile("File");

                if (file == null)
                {
                    ViewBag.Error = "El archivo no puede ser nulo";
                    return View("UploadForm");
                }

                if (!IsTxtFile(file))
                {
                    ViewBag.Error = "Por favor seleccione un archivo con extensión válida (txt).";
                    return View("UploadForm");
                }

                if (!HasContent(file))
                {
                    ViewBag.Error = "El archivo está vacío.";
                    return View("UploadForm");
                }

                var employeeList = GetEmployeeDataFromTxtFile(file);
                _empleoadoService.SaveEmployees(employeeList);

                ViewBag.MessageOk = "Los empleados se han guardado exitosamente";
                return View("UploadForm");
            }
            catch (Exception ex)
            {
                ViewBag.MessageOk = string.Empty;
                ViewBag.Exception = "Ocurrió un error al procesar la información: " + ex.Message;
                return View("UploadForm");
            }
        }

        private static bool IsTxtFile(IFormFile file)
        {
            var validExtensions = new[] { ".txt" };

            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);

                if (validExtensions.Contains(fileExtension.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasContent(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string contenido = reader.ReadToEnd();
                return !string.IsNullOrEmpty(contenido);
            }
        }

        private static List<Empleado> GetEmployeeDataFromTxtFile(IFormFile file)
        {
            var employeeList = new List<Empleado>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    string linea = reader.ReadLine();
                    string[] datos = linea.Split(';');

                    // Crear objeto Empleado con los datos de la línea
                    var empleado = new Empleado
                    {
                        Identificacion = datos[0],
                        Nombre = datos[1],
                        Apellido = datos[2],
                        Direccion = datos[3],
                        FechaNacimiento = DateTime.Parse(datos[4]),
                        Correo = datos[5],
                        PaisId = int.Parse(datos[6]),
                        AcuerdoId = int.Parse(datos[7])
                    };

                    employeeList.Add(empleado);
                }
            }

            return employeeList;
        }
    }
}
