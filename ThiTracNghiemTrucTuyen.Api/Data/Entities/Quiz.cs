using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThiTracNghiemTrucTuyen.Api.Data.Entities
{
  public class Quiz
  {
    [Key]
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    
    public int TotalQuestion {  get; set; }
    //public int TotalAnswer { get; set; }
    public int TimeMinute { get; set; }
    //public int TimeSecond { get; set; }
    public bool Active { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    //[ForeignKey("abc")]
    public virtual Category Category { get; set; }


    public ICollection<Question> Questions { get; set; } = [];

  }
}
