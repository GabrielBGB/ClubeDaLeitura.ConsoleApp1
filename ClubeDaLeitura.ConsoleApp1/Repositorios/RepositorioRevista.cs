using ClubeDaLeitura.ConsoleApp1.Entidades;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioRevista : RepositorioBase<Revista>
    {
        public RepositorioRevista() : base("revistas.json") { }

        public override void Editar(int id, Revista revistaAtualizada)
        {
            Revista revistaExistente = SelecionarPorId(id);
            if (revistaExistente != null)
            {
                revistaExistente.Titulo = revistaAtualizada.Titulo;
                revistaExistente.NumeroEdicao = revistaAtualizada.NumeroEdicao;
                revistaExistente.AnoPublicacao = revistaAtualizada.AnoPublicacao;
                revistaExistente.Caixa = revistaAtualizada.Caixa;
                // O status da revista é gerenciado por outras telas (Empréstimo, Reserva),
                // mas podemos adicioná-lo aqui se a edição de status for permitida diretamente.
                // revistaExistente.Status = revistaAtualizada.Status; 
                SalvarDados();
            }
        }
    }
}