
using Microsoft.AspNetCore.Mvc;
using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Controllers
{
    public class UsuariosController : Controller
    {


        public UsuariosController()
        {
        }

        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("Index", "Transacciones");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}