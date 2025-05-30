namespace ClubeDaLeitura.Entidades
{
    public enum StatusEmprestimo
    {
        Aberto,
        Concluido,
        Atrasado
    }

    public class Emprestimo
    {
        public int Id { get; set; }
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; private set; }
        public StatusEmprestimo Status { get; private set; }

        public Emprestimo()
        {
            Status = StatusEmprestimo.Aberto;
        }

        public bool Validar(out string erros)
        {
            erros = "";

            if (Amigo == null)
                erros += "Amigo é obrigatório.\n";

            if (Revista == null || Revista.Status != StatusRevista.Disponivel)
                erros += "Revista inválida ou indisponível.\n";

            return erros == "";
        }

        public void RegistrarEmprestimo()
        {
            DataEmprestimo = DateTime.Now;
            DataDevolucao = DataEmprestimo.AddDays(Revista.Caixa.DiasEmprestimo);
            Revista.Emprestar();
            Status = StatusEmprestimo.Aberto;
        }

        public void RegistrarDevolucao()
        {
            Revista.Devolver();

            if (DateTime.Now > DataDevolucao)
                Status = StatusEmprestimo.Atrasado;
            else
                Status = StatusEmprestimo.Concluido;
        }

        public void AtualizarStatus()
        {
            if (Status == StatusEmprestimo.Aberto && DateTime.Now > DataDevolucao)
                Status = StatusEmprestimo.Atrasado;
        }
    }
}