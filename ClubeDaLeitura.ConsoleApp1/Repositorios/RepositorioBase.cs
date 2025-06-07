// Local: Repositorios/RepositorioBase.cs
using ClubeDaLeitura.ConsoleApp1.Entidades;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System; // Adicionado para AppDomain

namespace ClubeDaLeitura.ConsoleApp1.Repositorios
{
    public abstract class RepositorioBase<T> where T : EntidadeBase
    {
        protected readonly List<T> registros;
        protected int contadorId = 1;
        private readonly string caminhoArquivo;

        private readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public RepositorioBase(string nomeArquivo)
        {
            caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
            registros = new List<T>(); // Inicializa a lista antes de carregar
            CarregarDados();
        }

        private void CarregarDados()
        {
            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);
                if (!string.IsNullOrEmpty(json))
                {
                    var dadosCarregados = JsonConvert.DeserializeObject<List<T>>(json, settings);
                    if (dadosCarregados != null)
                    {
                        registros.AddRange(dadosCarregados);
                        if (registros.Any())
                            contadorId = registros.Max(r => r.Id) + 1;
                    }
                }
            }
        }

        // --- MUDANÇA AQUI: de 'private' para 'protected' ---
        protected void SalvarDados()
        {
            string json = JsonConvert.SerializeObject(this.registros, settings);
            File.WriteAllText(caminhoArquivo, json);
        }

        public virtual void Inserir(T novoRegistro)
        {
            novoRegistro.Id = contadorId++;
            registros.Add(novoRegistro);
            SalvarDados();
        }

        public abstract void Editar(int id, T registroAtualizado);

        public void Excluir(int id)
        {
            T registro = SelecionarPorId(id);
            if (registro != null)
            {
                registros.Remove(registro);
                SalvarDados();
            }
        }

        public T SelecionarPorId(int id)
        {
            return registros.FirstOrDefault(r => r.Id == id);
        }

        public List<T> SelecionarTodos()
        {
            return registros.OrderBy(r => r.Id).ToList();
        }
    }
}