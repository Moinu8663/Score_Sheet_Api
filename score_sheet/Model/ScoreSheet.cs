using System.ComponentModel.DataAnnotations;

namespace score_sheet.Model
{
    public class ScoreSheet
    {
        [Key]
        public int Id { get; set;}
        public int RollNo { get; set;}
        public string Name { get; set;}
        public int Marks1 { get; set;}
        public int Marks2 { get; set;}
        public int Marks3 { get; set;}
        public int Total_Marks { get { return Marks1+Marks2+Marks3; } set { } }
        public decimal Score { get {return  (100*Total_Marks)/300; } set { } }
        public string Grade
        {
            get
            {
                if(Score<33)
                {
                    return "E";
                }
                else if(Score >= 33 && Score < 45)
                {
                    return "D";
                }
                else if (Score >= 45 && Score < 60)
                {
                    return "C";
                }
                else if (Score >= 60 && Score < 80)
                {
                    return "B";
                }
                else if (Score >= 80 && Score < 90)
                {
                    return "A";
                }
                else
                {
                    return "A+";
                }
            }
            set { }
        }
        public string Result
        {
            get
            {
                bool pass = Score > 33;
                return pass ? "pass" : "fail";
            }
            set { }
        }

    }
}
