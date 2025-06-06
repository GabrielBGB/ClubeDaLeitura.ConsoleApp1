using ClubeDaLeitura.ConsoleApp1.Entidades;
using ClubeDaLeitura.Entidades;
using ClubeDaLeitura.Repositorios;
using System.Collections.Generic;
using System.Linq;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public class RepositorioMulta : RepositorioBase<Multa>
    {
        public RepositorioMulta() : base("multas.json")
        {
        }

        public List<Multa> SelecionarMultasPendentes()
        {
            return registros.Where(m => m.Status == StatusMulta.Pendente).ToList();
        }

        public bool AmigoTemMultaPendente(Amigo amigo)
        {
            return registros.Any(m => m.Amigo.Id == amigo.Id && m.Status == StatusMulta.Pendente);
        }

        public List<Multa> SelecionarMultasPorAmigo(Amigo amigo)
        {
            return registros.Where(m => m.Emprestimo.Amigo.Id == amigo.Id).ToList();
        }
    }
}