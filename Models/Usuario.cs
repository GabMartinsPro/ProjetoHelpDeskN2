namespace ProjetoHelpDeskN2.Models
{
    public abstract class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; } // O '?' resolve o aviso
        public string? Email { get; set; }
    }
}