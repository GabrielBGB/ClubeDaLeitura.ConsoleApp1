namespace ClubeDaLeitura.Entidades
{
    public enum StatusRevista
    {
        Disponivel,
        Emprestada,
        Reservada
    }

    public class Revista
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int NumeroEdicao { get; set; }
        public int Ano { get; set; }
        public StatusRevista Status { get; set; } = StatusRevista.Disponivel;

        public Caixa Caixa { get; set; }

        public bool Validar(out string erros)
        {
            erros = "";

            if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 2 || Titulo.Length > 100)
                erros += "Título deve ter entre 2 e 100 caracteres.\n";

            if (NumeroEdicao <= 0)
                erros += "Número da edição deve ser positivo.\n";

            if (Ano < 1900 || Ano > DateTime.Now.Year)
                erros += "Ano de publicação inválido.\n";

            if (Caixa == null)
                erros += "Revista deve estar vinculada a uma caixa.\n";

            return erros == "";
        }

        public void Emprestar() => Status = StatusRevista.Emprestada;
        public void Devolver() => Status = StatusRevista.Disponivel;
        public void Reservar() => Status = StatusRevista.Reservada;
    }
}