using NUnit.Framework;
using System;
using Ttf.BusinessLayer.Foo.Dto;

namespace Ttf.BusinessLayer.Foo
{
    [TestFixture]
    public class FooCalculatorStrategyTest
    {
        [SetUp]
        public void SetUp()
        {
            target = new FooCalculatorStrategy(
                new IFooCalculator[]
                {
                    new BaseFooCalculator(),
                    new Specialized1FooCalculator(),
                    new Specialized2FooCalculator()
                });
        }

        [TearDown]
        public void TearDown()
        {
            target = null;
        }

        private FooCalculatorStrategy target;


        [Test]
        public void TestStrategy_Base_Calculate_A_B_NotC()
        {
            var request = new FooCalculatorRequest
            {
                A = true,
                B = true,
                C = false,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "base");

            Assert.AreEqual(XEnum.S, result.X);
            Assert.AreEqual(112.5, result.Y);
        }

        [Test]
        public void TestStrategy_Base_Calculate_A_B_C()
        {
            var request = new FooCalculatorRequest
            {
                A = true,
                B = true,
                C = true,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "base");

            Assert.AreEqual(XEnum.R, result.X);
            Assert.AreEqual(93.75, result.Y);
        }

        [Test]
        public void TestStrategy_Base_Calculate_NotA_B_C()
        {
            var request = new FooCalculatorRequest
            {
                A = false,
                B = true,
                C = true,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "base");

            Assert.AreEqual(XEnum.T, result.X);
            Assert.AreEqual(56.25, result.Y);
        }


        [Test]
        public void TestStrategy_Specialized1_Calculate_A_B_NotC()
        {
            var request = new FooCalculatorRequest
            {
                A = true,
                B = true,
                C = false,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "specialized1");

            Assert.AreEqual(XEnum.S, result.X);
            Assert.AreEqual(112.5, result.Y);
        }

        [Test]
        public void TestStrategy_Specialized1_Calculate_A_B_C()
        {
            var request = new FooCalculatorRequest
            {
                A = true,
                B = true,
                C = true,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "specialized1");

            Assert.AreEqual(XEnum.R, result.X);
            Assert.AreEqual(187.5, result.Y);
        }

        [Test]
        public void TestStrategy_Specialized1_Calculate_NotA_B_C()
        {
            var request = new FooCalculatorRequest
            {
                A = false,
                B = true,
                C = true,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "specialized1");

            Assert.AreEqual(XEnum.T, result.X);
            Assert.AreEqual(56.25, result.Y);
        }


        [Test]
        public void TestStrategy_Specialized2_Calculate_A_B_NotC()
        {
            var request = new FooCalculatorRequest
            {
                A = true,
                B = true,
                C = false,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "specialized2");

            Assert.AreEqual(XEnum.T, result.X);
            Assert.AreEqual(56.25, result.Y);
        }

        [Test]
        public void TestStrategy_Specialized2_Calculate_A_B_C()
        {
            var request = new FooCalculatorRequest
            {
                A = true,
                B = true,
                C = true,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "specialized2");

            Assert.AreEqual(XEnum.R, result.X);
            Assert.AreEqual(93.75, result.Y);
        }

        [Test]
        public void TestStrategy_Specialized2_Calculate_NotA_B_C()
        {
            var request = new FooCalculatorRequest
            {
                A = false,
                B = true,
                C = true,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "specialized2");

            Assert.AreEqual(XEnum.T, result.X);
            Assert.AreEqual(56.25, result.Y);
        }

        [Test]
        public void TestStrategy_Specialized2_Calculate_A_NotB_C()
        {
            var request = new FooCalculatorRequest
            {
                A = true,
                B = false,
                C = true,

                D = 75,
                E = 50,
                F = 25
            };

            var result = target.Calculate(request, "specialized2");

            Assert.AreEqual(XEnum.S, result.X);
            Assert.AreEqual(137.5, result.Y);
        }

        [Test]
        public void TestStrategy_Invalid()
        {
            var request = new FooCalculatorRequest
            {
                A = true,
                B = false,
                C = true,

                D = 75,
                E = 50,
                F = 25
            };

            Assert.Throws<ArgumentException>(() => target.Calculate(request, "foobar"));
        }
    }
}
