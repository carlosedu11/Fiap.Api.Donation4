using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation4.ViewModel
{
    public class LoginRequestVM
    {
        [Required(ErrorMessage = "E-mail é requerido")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "Senha é requerido")]
        public string Senha { get; set; }

        public LoginRequestVM()
        {
            
        }

        public LoginRequestVM(string emailUsuario, string senha)
        {
            EmailUsuario = emailUsuario;
            Senha = senha;
        }
    }
}
