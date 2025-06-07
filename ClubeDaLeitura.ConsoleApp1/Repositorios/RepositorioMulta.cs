using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioMulta : RepositorioBase<Multa>
    {
        public RepositorioMulta() : base("multas.json") { }

        public override void Editar(int id, Multa multaAtualizada)
        {
            Multa multaExistente = SelecionarPorId(id);
            if (multaExistente != null)
            {
                // A única coisa que se edita em uma multa é o seu status de pagamento
                multaExistente.EstaPaga = multaAtualizada.EstaPaga;
                SalvarDados();
            }
        }
    }
}