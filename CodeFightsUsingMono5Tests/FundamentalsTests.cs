using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeFightsUsingMono5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5.Tests
{
    [TestClass()]
    public class FundamentalsTests
    {
        [TestMethod()]
        public void centuryFromYearTest()
        {
            Assert.AreEqual(20, CodeFightsUsingMono5.Fundamentals.centuryFromYear(1905));
            Assert.AreEqual(20, CodeFightsUsingMono5.Fundamentals.centuryFromYear(1988));
            Assert.AreEqual(1, CodeFightsUsingMono5.Fundamentals.centuryFromYear(45));
            Assert.AreEqual(17, CodeFightsUsingMono5.Fundamentals.centuryFromYear(1700));
            Assert.AreEqual(21, CodeFightsUsingMono5.Fundamentals.centuryFromYear(2001));
            Assert.AreEqual(4, CodeFightsUsingMono5.Fundamentals.centuryFromYear(374));
        }

        [TestMethod()]
        public void adjacentElementsProductTest()
        {

            Assert.AreEqual(21, CodeFightsUsingMono5.Fundamentals.adjacentElementsProduct(new int[] { 3, 6, -2, -5, 7, 3 }));
            Assert.AreEqual(2, CodeFightsUsingMono5.Fundamentals.adjacentElementsProduct(new int[] { -1, -2 }));
            Assert.AreEqual(6, CodeFightsUsingMono5.Fundamentals.adjacentElementsProduct(new int[] { 5, 1, 2, 3, 1, 4 }));
            Assert.AreEqual(-12, CodeFightsUsingMono5.Fundamentals.adjacentElementsProduct(new int[] { -23, 4, -3, 8, -12 }));

            //Assert.Fail();
        }

        [TestMethod()]
        public void companyBotStrategyTest()
        {
            //trainingData = [[3, 1],
            //    [6, 1],
            //    [4, 1],
            //    [5, 1]]
            Assert.AreEqual(4.5, CodeFightsUsingMono5.CFBot.companyBotStrategy(new int[][] { new int[] { 3, 1 }, new int[] { 6, 1 }, new int[] { 4, 1 }, new int[] { 5, 1 } }));
            Assert.AreEqual(5.0, CodeFightsUsingMono5.CFBot.companyBotStrategy(new int[][] { new int[] { 4, 1 }, new int[] { 4, -1 }, new int[] { 0, 0 }, new int[] { 6, 1 } }));

        }

        [TestMethod()]
        public void shapeAreaTest()
        {
            Assert.AreEqual(195841841, CodeFightsUsingMono5.Fundamentals.shapeArea(9896));


        }

        [TestMethod()]
        public void removeKFromListTest()
        {
            CodeFightsUsingMono5.ListNode<int> l = new ListNode<int>() { value = 3 };
            l.next = new ListNode<int>() { value = 1 };
            l.next.next = new ListNode<int>() { value = 2 };
            l.next.next.next = new ListNode<int>() { value = 3 };
            l.next.next.next.next = new ListNode<int>() { value = 4 };
            l.next.next.next.next.next = new ListNode<int>() { value = 5 };
            //l: [3, 1, 2, 3, 4, 5]
            //k: 3
            CodeFightsUsingMono5.ListNode<int> testNode = CodeFightsUsingMono5.Fundamentals.removeKFromList(l, 3);
            List<int> results = new List<int>();
            List<int> expected = new List<int>() { 1, 2, 4, 5 };
            while (testNode != null)
            {
                results.Add(testNode.value);
                testNode = testNode.next;
            }

            if (results.Count != expected.Count) { Assert.Fail(); }
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i] != expected[i]) { Assert.Fail(); }
            }
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void zigzagTest()
        {
            int returnValue = CodeFightsUsingMono5.Fundamentals.zigzag(new int[] { 9, 8, 8, 5, 3, 5, 3, 2, 8, 6 });
            Assert.AreEqual(4, returnValue);
            //a: [2, 1, 4, 4, 1, 4, 4, 1, 2, 0, 1, 0, 0, 3, 1, 3, 4, 1, 3, 4]
            //          Output:
            //        4
            //Expected Output:
            //6
            returnValue = CodeFightsUsingMono5.Fundamentals.zigzag(new int[] { 2, 1, 4, 4, 1, 4, 4, 1, 2, 0, 1, 0, 0, 3, 1, 3, 4, 1, 3, 4 });
            Assert.AreEqual(6, returnValue);

        }

        [TestMethod()]
        public void constructSubmatrixTest()
        {

            //            Input:
            //            matrix: [[1, 0, 0, 2], 
            // [0,5,0,1], 
            // [0,0,3,5]]
            //rowsToDelete: [1]
            //        columnsToDelete: [0, 2]
            //        Output:
            //Empty
            //Expected Output:
            //[[0,2], 
            // [0,5]]


            int[][] returnValue = CodeFightsUsingMono5.Fundamentals.constructSubmatrix(new int[][] { new int[] { 1, 0, 0, 2 }, new int[] { 0, 5, 0, 1 }, new int[] { 0, 0, 3, 5 } }, new int[] { 1 }, new int[] { 0, 2 });
            int[][] expectedValue = new int[][] { new int[] { 0, 2 }, new int[] { 0, 5 } };
            for (int i = 0; i < returnValue.Length; i++)
            {
                for (int j = 0; j < returnValue[i].Length; j++)
                {
                    int temp = returnValue[i][j];
                    if (returnValue[i][j] != expectedValue[i][j]) { Assert.Fail(); }
                }
            }
        }

        [TestMethod()]
        public void bankRequestsTest()
        {
            int[] returnValue = CodeFightsUsingMono5.Fundamentals.bankRequests(
                new int[] { 10, 100, 20, 50, 30 },
                new string[] { "withdraw 2 10",
 "transfer 5 1 20",
 "deposit 5 20",
 "transfer 3 4 15"});

            int[] expectedValue = new int[] { 30, 90, 5, 65, 30 };

            for (int i = 0; i < expectedValue.Length; i++)
            {
                int temp = returnValue[i];
                if (returnValue[i] != expectedValue[i]) { Assert.Fail(); }
            }
            Assert.IsTrue(true);



            returnValue = CodeFightsUsingMono5.Fundamentals.bankRequests(
                new int[] { 20, 1000, 500, 40, 90 },
                new string[] { "deposit 3 400",
 "transfer 1 2 30",
 "withdraw 4 50"});

            expectedValue = new int[] { -2 };

            for (int i = 0; i < expectedValue.Length; i++)
            {
                int temp = returnValue[i];
                if (returnValue[i] != expectedValue[i]) { Assert.Fail(); }
            }
            Assert.IsTrue(true);
            //            accounts: [20, 1000, 500, 40, 90]
            //requests: ["deposit 3 400",
            // "transfer 1 2 30",
            // "withdraw 4 50"]

        }

        [TestMethod()]
        public void makeArrayConsecutive2Test()
        {
            int returnValue = CodeFightsUsingMono5.Fundamentals.makeArrayConsecutive2(new int[] { 6, 2, 3, 8 });
            Assert.AreEqual(3, returnValue);

            returnValue = CodeFightsUsingMono5.Fundamentals.makeArrayConsecutive2(new int[] { -23456, 2, 3, 345218 });
            Assert.AreEqual(368671, returnValue);
        }

        [TestMethod()]
        public void almostIncreasingSequenceTest()
        {
            bool returnValue = CodeFightsUsingMono5.Fundamentals.almostIncreasingSequence(new int[] { 40, 50, 60, 10, 20, 30 });
            Assert.AreEqual(false, returnValue);

            returnValue = CodeFightsUsingMono5.Fundamentals.almostIncreasingSequence(new int[] { 1, 2, 1, 2 });
            Assert.AreEqual(false, returnValue);

            //1, 3, 2

            returnValue = CodeFightsUsingMono5.Fundamentals.almostIncreasingSequence(new int[] { 1, 3, 2 });
            Assert.AreEqual(true, returnValue);

            //-1, 10, 1, 2, 3, 4, 5
            returnValue = CodeFightsUsingMono5.Fundamentals.almostIncreasingSequence(new int[] { -1, 10, 1, 2, 3, 4, 5 });
            Assert.AreEqual(true, returnValue);

            var testInt = Enumerable.Range(-1000000, 100000).ToList();
            testInt.Add(0);
            int Min = -500000;
            int Max = 500000;
            Random randNum = new Random();
            int[] test2 = Enumerable
                .Repeat(-500000, 500000)
                .Select(i => randNum.Next(Min, Max))
                .ToArray();
            int[] array1 = testInt.ToArray();
            int[] array2 = test2;
            int array1OriginalLength = array1.Length;
            Array.Resize<int>(ref array1, array1OriginalLength + array2.Length);
            Array.Copy(array2, 0, array1, array1OriginalLength, array2.Length);

            returnValue = CodeFightsUsingMono5.Fundamentals.almostIncreasingSequence(array1);
            Assert.AreEqual(false, returnValue);

        }

        [TestMethod()]
        public void matrixElementsSumTest()
        {
            int returnValue = CodeFightsUsingMono5.Fundamentals.matrixElementsSum(
                new int[][] { new int[] { 0, 1, 1, 2 }, new int[] { 0, 5, 0, 0 }, new int[] { 2, 0, 3, 3 } });
            Assert.AreEqual(9, returnValue);
            //matrix: [[0,1,1,2], 
            //[0,5,0,0], 
            //[2,0,3,3]]


        }

        [TestMethod()]
        public void allLongestStringsTest()
        {
            string[] inputArray = new string[] { "aba", "aa", "ad", "vcd", "aba" };
            string[] expected = new string[] { "aba", "vcd", "aba" };
            string[] result = CodeFightsUsingMono5.Fundamentals.allLongestStrings(inputArray);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod()]
        public void isLuckyTest()
        {
            bool returnValue = CodeFightsUsingMono5.Fundamentals.isLucky(1230);
            Assert.AreEqual(true, returnValue);
            returnValue = CodeFightsUsingMono5.Fundamentals.isLucky(239017);
            Assert.AreEqual(false, returnValue);
        }

        [TestMethod()]
        public void sortByHeightTest()
        {
            int[] result = CodeFightsUsingMono5.Fundamentals.sortByHeight(new int[] { -1, 150, 190, 170, -1, -1, 160, 180 });
            int[] expected = new int[] { -1, 150, 160, 170, -1, -1, 180, 190 };
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod()]
        public void reverseParenthesesTest()
        {
            string result = CodeFightsUsingMono5.Fundamentals.reverseParentheses("abc(cba)ab(bac)c");
            string expected = "abcabcabcabc";
            Assert.AreEqual(result, expected);
//            s: "abc(cba)ab(bac)c"
//Output:
//            Empty
//            Expected Output:
//            "abcabcabcabc"
        }
    }
}