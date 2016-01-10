using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }
        public virtual List<Student> Students { get; set; }
        public DateTime CreatedDate { get; set; }

        #region Constructor(s)

        public Class()
        {
            Students = new List<Student>();
        }

        #endregion
    }
}
