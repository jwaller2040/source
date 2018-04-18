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
    public class PenteTests
    {
        [TestMethod()]
        public void penteTest()
        {
            var pen = new Pente();
            int[][] playerOne;
            int[][] playerTwo;
            int[][] expected;
            int[][] result;
            playerOne = new int[][] {
                new int[] { 7,7 }, new int[] { 7,8},
                new int[] { 7,9}, new int[] { 10,9} };
            playerTwo = new int[][] {
                new int[] { 6,8 }, new int[] { 8,7},
                new int[] { 9,8}};
            /*
             * playerOne: [[7,7], 
 [7,8], 
 [7,9], 
 [10,9]]
playerTwo: [[6,8], 
 [8,7], 
 [9,8]]
Output:
Empty
Expected Output:
[[7,6], 
 [7,10]]
             * */
           expected = new int[][] {new int[] { 7,6 }, new int[] { 7,10}};
          // result = pen.pente(playerOne,playerTwo);
          var r = pen.pente(playerOne, playerTwo);

            //for (int i = 0; i < expected.Length; i++)
            //{
            //    Assert.IsTrue(expected[i][0] == result[i][0] && expected[i][1] == result[i][1]);
            //}
            Console.Write(r);
            Console.ReadKey();
        }
    }
}