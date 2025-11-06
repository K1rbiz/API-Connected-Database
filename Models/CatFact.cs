using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace API_Connected_Database.Models;

public class CatFact
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string fact { get; set; } = string.Empty;
    public int length { get; set; }
}
