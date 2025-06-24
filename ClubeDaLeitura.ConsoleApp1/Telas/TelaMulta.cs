// Local: ClubeDaLeitura.ConsoleApp1/Telas/TelaMulta.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.ConsoleApp1.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeDaLeitura.ConsoleApp1.Telas
{
    public class TelaMulta
    {
        private readonly RepositorioMulta repositorioMulta;
        private readonly RepositorioAmigo repositorioAmigo;
        private readonly TelaAmigo telaAmigo;

        public TelaMulta(RepositorioMulta repoMulta, RepositorioAmigo repoAmigo, TelaAmigo tAmigo)
        {
            repositorioMulta = repoMulta;
            repositorioAmigo = repoAmigo;
            telaAmigo = tAmigo;
        }

        public void ApresentarMenu()
        {
            bool voltar = false;
            while (!voltar)
            {
                MostrarCabecalho("Gerenciar Multas");
                Console.WriteLine("[1] Visualizar Multas Pendentes");
                Console.WriteLine("[2] Quitar uma Multa");
                Console.WriteLine("[3] Visualizar Multas por Amigo");
                Console.WriteLine("\n[0] Voltar");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1": ListarMultas(pendentes: true); break;
                    case "2": QuitarMulta(); break;
                    case "3": VisualizarMultasPorAmigo(); break;
                    case "0": voltar = true; break;
                    default: MostrarMensagem("Opção inválida!", ConsoleColor.Red); break;
                }
            }
        }

        public void QuitarMulta()
        {
            MostrarCabecalho("Quitar Multa Pendente");
            if (!ListarMultas(pendentes: true)) return;
            Console.Write("\nDigite o ID da multa que deseja quitar: ");
            int idMulta = ObterIdValido();
            Multa multa = repositorioMulta.SelecionarPorId(idMulta);
            if (multa == null || multa.EstaPaga)
            {
                MostrarMensagem("ID de multa inválido ou a multa já foi paga.", ConsoleColor.Red);
                return;
            }
            multa.EstaPaga = true;
            repositorioMulta.Editar(idMulta, multa);
            MostrarMensagem("Multa quitada com sucesso!", ConsoleColor.Green);
        }

        private void VisualizarMultasPorAmigo()
        {
            MostrarCabecalho("Visualizar Multas por Amigo");
            if (!telaAmigo.Listar()) { MostrarMensagem("Nenhum amigo cadastrado.", ConsoleColor.Yellow); return; }
            Console.Write("\nDigite o ID do amigo para ver suas multas: ");
            int idAmigo = ObterIdValido();
            if (repositorioAmigo.SelecionarPorId(idAmigo) == null) { MostrarMensagem("Amigo não encontrado.", ConsoleColor.Red); return; }
            List<Multa> multasDoAmigo = repositorioMulta.SelecionarTodos().Where(m => m.Emprestimo.Amigo.Id == idAmigo).ToList();
            Listar(multasDoAmigo);
        }

        private bool ListarMultas(bool pendentes)
        {
            MostrarCabecalho("Listando Multas " + (pendentes ? "Pendentes" : ""));
            List<Multa> multas = repositorioMulta.SelecionarTodos();
            if (pendentes)
                multas = multas.Where(m => !m.EstaPaga).ToList();
            return Listar(multas);
        }

        private bool Listar(List<Multa> multas)
        {
            if (multas.Count == 0)
            {
                MostrarMensagem("Nenhuma multa encontrada para os critérios informados.", ConsoleColor.Yellow);
                return false;
            }
            Console.WriteLine("{0,-5} | {1,-10} | {2,-20} | {3,-10}", "ID", "Status", "Amigo", "Valor");
            Console.WriteLine(new string('-', 60));
            foreach (Multa multa in multas)
            {
                string status = multa.EstaPaga ? "Quitada" : "Pendente";
                Console.WriteLine("{0,-5} | {1,-10} | {2,-20} | R$ {3,-8:F2}", multa.Id, status, multa.Emprestimo.Amigo.Nome, multa.Valor);
            }
            Console.ReadKey();
            return true;
        }

        
        private int ObterIdValido()
        {
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                MostrarMensagem("Entrada inválida. Por favor, digite um número de ID válido.", ConsoleColor.Red);
                Console.Write("Digite o ID novamente: ");
            }
            return id;
        }

        private void ApresentarErros(string[] erros)
        {
            MostrarMensagem("Por favor, corrija os seguintes erros:", ConsoleColor.Red);
            foreach (string erro in erros) Console.WriteLine($"- {erro}");
            Console.ReadKey();
        }

        private void MostrarCabecalho(string titulo)
        {
            Console.Clear();
            Console.WriteLine("--- Clube da Leitura ---");
            Console.WriteLine($"\n{titulo}\n");
        }

        private void MostrarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine($"\n{mensagem}");
            Console.ResetColor();
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
