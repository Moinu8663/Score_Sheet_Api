using score_sheet.Model;
using score_sheet.RedisCache;

namespace score_sheet.Repository
{
    public class Repo:IRepo
    {
        private readonly ScoreContext context;
        private readonly IRedisCache redisCache;
        public Repo(ScoreContext context, IRedisCache redisCache)
        {
            this.context=context;
            this.redisCache=redisCache;
        }

        public void AddScore(ScoreSheet scoresheet)
        {
            context.Add(scoresheet);
            context.SaveChanges();
            redisCache.RemoveData("AllScoreSheets");
        }

        public void DeleteScore(int RollNo)
        {
            var sc = GetScoreByRollNo(RollNo);
            if (sc != null)
            {
                context.scoresheets.Remove(sc);
                context.SaveChanges();

                // When deleting a score, remove any cached data related to scoresheets
                redisCache.RemoveData("AllScoreSheets");
            }
        }

        public List<ScoreSheet> GetAll()
        {
            // Check if data exists in cache
            var cachedData = redisCache.GetData<List<ScoreSheet>>("AllScoreSheets");
            if (cachedData != null)
            {
                return cachedData;
            }

            // If not cached, retrieve data from the database
            var allScoresheets = context.scoresheets.ToList();

            // Cache the data for future use
            redisCache.SetData("AllScoreSheets", allScoresheets, DateTimeOffset.Now.AddDays(1));

            return allScoresheets;
        }

        public ScoreSheet GetResult(ScoreSheet scoresheet)
        {
            var Resutobj = context.scoresheets.Where(u => u.RollNo == scoresheet.RollNo && u.Name == scoresheet.Name).FirstOrDefault();
            return Resutobj;
        }

        public ScoreSheet GetScoreByRollNo(int RollNo)
        {
            // Check if data exists in cache for the specific RollNo
            var cachedScore = redisCache.GetData<ScoreSheet>($"ScoreByRollNo:{RollNo}");
            if (cachedScore != null)
            {
                return cachedScore;
            }

            // If not cached, retrieve data from the database
            var score = context.scoresheets.FirstOrDefault(o => o.RollNo == RollNo);

            // Cache the data for future use
            if (score != null)
            {
                redisCache.SetData($"ScoreByRollNo:{RollNo}", score, DateTimeOffset.Now.AddDays(1));
            }

            return score;
        }

        public void UpdateScore(int RollNo, ScoreSheet scoresheet)
        {
            var sc = GetScoreByRollNo(RollNo);
            if (sc != null)
            {
                sc.Name = scoresheet.Name;
                sc.Marks1 = scoresheet.Marks1;
                sc.Marks2 = scoresheet.Marks2;
                sc.Marks3 = scoresheet.Marks3;
                context.SaveChanges();

                // When updating a score, remove any cached data related to scoresheets
                redisCache.RemoveData("AllScoreSheets");
            }
        }
    }
}
