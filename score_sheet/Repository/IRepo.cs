using score_sheet.Model;

namespace score_sheet.Repository
{
    public interface IRepo
    {
        void AddScore(ScoreSheet scoresheet);
        public List<ScoreSheet> GetAll();
        public ScoreSheet GetScoreByRollNo(int RollNo);
        public void UpdateScore(int RollNo, ScoreSheet scoresheet);
        public void DeleteScore(int RollNo);
        ScoreSheet GetResult(ScoreSheet scoresheet);
    }
}
