using System.ComponentModel.DataAnnotations.Schema;

namespace CommonListsApi.Model
{
    [Table("Rank",Schema = "CommonListsApi")]
    public class Rank
    {
        public int Id { get; set; }       
        public int Element { get; set; }
        public byte Sequence { get; set; } 
        public string Text { get; set; }
    }    
}
