using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThiTracNghiemTrucTuyen.Api.Data.Entities
{
  public class Question
  {
    [Key]
    public int Id { get; set; }
    //public string Title { get; set; }
    //public string Content { get; set; }
    public string Text { get; set; }
    public Guid QuizId { get; set; }
    [ForeignKey(nameof(QuizId))]
    public virtual Quiz Quiz { get; set; }

    public virtual ICollection<Option> Options { get; set; } = [];
  }
}
