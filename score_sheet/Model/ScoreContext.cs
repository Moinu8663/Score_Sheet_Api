using Microsoft.EntityFrameworkCore;

namespace score_sheet.Model
{
    public class ScoreContext:DbContext
    {
        public DbSet<ScoreSheet> scoresheets {  get; set; }
        public ScoreContext(DbContextOptions<ScoreContext> options) : base(options) { }
    }
}
