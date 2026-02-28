namespace Web_MZ.Models
{
    public class CreatorPaginationVM
    {
        public List<CreatorCardVM> Creators { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
