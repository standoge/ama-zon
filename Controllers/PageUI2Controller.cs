﻿using Microsoft.AspNetCore.Mvc;
using Spire.Doc;
using Spire.Doc.Documents;


namespace ama_zon.Controllers
{
    public class PageUI2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Nombre, string Apellidos, string Direccion, DateTime Fecha, string pais)
        {
            string nombre = Nombre + " " + Apellidos;
            Document doc = new Document("Controllers/Templates/permanent-template.docx");
            Document newdoc = doc.Clone();
            newdoc.SaveToFile("Controllers/Templates/" + nombre + ".docx");
            Document contrato = new Document();
            contrato.LoadFromFile("Controllers/Templates/" + nombre + ".docx");
            contrato.Replace("$nombre_receptor", nombre, false, true);
            contrato.Replace("$direccion_receptor", Direccion, false, true);
            contrato.Replace("$fecha_emision", Fecha.ToString("dd MMMM, yyyy"), false, true);
            contrato.Replace("$pais", pais, false, true);
            contrato.SaveToFile("Controllers/Templates/" + nombre + ".docx");
            contrato.Close();

            contrato.LoadFromFile("Controllers/Templates/" + nombre + ".docx");
            contrato.SaveToFile("Controllers/Templates/" + nombre + ".pdf", FileFormat.PDF);
            return View("Index");
        }


    }
}
