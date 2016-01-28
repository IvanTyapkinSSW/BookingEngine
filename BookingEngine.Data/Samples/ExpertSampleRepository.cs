using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookingEngine.Data.Entities;

namespace BookingEngine.Data
{
    public class ExpertSampleRepository : IExpertRepository
    {
        List<Expert> experts = new List<Expert>();
        public ExpertSampleRepository()
        {

            Expert adamC = new Expert()
            {
                ExpertId = 1,
                Code = "ADAMC",
                FirstName = "Adam",
                LastName = "Cogan",
                DefaultRate = 265,
                Description = "Adam Cogan is the Chief Architect at SSW, a Microsoft Certified Gold Partner specializing in custom .NET, SharePoint and CRM solutions (with a splash of Business Intelligence).&emsp;At SSW, Adam has been developing custom solutions for businesses across a range of industries such as Government, engineering, banking, insurance and manufacturing since 1990 for clients such as Microsoft, Worley Parsons and Aurecon.",
                ImageUrl = "AdamCogan.png"
            };
            experts.Add(adamC);

            Expert troyH = new Expert()
            {
                ExpertId = 1,
                Code = "TROYH",
                FirstName = "Troy",
                LastName = "Hunt",
                DefaultRate = 500,
                Description = "Troy is a software architect and Microsoft Most Valued Professional (MVP) focusing on security concepts and process improvement in software delivery within a large enterprise environment.&emsp; His specialties include C# ASP.Net, SQL Server, SOA, SharePoint, Security and Continuous Integration",
                ImageUrl = "TroyHunt.png"
            };
            experts.Add(troyH);

            Expert adamS = new Expert()
            {
                ExpertId = 1,
                Code = "ADAMS",
                FirstName = "Adam",
                LastName = "Stephensen",
                DefaultRate = 265,
                Description = "Adam Stephensen is a Solution Architect at SSW with a decade of experience performing needs analysis, designing and building scalable, database-driven, distributed enterprise solutions incorporating web and windows interfaces.",
                ImageUrl = "AdamStephensen.png"
            };
            experts.Add(adamS);

            Expert davidB = new Expert()
            {
                ExpertId = 1,
                Code = "DAVIDB",
                FirstName = "David",
                LastName = "Burela",
                DefaultRate = 265,
                Description = "David is a Solution Architect & Microsoft Azure MVP who runs our Melbourne SSW Office. He has been working with.Net for 10 + years, with 7 years experience as a consultant.In that time he has worked with the entire.Net stack but focus most of his attention on cloud development & smart client technologies (Desktop & native mobile).  David is heavily involved in the developer community and regularly speaks at conferences, and also helps organise community events.",
                ImageUrl = "DavidBurela.png"
            };
            experts.Add(davidB);
        }

        public void Delete(Expert o)
        {
            throw new NotImplementedException();
        }

        public List<Expert> Get(Expression<Func<Expert, bool>> where)
        {
            var query = experts.AsQueryable();

            if (where != null)
            {
                query = query.Where(where);
            }

            return query.ToList();
        }

        public List<Expert> Get(Expression<Func<Expert, bool>> where, string include)
        {
            var query = experts.AsQueryable();

            if (where != null)
            {
                query = query.Where(where);
            }
            return query.ToList();
        }

        public List<Expert> Get(Expression<Func<Expert, bool>> where, Func<IQueryable<Expert>, IOrderedQueryable<Expert>> orderBy, string include)
        {
            var query = experts.AsQueryable();

            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

        public void Insert(Expert o)
        {
            throw new NotImplementedException();
        }

        public void Update(Expert o)
        {
            throw new NotImplementedException();
        }
    }
}
