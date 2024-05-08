using score_sheet.Model;

namespace score_sheet.Services
{
    public interface Iservice
    {
        void AddScore(ScoreSheet scoresheet);
        public List<ScoreSheet> GetAll();
        public ScoreSheet GetScoreByRollNo(int RollNo);
        public void UpdateScore(int RollNo, ScoreSheet scoresheet);
        public void DeleteScore(int RollNo);
        ScoreSheet GetResult(ScoreSheet scoresheet);
    }
    public interface ITokenGenerator
    {
        string GenerateToken(ScoreSheet scoresheet);
    }
}
