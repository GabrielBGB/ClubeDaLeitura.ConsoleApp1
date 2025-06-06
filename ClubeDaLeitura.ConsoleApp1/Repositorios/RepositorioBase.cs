using System.Text.Json;

namespace ClubeDaLeitura.Repositorios
{
    public abstract class RepositorioBase<T> where T : EntidadeBase
    {
        protected List<T> registros = new();
        protected int contadorId = 1;
        private readonly string caminhoArquivo;

        public RepositorioBase(string nomeArquivo)
        {
            caminhoArquivo = Path.Combine("..", "..", "..", "dados", nomeArquivo);
            CarregarDados();
        }

        private void CarregarDados()
        {
            if (!File.Exists(caminhoArquivo)) return;
            string conteudoJson = File.ReadAllText(caminhoArquivo);
            if (string.IsNullOrEmpty(conteudoJson)) return;
            registros = JsonSerializer.Deserialize<List<T>>(conteudoJson);
            if (registros.Count > 0)
                contadorId = registros.Max(r => r.Id) + 1;
        }

        private void SalvarDados()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoArquivo));
            string conteudoJson = JsonSerializer.Serialize(registros, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(caminhoArquivo, conteudoJson);
        }

        public void Inserir(T novoRegistro)
        {
            novoRegistro.Id = contadorId++;
            registros.Add(novoRegistro);
            SalvarDados();
        }

        public void Editar(T registroEditado)
        {
            SalvarDados();
        }

        public void Excluir(T registroSelecionado)
        {
            registros.Remove(registroSelecionado);
            SalvarDados();
        }

        public List<T> SelecionarTodos()
        {
            return registros;
        }

        public T SelecionarPorId(int id)
        {
            return registros.FirstOrDefault(r => r.Id == id);
        }
    }
}