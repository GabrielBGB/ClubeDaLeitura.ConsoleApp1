using ClubeDaLeitura.Entidades;
using ClubeDaLeitura.Repositorios;

namespace ClubeDaLeitura.Telas
{
    public class TelaAmigo
    {
        private RepositorioAmigo repositorio;

        public TelaAmigo(RepositorioAmigo repo)
        {
            repositorio = repo;
        }

        public void Inserir()
        {
            Console.WriteLine("== Inserir Amigo ==");

            var amigo = ObterAmigo();

            if (!amigo.Validar(out string erros))
            {
                Console.WriteLine("Erros:\n" + erros);
                return;
            }

            if (repositorio.ExisteAmigoComMesmoNomeTelefone(amigo.Nome, amigo.Telefone))
            {
                Console.WriteLine("Já existe um amigo com o mesmo nome e telefone.");
                return;
            }

            repositorio.Inserir(amigo);
            Console.WriteLine("Amigo inserido com sucesso.");
        }

        public void VisualizarTodos()
        {
            var amigos = repositorio.SelecionarTodos();

            Console.WriteLine("== Amigos Cadastrados ==");
            foreach (var a in amigos)
            {
                Console.WriteLine($"ID: {a.Id} | Nome: {a.Nome} | Resp.: {a.Responsavel} | Tel: {a.Telefone}");
            }
        }

        public void Editar()
        {
            VisualizarTodos();
            Console.Write("Digite o ID do amigo que deseja editar: ");
            int id = int.Parse(Console.ReadLine()!);

            var novo = ObterAmigo();

            if (!novo.Validar(out string erros))
            {
                Console.WriteLine("Erros:\n" + erros);
                return;
            }

            if (repositorio.Editar(id, novo))
                Console.WriteLine("Amigo atualizado.");
            else
                Console.WriteLine("Amigo não encontrado.");
        }

        public void Excluir()
        {
            VisualizarTodos();
            Console.Write("Digite o ID do amigo para excluir: ");
            int id = int.Parse(Console.ReadLine()!);

            if (repositorio.Excluir(id))
                Console.WriteLine("Amigo excluído.");
            else
                Console.WriteLine("Não foi possível excluir. Verifique se o amigo possui empréstimos.");
        }

        private Amigo ObterAmigo()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine()!;
            Console.Write("Responsável: ");
            string responsavel = Console.ReadLine()!;
            Console.Write("Telefone (formato (XX) XXXXX-XXXX): ");
            string telefone = Console.ReadLine()!;

            return new Amigo
            {
                Nome = nome,
                Responsavel = responsavel,
                Telefone = telefone
            };
        }
    }
}