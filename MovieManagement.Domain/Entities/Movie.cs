using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagement.Domain;

public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MinLength(1)]
    public string? Name { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MinLength(1)]
    public string? Description { get; set; }

    [ForeignKey("ActorId")]
    public Guid? ActorId { get; set; }
    public Actor? Actor { get; set; }

    public List<Genre>? Genre { get; set; }

}
