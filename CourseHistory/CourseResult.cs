using System;

namespace CourseHistory
{
    public class CourseResult
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CadetId { get; set; }
        public byte Sequence { get; set; } 
        public string Result { get; set; }
    }
}
