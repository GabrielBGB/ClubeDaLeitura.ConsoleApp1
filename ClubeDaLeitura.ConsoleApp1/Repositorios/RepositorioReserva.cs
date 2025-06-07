using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioReserva : RepositorioBase<Reserva>
    {
        public RepositorioReserva() : base("reservas.json") { }

        public override void Editar(int id, Reserva reservaAtualizada)
        {
            Reserva reservaExistente = SelecionarPorId(id);
            if (reservaExistente != null)
            {
                // O status é a principal coisa a se editar em uma reserva (Ativa -> Concluída)
                reservaExistente.Status = reservaAtualizada.Status;
                SalvarDados();
            }
        }
    }
}