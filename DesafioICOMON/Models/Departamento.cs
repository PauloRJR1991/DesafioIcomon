using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DesafioICOMON.Models
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O código é obrigatório")]
        [Column(TypeName = "decimal(10,0)")]
        public decimal Codigo { get; set; }

        [Required(ErrorMessage = "O campo Ativo é obrigatório")]
        public bool Ativo { get; set; }
    }
}
