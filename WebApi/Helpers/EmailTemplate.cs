using System;
using Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebApi.Helpers
{
    public class EmailTemplate
    {
        private readonly AppSettings _appSettings;

        public EmailTemplate(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        private string Head()
        {
            return @"
                <!DOCTYPE html>
                <html lang='es'>
                <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <meta http-equiv='X-UA-Compatible' content='ie=edge'>
                <style>
                .negrita{
                font-weight: bold;
            }
            .gris{
                color:gray;
            }
            *{
                font-family:'Lucida Console', Arial, 'Helvetica';
                color:#404040;
            }
            a{
                color:#0000FF;
            }
            </style>
                </head>
                <body>";
        }

        private string Signature()
        {
            AppSettings appSettings = new AppSettings();
            return $@"<br>
                    <div>
                        <span class='negrita'>AsteriskMenu | Email: contact@astersikmenu.com</span>                
                    </div>
                <br>
                    <div>
                        <span class='gris'>
                            Si el receptor no es la persona a la que va dirigido este e-mail confidencial, 
                            se le notifica que la distribución o copias de esta comunicación está terminantemente prohibida.
                            Si usted ha recibido esta transmisión por error, le rogamos se ponga en contacto con el emisor de esta comunicación. Gracias.
                        </span>
                    </div>
                <br>
                    <div>
                        <span class='gris'>
                            If the reader is not the specified recipient of this confidential e-mail,
                            you are hereby notified that any distribution or copying of this communication is strictly prohibited.
                            If you are not the specified recipient, please notify the sender immediately. Thank you.
                        </span>
                    </div>
                <br>
                </body>
        </html>";
        }

        public string UsersVerify(User user)
        {
            
            return $"{Head()}"+
                   $"Hola, {user.FirstName} {user.LastName}.<br>" +
                   $"Se ha creado una cuenta en AsteriskMenu, para poder continuar tienes que verificar tu cuenta pulsando en el siguiente enlace, </br>" +
                   $"{_appSettings.URL}/Users/Verify/{user.TokenVerification} <br>" +
                   $"Si no has sido tu ponte en contacto con nostros." +
                   $"{Signature()}";
        }

        public string NewOrder()
        {
            return $"{Head()}"+
                   $"Hola.<br/>" +
                   $"Tienes una nueva orden, verifique la app.<br/>" +
                   $"{Signature()}";
        }

        /**
         * TODO
         */
        public string ResetPassword(Guid? token)
        {
            return $"{Head()}"+
                   $"Hola.<br>" +
                   $"Para restablecer tu contraseña haz click en el siguiente enlace, </br>" +
                   $"{_appSettings.URL}/Users/Password/Change/{token} <br>" +
                   $"Si no has sido tu ponte en contacto con nostros." +
                   $"{Signature()}";
        }
    }
}