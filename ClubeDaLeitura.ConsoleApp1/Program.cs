// Local: ClubeDaLeitura.ConsoleApp1/Program.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.ConsoleApp1.Repositorios;
using ClubeDaLeitura.ConsoleApp1.Telas;
using System;

namespace ClubeDaLeitura.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- Bloco de Criação dos Objetos (Instanciação) ---
            var repositorioCaixa = new RepositorioCaixa();
            var repositorioAmigo = new RepositorioAmigo();
            var repositorioRevista = new RepositorioRevista();
            var repositorioMulta = new RepositorioMulta();
            var repositorioEmprestimo = new RepositorioEmprestimo();
            var repositorioReserva = new RepositorioReserva();

            var telaCaixa = new TelaCaixa(repositorioCaixa);
            var telaAmigo = new TelaAmigo(repositorioAmigo);
            var telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa, telaCaixa);
            var telaMulta = new TelaMulta(repositorioMulta, repositorioAmigo, telaAmigo);
            var telaReservas = new TelaReservas(repositorioReserva, repositorioRevista, repositorioAmigo, repositorioMulta, telaRevista, telaAmigo);
            var telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioReserva, repositorioAmigo, repositorioRevista, repositorioMulta, telaAmigo, telaRevista, telaReservas);

            // --- Loop do Menu Principal ---
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("--- Clube da Leitura ---");
                Console.WriteLine("Data: " + DateTime.Now.ToLongDateString());
                Console.WriteLine("\n[1] Gerenciar Amigos");
                Console.WriteLine("[2] Gerenciar Caixas");
                Console.WriteLine("[3] Gerenciar Revistas");
                Console.WriteLine("[4] Gerenciar Empréstimos");
                Console.WriteLine("[5] Gerenciar Multas");
                Console.WriteLine("[6] Gerenciar Reservas");
                Console.WriteLine("\n[0] Sair");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": ExecutarMenuModulo("Gerenciar Amigos", telaAmigo); break;
                    case "2": ExecutarMenuModulo("Gerenciar Caixas", telaCaixa); break;
                    case "3": ExecutarMenuModulo("Gerenciar Revistas", telaRevista); break;
                    case "4": MenuEmprestimos(telaEmprestimo); break;
                    case "5": telaMulta.ApresentarMenu(); break;
                    case "6": telaReservas.ApresentarMenu(); break;
                    case "0": continuar = false; break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção inválida!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        // --- Métodos Auxiliares para os Menus ---

        static void ExecutarMenuModulo(string titulo, ITelaCadastravel tela)
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine($"--- {titulo} ---");
                Console.WriteLine("\n[1] Inserir novo registro");
                Console.WriteLine("[2] Editar registro existente");
                Console.WriteLine("[3] Excluir registro");
                Console.WriteLine("[4] Listar todos os registros");
                Console.WriteLine("\n[0] Voltar ao menu principal");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": tela.Inserir(); break;
                    case "2": tela.Editar(); break;
                    case "3": tela.Excluir(); break;
                    case "4":
                        tela.Listar();
                        Console.WriteLine("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case "0": voltar = true; break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção inválida!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MenuEmprestimos(TelaEmprestimo tela)
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("--- Gerenciar Empréstimos ---");
                Console.WriteLine("\n[1] Registrar novo empréstimo");
                Console.WriteLine("[2] Converter Reserva em Empréstimo");
                Console.WriteLine("[3] Registrar devolução");
                Console.WriteLine("[4] Listar todos os empréstimos");
                Console.WriteLine("\n[0] Voltar ao menu principal");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": tela.RegistrarEmprestimo(); break;
                    case "2": tela.ConverterReservaEmEmprestimo(); break;
                    case "3": tela.RegistrarDevolucao(); break;
                    case "4": tela.ListarEmprestimos(); break;
                    case "0": voltar = true; break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção inválida!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}