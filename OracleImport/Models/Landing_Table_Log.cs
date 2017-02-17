using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OracleImport.Models
{
    [Table("Control.Landing_Table_Log")]
    public class Landing_Table_Log
    {
        [Key]
        public int id { get; set; }
        public int? Landing_Table_Id { get; set; }
        public int? Success { get; set; }
        public DateTime? Start_Time { get; set; }
        public DateTime? End_Time { get; set; }
        public DateTime? Begin_Land { get; set; }
        public DateTime? End_Land { get; set; }
        public DateTime? Begin_Hash { get; set; }
        public DateTime? End_Hash { get; set; }
        public DateTime? Begin_Stage { get; set; }
        public DateTime? End_Stage { get; set; }
        public int? Source_Rows { get; set; }
        public int? Landing_Rows { get; set; }
        public int? Stage_Rows { get; set; }
        public int? Deleted_Stage_Rows { get; set; }
        public int? New_Stage_Rows { get; set; }
        public string Notes { get; set; }
    }

}
