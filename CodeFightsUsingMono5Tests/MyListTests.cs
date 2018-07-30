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
    public class MyListTests
    {
        [TestMethod()]
        public void findSubstringsTest()
        {
            var actual = CodeFightsUsingMono5.MyList<int>.findSubstrings(new string[] {"Apple",
 "Melon",
 "Orange",
 "Watermelon"}, new string[] {"a",
 "mel",
 "lon",
 "el",
 "An" });
            foreach (var item in actual)
            {

            }
        }
    }
}