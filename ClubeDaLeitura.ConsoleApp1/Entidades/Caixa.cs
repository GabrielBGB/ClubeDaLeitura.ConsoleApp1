// Local: Entidades/Caixa.cs
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    public class Caixa : EntidadeBase
    {
        public CorCaixa Cor { get; set; } // Usa o enum de cores
        public string Etiqueta { get; set; }
        public int DiasEmprestimo { get; set; }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Etiqueta))
                erros.Add("O campo 'Etiqueta' é obrigatório.");
            else if (Etiqueta.Length > 50)
                erros.Add("O campo 'Etiqueta' deve ter no máximo 50 caracteres.");

            if (DiasEmprestimo <= 0)
                erros.Add("O campo 'Dias de Empréstimo' deve ser um número positivo.");

            // A validação da cor agora é garantida pelo tipo Enum, não precisa de um 'if' aqui.

            return erros.ToArray();
        }

        public override string ToString()
        {
            return $"Id: {Id} | Etiqueta: {Etiqueta} | Cor: {Cor} | Prazo de Empréstimo: {DiasEmprestimo} dias";
        }
    }
}