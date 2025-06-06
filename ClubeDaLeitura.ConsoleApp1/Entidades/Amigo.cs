// Local: ClubeDaLeitura.ConsoleApp1/Entidades/Amigo.cs
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ClubeDaLeitura.ConsoleApp1.Entidades
{
    public class Amigo : EntidadeBase
    {
        public string Nome { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3)
                erros.Add("O campo 'Nome' é obrigatório e deve ter no mínimo 3 caracteres.");

            if (string.IsNullOrWhiteSpace(NomeResponsavel) || NomeResponsavel.Length < 3)
                erros.Add("O campo 'Nome do Responsável' é obrigatório e deve ter no mínimo 3 caracteres.");

            // --- INÍCIO DA CORREÇÃO ---
            // Primeiro, verificamos se o telefone é nulo ou vazio. Só depois tentamos validar o formato.
            if (string.IsNullOrWhiteSpace(Telefone))
            {
                erros.Add("O campo 'Telefone' é obrigatório.");
            }
            else if (!Regex.IsMatch(Telefone, @"^\(\d{2}\)\s\d{4,5}-\d{4}$"))
            {
                erros.Add("O campo 'Telefone' está em um formato inválido. Use (XX) XXXXX-XXXX.");
            }
            // --- FIM DA CORREÇÃO ---

            return erros.ToArray();
        }

        public override string ToString()
        {
            return $"Id: {Id} | Nome: {Nome} | Telefone: {Telefone} | Responsável: {NomeResponsavel}";
        }
    }
}