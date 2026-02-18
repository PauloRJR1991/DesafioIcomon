using System.ComponentModel.DataAnnotations;

namespace DesafioICOMON.Models
{
  public class Cargo
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "O nível é obrigatório")]
    [StringLength(2, ErrorMessage = "O nível deve ter 2 caracteres")]
    public string Nivel { get; set; }

    [Required(ErrorMessage = "O campo Ativo é obrigatório")]
    public bool Ativo { get; set; }
  }

  public enum NivelCargo
  {
    [Display(Name = "Estagiario")] Estagiario = 1
    , [Display(Name = "Junior")] Junior = 2
    , [Display(Name = "Pleno")] Pleno = 3
    , [Display(Name = "Senior")] Senior = 4
    , [Display(Name = "Coordenacao")] Coordenacao = 5
    , [Display(Name = "Gerencia")] Gerencia = 6
    , [Display(Name = "Diretoria")] Diretoria = 7
  }

}
