﻿using NUnit.Framework;
using Ttf.BusinessLayer.Foo.Dto;

namespace Ttf.BusinessLayer.Foo
{
    [TestFixture]
    public class Specialized2FooCalculatorTest
    {
        [SetUp]
        public void SetUp()
        {
            target = new Specialized2FooCalculator();
        }

        [TearDown]
        public void TearDown()
        {
            target = null;
        }

        private BaseFooCalculator target;


        [Test]
        public void TestAppliesTo()
        {
            Assert.IsTrue(target.AppliesTo("Specialized2"));
            Assert.IsTrue(target.AppliesTo("specialized2"));
            Assert.IsTrue(target.AppliesTo("SPECIALIZED2"));
            Assert.IsFalse(target.AppliesTo("foobar"));
        }

        [Test]
        public void TestCalculate_A_B_NotC()
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

            var result = target.Calculate(request);

            Assert.AreEqual(XEnum.T, result.X);
            Assert.AreEqual(56.25, result.Y);
        }

        [Test]
        public void TestCalculate_A_B_C()
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

            var result = target.Calculate(request);

            Assert.AreEqual(XEnum.R, result.X);
            Assert.AreEqual(93.75, result.Y);
        }

        [Test]
        public void TestCalculate_NotA_B_C()
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

            var result = target.Calculate(request);

            Assert.AreEqual(XEnum.T, result.X);
            Assert.AreEqual(56.25, result.Y);
        }

        [Test]
        public void TestCalculate_A_NotB_C()
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

            var result = target.Calculate(request);

            Assert.AreEqual(XEnum.S, result.X);
            Assert.AreEqual(137.5, result.Y);
        }

        [Test]
        public void TestInvalidOptions()
        {
            var requests = new FooCalculatorRequest[]
            {
                new FooCalculatorRequest { A = false, B = false, C = false },
                new FooCalculatorRequest { A = true, B = false, C = false },
                new FooCalculatorRequest { A = false, B = true, C = false },
                //new FooCalculatorRequest { A = true, B = true, C = false }, // valid
                //new FooCalculatorRequest { A = false, B = true, C = true }, // valid
                //new FooCalculatorRequest { A = true, B = true, C = true }, // valid
                new FooCalculatorRequest { A = false, B = false, C = true },
                //new FooCalculatorRequest { A = true, B = false, C = true }, // valid
            };

            foreach (var request in requests)
            {
                Assert.Throws<InvalidOptionException>(() => target.Calculate(request));
            }
        }
    }
}
