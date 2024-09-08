using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoSalao.Models.Entity
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }

        [NotMapped]
        public string TipoFormatada => GetTipo();

        private string GetTipo()
        {
            return Tipo switch
            {
                1 => "Administrador",
                2 => "Funcionário",
                3 => "Cliente",
                _ => "Administrador"
            };
        }
    }
}
