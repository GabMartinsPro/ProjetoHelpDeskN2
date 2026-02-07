using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoHelpDeskN2.Models;

// 1.BANCO DE TÉCNICOS
var bancoTecnicos = new List<Tecnico>
{
    new Tecnico { Id = 101, Nome = "Ana Souza", Especialidade = "Senha" },
    new Tecnico { Id = 102, Nome = "Carlos Redes", Especialidade = "Conexão" },
    new Tecnico { Id = 103, Nome = "Bia Hardware", Especialidade = "Equipamento" }
};

Console.WriteLine("=== SISTEMA HELP DESK N2 - INTERFACE DE ACESSO ===");

// 2. PAINEL DO CLIENTE
var cliente = new Cliente { Id = 1 };
Console.Write("Nome do Cliente: ");
cliente.Nome = Console.ReadLine() ?? "Usuário";
Console.Write("Empresa: ");
cliente.Empresa = Console.ReadLine() ?? "N/A";

Console.Write("\nRelate o seu problema: ");
string relato = Console.ReadLine() ?? "";

string senhaNova = "";
if (relato.ToLower().Contains("senha"))
{
    Console.Write("Qual a NOVA senha desejada? ");
    senhaNova = Console.ReadLine() ?? "";
}

var chamado = new Chamado { 
    Id = 500, 
    Titulo = "Atendimento N2",
    Status = "Aberto" 
};

chamado.Historico.Add(new HistoricoChamado($"Chamado aberto por {cliente.Nome} ({cliente.Empresa}). Relato: {relato}"));

// TRIAGEM 
var tecnicoSorteado = bancoTecnicos.FirstOrDefault(t => relato.ToLower().Contains(t.Especialidade.ToLower())) 
                      ?? bancoTecnicos[0];

chamado.AtribuirTecnico(tecnicoSorteado);

Console.WriteLine($"\n[SISTEMA]: Triagem concluída. Técnico {tecnicoSorteado.Nome} assumiu o caso.");
Console.WriteLine("Pressione Enter para acessar o Sistema de Gerenciamento...");
Console.ReadLine();

// 3. MENU DE NAVEGAÇÃO ENTRE PERFIS
bool sistemaAtivo = true;
while (sistemaAtivo)
{
    Console.Clear();
    Console.WriteLine("=== PORTAL DE SUPORTE N2 ===");
    Console.WriteLine($"Chamado #{chamado.Id} - Status: {chamado.Status}");
    Console.WriteLine("-----------------------------------------");
    Console.WriteLine("1 - Entrar como CLIENTE (Apenas Visualizar)");
    Console.WriteLine("2 - Entrar como TÉCNICO (Modificar Chamado)");
    Console.WriteLine("3 - Finalizar Sessão");
    Console.Write("\nEscolha seu perfil de acesso: ");
    string perfil = Console.ReadLine() ?? "";

    if (perfil == "1") // VISÃO DO CLIENTE
    {
        Console.Clear();
        Console.WriteLine($"=== PAINEL DO CLIENTE: {cliente.Nome} ===");
        Console.WriteLine($"SITUAÇÃO DO CHAMADO: {chamado.Status}");
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("HISTÓRICO DE ATENDIMENTO:");
        foreach (var log in chamado.Historico) 
        {
            Console.WriteLine($"[{log.Data:dd/MM HH:mm}] - {log.Descricao}");
        }
        Console.WriteLine("\n[!] Você está em modo de leitura.");
        Console.WriteLine("Pressione Enter para voltar ao menu principal...");
        Console.ReadLine();
    }
    else if (perfil == "2") // VISÃO DO TÉCNICO
    {
        if (chamado.Status == "Encerrado")
        {
            Console.WriteLine("\n[AVISO]: Este chamado já foi finalizado e não permite mais alterações.");
            Console.ReadLine();
            continue;
        }

        bool emAtendimento = true;
        while (emAtendimento)
        {
            Console.Clear();
            Console.WriteLine($"=== PAINEL DO TÉCNICO: {tecnicoSorteado.Nome} ===");
            Console.WriteLine($"Cliente: {cliente.Nome} | Relato: {relato}");
            if (!string.IsNullOrEmpty(senhaNova)) Console.WriteLine($"SOLICITAÇÃO: Alterar senha para '{senhaNova}'");

            Console.WriteLine("\nOPÇÕES DE EDIÇÃO:");
            Console.WriteLine("1 - Visualizar Histórico");
            Console.WriteLine("2 - Adicionar Nota Técnica");
            Console.WriteLine("3 - Encerrar Chamado");
            Console.WriteLine("4 - Sair do Painel Técnico");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine() ?? "";

            if (opcao == "1")
            {
                Console.WriteLine("\n--- HISTÓRICO ATUAL ---");
                foreach (var log in chamado.Historico) Console.WriteLine($"> {log.Descricao}");
                Console.WriteLine("\nPressione Enter para voltar...");
                Console.ReadLine();
            }
            else if (opcao == "2")
            {
                Console.Write("\nDigite a nota técnica: ");
                string nota = Console.ReadLine() ?? "";
                chamado.Historico.Add(new HistoricoChamado($"[NOTA TÉCNICA]: {nota}"));
            }
            else if (opcao == "3")
            {
                Console.Write("\nRelate o que foi feito: ");
                string solucao = Console.ReadLine() ?? "Resolvido conforme solicitado.";
                
                if (!string.IsNullOrEmpty(senhaNova)) 
                    solucao = $"[SEGURANÇA]: Senha alterada para '{senhaNova}'. " + solucao;

                chamado.Encerrar(solucao);
                emAtendimento = false;
            }
            else if (opcao == "4")
            {
                emAtendimento = false;
            }
        }
    }
    else if (perfil == "3")
    {
        sistemaAtivo = false;
    }
}

// 4. RELATÓRIO FINAL E MENSAGEM DE ENCERRAMENTO
Console.Clear();
Console.WriteLine("=== RELATÓRIO FINAL DE ATENDIMENTO ===");
Console.WriteLine($"STATUS: {chamado.Status}");
Console.WriteLine($"TÉCNICO: {chamado.TecnicoResponsavel?.Nome}");
Console.WriteLine("-----------------------------------------");
Console.WriteLine("HISTÓRICO COMPLETO:");

foreach (var log in chamado.Historico) {
    Console.WriteLine($"[{log.Data:dd/MM HH:mm}] - {log.Descricao}");
}

Console.WriteLine("=========================================");
Console.ResetColor();
Console.ForegroundColor = ConsoleColor.Cyan; 

Console.WriteLine("\n=================================================");
Console.WriteLine("       ATENDIMENTO FINALIZADO COM SUCESSO!       ");
Console.WriteLine("=================================================");
Console.ResetColor();

Console.WriteLine($"\nObrigado por utilizar o Suporte N2, {cliente.Nome}.");
Console.WriteLine("Seu protocolo foi registrado no GitHub.");
Console.WriteLine("Pressione qualquer tecla para fechar o sistema...");

Console.ReadKey();