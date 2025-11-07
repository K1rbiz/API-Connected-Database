using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace API_Connected_Database.Models;

// Model representing a Cat Fact
public class CatFact
{
    [PrimaryKey, AutoIncrement] // Primary key for the SQLite database
    public int Id { get; set; } // Unique identifier for each CatFact

    public string fact { get; set; } = string.Empty; // The cat fact text
    public int length { get; set; } // Length of the cat fact text
}
