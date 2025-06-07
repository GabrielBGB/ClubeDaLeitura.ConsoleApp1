using System;
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    public class Reserva : EntidadeBase
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataReserva { get; set; }
        public string Status { get; set; } = "Ativa"; // Status: Ativa / Concluída

        public override string[] Validar()
        {
            List<string> erros = new List<string>();
            if (Amigo == null) erros.Add("O Amigo é obrigatório");
            if (Revista == null) erros.Add("A Revista é obrigatória");
            else if (Revista.Status != "Disponível") erros.Add("A revista não está disponível para reserva.");
            return erros.ToArray();
        }

        public void Concluir()
        {
            Status = "Concluída";
            // O status da revista será atualizado na TelaEmprestimo quando for convertida
        }

        public override string ToString()
        {
            return $"Id: {Id} | Revista: {Revista.Titulo} | Amigo: {Amigo.Nome} | Data: {DataReserva.ToShortDateString()} | Status: {Status}";
        }
    }
}