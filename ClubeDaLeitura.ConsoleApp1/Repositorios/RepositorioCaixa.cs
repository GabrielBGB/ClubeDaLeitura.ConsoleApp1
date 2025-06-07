using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioCaixa : RepositorioBase<Caixa>
    {
        public RepositorioCaixa() : base("caixas.json") { }

        public override void Editar(int id, Caixa caixaAtualizada)
        {
            Caixa caixaExistente = SelecionarPorId(id);
            if (caixaExistente != null)
            {
                caixaExistente.Cor = caixaAtualizada.Cor;
                caixaExistente.Etiqueta = caixaAtualizada.Etiqueta;
                caixaExistente.DiasEmprestimo = caixaAtualizada.DiasEmprestimo;
                SalvarDados();
            }
        }
    }
}