using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoSalao.Models.Entity
{
    public class Agendamento : BaseEntity
    {
        public int IdServico { get; set; }
        public int IdProfissional { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataAgendamentoInicial { get; set; }
        public DateTime DataAgendamentoFinal { get; set; }

        [NotMapped]
        public string DataAgendamentoInicialFormatada => DataAgendamentoInicial.ToString("dd/MM/yy HH:mm");

        [NotMapped]
        public string DataAgendamentoFinalFormatada => DataAgendamentoFinal.ToString("dd/MM/yy HH:mm");

        [NotMapped]
        public string? NomeCliente { get; set; }

        [NotMapped]
        public string? NomeProfissional { get; set; }

        [NotMapped]
        public string? NomeServico { get; set; }
    }
}
