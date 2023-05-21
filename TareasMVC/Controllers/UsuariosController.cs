using TareasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Servicios;

namespace TareasMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext context;

        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            var usuario = new IdentityUser
            {
                Email = modelo.Email,
                UserName = modelo.Email
            };
            var resultado = await userManager.CreateAsync(usuario, password: modelo.Password);
            if (resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(modelo);
            }
        }
        [AllowAnonymous]
        public IActionResult Login(string mensaje = null)
        {
            if (mensaje is not null)
            {
                ViewData["Mensaje"] = mensaje;
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            var resultado = await signInManager.PasswordSignInAsync(modelo.Email, modelo.Password, modelo.Recuerdame, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña no válido");
                return View(modelo);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ChallengeResult LoginExterno(string proveedor, string urlRetorno = null)
        {
            var urlRedireccion = Url.Action("RegistrarUsuarioExterno", values: new { urlRetorno });
            var propiedades = signInManager.ConfigureExternalAuthenticationProperties(proveedor, urlRedireccion);
            return new ChallengeResult(proveedor, propiedades);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RegistrarUsuarioExterno(string urlRetorno = null, string remoteError = null)
        {
            urlRetorno = urlRetorno ?? Url.Content("~/");
            var mensaje = "";
            if (remoteError is not null)
            {
                mensaje = $"Error del proveedor externo: {remoteError}";
                return RedirectToAction("Login", routeValues: new { mensaje });
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info is null)
            {
                mensaje = "Error al cargar la información del proveedor externo";
                return RedirectToAction("Login", routeValues: new { mensaje });
            }
            var resultadoLoginExterno = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);

            // Si la cuenta existe
            if (resultadoLoginExterno.Succeeded)
            {
                return LocalRedirect(urlRetorno);
            }

            // Si la cuenta no existe, crearla
            string email = "";
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                email = info.Principal.FindFirstValue(ClaimTypes.Email);
            }
            else
            {
                mensaje = "Error leyendo el email del usuario del proveedor externo";
                return RedirectToAction("Login", routeValues: new { mensaje });
            }
            var usuario = new IdentityUser
            {
                UserName = email,
                Email = email
            };
            var resultado = await userManager.CreateAsync(usuario);
            if (!resultado.Succeeded)
            {
                mensaje = resultado.Errors.First().Description;
                return RedirectToAction("Login", routeValues: new { mensaje });
            }
            var resutadoAgregarLogin = await userManager.AddLoginAsync(usuario, info);
            if (resultadoLoginExterno.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true, info.LoginProvider);
                return LocalRedirect(urlRetorno);
            }

            mensaje = "Ha ocurrido un error agregando el login";
            return RedirectToAction("Login", routeValues: new { mensaje });
        }

        [HttpGet]
        public async Task<IActionResult> Listado(string mensaje = null)
        {
            var usuarios = await context.Users.Select(u => new UsuarioViewModel
            {
                Email = u.Email,
            }).ToListAsync();
            var modelo = new UsuariosListadoViewModel();
            modelo.Usuarios = usuarios;
            modelo.Mensaje = mensaje;
            return View(modelo);

        }

        [HttpPost]
        public async Task<IActionResult> HacerAdmin(string email)
        {
            var usuario = await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(usuario is null)
            {
                return NotFound();
            }
            await userManager.AddToRoleAsync(usuario, Constantes.RolAdmin);
            return RedirectToAction("Listado", routeValues: new { mensaje = $"El usuario {email} ahora es administrador" });
        }
        [HttpPost]
        public async Task<IActionResult> RemoverAdmin(string email)
        {
            var usuario = await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(usuario is null)
            {
                return NotFound();
            }
            await userManager.RemoveFromRoleAsync(usuario, Constantes.RolAdmin);
            return RedirectToAction("Listado", routeValues: new { mensaje = $"El usuario {email} ya no es administrador" });
        }


    }
}