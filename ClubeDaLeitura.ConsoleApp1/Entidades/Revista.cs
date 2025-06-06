// Local: ClubeDaLeitura.ConsoleApp1/Entidades/Revista.cs
using System;
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    public class Revista : EntidadeBase
    {
        public string Titulo { get; set; }
        public int NumeroEdicao { get; set; }
        public int AnoPublicacao { get; set; }
        public Caixa Caixa { get; set; }

        // ESTA PROPRIEDADE ESTAVA FALTANDO NO SEU ARQUIVO
        public string Status { get; set; } = "Disponível";

        public override string[] Validar()
        {
            List<string> erros = new List<string>();
            if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 2)
                erros.Add("O campo 'Título' é obrigatório e deve ter no mínimo 2 caracteres.");
            if (NumeroEdicao <= 0)
                erros.Add("O 'Número da Edição' deve ser positivo.");
            if (AnoPublicacao <= 1800 || AnoPublicacao > DateTime.Now.Year)
                erros.Add("O 'Ano de Publicação' deve ser uma data válida.");
            if (Caixa == null)
                erros.Add("A 'Caixa' onde a revista será guardada é obrigatória.");
            return erros.ToArray();
        }

        public override string ToString()
        {
            // O MÉTODO FOI ATUALIZADO PARA INCLUIR O STATUS
            return $"Id: {Id} | Título: {Titulo} | Edição: {NumeroEdicao} | Status: {Status}";
        }
    }
}