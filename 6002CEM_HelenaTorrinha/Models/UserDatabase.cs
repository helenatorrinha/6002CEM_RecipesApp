using Postgrest.Attributes;
using Postgrest.Models;

namespace _6002CEM_HelenaTorrinha.Models
{
	[Table("users")]
	public class UserDatabase: BaseModel
	{
		[PrimaryKey("id", false)]
		public int id { get; set; }

        [Column("username")]
        public string username { get; set; }

        [Column("firstName")]
        public string firstName { get; set; }

        [Column("lastName")]
        public string lastName { get; set; }

        [Column("email")]
		public string email { get; set; }

    }
}

