using System.Collections.Generic;
using ProjetoHelpDeskN2.Interfaces; // ESTA LINHA É FUNDAMENTAL

namespace ProjetoHelpDeskN2.Models
{
    public class Chamado : IAtribuivel, IEncerravel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Status { get; set; }
        public List<HistoricoChamado> Historico { get; private set; } = new List<HistoricoChamado>();
        public Tecnico TecnicoResponsavel { get; private set; }

        public void AtribuirTecnico(Tecnico t) {
            TecnicoResponsavel = t;
            Status = "Em Atendimento";
            Historico.Add(new HistoricoChamado($"Técnico {t.Nome} atribuído."));
        }

        public void Encerrar(string r) {
            Status = "Encerrado";
            Historico.Add(new HistoricoChamado($"Encerrado: {r}"));
        }
    }
}