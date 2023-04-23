using Portafolio.Interfaces;
using Portafolio.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace Portafolio.Services
{
    public class ServicioEmail : IServicioEmail
    {
        private readonly IConfiguration configuration;

        public ServicioEmail(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task Enviar(ContactoViewModel contactoViewModel)
        {
            var email = configuration.GetValue<string>("EMAIL_FROM");
            var password = configuration.GetValue<string>("EMAIL_PASSWORD");
            var nombre = configuration.GetValue<string>("EMAIL_NAME");

            var correo = new MailMessage();
            correo.From = new MailAddress(email);
            correo.To.Add(email);
            correo.Subject = $"El cliente {contactoViewModel.Nombre} quiere contactarte";
            correo.Body = @$"
            De: {contactoViewModel.Nombre}
            Email: {contactoViewModel.Email}
            Mensaje: {contactoViewModel.Mensaje}";

            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(email, password);

            try
            {
                // Enviar el correo
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}