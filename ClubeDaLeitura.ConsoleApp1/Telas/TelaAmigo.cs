// Local: ClubeDaLeitura.ConsoleApp1/Telas/TelaAmigo.cs

using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.ConsoleApp1.Repositorios;
using System;
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Telas
{
    public class TelaAmigo : ITelaCadastravel
    {
        private readonly RepositorioAmigo repositorioAmigo;

        public TelaAmigo(RepositorioAmigo repositorio)
        {
            repositorioAmigo = repositorio;
        }

        public void Inserir()
        {
            MostrarCabecalho("Inserir Novo Amigo");
            Amigo novoAmigo = ObterDadosAmigo(); // Este método agora está completo

            string[] erros = novoAmigo.Validar();
            if (erros.Length > 0)
            {
                ApresentarErros(erros);
                return;
            }

            repositorioAmigo.Inserir(novoAmigo);
            MostrarMensagem("Amigo inserido com sucesso!", ConsoleColor.Green);
        }

        public void Editar()
        {
            MostrarCabecalho("Editar Amigo");
            if (!Listar())
            {
                MostrarMensagem("Nenhum amigo para editar.", ConsoleColor.Yellow);
                return;
            }
            Console.Write("\nDigite o ID do amigo para editar: ");
            int id = ObterIdValido();
            if (repositorioAmigo.SelecionarPorId(id) == null)
            {
                MostrarMensagem("Amigo não encontrado.", ConsoleColor.Red);
                return;
            }

            Amigo amigoAtualizado = ObterDadosAmigo();
            string[] erros = amigoAtualizado.Validar();
            if (erros.Length > 0)
            {
                ApresentarErros(erros);
                return;
            }

            repositorioAmigo.Editar(id, amigoAtualizado);
            MostrarMensagem("Amigo editado com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            MostrarCabecalho("Excluir Amigo");
            if (!Listar())
            {
                MostrarMensagem("Nenhum amigo para excluir.", ConsoleColor.Yellow);
                return;
            }
            Console.Write("\nDigite o ID do amigo para excluir: ");
            int id = ObterIdValido();
            if (repositorioAmigo.SelecionarPorId(id) == null)
            {
                MostrarMensagem("Amigo não encontrado.", ConsoleColor.Red);
                return;
            }
            repositorioAmigo.Excluir(id);
            MostrarMensagem("Amigo excluído com sucesso!", ConsoleColor.Green);
        }

        public bool Listar()
        {
            MostrarCabecalho("Listando Todos os Amigos");
            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();
            if (amigos.Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado.");
                return false;
            }
            Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-20}", "ID", "Nome", "Telefone", "Responsável");
            Console.WriteLine(new string('-', 75));
            foreach (var amigo in amigos)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-20}", amigo.Id, amigo.Nome, amigo.Telefone, amigo.NomeResponsavel);
            }
            return true;
        }

        // ======================================================
        // MÉTODOS PRIVADOS DE AJUDA (AGORA COMPLETOS)
        // ======================================================

        private Amigo ObterDadosAmigo()
        {
            Amigo amigo = new Amigo();

            Console.Write("Nome do amigo: ");
            amigo.Nome = Console.ReadLine();

            Console.Write("Nome do responsável: ");
            amigo.NomeResponsavel = Console.ReadLine();

            Console.Write("Telefone (ex: (49) 99999-9999): ");
            amigo.Telefone = Console.ReadLine();

            return amigo;
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