using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ama_zon.DataBase;


namespace ama_zon.Controllers
{
    public class EmpleadoController : Controller
    {

        private readonly IConfiguration _configuration;
        private string? searchString;

        public EmpleadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get
        public IActionResult index(String searchString)
        {
            var empleados = db.Empleado.ToList(); // Reemplaza "Empleados" con el nombre de tu tabla en la base de datos

            if (!string.IsNullOrEmpty(searchString))
            {
                empleados = empleados.Where(e => e.Nombre.Contains(searchString)).ToList();
            }


            return View();
        }


    }
}
