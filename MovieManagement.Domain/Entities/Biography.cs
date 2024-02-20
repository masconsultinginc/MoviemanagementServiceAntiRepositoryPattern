using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagement.Domain;

public class Biography
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string? Description { get; set; }

    [ForeignKey("ActorId")]
    public Actor? Actor { get; set; }
    public Guid? ActorId { get; set; }
}
