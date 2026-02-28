using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_MZ.Entities;

namespace Web_MZ.Models
{
    public class ProjectViewModel
    {
            public int Id { get; set; }

            public string Title { get; set; }       // Nom du projet

            public string Description { get; set; } // Description du projet

            public string Technologies { get; set; } // Technologies utilisées (ex: C#, React)

            public string Link { get; set; }        // Lien vers le projet (optionnel)

            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

           public int CreatorId { get; set; }

    }
}

