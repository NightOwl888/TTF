using Ttf.BusinessLayer.Foo.Dto;

namespace Ttf.BusinessLayer.Foo
{
    public interface IFooCalculatorStrategy
    {
        FooCalculatorResult Calculate(FooCalculatorRequest request, string provider);
    }
}
