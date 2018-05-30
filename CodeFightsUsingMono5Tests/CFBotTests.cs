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
    public class CFBotTests
    {
        [TestMethod()]
        public void taskMakerTest()
        {
            string[] result = CodeFightsUsingMono5.CFBot.taskMaker(new string[] { "ans = 0;",
          "for (var i = 0; i < n; i++) {",
          "    for (var j = 0; j < n; j++) {",
          "    //DB 3//for (var j = 1; j < n; j++) {",
          "    //DB 2//for (var j = 0; j < n + 1; j++) {",
          "        ans++;",
          "    }",
          "}", }, 2);

            string[] expectedResult = new string[] {"ans = 0;",
          "for (var i = 0; i < n; i++) {",
          "    for (var j = 0; j < n; j++) {",
          "    //DB 3//for (var j = 1; j < n; j++) {",
          "    //DB 2//for (var j = 0; j < n + 1; j++) {",
          "        ans++;",
          "    }",
          "}" };

            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < expectedResult.Length; j++)
                {

                }
            }


        }

        [TestMethod()]
        public void plagiarismCheckTest()
        {

            bool actual = CodeFightsUsingMono5.CFBot.plagiarismCheck(
                new string[] { "def is_even_sum(a, b):",
                             "    return (a + b) % 2 == 0"},
                new string[] { "def is_even_sum(summand_1, summand_2):",
                "    return (summand_1 + summand_2) % 2 == 0"});
            Assert.IsTrue(actual);

            actual = CodeFightsUsingMono5.CFBot.plagiarismCheck(
                new string[] { "def return_smth(a, b):",
                    "  return a + a" },
                new string[]{"def return_smth(a, b):",
                    "  return a + b" });
            Assert.IsFalse(actual);

            actual = CodeFightsUsingMono5.CFBot.plagiarismCheck(
            new string[] { "if (2 * 2 == 5 &&",
             "true):",
             "  print 'Tricky test ;)'"},
            new string[] { "if (2 * 2 == 5",
             "&& true):",
             "  print 'Tricky test ;)'"});
            Assert.IsFalse(actual);

            actual = CodeFightsUsingMono5.CFBot.plagiarismCheck(new string[] {"function is_even_sum(a, b) {",
             "  return (a + b) % 2 === 0;",
             "}"},
           new string[] {"function is_even_sum(a, b) {",
             "  return (a + b) % 2 !== 1;",
             "}"});
            Assert.IsFalse(actual);

            actual = CodeFightsUsingMono5.CFBot.plagiarismCheck(new string[] {"def return_first(f, s):",
             "  t = f",
             "  return f"},
           new string[] {"def return_first(f, s):",
             "  f = f",
             "  return f"});
            Assert.IsTrue(actual);
            actual = CodeFightsUsingMono5.CFBot.plagiarismCheck(new string[] {"def f(a,b)",
             "    return a + b"},
           new string[] {"def f(b,a)",
             "    return b + a"});
            Assert.IsTrue(actual);

            actual = CodeFightsUsingMono5.CFBot.plagiarismCheckFromPython(new string[] {"def markToken(s):",
             "'*' + s"},
           new string[] {"def zmarkToken(asdfasd___):",
             "'*' + asdfasd___"});
            Assert.IsTrue(actual);

            /*
code1code1::  ["function return_four() {", 
 "  return 3 + 1;", 
 "}" 
code2: ["function return_four() {", 
 "  return 1 + 3;", 
 "}"]

false

*/
            actual = CodeFightsUsingMono5.CFBot.plagiarismCheck(new string[] {"function return_four() {",
             "  return 3 + 1;", "}" },
                    new string[] {"function return_four() {",
             "  return 1 + 3;","}"});
            Assert.IsFalse(actual);

        }

        [TestMethod()]
        public void marathonTaskScoreTest()
        {
            var actual = CodeFightsUsingMono5.CFBot.marathonTaskScore(100, 400, 4, 30);
            Assert.AreEqual(310, actual);

            actual = CodeFightsUsingMono5.CFBot.marathonTaskScore(100, 400, 95, 30);
            Assert.AreEqual(200, actual);

            actual = CodeFightsUsingMono5.CFBot.marathonTaskScore(100, 400, 95, -1);
            Assert.AreEqual(0, actual);

            actual = CodeFightsUsingMono5.CFBot.marathonTaskScore(100, 1000, 1, 99);
            Assert.AreEqual(505, actual);
            /*
             * 
             * 
marathonLengthmarath : 100
maxScore: 400
submissions: 95
successfulSubmissionTime: 30
Output:
Run the code to see output
Expected Output:
200
             * 
             * 
             * marathonLength: 100
maxScore: 400
submissions: 95
successfulSubmissionTime: -1
expeceted 0

            marathonLength: 100
maxScore: 1000
submissions: 1
successfulSubmissionTime: 99
Output:
Run the code to see output
Expected Output:
505



             * 
             * 
              marathonLength = 100,
        maxScore = 400,
        submissions = 4 and
        successfulSubmissionTime = 30, the output should be

        marathonTaskScore(marathonLength, maxScore, 
                          submissions, successfulSubmissionTime) = 310
        Three unsuccessful attempts cost 10 * 3 = 30 points. 30 minutes adds 30 * (400 / 2) * (1 / 100) = 60 more points to the total penalty. So the final score is 400 - 30 - 60 = 310.

        Keeping the same input parameters as above but changing the number of attempts to 95 we get:
        marathonTaskScore(marathonLength, maxScore, submissions, successfulSubmissionTime) = 200;

        400 - 10 * 94 - 30 * (400 / 2) * (1 / 100) = -600. But the score for this task cannot be less than 400 / 2 = 200, so the final score is 200 points.

        For marathonLength = 100, maxScore = 400, submissions = 4 and successfulSubmissionTime = -1, the output should be
        marathonTaskScore(marathonLength, maxScore, submissions, successfulSubmissionTime) = 0.

        The task wasn't solved, so it doesn't give any points.
             
             */
        }

        [TestMethod()]
        public void opponentMatchingTest()
        {

            foreach (var item in CodeFightsUsingMono5.CFBot.opponentMatching(new int[] { 200, 100, 70, 130, 100, 800, 810 }))
            {


                //foreach (var item2 in item)
                //{

                //}
            }

            /*
             * xp: [200, 100, 70, 130, 100, 800, 810]
    Output:
    Run the code to see output
    Expected Output:
    [[1,4], 
     [5,6], 
     [2,3]]


    xpxp::  [1, 1000000000][1, 10
    Output:
    Run the code to see output
    Expected Output:
    [[0,1]]

                xp: [1000000000, 100000000, 1]
    Output:
    Run the code to see output
    Expected Output:
    [[1,2]]

                xp: [239]
    Output:
    Run the code to see output
    Expected Output:
    []

                Input:
    xp: [1, 5, 11, 3, 1, 16, 100]
    Output:
    Run the code to see output
    Expected Output:
    [[0,4], 
     [1,3], 
     [2,5]]

             * 
             When you click the VS Fight button on CodeFights, the system tries to match you with the best opponent possible.
             The matching algorithm has become more complex over time,but initially it was a simple search for someone whose xp is as close to yours as possible.

            The easiest way to understand how it used to conduct the search is as follows:



            Imagine that each user looking for an opponent is standing at the center of a search circle on a horizontal xp axis.
            All the search circles have the same radius (the search radius), and initially search radius is equal to 0.
            At each step, the search radius is increased by 1.
            A match is found as soon as two search circles intersect. These circles are then removed immediately.
            For the sake of simplicity, assume that on each step no more than one pair of circles can intersect.
            Given a list of requests as user xps, match them up using the algorithm described above.

            Example

            For xp = [200, 100, 70, 130, 100, 800, 810], the output should be
            opponentMatching(xp) = [[1, 4], [5, 6], [2, 3]].

            Initially, search ranges for users 1 and 4 (these are their IDs equal to 0-based indices) coincide, so they form the first pair.
            After 5 steps search circles of users 5 and 6 intersect. Thus, they form the second pair.
            After 25 more steps search circles of users 2 and 3 intersect. Thus, they form the third pair.
            Finally, user 0 remains without an opponent.
            Input/Output

            [execution time limit] 3 seconds (cs)

            [input] array.integer xp

            Array of positive integers.
            xp[i] equals XP points earned by the user with ID = i.

            Guaranteed constraints:
            1 ≤ xp.length ≤ 15,
            1 ≤ xp[i] ≤ 109.

            [output] array.array.integer

            Array of pairs of opponents. Pairs should be stored in the same order as they were formed by the above-described algorithm. 
            Elements in pairs should be sorted according to their IDs.

                 */
        }

        [TestMethod()]
        public void shortestSolutionLengthTest()
        {
            var actual = CodeFightsUsingMono5.CFBot.shortestSolutionLength(
                new string[] { "//1//1",
                               "/*2*/",
                               "x = 2//*/"});
            Assert.AreEqual(3, actual);

            // look here: https://progzoo.net/wiki/Codefights_-_CFBot_Walkthrough#CFBot_Round_2

        }
    }
}