using AutoMapper;
using Microsoft.Web.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ttf.BusinessLayer.Foo;
using Ttf.BusinessLayer.Foo.Dto;
using Ttf.WebApi.Models;

namespace Ttf.WebApi.Controllers
{
    [ApiVersion("1")]
    public class FooController : ApiController
    {
        private readonly IFooCalculatorStrategy fooCalculatorStrategy;
        private readonly IMapper mapper;

        public FooController(IMapper mapper, IFooCalculatorStrategy fooCalculatorStrategy)
        {
            if (mapper == null)
                throw new ArgumentNullException("mapper");
            if (fooCalculatorStrategy == null)
                throw new ArgumentNullException("fooCalculatorStrategy");

            this.mapper = mapper;
            this.fooCalculatorStrategy = fooCalculatorStrategy;
        }

        [Route("api/v{version:apiVersion}/foo")]
        public HttpResponseMessage Get([FromUri] FooRequest request, [FromUri] string provider = "base")
        {
            if (ModelState.IsValid)
            {
                provider = string.IsNullOrWhiteSpace(provider) ? "base" : provider;

                // FooRequest (view model) -> FooCalculatorRequest (domain model)
                var calculatorRequest = this.mapper.Map<FooCalculatorRequest>(request);

                // Run business logic
                var calculatorResult = this.fooCalculatorStrategy.Calculate(calculatorRequest, provider);

                // FooCalculatorResult (domain model) -> FooResult (view model)
                var result = this.mapper.Map<FooResult>(calculatorResult);

                // Return result as JSON
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
