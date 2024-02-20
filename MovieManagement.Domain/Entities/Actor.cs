using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagement.Domain;

public class Actor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MinLength(1)]
    public string? FirstName { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MinLength(1)]
    public string? LastName { get; set; }

    public List<Movie>? Movies { get; set; }

    public Biography? Biography { get; set; }
}
