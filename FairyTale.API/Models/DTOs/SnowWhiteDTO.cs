namespace FairyTale.API.Models.DTOs
{
    public class SnowWhiteDTO
    {
        public string FullName { get; set; }

        public IEnumerable<DwarfDTO> Dwarfs { get; set;}
    }
}
