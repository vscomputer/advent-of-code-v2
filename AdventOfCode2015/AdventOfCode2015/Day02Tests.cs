using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2015
{
    [TestFixture]
    public class CalculatesWrappingPaperTests
    {
        [Test]
        [TestCase("2x3x4", 58)]
        [TestCase("1x1x10", 43)]
        public void Get_Dimensions_ReturnsRequiredPaper(string input, int expected)
        {
            var subject = new CalculatesWrappingPaper(new GetsDimensions());

            int result = subject.Calculate(input);

            result.Should().Be(expected);
        }

        [Test]
        public void Get_RealInput_GetsTotalDimensions()
        {
            var subject = new CalculatesWrappingPaper(new GetsDimensions());

            var input = File.ReadAllLines(@"C:\Projects\Git\advent-of-code-v2\2015\AdventOfCode2015\Day02Input.txt");
            var result = input.Sum(line => subject.Calculate(line));

            result.Should().Be(1588178);
        }
    }
    
    [TestFixture]
    public class CalculatesRibbonTests
    {
        [Test]
        [TestCase("2x3x4", 10)]
        [TestCase("1x1x10", 4)]
        public void CalculateWrapRibbon_TestInput_ReturnsExpected(string input, int expected)
        {
            var subject = new CalculatesRibbon(new GetsDimensions());

            int result = subject.CalculateWrap(input);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2x3x4", 24)]
        [TestCase("1x1x10", 10)]
        public void CalculateBow_TestInput_ReturnsExpected(string input, int expected)
        {
            var subject = new CalculatesRibbon(new GetsDimensions());

            int result = subject.CalculateBow(input);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2x3x4", 34)]
        [TestCase("1x1x10", 14)]
        public void CalculateTotalRibbon_TestInput_ReturnsExpected(string input, int expected)
        {
            var subject = new CalculatesRibbon(new GetsDimensions());

            int result = subject.CalculateTotalRibbon(input);

            result.Should().Be(expected);
        }

        [Test]
        public void CalculateTotalRibbon_RealInput_ReturnsRealAnswer()
        {
            var subject = new CalculatesRibbon(new GetsDimensions());
            var input = File.ReadAllLines(@"C:\Projects\Git\advent-of-code-v2\2015\AdventOfCode2015\Day02Input.txt");

            int result = input.Sum(line => subject.CalculateTotalRibbon(line));
            
            result.Should().Be(3783758);
        }
    }

    public class CalculatesWrappingPaper
    {
        private readonly GetsDimensions _getsDimensions;

        private int _smallestSide;

        public CalculatesWrappingPaper(GetsDimensions getsDimensions)
        {
            _getsDimensions = getsDimensions;
        }

        public int Calculate(string input)
        {
            _smallestSide = int.MaxValue;
            var dimensions = _getsDimensions.ParseDimensions(input);

            var result = 0;

            result += (2 * CalculateSide(dimensions[0], dimensions[1]));
            result += (2 * CalculateSide(dimensions[1], dimensions[2]));
            result += (2 * CalculateSide(dimensions[2], dimensions[0]));

            result += _smallestSide;

            return result;
        }

        private int CalculateSide(int dimensionX, int dimensionY)
        {
            var currentResult = dimensionX * dimensionY;
            if (currentResult < _smallestSide)
                _smallestSide = currentResult;
            return currentResult;
        }
    }

    

    public class CalculatesRibbon
    {
        private readonly GetsDimensions _getsDimensions;

        public CalculatesRibbon(GetsDimensions getsDimensions)
        {
            _getsDimensions = getsDimensions;
        }

        public int CalculateWrap(string input)
        {
            var highestValue = Int32.MinValue;
            var dimensions = _getsDimensions.ParseDimensions(input).ToList();
            foreach (var dimension in dimensions)
            {
                if (dimension > highestValue)
                {
                    highestValue = dimension;
                }
            }

            dimensions.Remove(highestValue);
            return (dimensions[0] * 2) + (dimensions[1] * 2);
        }

        public int CalculateBow(string input)
        {
            var dimensions = _getsDimensions.ParseDimensions(input).ToList();
            return dimensions[0] * dimensions[1] * dimensions[2];
        }

        public int CalculateTotalRibbon(string input)
        {
            return CalculateWrap(input) + CalculateBow(input);
        }
    }

    public class GetsDimensions
    {
        public int[] ParseDimensions(string input)
        {
            var stringDimensions = input.Split('x');
            var dimensions = new int[3];
            for (var i = 0; i < 3; i++)
            {
                dimensions[i] = int.Parse(stringDimensions[i]);
            }

            return dimensions;
        }
    }
}