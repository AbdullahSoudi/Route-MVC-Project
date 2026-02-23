namespace Route.DAL.Models.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime? LastModificationOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
