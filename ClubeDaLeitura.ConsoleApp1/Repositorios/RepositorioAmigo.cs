using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioAmigo : RepositorioBase<Amigo>
    {
        public RepositorioAmigo() : base("amigos.json") { }

        public override void Editar(int id, Amigo amigoAtualizado)
        {
            Amigo amigoExistente = SelecionarPorId(id);
            if (amigoExistente != null)
            {
                amigoExistente.Nome = amigoAtualizado.Nome;
                amigoExistente.NomeResponsavel = amigoAtualizado.NomeResponsavel;
                amigoExistente.Telefone = amigoAtualizado.Telefone;
                SalvarDados();
            }
        }
    }
}