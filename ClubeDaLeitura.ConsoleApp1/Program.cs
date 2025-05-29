using ClubeDaLeitura.Repositorios;
using ClubeDaLeitura.Telas;

class Program
{
    static void Main()
    {
        var repositorioAmigo = new RepositorioAmigo();
        var telaAmigo = new TelaAmigo(repositorioAmigo);

        while (true)
        {
            Console.WriteLine("\n--- Clube da Leitura ---");
            Console.WriteLine("1. Inserir Amigo");
            Console.WriteLine("2. Visualizar Amigos");
            Console.WriteLine("3. Editar Amigo");
            Console.WriteLine("4. Excluir Amigo");
            Console.WriteLine("0. Sair");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine()!;

            Console.Clear();

            switch (opcao)
            {
                case "1": telaAmigo.Inserir(); break;
                case "2": telaAmigo.VisualizarTodos(); break;
                case "3": telaAmigo.Editar(); break;
                case "4": telaAmigo.Excluir(); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }
}