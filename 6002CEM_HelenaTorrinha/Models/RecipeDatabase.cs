using Postgrest.Attributes;
using Postgrest.Models;

namespace _6002CEM_HelenaTorrinha.Models
{
	[Table("recipes")]
	public class RecipeDatabase: BaseModel
	{
		[PrimaryKey("id", false)]
		public int id { get; set; }

        [Column("user_id")]
        public int userID { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("prep_time")]
        public int prepTime { get; set; }

        [Column("cook_time")]
		public int cookTime { get; set; }

        [Column("servings")]
        public int servings { get; set; }

        [Column("ingredients")]
        public string ingredients { get; set; }

        [Column("instructions")]
        public string instructions { get; set; }
    }
}

