
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonListsApi.Model
{
    [Table("Element", Schema = "CommonListsApi")]
    public class Element
    {       
        public int Id { get; set; }
        public byte Sequence { get; set; }
        public string Text { get; set; }
    }
}
