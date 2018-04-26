using Ttf.BusinessLayer.Foo.Dto;

namespace Ttf.BusinessLayer.Foo
{
    public interface IFooCalculator
    {
        FooCalculatorResult Calculate(FooCalculatorRequest request);

        bool AppliesTo(string provider);
    }
}
