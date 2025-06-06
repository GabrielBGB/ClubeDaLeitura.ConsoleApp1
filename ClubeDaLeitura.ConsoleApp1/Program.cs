// Local: ClubeDaLeitura.ConsoleApp1/Program.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.ConsoleApp1.Repositorios;
using ClubeDaLeitura.ConsoleApp1.Telas;
using System;

// O NAMESPACE É A "CAIXA" EXTERNA DO PROJETO
namespace ClubeDaLeitura.ConsoleApp1
{
    // A CLASSE PROGRAM É A "CASA" ONDE O CÓDIGO DEVE FICAR
    class Program
    {
        // O MÉTODO MAIN É A "PORTA DE ENTRADA". TUDO COMEÇA AQUI.
        static void Main(string[] args)
        {
            // --- BLOCO DE CRIAÇÃO DOS OBJETOS (INSTANCIAÇÃO) ---
            // Primeiro, criamos todos os "armazéns" de dados
            var repositorioCaixa = new RepositorioCaixa();
            var repositorioAmigo = new RepositorioAmigo();
            var repositorioRevista = new RepositorioRevista();
            var repositorioMulta = new RepositorioMulta();
            var repositorioEmprestimo = new RepositorioEmprestimo();

            // Agora, criamos todas as telas, passando os repositórios que elas precisam
            var telaCaixa = new TelaCaixa(repositorioCaixa);
            var telaAmigo = new TelaAmigo(repositorioAmigo);
            var telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa, telaCaixa);
            var telaMulta = new TelaMulta(repositorioMulta);
            var telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista, repositorioMulta, telaAmigo, telaRevista);

            // --- LOOP DO MENU PRINCIPAL (COM A ORDEM AJUSTADA) ---
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("--- Clube da Leitura ---");
                Console.WriteLine("Data: " + DateTime.Now.ToLongDateString());

                // --- INÍCIO DA MUDANÇA ---
                Console.WriteLine("\n[1] Gerenciar Amigos");      // Amigos agora é a opção 1
                Console.WriteLine("[2] Gerenciar Caixas");       // Caixas agora é a opção 2
                // --- FIM DA MUDANÇA ---

                Console.WriteLine("[3] Gerenciar Revistas");
                Console.WriteLine("[4] Gerenciar Empréstimos");
                Console.WriteLine("[5] Gerenciar Multas");
                Console.WriteLine("\n[0] Sair");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    // --- INÍCIO DA MUDANÇA ---
                    case "1": ExecutarMenuModulo("Gerenciar Amigos", telaAmigo); break; // Case 1 agora chama a tela de Amigos
                    case "2": ExecutarMenuModulo("Gerenciar Caixas", telaCaixa); break; // Case 2 agora chama a tela de Caixas
                    // --- FIM DA MUDANÇA ---

                    case "3": ExecutarMenuModulo("Gerenciar Revistas", telaRevista); break;
                    case "4": MenuEmprestimos(telaEmprestimo); break;
                    case "5": telaMulta.ApresentarMenu(); break;
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

        // --- MÉTODOS AUXILIARES PARA OS MENUS (DENTRO DA CLASSE PROGRAM) ---

        // Menu para os cadastros simples (Caixa, Amigo, Revista)
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

        // Menu específico para a tela de Empréstimos
        static void MenuEmprestimos(TelaEmprestimo tela)
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("--- Gerenciar Empréstimos ---");
                Console.WriteLine("\n[1] Registrar novo empréstimo");
                Console.WriteLine("[2] Registrar devolução");
                Console.WriteLine("[3] Listar todos os empréstimos");
                Console.WriteLine("\n[0] Voltar ao menu principal");

                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": tela.RegistrarEmprestimo(); break;
                    case "2": tela.RegistrarDevolucao(); break;
                    case "3": tela.ListarEmprestimos(); break;
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
    } // FIM DA CLASSE PROGRAM
} // FIM DO NAMESPACE