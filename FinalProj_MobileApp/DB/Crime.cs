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
        [Column("title")] public string Title { get; set; }
        [Column("descritpion")] public string Description { get; set; }
        [Column("locationName")] public string LocationName { get; set; }
        [Column("date")] public DateTime Date { get; set; }
        [Column("severity")] public string Severity { get; set; }
        [Column("status")] public string Status { get; set; }
        [Column("latitude")] public double Latitude { get; set; }
        [Column("longitude")] public double Longitude { get; set; }

    }
}
