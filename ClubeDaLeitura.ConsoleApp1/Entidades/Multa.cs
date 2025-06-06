// Local: ClubeDaLeitura.ConsoleApp1/Entidades/Multa.cs
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    // PRECISA HERDAR DE ENTIDADEBASE PARA TER O 'Id'
    public class Multa : EntidadeBase
    {
        // AS PROPRIEDADES DEVEM TER LETRA MAIÚSCULA
        public decimal Valor { get; set; }
        public Emprestimo Emprestimo { get; set; }
        public bool EstaPaga { get; set; }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (Emprestimo == null)
                erros.Add("A multa deve estar associada a um empréstimo.");

            if (Valor <= 0)
                erros.Add("O valor da multa deve ser positivo.");

            return erros.ToArray();
        }

        public override string ToString()
        {
            string status = EstaPaga ? "Paga" : "Pendente";
            return $"Id: {Id} | Valor: R${Valor:F2} | Empréstimo do Amigo: {Emprestimo?.Amigo?.Nome} | Status: {status}";
        }
    }
}