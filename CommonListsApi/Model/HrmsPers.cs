using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommonListsApi.Model
{
    [Table("HrmsPers", Schema = "CommonListsApi")]
    public class HrmsPers
    {     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Key]
        public string EmployeeId { get; set; }
        public int RankId { get; set; }

    }
}
