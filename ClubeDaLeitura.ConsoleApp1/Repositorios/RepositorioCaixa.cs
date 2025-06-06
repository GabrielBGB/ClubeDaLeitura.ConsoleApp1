// Local: ClubeDaLeitura.ConsoleApp1/Repositorios/RepositorioCaixa.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioCaixa : RepositorioBase<Caixa>
    {
        // A única responsabilidade específica do RepositorioCaixa é saber
        // como transferir os dados de uma caixa atualizada para uma existente.
        public override void Editar(int id, Caixa caixaAtualizada)
        {
            Caixa caixaExistente = SelecionarPorId(id);
            if (caixaExistente != null)
            {
                caixaExistente.Cor = caixaAtualizada.Cor;
                caixaExistente.Etiqueta = caixaAtualizada.Etiqueta;
                caixaExistente.DiasEmprestimo = caixaAtualizada.DiasEmprestimo;
            }
        }
    }
}