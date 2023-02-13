using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Sdcb.Arithmetic.Gmp.Tests
{
    public class GmpRationalArithmeticTests
    {
        private readonly ITestOutputHelper _console;

        public GmpRationalArithmeticTests(ITestOutputHelper console)
        {
            _console = console;
        }

        [Theory]
        [InlineData("-1/3", "1/2", "1/6")]
        [InlineData("-1/3000000000000000000000000000000000000000000", "1/3000000000000000000000000000000000000000000", "0")]
        public void AddInplaceTest(string op1String, string op2String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational op2 = GmpRational.Parse(op2String);
            using GmpRational result = new GmpRational();

            GmpRational.AddInplace(result, op1, op2);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("-1/3", "1/2", "1/6")]
        [InlineData("-1/3000000000000000000000000000000000000000000", "1/3000000000000000000000000000000000000000000", "0")]
        public void AddStaticTest(string op1String, string op2String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational op2 = GmpRational.Parse(op2String);
            using GmpRational result = GmpRational.Add(op1, op2);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("-1/3", "1/2", "1/6")]
        [InlineData("-1/3000000000000000000000000000000000000000000", "1/3000000000000000000000000000000000000000000", "0")]
        public void AddTest(string op1String, string op2String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational op2 = GmpRational.Parse(op2String);
            using GmpRational result = op1 + op2;
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("-4/6", "3/5", "-38/30")]
        public void SubtractInplaceTest(string op1String, string op2String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational op2 = GmpRational.Parse(op2String);
            using GmpRational result = new GmpRational();
            using GmpRational expectedResult = GmpRational.Parse(expected);

            GmpRational.SubtractInplace(result, op1, op2);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("-1/3", "1/2", "-5/6")]
        public void SubtractStaticTest(string op1String, string op2String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational op2 = GmpRational.Parse(op2String);
            using GmpRational result = GmpRational.Subtract(op1, op2);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("7/8", "1/2", "3/8")]
        public void SubtractTest(string op1String, string op2String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational op2 = GmpRational.Parse(op2String);
            using GmpRational result = op1 - op2;
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData(6.899, 18.88, 130.25312)]
        public void MultiplyInplaceTest(double op1Double, double op2Double, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational op2 = GmpRational.From(op2Double);
            using GmpRational result = new GmpRational();
            using GmpRational expectedResult = GmpRational.From(expected);

            GmpRational.MultiplyInplace(result, op1, op2);
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble(), 13);
        }

        [Theory]
        [InlineData(-82, 8.932, -732.424)]
        public void MultiplyStaticTest(double op1Double, double op2Double, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational op2 = GmpRational.From(op2Double);
            using GmpRational result = GmpRational.Multiply(op1, op2);
            Assert.Equal(expected, result.ToDouble());
        }

        [Theory]
        [InlineData(5.55555, 6.66666, 37.036962963)]
        public void MultiplyTest(double op1Double, double op2Double, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational op2 = GmpRational.From(op2Double);
            using GmpRational result = op1 * op2;
            Assert.Equal(expected, result.ToDouble());
        }

        [Theory]
        [InlineData(5.5, 2, 22)]
        public void Multiply2ExpInplaceTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational result = new GmpRational();
            using GmpRational expectedResult = GmpRational.From(expected);

            GmpRational.Multiply2ExpInplace(result, op1, op2Uint);
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble());
        }

        [Theory]
        [InlineData(2, 2, 8)]
        public void Multiply2ExpStaticTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational expectedResult = GmpRational.From(expected);
            using GmpRational result = GmpRational.Multiply2Exp(op1, op2Uint);
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble());
        }

        [Theory]
        [InlineData(9, 3, 72)]
        public void Multiply2ExpTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational expectedResult = GmpRational.From(expected);
            using GmpRational result = op1 << op2Uint;
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble());
        }

        [Theory]
        [InlineData(5.6, 2, 2.8)]
        public void DivideInplaceTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational op2 = GmpRational.From(op2Uint);
            using GmpRational result = new GmpRational();

            GmpRational.DivideInplace(result, op1, op2);
            Assert.Equal(expected, result.ToDouble());
        }

        [Theory]
        [InlineData(6.888, 3, 2.296)]
        public void DivideStaticTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational op2 = GmpRational.From(op2Uint);
            using GmpRational result = GmpRational.Divide(op1, op2);
            Assert.Equal(expected, result.ToDouble());
        }

        [Theory]
        [InlineData(11.1111, 6, 1.85185)]
        public void DivideTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational op2 = GmpRational.From(op2Uint);
            using GmpRational result = op1 / op2;
            Assert.Equal(expected, result.ToDouble());
        }

        [Theory]
        [InlineData(-9, 2, -2.25)]
        public void Divide2ExpInplaceTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational result = new GmpRational();
            using GmpRational expectedResult = GmpRational.From(expected);

            GmpRational.Divide2ExpInplace(result, op1, op2Uint);
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble());
        }

        [Theory]
        [InlineData(9, 2, 2.25)]
        public void Divide2ExpStaticTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational expectedResult = GmpRational.From(expected);
            using GmpRational result = GmpRational.Divide2Exp(op1, op2Uint);
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble());
        }

        [Theory]
        [InlineData(5, 2, 1.25)]
        public void Divide2ExpTest(double op1Double, uint op2Uint, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational expectedResult = GmpRational.From(expected);
            using GmpRational result = op1 >> op2Uint;
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble());
        }

        [Theory]
        [InlineData(8.55532, -8.55532)]
        public void NegateInplaceTest(double op1Double, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational result = new GmpRational();
            using GmpRational expectedResult = GmpRational.From(expected);

            GmpRational.NegateInplace(result, op1);
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble());
        }

        [Theory]
        [InlineData(-2.22222132, 2.22222132)]
        public void NegateStaticTest(double op1Double, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);            
            using GmpRational expectedResult = GmpRational.From(expected);
            using GmpRational result = GmpRational.Negate(op1);
            Assert.Equal(expectedResult.ToDouble(), result.ToDouble());
        }

        [Theory]
        [InlineData("1/3", "-1/3")]
        public void NegateTest(string op1String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational result = -(op1);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData(-32423.1234, 32423.1234)]
        public void AbsInplaceTest(double op1Double, double expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational result = new GmpRational();
            GmpRational.AbsInplace(result, op1);
            Assert.Equal(expected, result.ToDouble());
        }

        [Theory]
        [InlineData("-4/6", "4/6")]
        public void AbsTest(string op1String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational result = GmpRational.Abs(op1);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("3/42323", "42323/3")]
        public void InvertInplaceTest(string op1String, string expected)
        {
            using GmpRational op1 = GmpRational.Parse(op1String);
            using GmpRational result = new GmpRational();
            GmpRational.InvertInplace(result, op1);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData(-324234, "-1/324234")]
        public void InvertTest(double op1Double, string expected)
        {
            using GmpRational op1 = GmpRational.From(op1Double);
            using GmpRational result = GmpRational.Invert(op1);
            Assert.Equal(expected, result.ToString());
        }
    }
}
