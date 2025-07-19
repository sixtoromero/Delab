﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delab.Shared.Entities;

public class Corporation
{
    [Key]
    public int CorporationId { get; set; }
    [Display(Name = "Logo")]
    public string? Imagen { get; set; }
    [MaxLength(100, ErrorMessage = "El Maximo de caracteres es {0}")]
    [Required(ErrorMessage = "El campo {0} es Requerido")]
    [Display(Name = "Empresa/Persona")]
    public string? Name { get; set; }

    [MaxLength(15, ErrorMessage = "El Maximo de caracteres es {0}")]
    [Required(ErrorMessage = "El {0} es Obligatorio")]
    [Display(Name = "RUC ó DNI")]
    public string? NroDocument { get; set; }

    [MaxLength(12, ErrorMessage = "El Máximo de caracteres es {1}")]
    [Required(ErrorMessage = "El {0} es Obligatorio")]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Teléfono")]
    public string? Phone { get; set; }

    [MaxLength(200, ErrorMessage = "El campo no puede ser mayor a {1} de largo")]
    [Required(ErrorMessage = "El {0} es Obligatorio")]
    [Display(Name = "Dirección")]
    public string? Address { get; set; }

    [Display(Name = "Pais")]
    public int CountryId { get; set; }

    [Display(Name = "Plan de Software")]
    [Required(ErrorMessage = "El {0} es Obligatorio")]
    public int SoftPlanId { get; set; }
    
    //Tiempo Activo de la cuenta
    [Required(ErrorMessage = "El {0} es Obligatorio")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Inicio")]
    public DateTime DateStart { get; set; }

    [Required(ErrorMessage = "El {0} es Obligatorio")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Vencimiento")]
    public DateTime DateEnd { get; set; }

    [Display(Name = "Activo")]
    public bool Active { get; set; }

    //Propiedad Virtual de Acceso a imagen
    public string ImageFullPath => Imagen == string.Empty || Imagen == null
        ? $"https://localhost:7123/Images/NoImage.png"
        : $"https://localhost:7123/Images/ImgCorporation/{Imagen}";

    [NotMapped]
    public string? ImgBase64 { get; set; }

    //Relaciones
    public SoftPlan? SoftPlan { get; set; }
    public Country? Country { get; set; }
    public ICollection<Manager>? Manager { get; set; }
}
