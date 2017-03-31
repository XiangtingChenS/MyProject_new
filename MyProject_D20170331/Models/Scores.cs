namespace MyProject_D20170331.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stats.Scores")]
    public partial class Scores
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string TestID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string StudentID { get; set; }

        public byte Score { get; set; }

        public virtual Tests Tests { get; set; }
    }
}
