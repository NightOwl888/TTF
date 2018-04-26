using Ttf.BusinessLayer.Foo.Dto;

namespace Ttf.BusinessLayer.Foo
{
    /// <summary>
    /// Specialized calculator. Overrides <see cref="XEnum.S"/> formula from <see cref="BaseFooCalculator"/> 
    /// and overrides <see cref="GetX(FooCalculatorRequest)"/> to provide new mapping for
    /// <see cref="XEnum.T"/> and <see cref="XEnum.S"/>.
    /// </summary>
    public class Specialized2FooCalculator : BaseFooCalculator
    {
        public Specialized2FooCalculator()
        {
            base.sFormula = (d, e, f) => f + d + (d * e / 100);
        }

        protected override XEnum GetX(FooCalculatorRequest request)
        {
            if (request.A && request.B && !request.C)
            {
                return XEnum.T;
            }
            else if (request.A && !request.B && request.C)
            {
                return XEnum.S;
            }

            return base.GetX(request);
        }
    }
}
