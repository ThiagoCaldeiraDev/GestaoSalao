namespace GestaoSalao.Models.ViewModels.Entidades
{
    public class AgendamentoViewModel
    {
        public int IdAgendamento { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Profissional { get; set; }
        public string Servico { get; set; }
        public bool AllDay { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string Time { get; set; }
    }
}
