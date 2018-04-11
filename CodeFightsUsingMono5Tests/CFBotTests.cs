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

            bool result = CodeFightsUsingMono5.CFBot.plagiarismCheck(
                new string[] { "def is_even_sum(a, b):",
                             "    return (a + b) % 2 == 0"}, 
                new string[] { "def is_even_sum(summand_1, summand_2):",
                "    return (summand_1 + summand_2) % 2 == 0"});
            Assert.IsTrue(result);

            result = CodeFightsUsingMono5.CFBot.plagiarismCheck(
                new string[] { "def return_smth(a, b):",
                    "  return a + a" },
                new string[]{"def return_smth(a, b):",
                    "  return a + b" });
            Assert.IsFalse(result);

            result = CodeFightsUsingMono5.CFBot.plagiarismCheck(
            new string[] { "if (2 * 2 == 5 &&",
             "true):",
             "  print 'Tricky test ;)'"},
            new string[] { "if (2 * 2 == 5",
             "&& true):",
             "  print 'Tricky test ;)'"});
            Assert.IsFalse(result);

            result = CodeFightsUsingMono5.CFBot.plagiarismCheck(new string[] {"function is_even_sum(a, b) {",
             "  return (a + b) % 2 === 0;",
             "}"},
           new string[] {"function is_even_sum(a, b) {",
             "  return (a + b) % 2 !== 1;",
             "}"});
            Assert.IsFalse(result);

            result = CodeFightsUsingMono5.CFBot.plagiarismCheck(new string[] {"def return_first(f, s):",
             "  t = f",
             "  return f"},
           new string[] {"def return_first(f, s):",
             "  f = f",
             "  return f"});
            Assert.IsTrue(result);
            result = CodeFightsUsingMono5.CFBot.plagiarismCheck(new string[] {"def f(a,b)",
             "    return a + b"},
           new string[] {"def f(b,a)",
             "    return b + a"});
            Assert.IsTrue(result);
        }
    }
}