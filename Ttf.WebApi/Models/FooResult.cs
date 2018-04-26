using Ttf.BusinessLayer.Foo;

namespace Ttf.WebApi.Models
{
    /// <summary>
    /// View model for a <see cref="Ttf.BusinessLayer.Foo.IFooCalculator"/> response.
    /// </summary>
    public class FooResult
    {
        public XEnum X { get; set; }

        public decimal Y { get; set; }
    }
}