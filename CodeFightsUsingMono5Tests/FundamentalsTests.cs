using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeFightsUsingMono5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace CodeFightsUsingMono5.Tests
{
    [TestClass()]
    public class FundamentalsTests
    {
        static Rootobject codefightTestData;
        public FundamentalsTests()
        {
            try
            {



                var memoryStream = new MemoryStream(CodeFightsUsingMono5.Properties.Resources.test_14);
                var s = new StreamReader(memoryStream).ReadToEnd();

                codefightTestData = JsonConvert.DeserializeObject<Rootobject>(s);

            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                throw;
            }

            // var test = 
            //

        }

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
            l.right = new ListNode<int>() { value = 1 };
            l.right.right = new ListNode<int>() { value = 2 };
            l.right.right.right = new ListNode<int>() { value = 3 };
            l.right.right.right.right = new ListNode<int>() { value = 4 };
            l.right.right.right.right.right = new ListNode<int>() { value = 5 };
            //l: [3, 1, 2, 3, 4, 5]
            //k: 3
            CodeFightsUsingMono5.ListNode<int> testNode = CodeFightsUsingMono5.Fundamentals.removeKFromList(l, 3);
            List<int> results = new List<int>();
            List<int> expected = new List<int>() { 1, 2, 4, 5 };
            while (testNode != null)
            {
                results.Add(testNode.value);
                testNode = testNode.right;
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

            string result = CodeFightsUsingMono5.Fundamentals.reverseParentheses("a(bcdefghijkl(mno)p)q");
            string expected = "apmnolkjihgfedcbq";
            Assert.AreEqual(result, expected, "a(bcdefghijkl(mno)p)q failed to become apmnolkjihgfedcbq return: " + expected);


            result = CodeFightsUsingMono5.Fundamentals.reverseParentheses("abc(cba)ab(bac)c");
            expected = "abcabcabcabc";
            Assert.AreEqual(result, expected, "abc(cba)ab(bac)c failed to become abcabcabcabc return: " + expected);


            result = CodeFightsUsingMono5.Fundamentals.reverseParentheses("a(bc)de");
            expected = "acbde";
            Assert.AreEqual(result, expected, "a(bc)de failed to become acbde return: " + expected);

            result = CodeFightsUsingMono5.Fundamentals.reverseParentheses("co(de(fight)s)");
            expected = "cosfighted";
            Assert.AreEqual(result, expected, "co(de(fight)s) failed to become apmnolkjihgfedcbq return: " + expected);

            /*
             *"a(bcdefghijkl(mno)p)q"
             "apmnolkjihgfedcbq"
             */

            //            s: "abc(cba)ab(bac)c"
            //Output:
            //            Empty
            //            Expected Output:
            //            "abcabcabcabc"
        }

        [TestMethod()]
        public void isListPalindromeTest()
        {
            ListNode<int> l = new ListNode<int>() { value = 1, right = new ListNode<int>() { value = 2, right = new ListNode<int>() { value = 3, right = new ListNode<int>() { value = 3, right = new ListNode<int>() { value = 2 } } } } };
            //1, 2, 3, 3, 2
            //int[] values = new int[] { 1, 2, 3, 3, 2 };

            //for (int i = 0; i < values.Length; i++)
            //{
            //    l.value = values[i];
            //    l.next = new ListNode<int>();
            //}

            bool result = CodeFightsUsingMono5.Fundamentals.isListPalindrome(l);
            bool expected = false;
            Assert.AreEqual(result, expected, "false? " + expected);
        }

        [TestMethod()]
        public void removeKFromListTest1()
        {
            ListNode<int> l = new ListNode<int>()
            {
                value = 3,
                right =
                new ListNode<int>()
                {
                    value = 1,
                    right =
                new ListNode<int>()
                {
                    value = 2,
                    right =
                new ListNode<int>()
                {
                    value = 3,
                    right =
                new ListNode<int>() { value = 4, right = new ListNode<int>() { value = 5, } }
                }
                }
                }
            };
            /*
             * input:
            l: [3, 1, 2, 3, 4, 5]
            k: 3
            */

            ListNode<int> result = CodeFightsUsingMono5.Fundamentals.removeKFromList(l, 3);
            ListNode<int> expected = new ListNode<int>()
            {
                value = 1,
                right =
                new ListNode<int>()
                {
                    value = 2,
                    right =
                new ListNode<int>()
                {
                    value = 4,
                    right =
                new ListNode<int>()
                {
                    value = 5
                }
                }
                }
            };
            Assert.AreEqual(result, expected, "false? " + expected);
        }

        [TestMethod()]
        public void addTwoHugeNumbersTest()
        {



            ListNode<int> a = fillListNode(codefightTestData.input.a);
            ListNode<int> b = fillListNode(codefightTestData.input.b);
            ListNode<int> result = CodeFightsUsingMono5.Fundamentals.addTwoHugeNumbers(a, b);

            var x = result;


        }


        private ListNode<int> fillListNode(int[] values)
        {
            ListNode<int> returnA = new ListNode<int>();
            ListNode<int> current = returnA;


            for (int i = 0; i < values.Length; i++)
            {
                ListNode<int> toAdd = new ListNode<int> { value = values[i] };

                while (current.right != null)
                {
                    current = current.right;
                }
                current.right = toAdd;
            }


            return returnA.right;
        }

        [TestMethod()]
        public void reverseNodesInKGroupsTest()
        {
            ListNode<int> a = fillListNode(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
            ListNode<int> result = CodeFightsUsingMono5.Fundamentals.reverseNodesInKGroups(a, 3);
            var test = result;

            /*l: [1, 2, 3, 4]
k: 2*/
            a = fillListNode(new int[] { 1, 2, 3, 4 });
            result = CodeFightsUsingMono5.Fundamentals.reverseNodesInKGroups(a, 2);
            test = result;

        }

        [TestMethod()]
        public void rearrangeLastNTest()
        {
            ListNode<int> a = fillListNode(new int[] { 1, 2, 3, 4, 5 });
            ListNode<int> result = CodeFightsUsingMono5.Fundamentals.rearrangeLastN(a, 3);
            var test = result;

            /*   [1, 2, 3, 4, 5] and n = 3 = 
             *   3, 4, 5, 1, 2
             *   l: [123, 456, 789, 0]
                 n: 4
                 Output:
                Run the code to see output
                Expected Output:
                [123, 456, 789, 0]
            */
            a = fillListNode(new int[] { 123, 456, 789, 0 });
            result = CodeFightsUsingMono5.Fundamentals.rearrangeLastN(a, 4);
            test = result;
        }

        [TestMethod()]
        public void groupingDishesTest()
        {
            string[][] dishes = new string[][]{ new string[] { "Salad", "Tomato", "Cucumber", "Salad", "Sauce" },
            new string[] {"Pizza", "Tomato", "Sausage", "Sauce", "Dough"},
            new string[] { "Quesadilla", "Chicken", "Cheese", "Sauce"},
            new string[] { "Sandwich", "Salad", "Bread", "Tomato", "Cheese"}};

            var result = CodeFightsUsingMono5.HashTables.groupingDishes(dishes);

            var x = result;
            /*
            Input:
dishes: [["dSaLKJGbxlxcBBv kMNOmzdojCluYeCb","zjxwKcRmpQTPSPRUKLfAhkIXxfzniZjsDfaKOJOcVDaxAnCF"], 
 ["R GBgXIv","fPhNHIdOTeKPnqaIPAYXScGrDyGWwlqktYtyOzondayKp","xJ hfufIWL","YduFVZrZEeqVmvACdSTdQd uMswBcadvet","WHYOLUzwSHKUuCNry"], 
 ["kvHxWutzASOCBHV","wpzmQKLROsw ","sxgFkhrwFKB","reRqP TFlpmiQa GoZTuErWVB","LA","BGQgMdEGXutmmE InKtapSHbwZlPHrvPwbSmRWclamnTW","QgBClPTxsIpAl","SWbXtEIFeVqoUgtSfXKcVmnwDrijLYsPeXfUauFVbBkbEmGDa"], 
 ["Hgpu","fvORUPNvHmBtpKpbTRbmdXzicYOZotLmfoLmQIqBInPnbCFN","WHYOLUzwSHKUuCNry"], 
 ["gZxWYomyYO","fvORUPNvHmBtpKpbTRbmdXzicYOZotLmfoLmQIqBInPnbCFN","YduFVZrZEeqVmvACdSTdQd uMswBcadvet","XxRAIFwrGmaarKfz","yJffViKwbqCATxHcQFDLgMX","poEnqRtrANh","QgBClPTxsIpAl","dyqdvHDdflvzxVAGRyxWPMBtIDJhv paNyJbWab"], 
 ["rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL","YduFVZrZEeqVmvACdSTdQd uMswBcadvet","QgBClPTxsIpAl","fPhNHIdOTeKPnqaIPAYXScGrDyGWwlqktYtyOzondayKp","udzzsbLVValZOWpRLhUKumkROw","dyqdvHDdflvzxVAGRyxWPMBtIDJhv paNyJbWab","WHYOLUzwSHKUuCNry","LA","fvORUPNvHmBtpKpbTRbmdXzicYOZotLmfoLmQIqBInPnbCFN"], 
 ["GrWh ROg zHXhYguurdcGjNAv","dyqdvHDdflvzxVAGRyxWPMBtIDJhv paNyJbWab","YduFVZrZEeqVmvACdSTdQd uMswBcadvet","AQglifKDgZIivthzfoWRklaKs","UcIBqQckdEJgLeWMlaRPlqfkhVRXjtZHAYDVRhPsFqPOuEVN","LA","MWhqbkFrSTnOuWKexjPewdd AOISBnSCilJ","QgBClPTxsIpAl"], 
 ["dLuvsltPzUjfXkynBCMgxBUTXhVCd","udzzsbLVValZOWpRLhUKumkROw","BGQgMdEGXutmmE InKtapSHbwZlPHrvPwbSmRWclamnTW","BjRRCVKznaySRzyAuNxAbkSYTmcUGlvOND","wpzmQKLROsw ","qLKOIfeBowxWwxPJWrWjtVXMkG NXOLxYxvCKoAagSHYRxVgK","WdfleYASWwVMQKoBINhwpjDbVBEaOOogkKMZ","AQglifKDgZIivthzfoWRklaKs","qRUsCllaFzNWuXIMvbOsNtTTAlbR"], 
 ["jOubIROdYWOKxwbZTLDueBiln","fTUBneoUSWxFERZjwPMrAQq","NPuomEOeOXBiozrNZXlXcKKB","ibogPWJoEbermlJfuizYaE","zpNFvjef mpEbEqy rdudPTGpzo n FwxTA"], 
 ["BiNYUHMFrRoSICZ","ufYAxvBidQjinsDCurHyjlzRHrOQ MbIVKGLwAq","nvHaaRJdHgRIgXXQteAchX MKldBbM","TdBMoUrYiEcJPlERejkAQdxYMTatLYXX","qLKOIfeBowxWwxPJWrWjtVXMkG NXOLxYxvCKoAagSHYRxVgK"]]
Output:
[["AQglifKDgZIivthzfoWRklaKs","dLuvsltPzUjfXkynBCMgxBUTXhVCd","GrWh ROg zHXhYguurdcGjNAv"], 
 ["BGQgMdEGXutmmE InKtapSHbwZlPHrvPwbSmRWclamnTW","dLuvsltPzUjfXkynBCMgxBUTXhVCd","kvHxWutzASOCBHV"], 
 ["dyqdvHDdflvzxVAGRyxWPMBtIDJhv paNyJbWab","GrWh ROg zHXhYguurdcGjNAv","gZxWYomyYO","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["fPhNHIdOTeKPnqaIPAYXScGrDyGWwlqktYtyOzondayKp","R GBgXIv","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["fvORUPNvHmBtpKpbTRbmdXzicYOZotLmfoLmQIqBInPnbCFN","gZxWYomyYO","Hgpu","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["LA","GrWh ROg zHXhYguurdcGjNAv","kvHxWutzASOCBHV","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["QgBClPTxsIpAl","GrWh ROg zHXhYguurdcGjNAv","gZxWYomyYO","kvHxWutzASOCBHV","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["qLKOIfeBowxWwxPJWrWjtVXMkG NXOLxYxvCKoAagSHYRxVgK","BiNYUHMFrRoSICZ","dLuvsltPzUjfXkynBCMgxBUTXhVCd"], 
 ["udzzsbLVValZOWpRLhUKumkROw","dLuvsltPzUjfXkynBCMgxBUTXhVCd","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["WHYOLUzwSHKUuCNry","Hgpu","R GBgXIv","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["wpzmQKLROsw ","dLuvsltPzUjfXkynBCMgxBUTXhVCd","kvHxWutzASOCBHV"], 
 ["YduFVZrZEeqVmvACdSTdQd uMswBcadvet","GrWh ROg zHXhYguurdcGjNAv","gZxWYomyYO","R GBgXIv","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"]]
Expected Output:
[["AQglifKDgZIivthzfoWRklaKs","GrWh ROg zHXhYguurdcGjNAv","dLuvsltPzUjfXkynBCMgxBUTXhVCd"], 
 ["BGQgMdEGXutmmE InKtapSHbwZlPHrvPwbSmRWclamnTW","dLuvsltPzUjfXkynBCMgxBUTXhVCd","kvHxWutzASOCBHV"], 
 ["LA","GrWh ROg zHXhYguurdcGjNAv","kvHxWutzASOCBHV","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["QgBClPTxsIpAl","GrWh ROg zHXhYguurdcGjNAv","gZxWYomyYO","kvHxWutzASOCBHV","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["WHYOLUzwSHKUuCNry","Hgpu","R GBgXIv","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["YduFVZrZEeqVmvACdSTdQd uMswBcadvet","GrWh ROg zHXhYguurdcGjNAv","R GBgXIv","gZxWYomyYO","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["dyqdvHDdflvzxVAGRyxWPMBtIDJhv paNyJbWab","GrWh ROg zHXhYguurdcGjNAv","gZxWYomyYO","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["fPhNHIdOTeKPnqaIPAYXScGrDyGWwlqktYtyOzondayKp","R GBgXIv","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["fvORUPNvHmBtpKpbTRbmdXzicYOZotLmfoLmQIqBInPnbCFN","Hgpu","gZxWYomyYO","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["qLKOIfeBowxWwxPJWrWjtVXMkG NXOLxYxvCKoAagSHYRxVgK","BiNYUHMFrRoSICZ","dLuvsltPzUjfXkynBCMgxBUTXhVCd"], 
 ["udzzsbLVValZOWpRLhUKumkROw","dLuvsltPzUjfXkynBCMgxBUTXhVCd","rMSYkYkFKlcdBTrUpCTdFgEIdgdTOcEucJdPqiLUWUZNjcoL"], 
 ["wpzmQKLROsw ","dLuvsltPzUjfXkynBCMgxBUTXhVCd","kvHxWutzASOCBHV"]]

             */

        }

        [TestMethod()]
        public void areFollowingPatternsTest()
        {
            /*
             Input:
strings: ["cat", 
 "dog", 
 "dog"]
patterns: ["a", 
 "b", 
 "b"]
Output:
Run the code to see output
Expected Output:
true
             */
            string[] strings = new string[] { "a", "b", "c" };
            string[] patterns = new string[] { "a", "b", "a" };

            var result = CodeFightsUsingMono5.HashTables.areFollowingPatterns(strings, patterns);
            var x = result;
        }

        [TestMethod()]
        public void containsCloseNumsTest()
        {
            var result = CodeFightsUsingMono5.HashTables.containsCloseNums(new int[] { 0, 1, 2, 3, 5, 2 }, 3);
            var x = result;
        }

        [TestMethod()]
        public void possibleSumsTest()
        {
            /*
             Input:
coins: [10, 50, 100, 500]
quantity: [5, 3, 2, 2]
Output:
39
Expected Output:
122
            
             */

            int[] maxCounts = new int[3];


            HashSet<int> htest = new HashSet<int>();
            CodeFightsUsingMono5.HashTables.possibleSums(new int[] { 10, 50, 100 }, new int[] { 1, 2, 1 });
            var test = htest.Count;
            Assert.AreEqual(9, test);
            //var result = CodeFightsUsingMono5.HashTables.possibleSums(new int[] { 10, 50, 100 }, new int[] { 1, 2, 1});
            //Assert.AreEqual(9, result);
            //result = CodeFightsUsingMono5.HashTables.possibleSums(new int[] { 10, 50, 100, 500 }, new int[] { 5, 3, 2, 2 } );


            maxCounts = new int[4];


            htest = new HashSet<int>();
            CodeFightsUsingMono5.HashTables.possibleSums(new int[] { 10, 50, 100, 500 }, new int[] { 5, 3, 2, 2 });
            test = htest.Count;
            Assert.AreEqual(122, test);
            //Assert.AreEqual(122, result);
        }

        [TestMethod()]
        public void areSimilarTest()
        {
            var result = CodeFightsUsingMono5.Fundamentals.areSimilar(new int[] { 1, 2, 2 }, new int[] { 2, 1, 1 });
            Assert.AreEqual(false, result);

            /*
             a: [2, 3, 1]
b: [1, 3, 2]
             
             */
            result = CodeFightsUsingMono5.Fundamentals.areSimilar(new int[] { 2, 3, 1 }, new int[] { 1, 3, 2 });
            Assert.AreEqual(true, result);

            /*
             a: [832, 998, 148, 570, 533, 561, 894, 147, 455, 279]
             b: [832, 998, 148, 570, 533, 561, 455, 147, 894, 279]
Output:
false
Expected Output:
true
             */
            result = CodeFightsUsingMono5.Fundamentals.areSimilar(
                new int[] { 832, 998, 148, 570, 533, 561, 894, 147, 455, 279 },
                new int[] { 832, 998, 148, 570, 533, 561, 455, 147, 894, 279 });
            Assert.AreEqual(true, result);

            /*
             a: [4, 6, 3]
b: [3, 4, 6]
Output:
true
Expected Output:
false
             
             */

            result = CodeFightsUsingMono5.Fundamentals.areSimilar(
             new int[] { 4, 6, 3 },
             new int[] { 3, 4, 6 });
            Assert.AreEqual(false, result);



            var memoryStream = new MemoryStream(CodeFightsUsingMono5.Properties.Resources.test_15);
            var s = new StreamReader(memoryStream).ReadToEnd();

            var testData = JsonConvert.DeserializeObject<Rootobject>(s);

            result = CodeFightsUsingMono5.Fundamentals.areSimilar(testData.input.a, testData.input.b);
            Assert.AreEqual(true, result);


        }

        [TestMethod()]
        public void swapLexOrderTest()
        {
            Assert.AreEqual("dbca", CodeFightsUsingMono5.Fundamentals.swapLexOrder("abdc", new int[][] { new int[] { 1, 4 }, new int[] { 3, 4 } }));
            /*
             Input:
str: "acxrabdz"
pairs: [[1,3], 
 [6,8], 
 [3,8], 
 [2,7]]
Output:
"zdaraxcb"
Expected Output:
"zdxrabca"
             
             */
            Assert.AreEqual("zdxrabca", CodeFightsUsingMono5.Fundamentals.swapLexOrder("acxrabdz",
                new int[][] { new int[] { 1, 3 }, new int[] { 6, 8 }, new int[] { 3, 8 }, new int[] { 2, 7 } }));


            /*
             
             str: "dznsxamwoj"
pairs: [[1,2], 
 [3,4], 
 [6,5], 
 [8,10]]
Output:
"zdsnaxmwoj"
Expected Output:
"zdsnxamwoj"
             
             */
            Assert.AreEqual("zdsnxamwoj", CodeFightsUsingMono5.Fundamentals.swapLexOrder("dznsxamwoj",
                           new int[][] { new int[] { 1, 2 }, new int[] { 3, 4 }, new int[] { 6, 5 }, new int[] { 8, 10 } }));

            /*
             Input:
str: "fixmfbhyutghwbyezkveyameoamqoi"
pairs: [[8,5], 
 [10,8], 
 [4,18], 
 [20,12], 
 [5,2], 
 [17,2], 
 [13,25], 
 [29,12], 
 [22,2], 
 [17,11]]
Output:
"fhxmybhzutaiwbyegkvoyemeoamqfi"
Expected Output:
"fzxmybhtuigowbyefkvhyameoamqei"
             
             */
            Assert.AreEqual("fzxmybhtuigowbyefkvhyameoamqei", CodeFightsUsingMono5.Fundamentals.swapLexOrder("fixmfbhyutghwbyezkveyameoamqoi",
                         new int[][] { new int[] {8, 5 },
                         new int[] {10, 8 },
                         new int[] { 4, 18 },
                         new int[] {20, 12 },
                         new int[] {5, 2 },
                         new int[] {17, 2 },
                         new int[] {13, 25 },
                         new int[] {29, 12 },
                         new int[] {22, 2 },
                         new int[] {17, 11 } }));

        }

        [TestMethod()]
        public void palindromeRearrangingTest()
        {
            var isTrue = CodeFightsUsingMono5.Fundamentals.palindromeRearranging("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaabc");
            isTrue = CodeFightsUsingMono5.Fundamentals.palindromeRearranging("aabbccdd");
            isTrue = CodeFightsUsingMono5.Fundamentals.palindromeRearranging("zaa");



        }

        [TestMethod()]
        public void areEquallyStrongTest()
        {

            var isTrue = CodeFightsUsingMono5.Fundamentals.areEquallyStrong(10, 15, 15, 10);

            /*
             * Input:
yourLeft: 10
yourRight: 15
friendsLeft: 15
friendsRight: 10
Output:
false
Expected Output:
true
             */
        }

        [TestMethod()]
        public void arrayMaximalAdjacentDifferenceTest()
        {
            int result = CodeFightsUsingMono5.Fundamentals.arrayMaximalAdjacentDifference(new int[] { -1, 4, 10, 3, -2 });
            Assert.AreEqual(7, result);

            //inputArray: [-1, 1, -3, -4]             Output:            2 Expected Output:4
            result = CodeFightsUsingMono5.Fundamentals.arrayMaximalAdjacentDifference(new int[] { -1, 1, -3, -4 });
            Assert.AreEqual(4, result);

        }

        [TestMethod()]
        public void plagueIncTest()
        {
            /*Input:
people: [[0,1], 
 [1,0]]
Output:
Run the code to see output
Expected Output:
0
people: [[0,1,2,3],  [1,0],  [2,0],  [3,0,4],  [4,3,5],  [5,4]]
Expected Output:3
 
             
             */
            int result = CodeFightsUsingMono5.PlagueInc.plagueInc(new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 } });

            result = CodeFightsUsingMono5.PlagueInc.plagueInc(new int[][] {
                new int[] {0, 1, 2, 3},
                new int[] { 1, 0 },
                new int[] { 2, 0 },
                new int[] { 3,0,4 },
                new int[] {4,3,5},
                new int[] {5,4}
            });
        }

        [TestMethod()]
        public void whatIsLoveTest()
        {
            var result = CodeFightsUsingMono5.Fundamentals.whatIsLove(4);
            result = CodeFightsUsingMono5.Fundamentals.whatIsLove(5);
            result = CodeFightsUsingMono5.Fundamentals.whatIsLove(6);
            result = CodeFightsUsingMono5.Fundamentals.whatIsLove(7);
            result = CodeFightsUsingMono5.Fundamentals.whatIsLove(456700);
            result = CodeFightsUsingMono5.Fundamentals.whatIsLove(1237);
            result = CodeFightsUsingMono5.Fundamentals.whatIsLove(23);


        }

        public void inOrderTraverseTree(Tree<int> focusNode, LinkedList<Tree<int>> output)
        {
            if (focusNode != null)
            {
                inOrderTraverseTree(focusNode.left, output);
                output.AddLast(focusNode);
                inOrderTraverseTree(focusNode.right, output);
            }
        }

        [TestMethod()]
        public void hasPathWithGivenSumTest()
        {
            Tree<int> btree = new Tree<int>()
            {
                value = 953,
                left = new Tree<int>()
                {
                    value = 719,
                    left = new Tree<int>()
                    {
                        value = 618,
                        left = new Tree<int>()
                        {
                            value = 532,
                            left = new Tree<int>()
                            {
                                value = 541,
                                left = new Tree<int>()
                                {
                                    value = 637,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 314,
                                    left = null,
                                    right = null
                                }
                            },
                            right = new Tree<int>()
                            {
                                value = -435,
                                left = new Tree<int>()
                                {
                                    value = 468,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 966,
                                    left = null,
                                    right = null
                                }
                            }
                        },
                        right = new Tree<int>()
                        {
                            value = -60,
                            left = new Tree<int>()
                            {
                                value = 509,
                                left = new Tree<int>()
                                {
                                    value = -481,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 160,
                                    left = null,
                                    right = null
                                }
                            },
                            right = new Tree<int>()
                            {
                                value = 798,
                                left = new Tree<int>()
                                {
                                    value = -511,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = -490,
                                    left = null,
                                    right = null
                                }
                            }
                        }
                    },
                    right = new Tree<int>()
                    {
                        value = 669,
                        left = new Tree<int>()
                        {
                            value = -55,
                            left = new Tree<int>()
                            {
                                value = -146,
                                left = new Tree<int>()
                                {
                                    value = 647,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 960,
                                    left = null,
                                    right = null
                                }
                            },
                            right = new Tree<int>()
                            {
                                value = 49,
                                left = new Tree<int>()
                                {
                                    value = -818,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = -900,
                                    left = null,
                                    right = null
                                }
                            }
                        },
                        right = new Tree<int>()
                        {
                            value = -741,
                            left = new Tree<int>()
                            {
                                value = 32,
                                left = new Tree<int>()
                                {
                                    value = -660,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 825,
                                    left = null,
                                    right = null
                                }
                            },
                            right = new Tree<int>()
                            {
                                value = -139,
                                left = new Tree<int>()
                                {
                                    value = -811,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = -876,
                                    left = null,
                                    right = null
                                }
                            }
                        }
                    }
                },
                right = new Tree<int>()
                {
                    value = -336,
                    left = new Tree<int>()
                    {
                        value = 937,
                        left = new Tree<int>()
                        {
                            value = -611,
                            left = new Tree<int>()
                            {
                                value = 84,
                                left = new Tree<int>()
                                {
                                    value = -621,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 668,
                                    left = null,
                                    right = null
                                }
                            },
                            right = new Tree<int>()
                            {
                                value = 355,
                                left = new Tree<int>()
                                {
                                    value = -785,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = -469,
                                    left = null,
                                    right = null
                                }
                            }
                        },
                        right = new Tree<int>()
                        {
                            value = 933,
                            left = new Tree<int>()
                            {
                                value = 824,
                                left = new Tree<int>()
                                {
                                    value = 572,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 850,
                                    left = null,
                                    right = null
                                }
                            },
                            right = new Tree<int>()
                            {
                                value = -285,
                                left = new Tree<int>()
                                {
                                    value = 987,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 401,
                                    left = null,
                                    right = null
                                }
                            }
                        }
                    },
                    right = new Tree<int>()
                    {
                        value = -105,
                        left = new Tree<int>()
                        {
                            value = 206,
                            left = new Tree<int>()
                            {
                                value = 451,
                                left = new Tree<int>()
                                {
                                    value = -697,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 898,
                                    left = null,
                                    right = null
                                }
                            },
                            right = new Tree<int>()
                            {
                                value = 64,
                                left = new Tree<int>()
                                {
                                    value = -458,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 164,
                                    left = null,
                                    right = null
                                }
                            }
                        },
                        right = new Tree<int>()
                        {
                            value = 989,
                            left = new Tree<int>()
                            {
                                value = 529,
                                left = new Tree<int>()
                                {
                                    value = -559,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = 79,
                                    left = null,
                                    right = null
                                }
                            },
                            right = new Tree<int>()
                            {
                                value = -880,
                                left = new Tree<int>()
                                {
                                    value = 59,
                                    left = null,
                                    right = null
                                },
                                right = new Tree<int>()
                                {
                                    value = -504,
                                    left = null,
                                    right = null
                                }
                            }
                        }
                    }
                }
            };

            /*
             t: {
                "value": 4,
                "left": {
                    "value": 1,
                    "left": {
                        "value": -2,
                        "left": null,
                        "right": {
                            "value": 3,
                            "left": null,
                            "right": null
                        }
                    },
                "right": null
                },
                "right": {
                    "value": 3,
                    "left": {
                        "value": 1,
                        "left": null,
                        "right": null
                    },
                "right": {
                    "value": 2,
                    "left": {
                        "value": -2,
                        "left": null,
                        "right": null
                    },
                "right": {
                    "value": -3,
                    "left": null,
                    "right": null
                }
            }
        }
    }
s: 7
Output:
Run the code to see output
Expected Output:
true
Console Output:
Empty
             */


            //LinkedList<Tree<int>> output = new LinkedList<Tree<int>>();
            //inOrderTraverseTree(bt, output);

            var result = CodeFightsUsingMono5.Fundamentals.hasPathWithGivenSum(btree, 4000);
            Assert.AreEqual(true, result);

            btree = new Tree<int>()
            {
                value = 8,
                left = null,
                right = new Tree<int>()
                {
                    value = 3,
                    left = null,
                    right = null
                }
            };

            result = CodeFightsUsingMono5.Fundamentals.hasPathWithGivenSum(btree, 8);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void treeBottomTest()
        {
            string tree = "(2 (7 (2 () ()) (6 (5 () ()) (11 () ()))) (5 () (9 (4 () ()) ())))";
            var result = CodeFightsUsingMono5.Fundamentals.treeBottom(tree);
            int[] expected = new int[] { 5, 11, 4 };
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }


        }

        [TestMethod()]
        public void isIPv4AddressTest()
        {
            var result = CodeFightsUsingMono5.Fundamentals.isIPv4Address("1");
            Assert.IsTrue(CodeFightsUsingMono5.Fundamentals.isIPv4Address("172.16.254.1"));

            Assert.IsFalse(CodeFightsUsingMono5.Fundamentals.isIPv4Address("172.316.254.1"));
            Assert.IsFalse(CodeFightsUsingMono5.Fundamentals.isIPv4Address("255.255.255.255abcdekjhf"));

        }

        [TestMethod()]
        public void avoidObstaclesTest()
        {
            int result = CodeFightsUsingMono5.Fundamentals.avoidObstacles(new int[] { 5, 3, 6, 7, 9 });
            Assert.AreEqual(4, result);
            result = CodeFightsUsingMono5.Fundamentals.avoidObstacles(new int[] { 2, 3 });
            Assert.AreEqual(4, result);
            result = CodeFightsUsingMono5.Fundamentals.avoidObstacles(new int[] { 1, 4, 10, 6, 2 });
            Assert.AreEqual(7, result);
            //19, 32, 11, 23
            result = CodeFightsUsingMono5.Fundamentals.avoidObstacles(new int[] { 19, 32, 11, 23 });
            Assert.AreEqual(3, result);
            //[5, 8, 9, 13, 14]
            result = CodeFightsUsingMono5.Fundamentals.avoidObstacles(new int[] { 5, 8, 9, 13, 14 });
            Assert.AreEqual(6, result);
        }

        [TestMethod()]
        public void boxBlurTest()
        {

            /*
             * image: [[36,0,18,9], 
                     [27,54,9,0], 
                     [81,63,72,45]]
                    Output:
                    Run the code to see output
                    Expected Output:
                    [[40,30]]
                                 * 
                                 * 
                                 Test 5
                    Input:
                    image: [[36,0,18,9,9,45,27], 
                     [27,0,54,9,0,63,90], 
                     [81,63,72,45,18,27,0], 
                     [0,0,9,81,27,18,45], 
                     [45,45,27,27,90,81,72], 
                     [45,18,9,0,9,18,45], 
                     [27,81,36,63,63,72,81]]
                    Output:
                    Run the code to see output
                    Expected Output:
                    [[39,30,26,25,31], 
                     [34,37,35,32,32], 
                     [38,41,44,46,42], 
                     [22,24,31,39,45], 
                     [37,34,36,47,59]]
                    Console Output:

             */
            var result = CodeFightsUsingMono5.Fundamentals.boxBlur(new int[][] {
                new int[]{36,  0, 18,  9 },
                new int[]{27, 54,  9,  0 },
                new int[]{81, 63, 72, 45 } });

            result = CodeFightsUsingMono5.Fundamentals.boxBlur(new int[][] {
                new int[]{36,0,18,9,9,45,27 },
                new int[]{27,0,54,9,0,63,90},
                new int[]{81,63,72,45,18,27,0},
                new int[]{0,0,9,81,27,18,45},
                new int[]{45,45,27,27,90,81,72},
                new int[]{45,18,9,0,9,18,45},
                new int[]{ 27, 81, 36, 63, 63, 72, 81 } });

            result = CodeFightsUsingMono5.Fundamentals.boxBlur(new int[][] {
                new int[]{0,18,9},
                new int[]{27,9,0 },
                new int[]{81, 63, 45 } });
            /*
             image: [[0,18,9], 
 [27,9,0], 
 [81,63,45]]
Output:
[[]]
Expected Output:
[[28]]
             
             */

        }

    }
}


public class Rootobject
{
    public Input input { get; set; }
    public object output { get; set; }
}

public class Input
{
    public int[] a { get; set; }
    public int[] b { get; set; }
}
