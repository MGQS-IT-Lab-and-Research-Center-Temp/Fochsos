using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.Class
{
    public class DateCreatedGroup
    {

        [DataType(DataType.Date)]
        public DateTime? DateCreated { get; set; }

        public int ClassCount { get; set; }
    }
}
