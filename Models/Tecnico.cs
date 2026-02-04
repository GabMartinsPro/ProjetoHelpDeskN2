namespace ProjetoHelpDeskN2.Models
{
    public class Tecnico : Usuario 
    { 
        public string? Especialidade { get; set; } // O '?' resolve o aviso
    }
}