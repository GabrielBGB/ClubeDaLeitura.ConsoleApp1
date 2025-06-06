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

        public TelaMulta(RepositorioMulta repositorio)
        {
            repositorioMulta = repositorio;
        }

        public void ApresentarMenu()
        {
            bool voltar = false;
            while (!voltar)
            {
                MostrarCabecalho("Gerenciar Multas");
                Console.WriteLine("[1] Listar todas as multas");
                Console.WriteLine("[2] Quitar uma multa");
                Console.WriteLine("\n[0] Voltar");

                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ListarTodas();
                        Console.ReadKey();
                        break;
                    case "2":
                        QuitarMulta();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        MostrarMensagem("Opção inválida!", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }
            }
        }

        public void QuitarMulta()
        {
            MostrarCabecalho("Quitar Multa Pendente");
            List<Multa> multasPendentes = repositorioMulta
                .SelecionarTodos()
                .Where(m => !m.EstaPaga)
                .ToList();

            if (!Listar(multasPendentes))
            {
                MostrarMensagem("Nenhuma multa pendente para quitar.", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

            Console.Write("\nDigite o ID da multa que deseja quitar: ");
            int idMulta = ObterIdValido();

            Multa multa = repositorioMulta.SelecionarPorId(idMulta);

            if (multa == null || multa.EstaPaga)
            {
                MostrarMensagem("ID de multa inválido ou a multa já foi paga.", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }

            multa.EstaPaga = true;

            // Criamos uma nova instância apenas para passar ao método Editar, como exige o padrão
            Multa multaAtualizada = new Multa();
            multaAtualizada.Valor = multa.Valor;
            multaAtualizada.EstaPaga = true;

            repositorioMulta.Editar(idMulta, multaAtualizada);

            MostrarMensagem("Multa quitada com sucesso!", ConsoleColor.Green);
            Console.ReadKey();
        }

        public void ListarTodas()
        {
            MostrarCabecalho("Listando Todas as Multas");
            List<Multa> multas = repositorioMulta.SelecionarTodos();
            if (!Listar(multas))
            {
                MostrarMensagem("Nenhuma multa registrada.", ConsoleColor.Yellow);
            }
        }

        private bool Listar(List<Multa> multas)
        {
            if (multas.Count == 0)
            {
                return false;
            }
            Console.WriteLine("{0,-5} | {1,-10} | {2,-20} | {3,-10}", "ID", "Status", "Amigo do Empréstimo", "Valor");
            Console.WriteLine(new string('-', 60));
            foreach (Multa multa in multas)
            {
                // USA AS PROPRIEDADES CORRETAS: Id, EstaPaga, Emprestimo.Amigo.Nome, Valor
                string status = multa.EstaPaga ? "Paga" : "Pendente";
                Console.WriteLine("{0,-5} | {1,-10} | {2,-20} | R$ {3,-8:F2}",
                    multa.Id,
                    status,
                    multa.Emprestimo?.Amigo?.Nome ?? "N/A",
                    multa.Valor);
            }
            return true;
        }

        private int ObterIdValido()
        {
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                MostrarMensagem("Entrada inválida. Por favor, digite um número de ID.", ConsoleColor.Red);
                Console.Write("Digite o ID novamente: ");
            }
            return id;
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
        }
    }
}