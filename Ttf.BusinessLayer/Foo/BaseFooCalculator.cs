using System;
using Ttf.BusinessLayer.Foo.Dto;

namespace Ttf.BusinessLayer.Foo
{
    /// <summary>
    /// Base class for our calculator. This class can be subclassed as necessary to customize behavior.
    /// </summary>
    public class BaseFooCalculator : IFooCalculator
    {
        // Define our formulas. These can be overridden by subclasses as necessary.
        protected Func<decimal, decimal, decimal, decimal> sFormula = (d, e, f) => d + (d * e / 100);
        protected Func<decimal, decimal, decimal, decimal> rFormula = (d, e, f) => d + (d * (e - f) / 100);
        protected Func<decimal, decimal, decimal, decimal> tFormula = (d, e, f) => d - (d * f / 100);

        private readonly string provider;

        public BaseFooCalculator()
        {
            // Load our provider name via Reflection.
            // Note that subclasses will get their own name, not the base controller,
            // but they must follow the correct naming convention for this to work.
            this.provider = this.GetType().Name.Replace("FooCalculator", string.Empty);
        }

        public virtual bool AppliesTo(string provider)
        {
            return this.provider.Equals(provider, StringComparison.OrdinalIgnoreCase);
        }

        public virtual FooCalculatorResult Calculate(FooCalculatorRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var x = GetX(request);
            return new FooCalculatorResult
            {
                X = x,
                Y = GetY(x, request)
            };
        }

        protected virtual XEnum GetX(FooCalculatorRequest request)
        {
            if (request.A && request.B && !request.C)
            {
                return XEnum.S;
            }
            else if (request.A && request.B && request.C)
            {
                return XEnum.R;
            }
            else if (!request.A && request.B && request.C)
            {
                return XEnum.T;
            }

            throw new InvalidOptionException();
        }

        protected virtual decimal GetY(XEnum x, FooCalculatorRequest request)
        {
            // NOTE: We are purposely casting int > decimal in these formulas
            // so the correct results are calculated.
            switch (x)
            {
                case XEnum.S:
                    return sFormula(request.D, request.E, request.F);

                case XEnum.R:
                    return rFormula(request.D, request.E, request.F);

                case XEnum.T:
                    return tFormula(request.D, request.E, request.F); 
            }

            throw new InvalidOptionException();
        }
    }
}
