namespace score_sheet.Exceptions
{
    public class ScoreAlreadyExistsException:FormatException
    {
        public ScoreAlreadyExistsException(string massage) : base(massage) { }
    }
}
