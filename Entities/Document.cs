namespace OneCore.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public string FileURL { get; set; }
        public string Description { get; set; }
        public string Resume { get; set; }
        public string Feeling { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
