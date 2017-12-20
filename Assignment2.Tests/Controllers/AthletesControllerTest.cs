using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2;
using Assignment2.Controllers;
using Moq;
using Assignment2.Models;

namespace Assignment2.Tests.Controllers
{
    /// <summary>
    /// Summary description for AthletesControllerTest
    /// </summary>
    [TestClass]

    public class AthletesControllerTest
    {
        AthletesController controller;
        Mock<IAthletesRepository> mock;
        List<Athlete> athletes;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            mock = new Mock<IAthletesRepository>();

            //Mock the Athlete Data
            athletes = new List<Athlete>
            {
                new Athlete{ Pk_Athlete_Id = 1 ,  Fk_Sport_Id = 1, FullName = "albert calamatta",  Email = "Albert.calamatta@gmail.com", MobileNo = "5198032269"},
                 new Athlete{ Pk_Athlete_Id = 2 ,  Fk_Sport_Id = 2, FullName = "amanda davidson",  Email = "Albert.calamatta@live.com", MobileNo = "5198032269"},
                  new Athlete{ Pk_Athlete_Id = 3,  Fk_Sport_Id = 3, FullName = "corutney bell",  Email = "Albert.calamatta@hotmail.com", MobileNo = "5198032269"}

            };

            // populate the mock object with our sample data 
            mock.Setup(m => m.Athletes).Returns(athletes.AsQueryable());

            //pass the mock to the 2nd constructor
            controller = new AthletesController(mock.Object);
        }

        [TestMethod]
        public void IndexViewLoads()
        {
            // Arrange


            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexReturnsAthletes()
        {
            //Act
            //call index, set result to an Athlete List as specified in Index's Model
            var actual = (List<Athlete>)controller.Index().Model;
            //Assert
            //check if the list returned in the view matches the list we passed in to the mock
            CollectionAssert.AreEqual(athletes, actual);
        }

        [TestMethod]
        public void DetailsValidAthlete()
        {
            //Act
            var actual = (Athlete)controller.Details(1).Model;

            //Assert
            Assert.AreEqual(athletes.ToList()[0], actual);
        }
        [TestMethod]
        public void DetailsInvalidAthlete()
        {
            //Act
            var actual = (Athlete)controller.Details(11111).Model;

            //Assert
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void DetailsInvalidNoId()
        {
            //Arrange 
            int? id = null;

            //Act
            var actual = controller.Details(id);

            //Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]

        public void EditAthleteValid()
        {
            //Act
            var actual = (Athlete)controller.Edit(1).Model;
        }

        [TestMethod]
        public void EditInvalidNoId()
        {
            //arange 
            int? id = null;

            //act 
            var actual = (ViewResult)controller.Edit(id);

            //assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditWithInvalidAthleteId()
        {
            //Act
            ViewResult result = controller.Edit(-314) as ViewResult;

            //Assert
            Assert.AreEqual("Error", result.ViewName);

        }

        [TestMethod]
        public void DeleteValidAthlete()
        {
            //Act 
            var actual = (Athlete)controller.Delete(1).Model;

            //Assert
            Assert.AreEqual(athletes.ToList()[0], actual);
        }

        [TestMethod]
        public void DeleteInvalidAthleteId()
        {
            //Arrange 
            int id = 87656765;

            //Act
            ViewResult actual = (ViewResult)controller.Delete(id);

            //Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidNoId()
        {
            //Arrange
            int? id = null;

            //Act
            ViewResult actual = controller.Delete(id);

            //assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void CreateViewLoads()
        {
            //Act - cast return type as ViewResult
            ViewResult actual = (ViewResult)controller.Create();

            //Assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        //POST: Create
        [TestMethod]
        public void CreateValidAthlete()
        {
            //Arrange
            Athlete athlete = new Athlete
            {
                Pk_Athlete_Id = 4,
                Fk_Sport_Id = 4,
                FullName = "john",
                Email = "albert.calamatta@gmail.com",
                MobileNo = "519-803-2269"
            };

            //Act
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Create(athlete);

            //Assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInvalidAthlete()
        {
            //arrange
            controller.ModelState.AddModelError("key", "errorMessage message");
            Athlete athlete = new Athlete
            {
                Pk_Athlete_Id = 4,
                Fk_Sport_Id = 4,
                FullName = "john",
                Email = "albert.calamatta@gmail.com",
                MobileNo = "519-803-2269"
            };

            //Act - cast return type as ViewResult
            ViewResult actual = (ViewResult)controller.Create(athlete);

            //Assert
            Assert.AreEqual("Create", actual.ViewName);
        }
    

        //POST: Create
        [TestMethod]
        public void EditValidAthlete()
        {
            //Arrange
            Athlete athlete = athletes.ToList()[0]; 
           

            //Act
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(athlete);

            //Assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditInvalidAthlete()
        {
            //arrange
            controller.ModelState.AddModelError("key", "error message");
            Athlete athlete = new Athlete
            {
                Pk_Athlete_Id = 4,
                Fk_Sport_Id = 4,
                FullName = "john",
                Email = "albert.calamatta@gmail.com",
                MobileNo = "519-803-2269"
            };

            //Act - cast return type as ViewResult
            ViewResult actual = (ViewResult)controller.Edit(athlete);

            //Assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        //POST: DeleteConfirmed
        [TestMethod]
        public void DeleteConfirmedValidAthlete()
        {
            //Act 
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            //Assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidAthleteId()
        {
            //Arrange 
            int id = 87656765;

            //Act
            ViewResult actual = (ViewResult)controller.DeleteConfirmed(id);

            //Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidNoId()
        {
            //Arrange
            int? id = null;

            //Act
            ViewResult actual = (ViewResult)controller.DeleteConfirmed(id);

            //assert
            Assert.AreEqual("Error", actual.ViewName);
        }
        [TestMethod]
        public void IndexPass()
        {
            //act
            var actual = (List<Athlete>)controller.Index().Model;

            //assert
            CollectionAssert.AreEqual(athletes, actual);
        }
        public AthletesControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

    }
}
