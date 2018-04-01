using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public class Fundamentals
    {

        public static int[][] rotateImage(int[][] a)
        {
            for (int i = a.Length - 1; i >= 0; i--)
            {

            }

            return new int[][] { };
        }


        public static char firstNotRepeatingCharacter(string s)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (dic.ContainsKey(s[i])){
                    dic[s[i]] += 1;
                }
                else
                {
                    dic.Add(s[i], 1);
                }
               
            }
            var result = (from c in dic where c.Value == 1 select c.Key).ToList();
            if (result.Count > 0)
            {
                return result[0];

            }
            else
            {
                return '_';
            }
            
        }



        /// <summary>
        /// So use hashset instead of array.indexof or dictionary with double loop or even parallel for with a nested loop
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int firstDuplicate(int[] a)
        {
            if (a.Distinct().Count() == a.Length)
            {
                return -1;
            }

            HashSet<int> h = new HashSet<int>();
            //int foundKey = a.Length;
            for (int i = 0; i < a.Length; i++)
            {
              if (h.Contains(a[i]))
                {
                    return a[i];
                }
                else
                {
                    h.Add(a[i]);
                }
             

            }
            return -1;
           
         
        }

        public static bool HasDuplicate(int[] a)
        {
            bool containsDuplicates = a.Distinct().Count() != a.Length;
            return containsDuplicates;
        }


    }
}
