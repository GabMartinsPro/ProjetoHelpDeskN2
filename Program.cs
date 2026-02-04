using System;
using ProjetoHelpDeskN2.Models;

Console.WriteLine("=== SISTEMA HELP DESK N2 - ENTRADA DE DADOS ===");

// --- DADOS DO CLIENTE ---
var cliente = new Cliente { Id = 1 };
Console.Write("Nome do Cliente: ");
cliente.Nome = Console.ReadLine();
Console.Write("Empresa: ");
cliente.Empresa = Console.ReadLine();

// --- DADOS DO TÉCNICO ---
var tecnico = new Tecnico { Id = 101 };
Console.WriteLine("\n--- ATRIBUIÇÃO DE TÉCNICO ---");
Console.Write("Nome do Técnico N2: ");
tecnico.Nome = Console.ReadLine();
Console.Write("Especialidade do Técnico: ");
tecnico.Especialidade = Console.ReadLine();

// --- CRIAÇÃO E FLUXO DO CHAMADO ---
var chamado = new Chamado { 
    Id = 500, 
    Titulo = "Instabilidade em Link de Fibra (Evento ao Vivo)",
    Status = "Aberto" 
};

// Registrando a abertura com os dados digitados
chamado.Historico.Add(new HistoricoChamado($"Chamado aberto por {cliente.Nome} ({cliente.Empresa})"));

// Atribuindo o técnico que você acabou de cadastrar
chamado.AtribuirTecnico(tecnico);

// Encerrando o chamado
Console.WriteLine("\nDigitando resolução do problema...");
Console.Write("O que foi feito? ");
string resolucao = Console.ReadLine() ?? "Resolvido sem descrição";
chamado.Encerrar(resolucao);

// --- RELATÓRIO FINAL ---
Console.WriteLine("\n=========================================");
Console.WriteLine($"CHAMADO #{chamado.Id} - {chamado.Titulo}");
Console.WriteLine($"TÉCNICO RESPONSÁVEL: {chamado.TecnicoResponsavel.Nome}");
Console.WriteLine("-----------------------------------------");
Console.WriteLine("HISTÓRICO DE ATENDIMENTO:");

foreach (var log in chamado.Historico) {
    Console.WriteLine($"[{log.Data:dd/MM HH:mm}] - {log.Descricao}");
}
Console.WriteLine("=========================================");