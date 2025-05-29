using ClubeDaLeitura.Entidades;
using ClubeDaLeitura.Repositorios;

namespace ClubeDaLeitura.Telas
{
    public class TelaCaixa
    {
        private RepositorioCaixa repositorio;

        public TelaCaixa(RepositorioCaixa repo)
        {
            repositorio = repo;
        }

        public void Inserir()
        {
            Console.WriteLine("== Inserir Caixa ==");

            var caixa = ObterCaixa();

            if (!caixa.Validar(out string erros))
            {
                Console.WriteLine("Erros:\n" + erros);
                return;
            }

            if (repositorio.ExisteEtiqueta(caixa.Etiqueta))
            {
                Console.WriteLine("Já existe uma caixa com esta etiqueta.");
                return;
            }

            repositorio.Inserir(caixa);
            Console.WriteLine("Caixa inserida com sucesso.");
        }

        public void VisualizarTodos()
        {
            var caixas = repositorio.SelecionarTodos();

            Console.WriteLine("== Caixas Cadastradas ==");
            foreach (var c in caixas)
            {
                Console.WriteLine($"ID: {c.Id} | Etiqueta: {c.Etiqueta} | Cor: {c.Cor} | Dias: {c.DiasEmprestimo}");
            }
        }

        public void Editar()
        {
            VisualizarTodos();
            Console.Write("Digite o ID da caixa para editar: ");
            int id = int.Parse(Console.ReadLine()!);

            var nova = ObterCaixa();

            if (!nova.Validar(out string erros))
            {
                Console.WriteLine("Erros:\n" + erros);
                return;
            }

            if (repositorio.Editar(id, nova))
                Console.WriteLine("Caixa atualizada.");
            else
                Console.WriteLine("Caixa não encontrada.");
        }

        public void Excluir()
        {
            VisualizarTodos();
            Console.Write("Digite o ID da caixa para excluir: ");
            int id = int.Parse(Console.ReadLine()!);

            if (repositorio.Excluir(id))
                Console.WriteLine("Caixa excluída.");
            else
                Console.WriteLine("Não foi possível excluir. Verifique se há revistas vinculadas.");
        }

        private Caixa ObterCaixa()
        {
            Console.Write("Etiqueta: ");
            string etiqueta = Console.ReadLine()!;
            Console.Write("Cor (ex: vermelho ou #FF0000): ");
            string cor = Console.ReadLine()!;
            Console.Write("Dias de Empréstimo (padrão 7): ");
            int dias = int.TryParse(Console.ReadLine(), out int diasInt) ? diasInt : 7;

            return new Caixa
            {
                Etiqueta = etiqueta,
                Cor = cor,
                DiasEmprestimo = dias
            };
        }
    }
}