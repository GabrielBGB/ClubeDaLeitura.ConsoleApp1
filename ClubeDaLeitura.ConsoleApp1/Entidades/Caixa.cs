// Local: ClubeDaLeitura.ConsoleApp1/Entidades/Caixa.cs
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    public class Caixa : EntidadeBase
    {
        public string Cor { get; set; }
        public string Etiqueta { get; set; }
        public int DiasEmprestimo { get; set; }

        // Sobrescreve o método Validar para implementar as regras de negócio específicas da Caixa.
        public override string[] Validar()
        {
            // Usa uma lista para facilitar a adição de múltiplos erros.
            List<string> erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Cor))
                erros.Add("O campo 'Cor' é obrigatório.");

            if (string.IsNullOrWhiteSpace(Etiqueta))
                erros.Add("O campo 'Etiqueta' é obrigatório.");
            else if (Etiqueta.Length > 50)
                erros.Add("O campo 'Etiqueta' não pode exceder 50 caracteres.");

            if (DiasEmprestimo <= 0)
                erros.Add("O campo 'Dias para Empréstimo' deve ser um número positivo.");

            // Converte a lista de erros em um array.
            return erros.ToArray();
        }

        // Sobrescreve o ToString para uma exibição amigável nos menus.
        public override string ToString()
        {
            return $"Id: {Id} | Etiqueta: {Etiqueta} | Cor: {Cor} | Prazo de Empréstimo: {DiasEmprestimo} dias";
        }
    }
}