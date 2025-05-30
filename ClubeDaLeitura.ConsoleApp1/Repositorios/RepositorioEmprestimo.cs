using ClubeDaLeitura.Entidades;

namespace ClubeDaLeitura.Repositorios
{
    public class RepositorioEmprestimo
    {
        private List<Emprestimo> emprestimos = new();
        private int contadorId = 1;

        public void Inserir(Emprestimo emprestimo)
        {
            emprestimo.Id = contadorId++;
            emprestimos.Add(emprestimo);
        }

        public List<Emprestimo> SelecionarTodos() => emprestimos;

        public Emprestimo? SelecionarPorId(int id) =>
            emprestimos.FirstOrDefault(e => e.Id == id);

        public List<Emprestimo> SelecionarAbertos() =>
            emprestimos.Where(e => e.Status == StatusEmprestimo.Aberto || e.Status == StatusEmprestimo.Atrasado).ToList();

        public List<Emprestimo> SelecionarConcluidos() =>
            emprestimos.Where(e => e.Status == StatusEmprestimo.Concluido).ToList();

        public bool AmigoTemEmprestimoAtivo(Amigo amigo)
        {
            return emprestimos.Any(e => e.Amigo == amigo && (e.Status == StatusEmprestimo.Aberto || e.Status == StatusEmprestimo.Atrasado));
        }
    }
}