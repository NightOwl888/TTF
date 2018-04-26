using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Ttf.BusinessLayer.Foo;
using Ttf.BusinessLayer.Foo.Dto;
using Ttf.WebApi.Models;

namespace Ttf.WebApi.Controllers
{
    [TestFixture]
    public class TestFooController
    {
        [Test]
        public void Get_ShouldCallMapAndCalculate()
        {
            var mapper = new Mock<IMapper>();
            var fooCalculatorStrategy = new Mock<IFooCalculatorStrategy>();
            var target = new FooController(mapper.Object, fooCalculatorStrategy.Object);
            target.Request = new HttpRequestMessage();
            target.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var request = new FooRequest();

            var response = target.Get(request, "base");

            mapper.Verify(m => m.Map<FooCalculatorRequest>(It.IsAny<object>()));
            fooCalculatorStrategy.Verify(m => m.Calculate(It.IsAny<FooCalculatorRequest>(), "base"));
            mapper.Verify(m => m.Map<FooResult>(It.IsAny<object>()));
        }
    }
}
