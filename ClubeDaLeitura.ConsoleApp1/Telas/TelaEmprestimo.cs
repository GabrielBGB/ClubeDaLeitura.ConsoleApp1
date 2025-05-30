using ClubeDaLeitura.Entidades;
using ClubeDaLeitura.Repositorios;

namespace ClubeDaLeitura.Telas
{
    public class TelaEmprestimo
    {
        private RepositorioEmprestimo repoEmprestimo;
        private RepositorioAmigo repoAmigo;
        private RepositorioRevista repoRevista;

        public TelaEmprestimo(RepositorioEmprestimo rEmp, RepositorioAmigo rAmi, RepositorioRevista rRev)
        {
            repoEmprestimo = rEmp;
            repoAmigo = rAmi;
            repoRevista = rRev;
        }

        public void RegistrarEmprestimo()
        {
            Console.WriteLine("== Novo Empréstimo ==");

            var amigo = SelecionarAmigo();
            if (repoEmprestimo.AmigoTemEmprestimoAtivo(amigo))
            {
                Console.WriteLine("Este amigo já possui um empréstimo ativo.");
                return;
            }

            var revista = SelecionarRevistaDisponivel();

            var emprestimo = new Emprestimo
            {
                Amigo = amigo,
                Revista = revista
            };

            if (!emprestimo.Validar(out string erros))
            {
                Console.WriteLine("Erros:\n" + erros);
                return;
            }

            emprestimo.RegistrarEmprestimo();
            repoEmprestimo.Inserir(emprestimo);
            Console.WriteLine("Empréstimo registrado com sucesso.");
        }

        public void RegistrarDevolucao()
        {
            Console.WriteLine("== Devolução de Empréstimo ==");

            var abertos = repoEmprestimo.SelecionarAbertos();
            if (abertos.Count == 0)
            {
                Console.WriteLine("Nenhum empréstimo em aberto.");
                return;
            }

            foreach (var e in abertos)
                Console.WriteLine($"ID: {e.Id} | Amigo: {e.Amigo.Nome} | Revista: {e.Revista.Titulo} | Devolução: {e.DataDevolucao:d}");

            Console.Write("Digite o ID do empréstimo para devolver: ");
            int id = int.Parse(Console.ReadLine()!);

            var emprestimo = repoEmprestimo.SelecionarPorId(id);
            if (emprestimo == null)
            {
                Console.WriteLine("Empréstimo não encontrado.");
                return;
            }

            emprestimo.RegistrarDevolucao();
            Console.WriteLine("Devolução registrada.");
        }

        public void VisualizarEmprestimos()
        {
            Console.WriteLine("== Empréstimos ==");

            var todos = repoEmprestimo.SelecionarTodos();
            if (todos.Count == 0)
            {
                Console.WriteLine("Nenhum empréstimo encontrado.");
                return;
            }

            foreach (var e in todos)
            {
                e.AtualizarStatus();
                Console.WriteLine($"ID: {e.Id} | Amigo: {e.Amigo.Nome} | Revista: {e.Revista.Titulo} | Empréstimo: {e.DataEmprestimo:d} | Devolução: {e.DataDevolucao:d} | Status: {e.Status}");
            }
        }

        private Amigo SelecionarAmigo()
        {
            var amigos = repoAmigo.SelecionarTodos();
            foreach (var a in amigos)
                Console.WriteLine($"ID: {a.Id} | Nome: {a.Nome}");

            Console.Write("ID do amigo: ");
            int id = int.Parse(Console.ReadLine()!);
            return repoAmigo.SelecionarPorId(id)!;
        }

        private Revista SelecionarRevistaDisponivel()
        {
            var revistas = repoRevista.SelecionarTodos().Where(r => r.Status == StatusRevista.Disponivel).ToList();

            foreach (var r in revistas)
                Console.WriteLine($"ID: {r.Id} | Título: {r.Titulo}");

            Console.Write("ID da revista: ");
            int id = int.Parse(Console.ReadLine()!);
            return repoRevista.SelecionarPorId(id)!;
        }
    }
}