using ClubeDaLeitura.Entidades;
using System;

namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    public enum StatusMulta
    {
        Pendente,
        Quitada
    }

    public class Multa : EntidadeBase
    {
        public Emprestimo Emprestimo { get; set; }
        public decimal Valor { get; private set; }
        public StatusMulta Status { get; set; }
        public Amigo Amigo => Emprestimo.Amigo;

        public Multa(Emprestimo emprestimo)
        {
            Emprestimo = emprestimo;
            Status = StatusMulta.Pendente;
            CalcularValor();
        }

        public void CalcularValor()
        {
            if (Emprestimo.Status == StatusEmprestimo.Atrasado)
            {
                TimeSpan diasEmAtraso = DateTime.Now.Date - Emprestimo.DataDevolucao.Date;
                if (diasEmAtraso.Days > 0)
                {
                    Valor = diasEmAtraso.Days * 2.00m;
                }
            }
        }

        public void Quitar()
        {
            Status = StatusMulta.Quitada;
        }
    }
}