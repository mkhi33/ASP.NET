using Portafolio.Models;

public interface IServicioEmail
{   
    Task Enviar(ContactoViewModel contactoViewModel);
}