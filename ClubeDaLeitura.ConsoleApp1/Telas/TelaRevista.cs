// Local: ClubeDaLeitura.ConsoleApp1/Telas/TelaRevista.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.ConsoleApp1.Repositorios;
using System;
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Telas
{
    public class TelaRevista : ITelaCadastravel
    {
        private readonly RepositorioRevista repositorioRevista;
        private readonly RepositorioCaixa repositorioCaixa;
        private readonly TelaCaixa telaCaixa;

        public TelaRevista(RepositorioRevista repoRevista, RepositorioCaixa repoCaixa, TelaCaixa tCaixa)
        {
            repositorioRevista = repoRevista;
            repositorioCaixa = repoCaixa;
            telaCaixa = tCaixa;
        }

        public void Inserir()
        {
            MostrarCabecalho("Inserir Nova Revista");
            Revista novaRevista = ObterDadosRevista();
            if (novaRevista == null) return;

            string[] erros = novaRevista.Validar();
            if (erros.Length > 0)
            {
                ApresentarErros(erros);
                return;
            }

            repositorioRevista.Inserir(novaRevista);
            MostrarMensagem("Revista inserida com sucesso!", ConsoleColor.Green);
        }

        public void Editar()
        {
            MostrarCabecalho("Editar Revista");
            if (!Listar()) return;

            Console.Write("\nDigite o ID da revista para editar: ");
            int id = ObterIdValido();
            if (repositorioRevista.SelecionarPorId(id) == null)
            {
                MostrarMensagem("Revista não encontrada.", ConsoleColor.Red);
                return;
            }

            Revista revistaAtualizada = ObterDadosRevista();
            if (revistaAtualizada == null) return;

            string[] erros = revistaAtualizada.Validar();
            if (erros.Length > 0)
            {
                ApresentarErros(erros);
                return;
            }

            repositorioRevista.Editar(id, revistaAtualizada);
            MostrarMensagem("Revista editada com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            MostrarCabecalho("Excluir Revista");
            if (!Listar()) return;

            Console.Write("\nDigite o ID da revista para excluir: ");
            int id = ObterIdValido();
            Revista revista = repositorioRevista.SelecionarPorId(id);
            if (revista == null)
            {
                MostrarMensagem("Revista não encontrada.", ConsoleColor.Red);
                return;
            }

            if (revista.Status == "Emprestada")
            {
                MostrarMensagem("Não é possível excluir uma revista que está atualmente emprestada.", ConsoleColor.Red);
                return;
            }

            repositorioRevista.Excluir(id);
            MostrarMensagem("Revista excluída com sucesso!", ConsoleColor.Green);
        }

        public bool Listar()
        {
            MostrarCabecalho("Listando Todas as Revistas");
            List<Revista> todasAsRevistas = repositorioRevista.SelecionarTodos();
            return Listar(todasAsRevistas);
        }

        public bool Listar(List<Revista> revistasParaExibir)
        {
            if (revistasParaExibir.Count == 0)
            {
                MostrarMensagem("Nenhuma revista encontrada.", ConsoleColor.Yellow);
                return false;
            }

            Console.WriteLine("{0,-5} | {1,-25} | {2,-10} | {3,-15} | {4,-10}", "ID", "Título", "Edição", "Caixa (Etiqueta)", "Status");
            Console.WriteLine(new string('-', 80));

            foreach (var revista in revistasParaExibir)
            {
                Console.WriteLine("{0,-5} | {1,-25} | {2,-10} | {3,-15} | {4,-10}",
                    revista.Id,
                    revista.Titulo,
                    revista.NumeroEdicao,
                    revista.Caixa.Etiqueta,
                    revista.Status);
            }
            return true;
        }

        private Revista ObterDadosRevista()
        {
            Revista revista = new Revista();
            Console.Write("Título: ");
            revista.Titulo = Console.ReadLine();
            Console.Write("Número da Edição: ");
            revista.NumeroEdicao = ObterIdValido();
            Console.Write("Ano de Publicação: ");
            revista.AnoPublicacao = ObterIdValido();

            Console.WriteLine();
            if (!telaCaixa.Listar())
            {
                MostrarMensagem("Nenhuma caixa cadastrada. Cadastre uma caixa primeiro.", ConsoleColor.Yellow);
                return null;
            }

            Console.Write("\nDigite o ID da Caixa onde a revista será guardada: ");
            int idCaixa = ObterIdValido();
            Caixa caixaSelecionada = repositorioCaixa.SelecionarPorId(idCaixa);

            if (caixaSelecionada == null)
            {
                MostrarMensagem("Caixa não encontrada.", ConsoleColor.Red);
                return null;
            }
            revista.Caixa = caixaSelecionada;

            return revista;
        }

        private int ObterIdValido()
        {
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                MostrarMensagem("Entrada inválida. Por favor, digite um número de ID válido.", ConsoleColor.Red);
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