using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FramaFlix.Models;

[Table("Movie")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Display(Name = "Título Original")]
    [Required(ErrorMessage = "Por favor, informe o título Original")]
    [StringLength(100, ErrorMessage = "O título original deve possuir no máximo 100 caracteres")]
    public string OriginalTitle { get; set; }

    [Display(Name = "Título")]
    [Required(ErrorMessage = "Por favor, informe o título")]
    [StringLength(100, ErrorMessage = "O título deve possuir no máximo 100 caracteres")]
    public string Title { get; set; }

    [Display(Name = "Sinopse")]
    [StringLength(8000, ErrorMessage = "A sinopse deve possuir no máximo 8000 caracteres")]
    public string Synopsis { get; set; }

    [Column(TypeName = "Year")]
    [Display(Name = "Ano de estreia")]
    [Required(ErrorMessage = "Por favor, informe o ano de estreia.")]
    public Int16 MovieYear { get; set; }
 [Display(Name = "Duração(em minutos)")]
 [Required(ErrorMessage ="Por favor, informe a duração")]
    public Int16 Duration { get; set; }

    [Display(Name ="Classificação etária")]
    [Required(ErrorMessage ="Por favor, informe a classificação etária")]
    public byte AgeRating { get; set; } = 0;

    [StringLength(200)]
    [Display(Name = "Foto")]

    public string Image { get; set; }
    
     // h - hora; hh - hora com digitos; m- minutos; s
     // d - dias; M - mês; yyyy- ano
    [NotMapped]
    [Display(Name = "Duração")]
     public string HourDuration { get {
        return TimeSpan.FromMinutes(Duration).ToString(@"%h'h'm'min'" );
     } }
    
     public ICollection<MovieGenre> Genres { get; set; }

}