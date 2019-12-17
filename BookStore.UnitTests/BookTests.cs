using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.WebUI.Controllers;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using BookStore.WebUI.Models;
using BookStore.WebUI.HtmlHelpers;

namespace BookStore.UnitTests
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void can_Paginate()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
                {
                     new Book {ISBN=1,Title="Book1" },
                     new Book {ISBN=2,Title="Book2" },
                     new Book {ISBN=3,Title="Book3" },
                     new Book {ISBN=4,Title="Book4" },
                     new Book {ISBN=5,Title="Book5" }
                }
                );
            BookController controller = new BookController(mock.Object);
            controller.pageSize = 3;

            //Act
            BookListViewModel result = (BookListViewModel)controller.List(null,2).Model;

            //Assert
            Book[] bookArray = result.Books.ToArray();
            Assert.IsTrue(bookArray.Length == 2);
            Assert.AreEqual(bookArray[0].Title, "Book4");
            Assert.AreEqual(bookArray[1].Title, "Book5");

        }

        [TestMethod]
        public void Can_Generate_Page_Link()
        {
            //Arrange
            HtmlHelper myHelper = null;
            PageingInfo pageinfo = new PageingInfo
            {
                CurrentPage = 2,
                TotalItems = 14,
                ItemPerPage = 5

            };
            Func<int, string> pageUrlDelegate = i => "Page"+ i;
            string expectedResult = "<a class=\"btn btn-default\" href=\"Page1\">1</a>"
                                   + "<a class=\"btn btn-default btn-primary selected\"href=\"Page2\">2</a>"
                                   + "<a class=\"btn btn-default\" href=\"Page3\">3</a>";

            //Act
            string result = myHelper.PageLinks(pageinfo, pageUrlDelegate).ToString();


            //Assert
            Assert.AreEqual(expectedResult, result);
            }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
                {
                     new Book {ISBN=1,Title="Book1" },
                     new Book {ISBN=2,Title="Book2" },
                     new Book {ISBN=3,Title="Book3" },
                     new Book {ISBN=4,Title="Book4" },
                     new Book {ISBN=5,Title="Book5" }
                }
                );

            //Act
            BookController controller = new BookController(mock.Object);
            controller.pageSize = 3;
            //Assert
            BookListViewModel result = (BookListViewModel)controller.List(null,2).Model;
            PageingInfo pageinfo = result.paginginfo;
            Assert.AreEqual(pageinfo.CurrentPage,2);
            Assert.AreEqual(pageinfo.ItemPerPage, 3);
            Assert.AreEqual(pageinfo.TotalItems, 5);
            Assert.AreEqual(pageinfo.TotalPages, 2);


        }



        [TestMethod]
        public void Can_Filter_Books()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
                {
                     new Book {ISBN=1,Title="Book1",Specialization="IT" },
                     new Book {ISBN=2,Title="Book2",Specialization="IS" },
                     new Book {ISBN=3,Title="Book3",Specialization="IS" },
                     new Book {ISBN=4,Title="Book4",Specialization="IS"},
                     new Book {ISBN=5,Title="MIS",Specialization="IS" }
                }
                );
            BookController controller = new BookController(mock.Object);
            controller.pageSize = 3;

            //Act
            Book[] result = ((BookListViewModel)controller.List("IS", 2).Model).Books.ToArray();
            //Assert
            
            Assert.AreEqual(result.Length,1);
            Assert.IsTrue(result[0].Title=="MIS"&&result[0].Specialization=="IS");
          
        }



        [TestMethod]
        public void Can_Create_Specialization()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
                {
                     new Book {ISBN=1,Title="Book1",Specialization="IT" },
                     new Book {ISBN=2,Title="Book2",Specialization="IS" },
                     new Book {ISBN=3,Title="Book3",Specialization="IS" },
                     new Book {ISBN=4,Title="Book4",Specialization="IS"},
                     new Book {ISBN=5,Title="MIS",Specialization="IS" }
                }
                );
            NavController controller = new NavController(mock.Object);
          

            //Act
            string[] result = ((IEnumerable<string>)controller.Menu(null).Model).ToArray();
            //Assert

            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0]== "IT" && result[1]== "IS");

        }



        [TestMethod]
        public void Can_Indicates_Selected_Spec()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
                {
                     new Book {ISBN=1,Title="Book1",Specialization="IT" },
                     new Book {ISBN=2,Title="Book2",Specialization="IS" },
                     new Book {ISBN=3,Title="Book3",Specialization="IS" },
                     new Book {ISBN=4,Title="Book4",Specialization="IS"},
                     new Book {ISBN=5,Title="MIS",Specialization="IS" }
                }
                );
            NavController controller = new NavController(mock.Object);


            //Act
            string result = controller.Menu("IS").ViewBag.SelectedSpec;
            //Assert

            Assert.AreEqual("IS",result);
            

        }


        [TestMethod]
        public void Can_Generate_Spec_Specific_Book_Count()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
                {
                     new Book {ISBN=1,Title="Book1",Specialization="IT" },
                     new Book {ISBN=2,Title="Book2",Specialization="IS" },
                     new Book {ISBN=3,Title="Book3",Specialization="IS" },
                     new Book {ISBN=4,Title="Book4",Specialization="IS"},
                     new Book {ISBN=5,Title="MIS",Specialization="IS" }
                }
                );
            BookController controller = new BookController(mock.Object);


            //Act
           int result1 = ((BookListViewModel)controller.List("IS").Model).paginginfo.TotalItems;
            int result2 = ((BookListViewModel)controller.List("IT").Model).paginginfo.TotalItems;
            //Assert

            Assert.AreEqual(result1,4);
            Assert.AreEqual(result2, 1);


        }
    }

    }
