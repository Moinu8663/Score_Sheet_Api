namespace score_sheet.Exceptions
{
    public class ScoreNotFoundException:FormatException
    {
        public ScoreNotFoundException(string massage) : base(massage) { }
    }
}
