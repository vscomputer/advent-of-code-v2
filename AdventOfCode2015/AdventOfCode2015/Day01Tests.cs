using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2015
{
    [TestFixture]
    public class FindsFloorTests
    {
        [Test]
        [TestCase("(())", 0)]
        [TestCase("()()", 0)]
        [TestCase("(((", 3)]
        [TestCase("))(((((", 3)]
        [TestCase("())", -1)]
        [TestCase("))(", -1)]
        [TestCase(")))", -3)]
        public void Find_Parens_FindsCorrectFloor(string input, int expected)
        {
            var subject = new FindsFloor();
            
            var result = subject.Find(input);

            result.Should().Be(expected);
        }

        [Test]
        public void Find_RealInput_FindsCorrectFloor()
        {
            var subject = new FindsFloor();
            var input = File.ReadAllText(
                "C:\\Projects\\Git\\advent-of-code-v2\\2015\\AdventOfCode2015\\Day01Input.txt");

            var result = subject.Find(input);

            result.Should().Be((74));
        }

        [Test]
        public void Find_RealInput_FindsMinusOneIteration()
        {
            var subject = new FindsFloor();
            var input = File.ReadAllText(
                "C:\\Projects\\Git\\advent-of-code-v2\\2015\\AdventOfCode2015\\Day01Input.txt");

            var result = subject.Find(input, true);

            result.Should().Be((1795));
        }
    }

    public class FindsFloor
    {
        private int Result { get; set; }
        private int Iterations { get; set; }
        
        public int Find(string input, bool findMinusOneIteration = false)
        {
            
            foreach (var paren in input)
            {
                switch (paren)
                {
                    case '(':
                        Result++;
                        break;
                    case ')':
                        Result--;
                        break;
                }

                Iterations++;
                if (Result == -1 && findMinusOneIteration)
                {
                    return Iterations;
                }
            }
            
            return Result;
        }
    }
}