namespace ProjetoHelpDeskN2.Interfaces
{
    public interface IAtribuivel
    {
        // Usamos o caminho completo para evitar o erro de não encontrar o Tecnico
        void AtribuirTecnico(Models.Tecnico tecnico);
    }
}