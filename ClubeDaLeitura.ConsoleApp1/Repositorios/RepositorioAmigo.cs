using ClubeDaLeitura.Entidades;

namespace ClubeDaLeitura.Repositorios
{
    public class RepositorioAmigo
    {
        private List<Amigo> amigos = new();
        private int contadorId = 1;

        public void Inserir(Amigo amigo)
        {
            amigo.Id = contadorId++;
            amigos.Add(amigo);
        }

        public bool Editar(int id, Amigo novo)
        {
            var amigo = SelecionarPorId(id);
            if (amigo == null) return false;

            amigo.Nome = novo.Nome;
            amigo.Responsavel = novo.Responsavel;
            amigo.Telefone = novo.Telefone;

            return true;
        }

        public bool Excluir(int id)
        {
            var amigo = SelecionarPorId(id);
            if (amigo == null || amigo.IdsEmprestimos.Any())
                return false;

            amigos.Remove(amigo);
            return true;
        }

        public List<Amigo> SelecionarTodos() => amigos;

        public Amigo? SelecionarPorId(int id) => amigos.FirstOrDefault(a => a.Id == id);

        public bool ExisteAmigoComMesmoNomeTelefone(string nome, string telefone)
        {
            return amigos.Any(a => a.Nome == nome && a.Telefone == telefone);
        }
    }
}