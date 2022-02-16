namespace Common.Dto.Shared
{
    public class GenericFilter
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortName { get; set; }
        public string SortingDirection { get; set; }
    }
}
