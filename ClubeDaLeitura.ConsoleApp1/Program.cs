using ClubeDaLeitura.Repositorios;
using ClubeDaLeitura.Telas;

class Program
{
    static void Main()
    {
        var repositorioAmigo = new RepositorioAmigo();
        var telaAmigo = new TelaAmigo(repositorioAmigo);

        var repositorioCaixa = new RepositorioCaixa();
        var telaCaixa = new TelaCaixa(repositorioCaixa);

        while (true)
        {
            Console.WriteLine("\n--- Clube da Leitura ---");
            Console.WriteLine("1. Módulo Amigos");
            Console.WriteLine("2. Módulo Caixas");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha o módulo: ");
            string modulo = Console.ReadLine()!;
            Console.Clear();

            if (modulo == "0") break;

            switch (modulo)
            {
                case "1": MenuAmigos(telaAmigo); break;
                case "2": MenuCaixas(telaCaixa); break;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }

    static void MenuAmigos(TelaAmigo tela)
    {
        while (true)
        {
            Console.WriteLine("\n--- Módulo Amigos ---");
            Console.WriteLine("1. Inserir Amigo");
            Console.WriteLine("2. Visualizar Amigos");
            Console.WriteLine("3. Editar Amigo");
            Console.WriteLine("4. Excluir Amigo");
            Console.WriteLine("0. Voltar");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine()!;
            Console.Clear();

            switch (opcao)
            {
                case "1": tela.Inserir(); break;
                case "2": tela.VisualizarTodos(); break;
                case "3": tela.Editar(); break;
                case "4": tela.Excluir(); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }

    static void MenuCaixas(TelaCaixa tela)
    {
        while (true)
        {
            Console.WriteLine("\n--- Módulo Caixas ---");
            Console.WriteLine("1. Inserir Caixa");
            Console.WriteLine("2. Visualizar Caixas");
            Console.WriteLine("3. Editar Caixa");
            Console.WriteLine("4. Excluir Caixa");
            Console.WriteLine("0. Voltar");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine()!;
            Console.Clear();

            switch (opcao)
            {
                case "1": tela.Inserir(); break;
                case "2": tela.VisualizarTodos(); break;
                case "3": tela.Editar(); break;
                case "4": tela.Excluir(); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }
}