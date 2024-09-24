using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
  public class Project
  {
      [Key]
      public int Id { get; set; }
      public int OwnerId { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }

      [ForeignKey(nameof(OwnerId))]
      public User Owner { get; set; }
  }
}
