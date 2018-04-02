using CodeFightsUsingMono5;
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace CodeFightsUsingMono5.Tests
{
    [TestClass()]
    public class UnitTest1
    {

        static int[] testList;
        static Rootobject codefightTestData;
        public UnitTest1()
        {
            Random rand = new Random();
            List<int> result = new List<int>();

            for (Int32 i = 0; i < 100000; i++)
            {
                int curValue = rand.Next(1, 100000);

                result.Add(curValue);

            }

            testList = result.ToArray();


            codefightTestData = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(@"C:\Users\xsc7529\Downloads\test-22.json"));


        }

        [TestMethod]
        public void cryptoTest1()
        {
            Assert.AreEqual(true, Fundamentals.isCryptSolution(
                new string[] {"AAAAAAAAAAAAAA", "BBBBBBBBBBBBBB","CCCCCCCCCCCCCC"}, 
                new char[][] {
                    new char[]{'O', '0'}, new char[]{'M','1'},new char[] {'Y','2'},
                    new char[]{'E','5'},new char[] {'N','6'}, new char[]{'D','7'},
                    new char[] {'R','8'}, new char[] {'S','9'} }));

//            crypt: ["SEND",
// "MORE",
// "MONEY"]
//solution: {{'O', '0'}, 
// {'M','1'}, 
// {'Y','2'}, 
// {'E','5'}, 
// {'N','6'}, 
// {'D','7'}, 
// {'R','8'}, 
// {'S','9'}}

        }

        [TestMethod]//[Timeout(2500)]
        public void sudoku2Test1()
        {
            char[][] testData = new char[][]{
                    new char[] { '.','.','5','.','.','.','.','.','.'},
                    new char[] { '.','.','.','8','.','.','.','3','.'},
                    new char[] { '.','5','.','.','2','.','.','.','.'},
                    new char[] { '.','.','.','.','.','.','.','.','.'},
                    new char[] { '.','.','.','.','.','.','.','.','9'},
                    new char[] { '.','.','.','.','.','.','4','.','.'},
                    new char[] { '.','.','.','.','.','.','.','.','7'},
                    new char[] { '.','1','.','.','.','.','.','.','.'},
                    new char[] { '2','4','.','.','.','.','9','.','.'}};

            Assert.AreEqual(false, Fundamentals.sudoku2(testData));
        }

       

        [TestMethod]
        [Timeout(2500)]
        public void rotateImageTest1()
        {
 //[[1,2,3], 
 //[4,5,6], 
 //[7,8,9]]
 //expecting
 //[[7,4,1], 
 // [8,5,2], 
 //[9,6,3]]

            int[][] result = Fundamentals.rotateImage(new int[][] { new int[] { 1,2,3}, new int[] { 4, 5, 6 }, new int[] { 7,8,9 } });
            int[][] expectingResult = new int[][] { new int[] { 7, 4, 1 }, new int[] { 8, 5, 2 }, new int[] { 9, 6, 3 } };
            //CollectionAssert.AreEqual(new int[][] { new int[] { 7, 4, 1 }, new int[] { 8, 5, 2 }, new int[] { 9, 6, 3 } }, result);

            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result[i].Length; j++)
                {
                    if (result[i][j] != expectingResult[i][j])
                    {
                        Assert.IsFalse(true);
                    }
                }

            }

            Assert.IsTrue(true);

        }

        [TestMethod()]
        [Timeout(2500)]
        public void firstDuplicateTest()
        {
            int result = Fundamentals.firstDuplicate(codefightTestData.input.a);
             Assert.AreEqual(-1, result); //for test 22
            // Assert.AreEqual(33978, result); //for test 21

        }
        [TestMethod][Timeout(2500)]
        public void firstNotRepeatingTest1()
        {
            char result = Fundamentals.firstNotRepeatingCharacter("abcdefghihklmnop");
            char r = result;
            Assert.IsTrue(true);
        }

        [TestMethod]
        [Timeout(3000)]
        public void firstDulicateLinqTest2()
        {
            bool result = Fundamentals.HasDuplicate(codefightTestData.input.a);
            Assert.IsTrue(result);
        }
    }
}

