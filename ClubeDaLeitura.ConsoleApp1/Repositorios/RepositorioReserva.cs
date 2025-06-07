using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioReserva : RepositorioBase<Reserva>
    {
        public override void Editar(int id, Reserva registroAtualizado)
        {
            Reserva reservaExistente = SelecionarPorId(id);
            if (reservaExistente != null)
            {
                reservaExistente.Status = registroAtualizado.Status;
            }
        }
    }
}