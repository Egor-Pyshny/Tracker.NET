using SQLite;

namespace Tracker.Controllers.DataBaseController.Models
{
    [Table("user_table")]
    public class UserModel
    {
        [PrimaryKey, AutoIncrement, NotNull]
        [Column("id")]
        public int Id { get; set; }

        [NotNull]
        [Column("first_name")]
        [Indexed(Name = "UC_Column1_Column2_Column3", Unique = true)]

        public string FirstName { get; set; }

        [NotNull]
        [Column("second_name")]
        [Indexed(Name = "UC_Column1_Column2_Column3", Unique = true)]
        public string SecondName { get; set; }

        [NotNull]
        [Column("third_name")]
        [Indexed(Name = "UC_Column1_Column2_Column3", Unique = true)]

        public string ThirdName { get; set; }

        [NotNull]
        [Column("birth_date")]
        public string DateOfBirth { get; set; }

        [NotNull]
        [Column("disiase")]
        [MaxLength(45)]
        public string Description { get; set; }

        [NotNull]
        public int Role { get; set; } = 0;
    }
}
