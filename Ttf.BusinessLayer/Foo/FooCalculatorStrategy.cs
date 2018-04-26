using System;
using System.Linq;
using Ttf.BusinessLayer.Foo.Dto;

namespace Ttf.BusinessLayer.Foo
{
    /// <summary>
    /// Implementation of Strategy pattern to swap calculation formulas based on <paramref name="provider"/>.
    /// <para/>
    /// New <see cref="IFooCalculator"/> instances can be added via dependency injection without 
    /// changing the design of <see cref="FooCalculatorStrategy"/> or any
    /// existing <see cref="IFooCalculator"/> implementations.
    /// </summary>
    public class FooCalculatorStrategy : IFooCalculatorStrategy
    {
        private readonly IFooCalculator[] calculators;

        public FooCalculatorStrategy(IFooCalculator[] calculators)
        {
            if (calculators == null)
                throw new ArgumentNullException("calculators");
            this.calculators = calculators;
        }

        public FooCalculatorResult Calculate(FooCalculatorRequest request, string provider)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (string.IsNullOrWhiteSpace(provider))
            {
                throw new ArgumentNullException("provider");
            }

            var calculator = calculators.Where(x => x.AppliesTo(provider)).FirstOrDefault();

            if (calculator == null)
            {
                throw new ArgumentException("Provider '" + provider + "' is not valid or is not registered.");
            }

            return calculator.Calculate(request);
        }
    }
}
