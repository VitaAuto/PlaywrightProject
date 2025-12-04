using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using System.Numerics;

namespace PlaywrightProject.UnitTests
{
    // 1. MathUtils: universal sum for any numeric types
    public static class MathUtils
    {
        public static T Sum<T>(params T[] numbers) where T : INumber<T>
        {
            if (numbers == null || numbers.Length == 0) return T.Zero;
            T sum = T.Zero;
            foreach (var n in numbers)
                sum += n;
            return sum;
        }
    }

    // 2. TupleUtils: min, max, average (tuple of 3 elements)
    public static class TupleUtils
    {
        public static (T Min, T Max, double Average) GetStats<T>(IEnumerable<T> numbers) where T : INumber<T>
        {
            if (numbers == null || !numbers.Any())
                throw new ArgumentException("Collection is empty");

            var min = numbers.Min();
            var max = numbers.Max();
            var avg = numbers.Select(x => double.CreateChecked(x)).Average();
            return (min, max, avg);
        }
    }

    // 3. GenericUtils: universal filter by predicate
    public static class GenericUtils
    {
        public static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> predicate)
            => source.Where(predicate);
    }

    // 4. ProductUtils: working with records and generics
    public record Product<T>(string Name, T Price) where T : INumber<T>;

    public static class ProductUtils
    {
        public static IEnumerable<Product<T>> FilterByMinPrice<T>(IEnumerable<Product<T>> products, T minPrice) where T : INumber<T>
            => products.Where(p => p.Price >= minPrice);

        public static (Product<T> Cheapest, Product<T> MostExpensive, double AveragePrice) GetProductStats<T>(IEnumerable<Product<T>> products) where T : INumber<T>
        {
            if (products == null || !products.Any())
                throw new ArgumentException("No products provided");

            var cheapest = products.OrderBy(p => p.Price).First();
            var expensive = products.OrderByDescending(p => p.Price).First();
            var avg = products.Select(p => double.CreateChecked(p.Price)).Average();
            return (cheapest, expensive, avg);
        }
    }

    // ======================= TESTS =======================

    [TestFixture]
    public class UtilsTests
    {
        // MathUtils.Sum (int)
        [TestCase(new int[] { 1, 2, 3, 4 }, 10)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { -1, 1 }, 0)]
        [TestCase(new int[] { 100 }, 100)]
        public void Sum_Int_ShouldReturnCorrectResult(int[] numbers, int expected)
        {
            MathUtils.Sum(numbers).Should().Be(expected);
        }

        // MathUtils.Sum (double)
        public static IEnumerable<TestCaseData> DoubleSumCases
        {
            get
            {
                yield return new TestCaseData(new double[] { 1.5, 2.5, 3.0 }, 7.0);
                yield return new TestCaseData(new double[] { }, 0.0);
                yield return new TestCaseData(new double[] { -1.0, 1.0 }, 0.0);
                yield return new TestCaseData(new double[] { 100.5 }, 100.5);
            }
        }

        [Test, TestCaseSource(nameof(DoubleSumCases))]
        public void Sum_Double_ShouldReturnCorrectResult(double[] numbers, double expected)
        {
            MathUtils.Sum(numbers).Should().BeApproximately(expected, 0.0001);
        }

        // MathUtils.Sum (decimal)
        public static IEnumerable<TestCaseData> DecimalSumCases
        {
            get
            {
                yield return new TestCaseData(new decimal[] { 1.1m, 2.2m, 3.3m }, 6.6m);
                yield return new TestCaseData(new decimal[] { }, 0.0m);
                yield return new TestCaseData(new decimal[] { -1.1m, 1.1m },
0.0m);
                yield return new TestCaseData(new decimal[] { 100.1m }, 100.1m);
            }
        }

        [Test, TestCaseSource(nameof(DecimalSumCases))]
        public void Sum_Decimal_ShouldReturnCorrectResult(decimal[] numbers, decimal expected)
        {
            MathUtils.Sum(numbers).Should().BeApproximately(expected, 0.0001m);
        }

        // TupleUtils.GetStats (int)
        [TestCase(new int[] { 5, 2, 8, 1, 9 }, 1, 9, 5.0)]
        [TestCase(new int[] { 10, 10, 10 }, 10, 10, 10.0)]
        public void GetStats_Int_ShouldReturnMinMaxAverage(int[] input, int expectedMin, int expectedMax, double expectedAvg)
        {
            var (min, max, avg) = TupleUtils.GetStats(input);
            min.Should().Be(expectedMin);
            max.Should().Be(expectedMax);
            avg.Should().BeApproximately(expectedAvg, 0.0001);
        }

        // TupleUtils.GetStats (double)
        public static IEnumerable<TestCaseData> DoubleStatsCases
        {
            get
            {
                yield return new TestCaseData(new double[] { 1.5, 2.5, 3.5 }, 1.5, 3.5, 2.5);
                yield return new TestCaseData(new double[] { 2.0, 2.0, 2.0 }, 2.0, 2.0, 2.0);
            }
        }

        [Test, TestCaseSource(nameof(DoubleStatsCases))]
        public void GetStats_Double_ShouldReturnMinMaxAverage(double[] input, double expectedMin, double expectedMax, double expectedAvg)
        {
            var (min, max, avg) = TupleUtils.GetStats(input);
            min.Should().BeApproximately(expectedMin, 0.0001);
            max.Should().BeApproximately(expectedMax, 0.0001);
            avg.Should().BeApproximately(expectedAvg, 0.0001);
        }

        // GenericUtils.Filter (string)
        public static IEnumerable<TestCaseData> StringFilterCases
        {
            get
            {
                yield return new TestCaseData(new[] { "apple", "banana", "pear" }, 'a', new[] { "apple", "banana", "pear" });
                yield return new TestCaseData(new[] { "kiwi", "melon", "plum" }, 'm', new[] { "melon", "plum" });
            }
        }

        [Test, TestCaseSource(nameof(StringFilterCases))]
        public void Filter_String_ShouldReturnFilteredCollection(string[] input, char containsChar, string[] expected)
        {
            var result = GenericUtils.Filter(input, s => s.Contains(containsChar));
            result.Should().Equal(expected);
        }

        // GenericUtils.Filter (int)
        public static IEnumerable<TestCaseData> IntFilterCases
        {
            get
            {
                yield return new TestCaseData(new[] { 1, 2, 3, 4, 5 }, 3, new[] { 4, 5 });
                yield return new TestCaseData(new[] { 10, 20, 30 }, 15, new[] { 20, 30 });
            }
        }

        [Test, TestCaseSource(nameof(IntFilterCases))]
        public void Filter_Ints_ShouldReturnGreaterThanValue(int[] input, int threshold, int[] expected)
        {
            var result = GenericUtils.Filter(input, x => x > threshold);
            result.Should().Equal(expected);
        }

        // ProductUtils with records and generics
        [Test]
        public void FilterByMinPrice_ShouldReturnCorrectProducts()
        {
            var products = new[]
            {
                new Product<int>("A", 10),
                new Product<int>("B", 5),
                new Product<int>("C", 20)
            };
            var result = ProductUtils.FilterByMinPrice(products, 10);
            result.Should().Equal(
                new Product<int>("A", 10),
                new Product<int>("C", 20)
            );
        }

        [Test]
        public void GetProductStats_ShouldReturnCheapestMostExpensiveAndAverage()
        {
            var products = new[]
            {
                new Product<decimal>("A", 10m),
                new Product<decimal>("B", 5m),
                new Product<decimal>("C", 20m)
            };
            var (cheapest, expensive, avg) = ProductUtils.GetProductStats(products);
            cheapest.Name.Should().Be("B");
            expensive.Name.Should().Be("C");
            avg.Should().BeApproximately(11.6667, 0.0001);
        }
    }
}