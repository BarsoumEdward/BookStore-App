using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.Domain.Concrete;

using System.Configuration;
using static BookStore.Domain.Concrete.EmailSetting;
using BookStore.WebUI.Infrastrusture.Abstract;
using BookStore.WebUI.Infrastrusture.Concrete;

namespace BookStore.WebUI.Infrastrusture
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelparam)
        {
            kernel = kernelparam;
            AddBindings();
        }
        private void AddBindings()
        {
            //Mock<IBookRepository> mock = new Mock<IBookRepository>();
            //mock.Setup(b => b.Books).Returns(
            //    new List<Book> {new Book {Title="C# Programming",Price=500m },
            //                    new Book {Title="PHP Programming",Price=700.5m },
            //                    new Book {Title="ASP.NET Programming",Price=599.99m },
            //                    new Book {Title="JAVA Programming",Price=800m },
            //    }
            //    );


            EmailSetting emailSetting = new EmailSetting
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            }; 

            kernel.Bind<IBookRepository>().To<EFBookRepository>();
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("setting",emailSetting);
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}