using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { 
            get => _descricao; 
            set
            {
                if(value == null) 
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                _descricao = value;
            }
        }
        public double Quantidade {  get; set; }
        public double Preco {  get; set; }

        // Desafio 1: Categoria
        public string Categoria { get; set; }

        // Desafio 2: Data de Cadastro
        public DateTime DataCadastro { get; set; }
        public double Total { get => Quantidade * Preco; }
    }
}
