﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThiTracNghiemTrucTuyen.Api.Data.Entities
{
  public class Option
  {
    [Key]
    public int Id { get; set; }
    public int QuestionId { get; set; }
    //[MaxLength(4000)]
    public string Text { get; set; }

    public bool Correct { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public virtual Question Question { get; set; }
  }
}
