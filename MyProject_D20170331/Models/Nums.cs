namespace MyProject_D20170331.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Nums
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int n { get; set; }
    }
}
