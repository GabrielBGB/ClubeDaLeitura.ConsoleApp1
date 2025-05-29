namespace ClubeDaLeitura.Entidades
{
    public class Caixa
    {
        public int Id { get; set; }
        public string Etiqueta { get; set; }
        public string Cor { get; set; }
        public int DiasEmprestimo { get; set; }

        public List<int> IdsRevistas { get; set; } = new();

        public bool Validar(out string erros)
        {
            erros = "";

            if (string.IsNullOrWhiteSpace(Etiqueta) || Etiqueta.Length > 50)
                erros += "Etiqueta é obrigatória e deve ter no máximo 50 caracteres.\n";

            if (string.IsNullOrWhiteSpace(Cor))
                erros += "Cor é obrigatória.\n";

            if (DiasEmprestimo <= 0)
                erros += "Dias de empréstimo deve ser maior que 0.\n";

            return erros == "";
        }
    }
}
