using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeFightsUsingMono5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodeFightsUsingMono5.Tests
{
    [TestClass()]
    public class ChallengesTests
    {
        [TestMethod()]
        public void autocompleteOriginalTest()
        {

            var result = CodeFightsUsingMono5.Challenges.autocompleteOriginal(
                arrayItems,
                new string[] {"s",
 "PAUSE",
 "w",
 "e",
 "PAUSE",
 "a",
 "PAUSE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "n",
 "PAUSE",
 "BACKSPACE",
 "c",
 "PAUSE",
 "a",
 "r",
 "PAUSE",
 "e",
 "PAUSE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "a",
 "PAUSE",
 "BACKSPACE",
 "d",
 "PAUSE"});

            string[][] expected = new string[][] {
                new string[]{"all","of","the","words"},
               };

        }

        [TestMethod()]
        public void autocompleteTest()
        {


            var result = CodeFightsUsingMono5.Challenges.autocomplete(
                arrayItems,
                new string[] {"s",
 "PAUSE",
 "w",
 "e",
 "PAUSE",
 "a",
 "PAUSE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "n",
 "PAUSE",
 "BACKSPACE",
 "c",
 "PAUSE",
 "a",
 "r",
 "PAUSE",
 "e",
 "PAUSE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "a",
 "PAUSE",
 "BACKSPACE",
 "d",
 "PAUSE"});

            string[][] expected = new string[][] {
                new string[]{"all","of","the","words"},
               };

        }

        [TestMethod()]
        public void autocompleteHighestCsharpTest()
        {
            //autocompleteHighestCsharp
            var result = CodeFightsUsingMono5.Challenges.autocompleteHighestCsharp(
                arrayItems,
                new string[] {"s",
 "PAUSE",
 "w",
 "e",
 "PAUSE",
 "a",
 "PAUSE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "n",
 "PAUSE",
 "BACKSPACE",
 "c",
 "PAUSE",
 "a",
 "r",
 "PAUSE",
 "e",
 "PAUSE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "BACKSPACE",
 "a",
 "PAUSE",
 "BACKSPACE",
 "d",
 "PAUSE"});

            // var t1 = result.AsQueryable<object>();
            var t1 = result.ToArray();

            for (int i = 0; i < t1.Length; i++)
            {




                // string[] enumerable = new string[] { (string[])t1[i]};
                //for (int j = 0; j < x1.Length; j++)
                //{

                //}
            }
        }
        List<string> items;
        string[] arrayItems;

        public ChallengesTests()
        {
            var stream = new StreamReader(@"C:\Users\xsc7529\Documents\word2.txt");
            items = new List<string>();
            while (!stream.EndOfStream)
                items.Add(stream.ReadLine());
            arrayItems = items.ToArray();
        }

        [TestMethod()]
        public void teeNineTest()
        {

            /*
             message:        "I think that is supercalifragilisticexpialidocious."
            Expected Output: "g thmj tgat gp ptpdpbjgdpahjgptgadwpgajgdmagmtp."
             */
            string actual = CodeFightsUsingMono5.Challenges.teeNine("I think that is supercalifragilisticexpialidocious.");
            string expected = "g thmj tgat gp ptpdpbjgdpahjgptgadwpgajgdmagmtp.";
            Assert.AreEqual(expected, actual);

            /*
            message: "hii badelkjiggedtvted ghion zwymnout"
            Expected Output:"i believe in you"
             */
            actual = CodeFightsUsingMono5.Challenges.teeNine("hii badelkjiggedtvted ghion zwymnout");
            expected = "i believe in you";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void decodeStringTest()
        {
            var actual = CodeFightsUsingMono5.Challenges.decodeString("4[ab]");
            Assert.AreEqual("abababab", actual);
            actual = CodeFightsUsingMono5.Challenges.decodeString("2[b3[a]]");
            Assert.AreEqual("baaabaaa", actual);
            actual = CodeFightsUsingMono5.Challenges.decodeString("z1[y]zzz2[abc]");
            Assert.AreEqual("zyzzzabcabc", actual);
            actual = CodeFightsUsingMono5.Challenges.decodeString("2[2[2[b]]]");
            Assert.AreEqual("bbbbbbbb", actual);
            actual = CodeFightsUsingMono5.Challenges.decodeString("2[a]10[b]4[c]5[d]");
            Assert.AreEqual("aabbbbbbbbbbccccddddd", actual);

            actual = CodeFightsUsingMono5.Challenges.decodeString("sd2[f2[e]g]i");
            Assert.AreEqual("sdfeegfeegi", actual);
            actual = CodeFightsUsingMono5.Challenges.decodeString("codefights");
            Assert.AreEqual("codefights", actual);

            /*
             * 
             * 
ss::  "sd2[f2[e]g]i""sd2[f
Output:
"sdfeefee"
Expected Output:
"sdfeegfeegi"

            s: "codefights"
Output:
"codefight"
Expected Output:
"codefights"
            * Given an encoded string, return its corresponding decoded string.
            The encoding rule is: k[encoded_string], where the encoded_string inside the square brackets is repeated exactly k times. 
            Note: k is guaranteed to be a positive integer.
            Note that your solution should have linear complexity because this is what you will be asked during an interview.

            Example
            For s = "4[ab]", the output should be
            decodeString(s) = "abababab";
            For s = "2[b3[a]]", the output should be
            decodeString(s) = "baaabaaa";
            For s = "z1[y]zzz2[abc]", the output should be
            decodeString(s) = "zyzzzabcabc".
            s: "2[2[2[b]]]"
            Output:
            Run the code to see output
            Expected Output:
            "bbbbbbbb"
            s: "2[a]3[b]4[c]5[d]"
            Output:
            Run the code to see output
            Expected Output:
            "aabbbccccddddd"
       */
        }
    }

}