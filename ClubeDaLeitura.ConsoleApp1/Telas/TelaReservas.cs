// Local: ClubeDaLeitura.ConsoleApp1/Telas/TelaReservas.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.ConsoleApp1.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeDaLeitura.ConsoleApp1.Telas
{
    public class TelaReservas
    {
        private readonly RepositorioReserva repositorioReserva;
        private readonly RepositorioRevista repositorioRevista;
        private readonly RepositorioAmigo repositorioAmigo;
        private readonly RepositorioMulta repositorioMulta;
        private readonly TelaRevista telaRevista;
        private readonly TelaAmigo telaAmigo;

        public TelaReservas(RepositorioReserva repoRes, RepositorioRevista repoRev, RepositorioAmigo repoAmg, RepositorioMulta repoMulta, TelaRevista tRev, TelaAmigo tAmg)
        {
            this.repositorioReserva = repoRes;
            this.repositorioRevista = repoRev;
            this.repositorioAmigo = repoAmg;
            this.repositorioMulta = repoMulta;
            this.telaRevista = tRev;
            this.telaAmigo = tAmg;
        }

        public void ApresentarMenu()
        {
            bool voltar = false;
            while (!voltar)
            {
                MostrarCabecalho("Gerenciar Reservas");
                Console.WriteLine("[1] Criar Nova Reserva");
                Console.WriteLine("[2] Cancelar Reserva");
                Console.WriteLine("[3] Visualizar Reservas Ativas");
                Console.WriteLine("\n[0] Voltar");

                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": CriarReserva(); break;
                    case "2": CancelarReserva(); break;
                    case "3": VisualizarReservasAtivas(); break;
                    case "0": voltar = true; break;
                    default: MostrarMensagem("Opção inválida!", ConsoleColor.Red); break;
                }
            }
        }

        public void CriarReserva()
        {
            MostrarCabecalho("Criar Nova Reserva");

            if (!telaAmigo.Listar()) { MostrarMensagem("Nenhum amigo cadastrado.", ConsoleColor.Yellow); return; }
            Console.Write("\nDigite o ID do amigo para a reserva: ");
            Amigo amigo = repositorioAmigo.SelecionarPorId(ObterIdValido());
            if (amigo == null) { MostrarMensagem("Amigo não encontrado.", ConsoleColor.Red); return; }

            bool amigoTemMultaPendente = repositorioMulta.SelecionarTodos().Any(m => m.Emprestimo.Amigo.Id == amigo.Id && !m.EstaPaga);
            if (amigoTemMultaPendente)
            {
                MostrarMensagem("Este amigo não pode fazer reservas pois possui multas pendentes.", ConsoleColor.Red);
                return;
            }

            List<Revista> revistasDisponiveis = repositorioRevista.SelecionarTodos().Where(r => r.Status == "Disponível").ToList();
            if (!telaRevista.Listar(revistasDisponiveis)) { MostrarMensagem("Nenhuma revista disponível para reservar.", ConsoleColor.Yellow); return; }

            Console.Write("\nDigite o ID da revista para reservar: ");
            Revista revista = repositorioRevista.SelecionarPorId(ObterIdValido());
            if (revista == null || revista.Status != "Disponível") { MostrarMensagem("Revista não encontrada ou indisponível.", ConsoleColor.Red); return; }

            Reserva novaReserva = new Reserva { Amigo = amigo, Revista = revista, DataReserva = DateTime.Now };

            string[] erros = novaReserva.Validar();
            if (erros.Length > 0)
            {
                ApresentarErros(erros);
                return;
            }

            repositorioReserva.Inserir(novaReserva);
            revista.Status = "Reservada";
            MostrarMensagem("Reserva criada com sucesso!", ConsoleColor.Green);
        }

        public void CancelarReserva()
        {
            MostrarCabecalho("Cancelar Reserva Ativa");
            if (!VisualizarReservasAtivas(pausar: false)) return;

            Console.Write("\nDigite o ID da reserva que deseja cancelar: ");
            int idReserva = ObterIdValido();
            Reserva reserva = repositorioReserva.SelecionarPorId(idReserva);

            if (reserva == null || reserva.Status != "Ativa")
            {
                MostrarMensagem("Reserva não encontrada ou não está mais ativa.", ConsoleColor.Red);
                return;
            }

            reserva.Concluir();
            reserva.Revista.Status = "Disponível";

            MostrarMensagem("Reserva cancelada com sucesso! A revista está disponível novamente.", ConsoleColor.Green);
        }

        public bool VisualizarReservasAtivas(bool pausar = true)
        {
            MostrarCabecalho("Visualizar Reservas Ativas");
            List<Reserva> reservasAtivas = repositorioReserva.SelecionarTodos().Where(r => r.Status == "Ativa").ToList();

            if (reservasAtivas.Count == 0)
            {
                MostrarMensagem("Nenhuma reserva ativa no momento.", ConsoleColor.Yellow);
                return false;
            }

            Console.WriteLine("{0,-5} | {1,-25} | {2,-20} | {3,-15}", "ID", "Revista", "Amigo", "Data da Reserva");
            Console.WriteLine(new string('-', 75));
            foreach (var reserva in reservasAtivas)
            {
                Console.WriteLine("{0,-5} | {1,-25} | {2,-20} | {3,-15}", reserva.Id, reserva.Revista.Titulo, reserva.Amigo.Nome, reserva.DataReserva.ToShortDateString());
            }

            if (pausar)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

            return true;
        }

        // ======================================================
        // MÉTODOS PRIVADOS DE AJUDA (AGORA COMPLETOS)
        // ======================================================
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