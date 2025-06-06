using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>
    {
        public override void Editar(int id, Emprestimo emprestimoAtualizado)
        {
            Emprestimo emprestimoExistente = SelecionarPorId(id);
            if (emprestimoExistente != null)
            {
                // Copie os campos aqui
            }
        }

        // REMOVA OS MÉTODOS Excluir, SelecionarPorId e SelecionarTodos DESTE ARQUIVO
    }
}