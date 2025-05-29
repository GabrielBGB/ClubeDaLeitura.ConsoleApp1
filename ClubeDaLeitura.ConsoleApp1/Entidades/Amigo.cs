namespace ClubeDaLeitura.Entidades
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Responsavel { get; set; }
        public string Telefone { get; set; }

        public List<int> IdsEmprestimos { get; set; } = new();

        public bool Validar(out string erros)
        {
            erros = "";

            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
                erros += "Nome deve ter entre 3 e 100 caracteres.\n";

            if (string.IsNullOrWhiteSpace(Responsavel) || Responsavel.Length < 3 || Responsavel.Length > 100)
                erros += "Responsável deve ter entre 3 e 100 caracteres.\n";

            if (!System.Text.RegularExpressions.Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
                erros += "Telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.\n";

            return erros == "";
        }
    }
}