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
        private readonly RepositorioReserva repositorioReserva;
        private readonly RepositorioAmigo repositorioAmigo;
        private readonly RepositorioRevista repositorioRevista;
        private readonly RepositorioMulta repositorioMulta;
        private readonly TelaAmigo telaAmigo;
        private readonly TelaRevista telaRevista;
        private readonly TelaReservas telaReservas;

        public TelaEmprestimo(RepositorioEmprestimo repoEmp, RepositorioReserva repoRes, RepositorioAmigo repoAmg, RepositorioRevista repoRev, RepositorioMulta repoMulta, TelaAmigo tAmg, TelaRevista tRev, TelaReservas tRes)
        {
            this.repositorioEmprestimo = repoEmp;
            this.repositorioReserva = repoRes;
            this.repositorioAmigo = repoAmg;
            this.repositorioRevista = repoRev;
            this.repositorioMulta = repoMulta;
            this.telaAmigo = tAmg;
            this.telaRevista = tRev;
            this.telaReservas = tRes;
        }

        public void RegistrarEmprestimo()
        {
            MostrarCabecalho("Registrar Novo Empréstimo");
            if (!telaAmigo.Listar()) { MostrarMensagem("Nenhum amigo cadastrado.", ConsoleColor.Yellow); return; }

            Console.Write("\nDigite o ID do amigo: ");
            Amigo amigo = repositorioAmigo.SelecionarPorId(ObterIdValido());
            if (amigo == null) { MostrarMensagem("Amigo não encontrado.", ConsoleColor.Red); return; }

            bool amigoTemMultaPendente = repositorioMulta.SelecionarTodos().Any(m => m.Emprestimo.Amigo.Id == amigo.Id && !m.EstaPaga);
            if (amigoTemMultaPendente) { MostrarMensagem("Este amigo possui multas pendentes e não pode fazer empréstimos.", ConsoleColor.Red); return; }

            List<Revista> revistasDisponiveis = repositorioRevista.SelecionarTodos().Where(r => r.Status == "Disponível").ToList();
            if (!telaRevista.Listar(revistasDisponiveis)) { MostrarMensagem("Nenhuma revista disponível.", ConsoleColor.Yellow); return; }

            Console.Write("\nDigite o ID da revista: ");
            Revista revista = repositorioRevista.SelecionarPorId(ObterIdValido());

            if (revista?.Status == "Reservada") { MostrarMensagem("Esta revista está reservada.", ConsoleColor.Red); return; }
            if (revista == null || revista.Status != "Disponível") { MostrarMensagem("Revista não encontrada ou indisponível.", ConsoleColor.Red); return; }

            Emprestimo novoEmprestimo = new Emprestimo { Amigo = amigo, Revista = revista, DataEmprestimo = DateTime.Now };
            novoEmprestimo.DataDevolucao = novoEmprestimo.DataEmprestimo.AddDays(revista.Caixa.DiasEmprestimo);

            revista.Status = "Emprestada";
            repositorioEmprestimo.Inserir(novoEmprestimo);
            MostrarMensagem("Empréstimo registrado com sucesso!", ConsoleColor.Green);
        }

        public void ConverterReservaEmEmprestimo()
        {
            MostrarCabecalho("Converter Reserva em Empréstimo");
            if (!telaReservas.VisualizarReservasAtivas(pausar: false)) return;

            Console.Write("\nDigite o ID da reserva para converter: ");
            int idReserva = ObterIdValido();
            Reserva reserva = repositorioReserva.SelecionarPorId(idReserva);

            if (reserva == null || reserva.Status != "Ativa")
            {
                MostrarMensagem("Reserva não encontrada ou não está mais ativa.", ConsoleColor.Red);
                return;
            }

            Emprestimo novoEmprestimo = new Emprestimo
            {
                Amigo = reserva.Amigo,
                Revista = reserva.Revista,
                DataEmprestimo = DateTime.Now
            };
            novoEmprestimo.DataDevolucao = novoEmprestimo.DataEmprestimo.AddDays(reserva.Revista.Caixa.DiasEmprestimo);

            reserva.Concluir();
            reserva.Revista.Status = "Emprestada";
            repositorioEmprestimo.Inserir(novoEmprestimo);
            MostrarMensagem("Reserva convertida em empréstimo com sucesso!", ConsoleColor.Green);
        }

        public void RegistrarDevolucao()
        {
            MostrarCabecalho("Registrar Devolução");
            List<Emprestimo> emprestimosAbertos = repositorioEmprestimo.SelecionarTodos().Where(e => e.Status == "Aberto").ToList();
            if (emprestimosAbertos.Count == 0) { MostrarMensagem("Nenhum empréstimo em aberto.", ConsoleColor.Yellow); return; }

            foreach (var emp in emprestimosAbertos) Console.WriteLine(emp.ToString());

            Console.Write("\nDigite o ID do empréstimo a ser devolvido: ");
            int idEmprestimo = ObterIdValido();
            Emprestimo emprestimo = repositorioEmprestimo.SelecionarPorId(idEmprestimo);

            if (emprestimo == null || emprestimo.Status != "Aberto") { MostrarMensagem("Empréstimo não encontrado ou já fechado.", ConsoleColor.Red); return; }

            if (DateTime.Now.Date > emprestimo.DataDevolucao.Date)
            {
                TimeSpan diasDeAtraso = DateTime.Now.Date - emprestimo.DataDevolucao.Date;
                decimal valorMulta = (decimal)diasDeAtraso.TotalDays * 2.00m;
                if (valorMulta > 0)
                {
                    Multa novaMulta = new Multa { Emprestimo = emprestimo, Valor = Math.Round(valorMulta, 2), EstaPaga = false };
                    repositorioMulta.Inserir(novaMulta);
                    MostrarMensagem($"Devolução com atraso! Multa de R${novaMulta.Valor:F2} gerada.", ConsoleColor.Yellow);
                }
            }
            emprestimo.Fechar();
            MostrarMensagem("Devolução registrada com sucesso!", ConsoleColor.Green);
        }

        public void ListarEmprestimos()
        {
            MostrarCabecalho("Listando Todos os Empréstimos");
            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();
            if (emprestimos.Count == 0)
            {
                MostrarMensagem("Nenhum empréstimo registrado.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("{0,-5} | {1,-10} | {2,-15} | {3,-15} | {4,-15}", "ID", "Status", "Amigo", "Revista", "Devolução");
            Console.WriteLine(new string('-', 80));
            foreach (var emp in emprestimos)
            {
                Console.WriteLine("{0,-5} | {1,-10} | {2,-15} | {3,-15} | {4,-15}",
                    emp.Id, emp.Status, emp.Amigo.Nome, emp.Revista.Titulo, emp.DataDevolucao.ToShortDateString());
            }
            Console.ReadKey();
        }

        // ======================================================
        // MÉTODOS PRIVADOS DE AJUDA (ESTES ESTAVAM FALTANDO)
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