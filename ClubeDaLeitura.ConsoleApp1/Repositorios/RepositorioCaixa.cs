using ClubeDaLeitura.Entidades;

namespace ClubeDaLeitura.Repositorios
{
    public class RepositorioCaixa
    {
        private List<Caixa> caixas = new();
        private int contadorId = 1;

        public void Inserir(Caixa caixa)
        {
            caixa.Id = contadorId++;
            caixas.Add(caixa);
        }

        public bool Editar(int id, Caixa nova)
        {
            var caixa = SelecionarPorId(id);
            if (caixa == null) return false;

            caixa.Etiqueta = nova.Etiqueta;
            caixa.Cor = nova.Cor;
            caixa.DiasEmprestimo = nova.DiasEmprestimo;

            return true;
        }

        public bool Excluir(int id)
        {
            var caixa = SelecionarPorId(id);
            if (caixa == null || caixa.IdsRevistas.Any())
                return false;

            caixas.Remove(caixa);
            return true;
        }

        public List<Caixa> SelecionarTodos() => caixas;

        public Caixa? SelecionarPorId(int id) => caixas.FirstOrDefault(c => c.Id == id);

        public bool ExisteEtiqueta(string etiqueta)
        {
            return caixas.Any(c => c.Etiqueta.Equals(etiqueta, StringComparison.OrdinalIgnoreCase));
        }
    }
}