using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using BookingEngine.Data.Entities;
using BookingEngine.Data;
using BookingEngine.Web.Services;

namespace BookingEngine.Tests
{
    [TestClass]
    public class NonQueryTests
    {
        [TestMethod]
        public void PresenterServiceGetPresenterTest()
        {
            var list = new List<Expert>();
            list.Add(new Expert() { ExpertId = 1, FirstName = "Troy", LastName = "Hunt", Description = "Long description" });
            list.Add(new Expert() { ExpertId = 2, FirstName = "Adam", LastName = "Cogan", Description = "Long description" });
            list.Add(new Expert() { ExpertId = 3, FirstName = "Adam", LastName = "Stephensen", Description = "Long description" });

            var data = list.AsQueryable();

            var mockSet = new Mock<DbSet<Expert>>();

            mockSet.As<IQueryable<Expert>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<BookingEngineContext>();
            mockContext.Setup(n => n.Experts).Returns(mockSet.Object);

            var mockUnitOfWork = new Mock<DbUnitOfWork>(mockContext);

            var mockFactory = new Mock<IUnitOfWorkFactory>();
            mockFactory.Setup(n => n.Create()).Returns(mockUnitOfWork.Object);

            ExpertQryService svc = new ExpertQryService(mockFactory.Object);
            var model = svc.GetExpert("");

            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("Troy", model.FirstName);
            Assert.AreEqual("Hunt", model.LastName);
            Assert.AreEqual("Long description", model.Description);

            var model2 = svc.GetExpert("");
            Assert.IsNull(model2);
        }

        [TestMethod]
        public void IntegerationTest()
        {

            DbUnitOfWorkFactory factory = new DbUnitOfWorkFactory();
            var context = factory.Create();
            var p = context.Experts.Get(n => n.ExpertId == 1).FirstOrDefault();
            if (p != null)
            {
                context.Experts.Delete(p);
            }
            context.Experts.Insert(new Expert { ExpertId = 1, FirstName = "Ivan", LastName = "Tyapkin", Description = "Test", DefaultRate = 225 });
            context.SaveChanges();

            var p2 = context.Experts.Get(n => n.ExpertId == 1).FirstOrDefault();
     
            Assert.IsNotNull(p2);
        }

    }
}
