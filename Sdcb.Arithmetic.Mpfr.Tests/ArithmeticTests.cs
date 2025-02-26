﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Sdcb.Arithmetic.Mpfr.Tests
{
    public class ArithmeticTests
    {
        private readonly ITestOutputHelper _console;

        public ArithmeticTests(ITestOutputHelper console)
        {
            _console = console;
        }

        [Fact]
        public void SumTest()
        {
            using MpfrFloat a = MpfrFloat.From(3);
            using MpfrFloat b = MpfrFloat.From(4.25);
            using MpfrFloat c = MpfrFloat.From(-2);

            using MpfrFloat result = MpfrFloat.Sum(new[] { a, b, c });
            Assert.Equal(5.25, result.ToDouble());
        }

        [Fact]
        public void SumInplaceTest()
        {
            using MpfrFloat a = MpfrFloat.From(3);
            using MpfrFloat b = MpfrFloat.From(4.25);
            using MpfrFloat c = MpfrFloat.From(-2);

            using MpfrFloat result = new(precision: 100);
            MpfrFloat.SumInplace(result, new[] { a, b, c });
            Assert.Equal(5.25, result.ToDouble());
        }

        [Fact]
        public void HypotTest()
        {
            using MpfrFloat a = MpfrFloat.From(3);
            using MpfrFloat b = MpfrFloat.From(4);

            using MpfrFloat result = MpfrFloat.Hypot(a, b);
            Assert.Equal(5, result.ToDouble());
        }

        [Fact]
        public void FMMSTest()
        {
            using MpfrFloat a = MpfrFloat.From(3);
            using MpfrFloat b = MpfrFloat.From(4);
            using MpfrFloat c = MpfrFloat.From(5);
            using MpfrFloat d = MpfrFloat.From(6);

            // (3x4) - (5x6) = 12 - 30 = -18
            using MpfrFloat result = MpfrFloat.FMMS(a, b, c, d);
            Assert.Equal(-18, result.ToDouble());
        }

        [Fact]
        public void FactorialTest()
        {
            using MpfrFloat r = MpfrFloat.Factorial(5);
            Assert.Equal(120, r.ToInt32());
        }
    }
}
