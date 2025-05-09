using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exportar
{
    internal class Correo
    {
        public void EnviarCorreo(string error)
        {
            try
            {
                var fromAddress = new MailAddress("113117@alumnouninter.mx", "Sistema Exportar"); // Reemplaza con tu correo
                var toAddress = new MailAddress("ecorrales@uninter.edu.mx", "Eduardo Corrales");

                const string subject = "Error en el sistema Exportar";
                string body = $"Se ha producido el siguiente error:\n\n{error}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("113117@alumnouninter.mx", "20051219S0f") // Reemplaza con tus credenciales
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                // En un entorno real podrías loguearlo en un archivo local, base de datos, etc.
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}