namespace DesafioICOMON.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
        public class Funcionario
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "O nome é obrigatório")]
            public required string Nome { get; set; }

            [Required(ErrorMessage = "O email é obrigatório")]
            [EmailAddress(ErrorMessage = "Formato de email inválido")]
            public required string Email { get; set; }

            [Required(ErrorMessage = "Departamento é obrigatório")]
            public int DepartamentoId { get; set; }

            [Required(ErrorMessage = "Cargo é obrigatório")]
            public int CargoId { get; set; }

            [Required(ErrorMessage = "Data de admissão é obrigatória")]
            [DataType(DataType.Date)]
            public DateTime DataAdmissao { get; set; }

  
            public int? ManagerId { get; set; }

            [ForeignKey("DepartamentoId")]
            public  Departamento Departamento { get; set; }

            [ForeignKey("CargoId")]
            public  Cargo Cargo { get; set; }

            [ForeignKey("ManagerId")]
            public  Funcionario Manager { get; set; }
        }
}
