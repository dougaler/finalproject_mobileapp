using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FinalProj_MobileApp.DB
{
    [Table("crime")]
    public class Crime
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")] public int Id { get; set; }
        [Column("crimeType")] public string CrimeType { get; set; }
        [Column("crimeLocation")] public string CrimeLocation { get; set; }
        [Column("crimeStatus")] public bool CrimeStatus { get; set; }
    }
}
