// Local: ClubeDaLeitura.ConsoleApp1/Telas/TelaCaixa.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.ConsoleApp1.Repositorios;
using System;
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Telas
{
    public class TelaCaixa : ITelaCadastravel
    {
        private readonly RepositorioCaixa repositorioCaixa;

        public TelaCaixa(RepositorioCaixa repositorio)
        {
            repositorioCaixa = repositorio;
        }

        public void Inserir()
        {
            MostrarCabecalho("Inserir Nova Caixa");
            Caixa novaCaixa = ObterDadosCaixa();
            string[] erros = novaCaixa.Validar();

            if (erros.Length > 0)
            {
                ApresentarErros(erros);
                return;
            }

            repositorioCaixa.Inserir(novaCaixa);
            MostrarMensagem("Caixa inserida com sucesso!", ConsoleColor.Green);
        }

        public void Editar()
        {
            MostrarCabecalho("Editar Caixa");
            if (!Listar())
            {
                MostrarMensagem("Nenhuma caixa para editar.", ConsoleColor.Yellow);
                return;
            }
            Console.Write("\nDigite o ID da caixa para editar: ");
            int id = ObterIdValido();
            if (repositorioCaixa.SelecionarPorId(id) == null)
            {
                MostrarMensagem("Caixa não encontrada.", ConsoleColor.Red);
                return;
            }
            Caixa caixaAtualizada = ObterDadosCaixa();
            string[] erros = caixaAtualizada.Validar();
            if (erros.Length > 0)
            {
                ApresentarErros(erros);
                return;
            }
            repositorioCaixa.Editar(id, caixaAtualizada);
            MostrarMensagem("Caixa editada com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            MostrarCabecalho("Excluir Caixa");
            if (!Listar())
            {
                MostrarMensagem("Nenhuma caixa para excluir.", ConsoleColor.Yellow);
                return;
            }
            Console.Write("\nDigite o ID da caixa para excluir: ");
            int id = ObterIdValido();
            if (repositorioCaixa.SelecionarPorId(id) == null)
            {
                MostrarMensagem("Caixa não encontrada.", ConsoleColor.Red);
                return;
            }
            repositorioCaixa.Excluir(id);
            MostrarMensagem("Caixa excluída com sucesso!", ConsoleColor.Green);
        }

        public bool Listar()
        {
            MostrarCabecalho("Listando Todas as Caixas");
            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();
            if (caixas.Count == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada.");
                return false;
            }
            Console.WriteLine("{0,-5} | {1,-20} | {2,-15} | {3,-10}", "ID", "Etiqueta", "Cor", "Prazo (dias)");
            Console.WriteLine(new string('-', 60));
            foreach (var caixa in caixas)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-15} | {3,-10}", caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.DiasEmprestimo);
            }
            return true;
        }

        // ======================================================
        // MÉTODOS PRIVADOS DE AJUDA
        // ======================================================

        private Caixa ObterDadosCaixa()
        {
            Caixa caixa = new Caixa();
            Console.Write("Digite a cor da caixa: ");
            caixa.Cor = Console.ReadLine();
            Console.Write("Digite a etiqueta da caixa: ");
            caixa.Etiqueta = Console.ReadLine();
            Console.Write("Digite o prazo máximo de empréstimo (em dias): ");

            int diasEmprestimo;
            while (!int.TryParse(Console.ReadLine(), out diasEmprestimo) || diasEmprestimo <= 0)
            {
                MostrarMensagem("Entrada inválida. Por favor, digite um número positivo.", ConsoleColor.Red);
                Console.Write("Digite o prazo máximo de empréstimo (em dias): ");
            }
            caixa.DiasEmprestimo = diasEmprestimo;
            return caixa;
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