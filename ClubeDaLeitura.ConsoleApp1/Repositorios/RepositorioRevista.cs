using ClubeDaLeitura.Entidades;

namespace ClubeDaLeitura.Repositorios
{
    public class RepositorioRevista
    {
        private List<Revista> revistas = new();
        private int contadorId = 1;

        public void Inserir(Revista revista)
        {
            revista.Id = contadorId++;
            revistas.Add(revista);
        }

        public bool Editar(int id, Revista nova)
        {
            var revista = SelecionarPorId(id);
            if (revista == null) return false;

            revista.Titulo = nova.Titulo;
            revista.NumeroEdicao = nova.NumeroEdicao;
            revista.Ano = nova.Ano;
            revista.Caixa = nova.Caixa;

            return true;
        }

        public bool Excluir(int id)
        {
            var revista = SelecionarPorId(id);
            if (revista == null) return false;

            revistas.Remove(revista);
            return true;
        }

        public List<Revista> SelecionarTodos() => revistas;

        public Revista? SelecionarPorId(int id) => revistas.FirstOrDefault(r => r.Id == id);

        public bool ExisteComMesmoTituloEEdicao(string titulo, int edicao)
        {
            return revistas.Any(r => r.Titulo == titulo && r.NumeroEdicao == edicao);
        }
    }
}