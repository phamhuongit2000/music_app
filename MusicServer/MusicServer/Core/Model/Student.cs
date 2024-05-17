using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public byte[] Photo { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Height { get; set; }
    public float Weight { get; set; }

    public Grade Grade { get; set; }
}