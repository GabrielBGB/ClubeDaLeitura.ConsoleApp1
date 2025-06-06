// Local: ClubeDaLeitura.ConsoleApp1/Entidades/EntidadeBase.cs
namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    public abstract class EntidadeBase
    {
        // Propriedade pública com PascalCase, padrão do C#
        public int Id { get; set; }

        // Método abstrato que força as classes filhas a implementarem sua própria lógica de validação.
        // Retorna um array de strings contendo as mensagens de erro. Se não houver erros, retorna um array vazio.
        public abstract string[] Validar();
    }
}