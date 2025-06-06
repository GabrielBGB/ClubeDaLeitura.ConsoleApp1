// Local: ClubeDaLeitura.ConsoleApp1/Repositorios/RepositorioBase.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    // A restrição "where T : EntidadeBase" garante que qualquer tipo 'T' usado aqui
    // DEVE ser uma classe que herda de EntidadeBase. Isso nos dá acesso ao 'Id'.
    public abstract class RepositorioBase<T> where T : EntidadeBase
    {
        protected readonly List<T> registros = new List<T>();
        protected int contadorId = 1;

        // "virtual" permite que este método seja sobrescrito por classes filhas, se necessário.
        public virtual void Inserir(T novoRegistro)
        {
            novoRegistro.Id = contadorId++;
            registros.Add(novoRegistro);
        }

        // O método Editar agora é abstrato, forçando a classe filha a implementar
        // a lógica de como os campos devem ser atualizados.
        public abstract void Editar(int id, T registroAtualizado);

        public void Excluir(int id)
        {
            T registro = SelecionarPorId(id);
            if (registro != null)
                registros.Remove(registro);
        }

        public List<T> SelecionarTodos()
        {
            // Retorna uma cópia da lista ordenada por ID para exibição consistente.
            return registros.OrderBy(r => r.Id).ToList();
        }

        public T SelecionarPorId(int id)
        {
            // Usa LINQ para encontrar o primeiro registro com o Id correspondente.
            return registros.FirstOrDefault(r => r.Id == id);
        }
    }
}