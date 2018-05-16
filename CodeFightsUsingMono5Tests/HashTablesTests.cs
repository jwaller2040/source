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
    public class HashTablesTests
    {
        /*
        Given an array of equal-length strings, check if it is possible to rearrange the strings in 
        such a way that after the rearrangement the strings at consecutive positions would differ by exactly one character.
        Example

        For inputArray = ["aba", "bbb", "bab"], the output should be
        stringsRearrangement(inputArray) = false;

        All rearrangements don't satisfy the description condition.
        For inputArray = ["ab", "bb", "aa"], the output should be
        stringsRearrangement(inputArray) = true.

        Strings can be rearranged in the following way: "aa", "ab", "bb".
        Input/Output
        [input] array.string inputArray
        Guaranteed constraints:
        2 ≤ inputArray.length ≤ 10,
        1 ≤ inputArray[i].length ≤ 15.
             */
        [TestMethod()]
        public void stringsRearrangementTest()
        {
            Assert.AreEqual(false, CodeFightsUsingMono5.HashTables.stringsRearrangement(
                new string[] { "aaaaab", "aaaaab", "aaaaab" }));
            Assert.AreEqual(true, CodeFightsUsingMono5.HashTables.stringsRearrangement(
                new string[] { "ab", "bb", "aa" }));
            Assert.AreEqual(false, CodeFightsUsingMono5.HashTables.stringsRearrangement(
                new string[] { "aba", "bbb", "bab" }));
            Assert.AreEqual(true, CodeFightsUsingMono5.HashTables.stringsRearrangement(
                new string[] { "zzzabzczaba", "zzzabzczaaa", "zzzabzczabb" , "zzzabzczbbb"}));
            /*
             ["zzzabzczaba",
"zzzabzczaaa",
"zzzabzczabb",
"zzzabzczbbb"] = true

["abacabaabczzzzz",
"abacababefzzzzz",
"abacababcczzzzz",
"abacababeczzzzz",
"abacababbczzzzz",
"abacababdczzzzz"] = true

["abc",
"xbc",
"axc",
"abx"] = false
             
             */
            Assert.AreEqual(true, CodeFightsUsingMono5.HashTables.stringsRearrangement(
              new string[] { "abacabaabczzzzz", "abacababefzzzzz", "abacababcczzzzz", "abacababeczzzzz" , "abacababbczzzzz", "abacababdczzzzz" }));
            Assert.AreEqual(false, CodeFightsUsingMono5.HashTables.stringsRearrangement(
               new string[] { "abc", "xbc", "axc", "abx" }));

        }
    }
}