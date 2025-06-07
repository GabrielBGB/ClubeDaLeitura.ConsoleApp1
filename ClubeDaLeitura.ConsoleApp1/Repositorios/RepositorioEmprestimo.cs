using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>
    {
        public RepositorioEmprestimo() : base("emprestimos.json") { }

        public override void Editar(int id, Emprestimo emprestimoAtualizado)
        {
            Emprestimo emprestimoExistente = SelecionarPorId(id);
            if (emprestimoExistente != null)
            {
                // Geralmente só o status de um empréstimo é editado (ao fechar)
                emprestimoExistente.Status = emprestimoAtualizado.Status;
                SalvarDados();
            }
        }
    }
}