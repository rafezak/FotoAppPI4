using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoApp.JoinTables
{
    public class AssignmentTheme
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int AssignmentId { get; set; }

        public int ThemeId { get; set; }
    }

}
