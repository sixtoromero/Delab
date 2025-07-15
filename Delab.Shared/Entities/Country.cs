using System.ComponentModel.DataAnnotations;

namespace Delab.Shared.Entities;

public class Country
{
    [Key]
    public int CountryId { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor de {1} caracteres")]
    [Display(Name = "Pais")]
    public string Name { get; set; } = null!; //Para no aceptar nulos en el campo Name
    [MaxLength(10, ErrorMessage = "El campo {0} no puede ser mayor de {1} caracteres")]
    [Display(Name = "Cod Phone")]
    public string? CodPhone { get; set; }

    //Relaciones
    public ICollection<State>? States { get; set; }

}
