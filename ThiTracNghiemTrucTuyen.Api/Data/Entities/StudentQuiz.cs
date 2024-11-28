using System.ComponentModel.DataAnnotations.Schema;

namespace ThiTracNghiemTrucTuyen.Api.Data.Entities
{
  public class StudentQuiz
  {
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Guid QuizId { get; set; }

    // thời gian bắt đầu làm bài kiểm tra trắc nghiệm
    //public DateTime StartedOn { get; set; }
    public DateTime StartQuiz { get; set; }
    // thời gian kết bài thi
    //public DateTime CompleteOn { get; set; }
    public DateTime EndQuiz { get; set; }

    // điểm số
    public double Score { get; set; }

    [ForeignKey(nameof(StudentId))]
    public virtual User Student { get; set; }

    [ForeignKey(nameof(QuizId))]
    public virtual Quiz Quiz { get; set; }
  }
}
