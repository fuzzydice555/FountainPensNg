namespace FountainPensNg.Server.Data.DTO {
    public class InkedUpForListDTO {
        public DateTime InkedAt { get; set; }
        public int MatchRating { get; set; }
        public string? PenMaker { get; set; }
        public string? PenName { get; set; }
        public string? InkMaker { get; set; }
        public string? InkName { get; set; }
        public string? PenColor { get; set; }
        public string? InkColor { get; set; }
    }
}