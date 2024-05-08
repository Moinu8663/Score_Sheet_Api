using score_sheet.Model;

namespace score_sheet.Repository
{
    public class Repo:IRepo
    {
        private readonly ScoreContext context;
        public Repo(ScoreContext context)
        {
            this.context=context;
        }

        public void AddScore(ScoreSheet scoresheet)
        {
            context.Add(scoresheet);
            context.SaveChanges();
        }

        public void DeleteScore(int RollNo)
        {
            var sc = GetScoreByRollNo(RollNo);
            context.scoresheets.Remove(sc);
            context.SaveChanges();
        }

        public List<ScoreSheet> GetAll()
        {
            return context.scoresheets.ToList();
        }

        public ScoreSheet GetResult(ScoreSheet scoresheet)
        {
            var Resutobj = context.scoresheets.Where(u => u.RollNo == scoresheet.RollNo && u.Name == scoresheet.Name).FirstOrDefault();
            return Resutobj;
        }

        public ScoreSheet GetScoreByRollNo(int RollNo)
        {
            return context.scoresheets.Where(o => o.RollNo == RollNo).FirstOrDefault();
        }

        public void UpdateScore(int RollNo, ScoreSheet scoresheet)
        {
            var sc = GetScoreByRollNo(RollNo);
            sc.Name = scoresheet.Name;
            sc.Marks1 = scoresheet.Marks1;
            sc.Marks2 = scoresheet.Marks2;
            sc.Marks3 = scoresheet.Marks3;
            context.SaveChanges();
        }
    }
}
