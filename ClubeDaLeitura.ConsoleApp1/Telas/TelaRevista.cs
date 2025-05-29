using ClubeDaLeitura.Entidades;
using ClubeDaLeitura.Repositorios;

namespace ClubeDaLeitura.Telas
{
    public class TelaRevista
    {
        private RepositorioRevista repoRevista;
        private RepositorioCaixa repoCaixa;

        public TelaRevista(RepositorioRevista rRev, RepositorioCaixa rCx)
        {
            repoRevista = rRev;
            repoCaixa = rCx;
        }

        public void Inserir()
        {
            Console.WriteLine("== Inserir Revista ==");

            var revista = ObterRevista();

            if (!revista.Validar(out string erros))
            {
                Console.WriteLine("Erros:\n" + erros);
                return;
            }

            if (repoRevista.ExisteComMesmoTituloEEdicao(revista.Titulo, revista.NumeroEdicao))
            {
                Console.WriteLine("Já existe revista com esse título e edição.");
                return;
            }

            repoRevista.Inserir(revista);
            Console.WriteLine("Revista inserida com sucesso.");
        }

        public void VisualizarTodos()
        {
            var revistas = repoRevista.SelecionarTodos();

            Console.WriteLine("== Revistas Cadastradas ==");
            foreach (var r in revistas)
            {
                Console.WriteLine($"ID: {r.Id} | Título: {r.Titulo} | Edição: {r.NumeroEdicao} | Ano: {r.Ano} | Status: {r.Status} | Caixa: {r.Caixa.Etiqueta}");
            }
        }

        public void Editar()
        {
            VisualizarTodos();
            Console.Write("Digite o ID da revista para editar: ");
            int id = int.Parse(Console.ReadLine()!);

            var nova = ObterRevista();

            if (!nova.Validar(out string erros))
            {
                Console.WriteLine("Erros:\n" + erros);
                return;
            }

            if (repoRevista.Editar(id, nova))
                Console.WriteLine("Revista atualizada.");
            else
                Console.WriteLine("Revista não encontrada.");
        }

        public void Excluir()
        {
            VisualizarTodos();
            Console.Write("Digite o ID da revista para excluir: ");
            int id = int.Parse(Console.ReadLine()!);

            if (repoRevista.Excluir(id))
                Console.WriteLine("Revista excluída.");
            else
                Console.WriteLine("Erro ao excluir revista.");
        }

        private Revista ObterRevista()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine()!;
            Console.Write("Número da edição: ");
            int edicao = int.Parse(Console.ReadLine()!);
            Console.Write("Ano de publicação: ");
            int ano = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Caixas disponíveis:");
            var caixas = repoCaixa.SelecionarTodos();
            foreach (var c in caixas)
                Console.WriteLine($"ID: {c.Id} | Etiqueta: {c.Etiqueta}");

            Console.Write("Escolha o ID da caixa: ");
            int idCaixa = int.Parse(Console.ReadLine()!);
            var caixa = repoCaixa.SelecionarPorId(idCaixa);

            return new Revista
            {
                Titulo = titulo,
                NumeroEdicao = edicao,
                Ano = ano,
                Caixa = caixa!
            };
        }
    }
}