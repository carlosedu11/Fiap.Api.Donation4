using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation4.Models
{
    public class CategoriaModel
    {
        public int CategoriaId { get; set; }
        public string? NomeCategoria { get; set; }
        public string? Descricao { get; set; }


    }
}