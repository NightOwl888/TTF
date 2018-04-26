namespace Ttf.BusinessLayer.Foo
{
    /// <summary>
    /// Specialized calculator. Overrides <see cref="XEnum.R"/> formula from <see cref="BaseFooCalculator"/>.
    /// </summary>
    public class Specialized1FooCalculator : BaseFooCalculator
    {
        public Specialized1FooCalculator()
        {
            base.rFormula = (d, e, f) => (2 * d) + (d * e / 100);
        }
    }
}
