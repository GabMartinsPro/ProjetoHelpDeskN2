using System;
namespace ProjetoHelpDeskN2.Models
{
    public class HistoricoChamado
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public HistoricoChamado(string d) { Data = DateTime.Now; Descricao = d; }
    }
}