namespace Ttf.BusinessLayer.Foo.Dto
{
    /// <summary>
    /// Domain model for a <see cref="IFooCalculator"/> response.
    /// </summary>
    public class FooCalculatorResult
    {
        public XEnum X { get; set; }

        public decimal Y { get; set; }
    }
}
