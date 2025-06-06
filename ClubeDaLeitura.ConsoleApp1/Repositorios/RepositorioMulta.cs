// Local: ClubeDaLeitura.ConsoleApp1/Repositorios/RepositorioMulta.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioMulta : RepositorioBase<Multa>
    {
        // O repositório filho só precisa saber COMO editar. O resto é herdado.
        public override void Editar(int id, Multa multaAtualizada)
        {
            Multa multaExistente = SelecionarPorId(id);

            if (multaExistente != null)
            {
                multaExistente.Valor = multaAtualizada.Valor;
                multaExistente.EstaPaga = multaAtualizada.EstaPaga;
            }
        }

        // NÃO COLOQUE OS MÉTODOS Inserir, Excluir, SelecionarTodos ou SelecionarPorId AQUI.
        // Eles já existem na classe RepositorioBase!
    }
}