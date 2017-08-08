using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Diagnostics;


namespace Exercise3NonCore
{
    [TestFixture]
    public class NUnitTester
    {
        [TestCase]
        public void Test1()
        {
            var f = new byte[] { 3, 1, 2 };
            var s = new byte[] { 3, 1, 2 };
            var addedValues = Add_UsingARecursiveAlgorithm_ValuesAreAdded(f, s);
            Assert.AreEqual(addedValues.Result, new byte[3] { 6, 2, 4 });
            Assert.AreEqual(addedValues.F, f);
            Assert.AreEqual(addedValues.S, s);
        }

        [TestCase]
        public void Test2()
        {
            var f = new byte[] { 1, 1, 255 };
            var s = new byte[] { 0, 0, 2 };
            var addedValues = Add_UsingARecursiveAlgorithm_ValuesAreAdded(f, s);
            Assert.AreEqual(addedValues.Result, new byte[3] { 1, 2, 1 });
            Assert.AreEqual(addedValues.F, f);
            Assert.AreEqual(addedValues.S, s);
        }

        [TestCase]
        public void Test3()
        {
            var f = new byte[] { 255, 4, 1 };
            var s = new byte[] { 255, 1, 3 };
            var addedValues = Add_UsingARecursiveAlgorithm_ValuesAreAdded(f, s);
            Assert.AreEqual(addedValues.Result, new byte[4] { 1, 254, 5, 4 });
            Assert.AreEqual(addedValues.F, f);
            Assert.AreEqual(addedValues.S, s);
        }

        [TestCase]
        public void Test4()
        {
            var f = new byte[] { 3, 1, 255 };
            var s = new byte[] { 0, 0, 1 };
            var addedValues = Add_UsingARecursiveAlgorithm_ValuesAreAdded(f, s);
            Assert.AreEqual(addedValues.Result, new byte[] { 3, 2, 0 });
        }

        [TestCase]
        public void Test5()
        {
            var f = new byte[] { 255, 255, 255 };
            var s = new byte[] { 255, 255, 255 };
            var addedValues = Add_UsingARecursiveAlgorithm_ValuesAreAdded(f, s);
            Assert.AreEqual(addedValues.Result, new byte[] { 1, 255, 255, 254 });
        }

        [TestCase]
        public void StressTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            var f = new byte[1000];
            var s = new byte[1000];
            var r = new byte[1001];

            for (int i = 0; i < 1000; i++)
            {
                f[i] = 255;
                s[i] = 255;
                r[i] = 255;
            }

            r[0] = 1;
            r[r.Length - 1] = 254;
            var addedValues = Add_UsingARecursiveAlgorithm_ValuesAreAdded(f, s);
            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.AreEqual(addedValues.Result, r);
        }

        [Test]
        [TestCaseSource("Add_Source")]
        public AddResult Add_UsingARecursiveAlgorithm_ValuesAreAdded(byte[] f, byte[] s)
        {
            //Arrange

            //Act
            var result = AddRecursive(f, s);

            //Assert - Not really asserting anything here, but this is how the question was phrased
            return new AddResult(f, s, result);
        }

        public byte[] AddRecursive(byte[] f, byte[] s)
        {
            return AddRecursiveImpl(f, s, 0).ToArray();
        }

        private IEnumerable<byte> AddRecursiveImpl(byte[] f, byte[] s, int previousResult)
        {
            var lastCharacterInF = f[f.Length - 1];
            var lastCharacterInS = s[s.Length - 1];

            var sumOfTwoBytes = lastCharacterInF + lastCharacterInS + (previousResult > byte.MaxValue ? 1 : 0);

            if (f.Length > 1)
            {
                var temp = AddRecursiveImpl(ChopLastByte(f), ChopLastByte(s), sumOfTwoBytes);
                foreach (var t in temp)
                {
                    yield return t;
                }
            }
            else
            {
                if (sumOfTwoBytes > byte.MaxValue)      //If the Sum on the most significant digit is a carry, add a final 1
                {
                    yield return 1;
                }
            }

            yield return (byte)sumOfTwoBytes;
        }

        private byte[] ChopLastByte(byte[] input)
        {
            return input.Take(input.Length - 1).ToArray();
        }

        public class AddResult
        {
            public byte[] Result;
            public byte[] F;
            public byte[] S;

            public AddResult(byte[] f, byte[] s, byte[] result)
            {
                Result = result;
                F = f;
                S = s;
            }
        }
    }

    

}
