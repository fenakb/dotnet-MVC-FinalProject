using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DAL
{
    public class MercadoNaturalEntities : DbContext
    {
        public MercadoNaturalEntities()
            : base("name=MercadoNaturalEntities")
        {
        }

        public DbSet<Empregado> Empregados { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<LinhasFatura> LinhasFatura { get; set; }
    }

    [Table("Empregados")]
    public class Empregado
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; }
    }

    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Preço")]
        public float Preco { get; set; }
        [ForeignKey("Empregado")]
        [Display(Name = "Empregado")]
        public int IdEmpregado { get; set; }
        public Empregado Empregado { get; set; }
        [Display(Name = "Data inserção")]
        public DateTime DataCriacao { get; set; }

    }

    [Table("Faturas")]
    public class Fatura
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Data de registro")]
        public DateTime DataRegistro { get; set; }
        [Display(Name = "Preço total")]
        public float PrecoTotal { get; set; }

    }

    [Table("LinhasFatura")]
    public class LinhasFatura
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Produto")]
        [Display(Name = "Produto")]
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
        [ForeignKey("Fatura")]
        [Display(Name = "Fatura")]
        public int IdFatura { get; set; }
        public Fatura Fatura { get; set; }
    }
}
