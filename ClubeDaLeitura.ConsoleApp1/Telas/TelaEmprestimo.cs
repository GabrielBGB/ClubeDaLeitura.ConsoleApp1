// Local: ClubeDaLeitura.ConsoleApp1/Telas/TelaEmprestimo.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.ConsoleApp1.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeDaLeitura.ConsoleApp1.Telas
{
    public class TelaEmprestimo
    {
        private readonly RepositorioEmprestimo repositorioEmprestimo;
        private readonly RepositorioAmigo repositorioAmigo;
        private readonly RepositorioRevista repositorioRevista;
        private readonly RepositorioMulta repositorioMulta;
        private readonly TelaAmigo telaAmigo;
        private readonly TelaRevista telaRevista;

        public TelaEmprestimo(
            RepositorioEmprestimo repoEmp,
            RepositorioAmigo repoAmg,
            RepositorioRevista repoRev,
            RepositorioMulta repoMulta,
            TelaAmigo tAmg,
            TelaRevista tRev)
        {
            repositorioEmprestimo = repoEmp;
            repositorioAmigo = repoAmg;
            repositorioRevista = repoRev;
            repositorioMulta = repoMulta;
            telaAmigo = tAmg;
            telaRevista = tRev;
        }

        public void RegistrarEmprestimo()
        {
            MostrarCabecalho("Registrar Novo Empréstimo");

            if (!telaAmigo.Listar())
            {
                MostrarMensagem("Nenhum amigo cadastrado. Cadastre um amigo primeiro.", ConsoleColor.Yellow);
                return;
            }
            Console.Write("\nDigite o ID do amigo que está pegando a revista: ");
            int idAmigo = ObterIdValido();
            Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);
            if (amigoSelecionado == null)
            {
                MostrarMensagem("Amigo não encontrado.", ConsoleColor.Red);
                return;
            }

            List<Revista> revistasDisponiveis = repositorioRevista.SelecionarTodos().Where(r => r.Status == "Disponível").ToList();
            if (revistasDisponiveis.Count == 0)
            {
                MostrarMensagem("Nenhuma revista disponível no momento.", ConsoleColor.Yellow);
                return;
            }
            telaRevista.Listar(revistasDisponiveis);

            Console.Write("\nDigite o ID da revista a ser emprestada: ");
            int idRevista = ObterIdValido();
            Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);
            if (revistaSelecionada == null || revistaSelecionada.Status != "Disponível")
            {
                MostrarMensagem("Revista não encontrada ou não está disponível.", ConsoleColor.Red);
                return;
            }

            Emprestimo novoEmprestimo = new Emprestimo
            {
                Amigo = amigoSelecionado,
                Revista = revistaSelecionada,
                DataEmprestimo = DateTime.Now
            };
            novoEmprestimo.DataDevolucao = novoEmprestimo.DataEmprestimo.AddDays(revistaSelecionada.Caixa.DiasEmprestimo);

            string[] erros = novoEmprestimo.Validar();
            if (erros.Length > 0)
            {
                ApresentarErros(erros);
                return;
            }

            revistaSelecionada.Status = "Emprestada";
            repositorioEmprestimo.Inserir(novoEmprestimo);
            MostrarMensagem("Empréstimo registrado com sucesso!", ConsoleColor.Green);
        }

        // MÉTODO AGORA COMPLETO
        public void RegistrarDevolucao()
        {
            MostrarCabecalho("Registrar Devolução de Revista");
            List<Emprestimo> emprestimosAbertos = repositorioEmprestimo.SelecionarTodos().Where(e => e.Status == "Aberto").ToList();
            if (emprestimosAbertos.Count == 0)
            {
                MostrarMensagem("Nenhum empréstimo em aberto para devolver.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-15}", "ID", "Amigo", "Revista", "Data Devolução");
            Console.WriteLine(new string('-', 70));
            foreach (var emp in emprestimosAbertos)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-15}", emp.Id, emp.Amigo.Nome, emp.Revista.Titulo, emp.DataDevolucao.ToShortDateString());
            }

            Console.Write("\nDigite o ID do empréstimo a ser devolvido: ");
            int idEmprestimo = ObterIdValido();
            Emprestimo emprestimo = repositorioEmprestimo.SelecionarPorId(idEmprestimo);

            if (emprestimo == null || emprestimo.Status != "Aberto")
            {
                MostrarMensagem("Empréstimo não encontrado ou já está fechado.", ConsoleColor.Red);
                return;
            }

            if (DateTime.Now > emprestimo.DataDevolucao)
            {
                TimeSpan diasDeAtraso = DateTime.Now.Date - emprestimo.DataDevolucao.Date;
                decimal valorMulta = (decimal)diasDeAtraso.TotalDays * 2.00m;

                if (valorMulta > 0)
                {
                    Multa novaMulta = new Multa
                    {
                        Emprestimo = emprestimo,
                        Valor = Math.Round(valorMulta, 2),
                        EstaPaga = false
                    };
                    repositorioMulta.Inserir(novaMulta);
                    MostrarMensagem($"Devolução com atraso! Multa de R${novaMulta.Valor:F2} gerada.", ConsoleColor.Yellow);
                }
            }

            emprestimo.Fechar();

            MostrarMensagem("Devolução registrada com sucesso!", ConsoleColor.Green);
        }

        // MÉTODO AGORA COMPLETO
        public void ListarEmprestimos()
        {
            MostrarCabecalho("Listando Todos os Empréstimos");
            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();
            if (emprestimos.Count == 0)
            {
                MostrarMensagem("Nenhum empréstimo registrado.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("{0,-5} | {1,-10} | {2,-15} | {3,-15} | {4,-15}", "ID", "Status", "Amigo", "Revista", "Data Devolução");
            Console.WriteLine(new string('-', 80));
            foreach (var emp in emprestimos)
            {
                Console.WriteLine("{0,-5} | {1,-10} | {2,-15} | {3,-15} | {4,-15}",
                    emp.Id, emp.Status, emp.Amigo.Nome, emp.Revista.Titulo, emp.DataDevolucao.ToShortDateString());
            }
            // ADICIONE ESTA LINHA PARA PAUSAR A TELA E MANTER A LISTA VISÍVEL
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        // ======================================================
        // MÉTODOS PRIVADOS DE AJUDA (AGORA COMPLETOS)
        // ======================================================

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

        private void ApresentarErros(string[] erros)
        {
            MostrarMensagem("Por favor, corrija os seguintes erros:", ConsoleColor.Red);
            foreach (string erro in erros)
            {
                Console.WriteLine($"- {erro}");
            }
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