using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using score_sheet.Exceptions;
using score_sheet.Model;
using score_sheet.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace score_sheet.Services
{
    public class Service:Iservice
    {
        private readonly IRepo repo;
        public Service(IRepo repo)
        {
            this.repo=repo;
        }

        public void AddScore(ScoreSheet scoresheet)
        {
            if (scoresheet != null)
            {
                repo.AddScore(scoresheet);
            }
            else
            {
                throw new ScoreAlreadyExistsException("ScoreSheet already exists");
            }
        }

        public void DeleteScore(int RollNo)
        {  
            var us = GetScoreByRollNo(RollNo);
            if (us == null)
            {
                throw new ScoreNotFoundException($"ScoreSheet with RollNo {RollNo} not found");
            }
            else
            {
                repo.DeleteScore(RollNo);
            }
        }

        public List<ScoreSheet> GetAll()
        {
            return repo.GetAll();
        }

        public ScoreSheet GetResult(ScoreSheet scoresheet)
        {
            
            if (scoresheet != null)
            {
                return repo.GetResult(scoresheet);
            }
            else
            {
                throw new ScoreNotFoundException($"GetResult Not Found");
            }
        }

        public ScoreSheet GetScoreByRollNo(int RollNo)
        {
            if (RollNo != null)
            {
                return repo.GetScoreByRollNo(RollNo);

            }
            else
            {
                throw new ScoreNotFoundException($"ScoreSheet with RollNo {RollNo} not found");
            }
        }

        public void UpdateScore(int RollNo, ScoreSheet scoresheet)
        {
            
            var us = GetScoreByRollNo(RollNo);
            if (us == null)
            {
                throw new ScoreNotFoundException($"ScoreSheet with RollNo {RollNo} not found");
            }
            else
            {
                repo.UpdateScore(RollNo, scoresheet);
            }
        }
    }
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration configuration;
        public string GenerateToken(ScoreSheet scoresheet)
        {
            //1. Create the claims
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Role,$"{scoresheet.RollNo}"),
            }; //payload

            //2. Create ur secret key, and also the Hashing Algorithm (Signing Credentials)
            var secret = "MoinuddinshaikhmainprojectAddScoreSheet";
            var key = Encoding.UTF8.GetBytes(secret);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            //3. Create the token
            var token = new JwtSecurityToken(
                issuer: "authapiMoinuddin",
                audience: "userapi",
                claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(30)
                );

            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return JsonConvert.SerializeObject(response);
        }
    }
}
