// Local: ClubeDaLeitura.ConsoleApp1/Entidades/Emprestimo.cs
using System;
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    public class Emprestimo : EntidadeBase
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        // ADICIONE ESTA PROPRIEDADE
        public string Status { get; set; } = "Aberto"; // Valor padrão

        public override string[] Validar()
        {
            List<string> erros = new List<string>();
            if (Amigo == null)
                erros.Add("O campo 'Amigo' é obrigatório.");
            if (Revista == null)
                erros.Add("O campo 'Revista' é obrigatório.");
            else if (Revista.Status != "Disponível")
                erros.Add("A revista selecionada não está disponível para empréstimo.");

            // Adicionar regra que amigo não pode ter empréstimo em aberto ou multas...
            return erros.ToArray();
        }

        public void Fechar()
        {
            if (Status == "Aberto")
            {
                Status = "Fechado";
                Revista.Status = "Disponível";
            }
        }

        public override string ToString()
        {
            
            return $"Id: {Id} | Status: {Status} | Amigo: {Amigo?.Nome} | Revista: {Revista?.Titulo} | Devolução: {DataDevolucao.ToShortDateString()}";
        }
    }
}