namespace UnitTestCodeFightMono
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [Timeout(3000)]
        public void TestMethod1()
        {
            CollectionAssert.AreEqual(resultTest, CodeFightsUsingMono5.TreeClass.countAPI(new string[] { "/project1/subproject1/method1", "/project2/subproject1/method1", "/project1/subproject1/method1", "/project1/subproject2/method1", "/project1/subproject1/method2", "/project1/subproject2/method1", "/project2/subproject1/method1", "/project1/subproject2/method1" }));


        }

        private static readonly string[] resultTest = { "--project1 (6)", "----subproject1 (3)", "------method1 (2)", "------method2 (1)", "----subproject2 (3)", "------method1 (3)", "--project2 (2)", "----subproject1 (2)", "------method1 (2)" };
        [TestMethod]
        [Timeout(3000)]
        public void TestMethod2()
        {
            CollectionAssert.AreEqual(resultTest2, CodeFightsUsingMono5.TreeClass.countAPI(new string[] { "/project5/subproject2/method5", "/project3/subproject5/method4", "/project4/subproject1/method4", "/project4/subproject3/method3", "/project5/subproject1/method2", "/project2/subproject2/method1", "/project3/subproject1/method4", "/project1/subproject2/method5", "/project2/subproject1/method2", "/project1/subproject3/method3", "/project2/subproject1/method8", "/project3/subproject3/method3", "/project1/subproject1/method10", "/project1/subproject3/method3", "/project2/subproject1/method9", "/project5/subproject2/method3", "/project5/subproject2/method5", "/project5/subproject1/method7", "/project5/subproject1/method9", "/project1/subproject1/method5" }));
        }

        private static readonly string[] resultTest2 = {"--project5 (6)",
 "----subproject2 (3)",
 "------method5 (2)",
 "------method3 (1)",
 "----subproject1 (3)",
 "------method2 (1)",
 "------method7 (1)",
 "------method9 (1)",
 "--project3 (3)",
 "----subproject5 (1)",
 "------method4 (1)",
 "----subproject1 (1)",
 "------method4 (1)",
 "----subproject3 (1)",
 "------method3 (1)",
 "--project4 (2)",
 "----subproject1 (1)",
 "------method4 (1)",
 "----subproject3 (1)",
 "------method3 (1)",
 "--project2 (4)",
 "----subproject2 (1)",
 "------method1 (1)",
 "----subproject1 (3)",
 "------method2 (1)",
 "------method8 (1)",
 "------method9 (1)",
 "--project1 (5)",
 "----subproject2 (1)",
 "------method5 (1)",
 "----subproject3 (2)",
 "------method3 (2)",
 "----subproject1 (2)",
 "------method10 (1)",
 "------method5 (1)" };

        [TestMethod]
        [Timeout(3000)]
        public void TestChatBot()
        {
            CollectionAssert.AreEqual(resultChat, CodeFightsUsingMono5.ChatBotChallenge.chatBot(new string[][] { new string[] { "where", "are", "you", "live", "i", "live", "in", "new", "york" }, new string[] { "are", "you", "going", "somewhere", "tonight", "no", "i", "am", "too", "tired", "today" }, new string[] { "hello", "what", "is", "your", "name", "my", "name", "is", "john" } }, new string[] { "hello", "john", "do", "you", "have", "a", "favorite", "city", "to", "live", "in", "yes", "it", "is" }));


        }
        private static readonly string[] resultChat = { "hello", "john", "do", "you", "have", "a", "favorite", "city", "to", "live", "in", "yes", "it", "is", "new", "york" };


        [TestMethod]
        [Timeout(3000)]
        public void TestChatBot4()
        {
            CollectionAssert.AreEqual(resultChat4, CodeFightsUsingMono5.ChatBotChallenge.chatBot(new string[][] { new string[] { "tonight", "i", "need", "dollar", "bills" }, new string[] { "i","dont","keep","fun"},
 new string[] { "cheap","thrills","long","to","feel","money"},
 new string[] { "the","bills","dont","need","the","dancing","baby"},
  new string[] {"fun","dollar","dancing","thrills","the","baby","i","need"},
 new string[] { "dont", "have", "fun" },
 new string[] { "no", "no", "dont", "have", "dancing", "fun", "tonight" }}, new string[] { "beat", "the", "can", "as", "i", "dont", "feel", "thrills" }));

        }
        private static readonly string[] resultChat4 = { "beat",
 "the",
 "can",
 "as",
 "i",
 "dont",
 "feel",
 "thrills",
 "need" };


        [TestMethod]
        [Timeout(3000)]
        public void TestChatBotTesting()
        {
            CollectionAssert.AreEqual(resultChatEasy, CodeFightsUsingMono5.ChatBotChallenge.chatBot(new string[][] { new string[] { "ab", "cd" }, new string[] { "ef", "gh" } },
                new string[] { "ab", "cd", "ef" }));


        }
        private static readonly string[] resultChatEasy = { "ab", "cd", "ef" };

        [TestMethod]
        public void TestRoadmap4()
        {
            string[][] returnResults = CodeFightsUsingMono5.Roadmap.roadmap(new string[][] { new string[] {"LNWBN", "2017-08-13", "2017-12-24", "Corey", "Kyle", "Kaleb", "Reuben" },
new string[] {"NSXEN","2017-08-20","2017-09-18","Kai"},
new string[] {"DNMDC","2017-06-19","2017-08-07","Kaleb","Kai","Kyle","Reuben"},
new string[] {"UYWEQ","2017-04-23","2017-07-18","Corey","Kyle","Reuben","Kai"},
new string[] {"LIVNH","2017-11-01","2017-12-24","Kaleb","Kai"}},
new string[][] { new string[] { "Corey","2017-10-21" },
new string[] { "Reuben","2017-03-16"},
 new string[] { "Kaleb","2017-11-22"},
 new string[] { "Kaleb","2017-03-22"},
 new string[] { "Reuben","2017-10-06"}});
            //Output:
            //[["LNWBN"], 
            // [], 
            // ["LNWBN","LIVNH"], 
            // [], 
            // ["LNWBN"]]
            //Expected Output:
            //[["LNWBN"], 
            // [], 
            // ["LIVNH","LNWBN"], 
            // [], 
            //["LNWBN"]]

            string[][] resultTest = new string[][] { new string[] { "LNWBN" }, new string[] { }, new string[] { "LIVNH", "LNWBN" }, new string[] { }, new string[] { "LNWBN" } };
            //[[],  ["HRCPX"], ["QOEHU"],  ["HRCPX"],  []]
            for (int i = 0; i < resultTest.Length; i++)
            {
                for (int j = 0; j < returnResults[i].Length; j++)
                {
                    if (resultTest[i][j] != returnResults[i][j])
                    {
                        Assert.IsFalse(true);
                    }
                }

            }

            Assert.IsTrue(true);

        }



        [TestMethod]
        public void TestRoadmap3()
        {
            string[][] returnResults = CodeFightsUsingMono5.Roadmap.roadmap(
          new string[][] { new string[] {"RXGWB", "2017-10-10", "2017-12-09", "Kyle"},
              new string[] {"QOEHU","2017-08-25","2017-12-11","Corey","Kai","Kaleb","Reuben"},
              new string[] {"HRCPX","2017-04-04","2017-07-21","Corey","Jenson","Kyle"},
              new string[] {"SQFYX","2017-07-07","2017-12-02","Reuben","Kaleb","Kai","Kyle"},
              new string[] {"BIDVM","2017-04-20","2017-12-08","Kaleb"}},
            new string[][] { new string[] {"Reuben","2017-06-09"},
              new string[] {"Jenson","2017-04-13"},
              new string[] {"Corey","2017-12-01"},
              new string[] {"Jenson","2017-05-23"},
              new string[] {"Corey","2017-08-19"} });

            string[][] resultTest = new string[][] { new string[] { }, new string[] { "HRCPX" }, new string[] { "QOEHU" }, new string[] { "HRCPX" }, new string[] { } };
            //[[],  ["HRCPX"], ["QOEHU"],  ["HRCPX"],  []]
            for (int i = 0; i < resultTest.Length; i++)
            {
                for (int j = 0; j < returnResults[i].Length; j++)
                {
                    if (resultTest[i][j] != returnResults[i][j])
                    {
                        Assert.IsFalse(true);
                    }
                }

            }

            Assert.IsTrue(true);
        }
        [TestMethod]
        public void TestRoadmap()
        {

            string[][] returnResults = CodeFightsUsingMono5.Roadmap.roadmap(
                new string[][] {
                    new string[] { "A", "2017-02-01", "2017-03-01", "Sam", "Evan", "Troy" },
                    new string[] { "B", "2017-01-16", "2017-02-15", "Troy" },
                    new string[] { "C", "2017-02-13", "2017-03-13", "Sam", "Evan" } },
                    new string[][] {
                        new string[] { "Evan", "2017-03-10" },
                        new string[] { "Troy", "2017-02-04" } });

            string[][] resultTest = new string[][] { new string[] { "C" }, new string[] { "B", "A" } };

            for (int i = 0; i < resultTest.Length; i++)
            {
                for (int j = 0; j < returnResults[i].Length; j++)
                {
                    if (resultTest[i][j] != returnResults[i][j])
                    {
                        Assert.IsFalse(true);
                    }
                }

            }

            Assert.IsTrue(true);
            ////string[][] returnResults = new string[][] { new string[] { "C" }, new string[] { "B", "A" } };
            ////string[][] resultTest = new string[][] { new string[] { "C" }, new string[] { "B", "A" } };
            //var equal =returnResults.Rank == resultTest.Rank && Enumerable.Range(0, returnResults.Rank).All(dimension => 
            //returnResults.GetLength(dimension) == resultTest.GetLength(dimension)) &&  returnResults.Cast<string[]>().SequenceEqual(resultTest.Cast<string[]>());


            //Assert.IsFalse(equal);
            //// A similar array with string elements.

            // A similar array with string elements.

        }
        //private static readonly string[][] resultRoadmap = { {"C"}, {"B","A"} };
        public static readonly int[,] i = { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };


        //CodeWarsBeta

        [TestMethod]
        //[Timeout(3000)]
        public void Test1CodeWarsBeta()
        {
            Assert.AreEqual(10, CodeFightsUsingMono5.CodeWarsBeta.GetScienceScore("CGT"), "CGT failed");

        }

        [TestMethod]
        public void TestOtherExample()
        {
            Assert.AreEqual(25, CodeFightsUsingMono5.CodeWarsBeta.GetScienceScore("TC"), "TC failed");
        }

    }

    //[TestFixture]
    //public class Tests
    //{
    //    [Test]
    //    public void TestSingleSet()
    //    {
    //        Assert.AreEqual(10, SevenWondersScorer.GetScienceScore("CGT"), "CGT failed");
    //    }

    //    [Test]
    //    public void TestFirstExample()
    //    {
    //        Assert.AreEqual(43, SevenWondersScorer.GetScienceScore("CCCCGGGTT"), "Empty string means 0 science score");
    //    }

    //    [Test]
    //    public void TestOtherExample()
    //    {
    //        Assert.AreEqual(31, SevenWondersScorer.GetScienceScore("CCCGGTW"), "CCCGGTW failed");
    //    }
    //}
}

public class Rootobject
{
    public Input input { get; set; }
    public int output { get; set; }
}

public class Input
{
    public int[] a { get; set; }
}
