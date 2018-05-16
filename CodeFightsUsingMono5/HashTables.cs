using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public class HashTables
    {

        public static bool stringsRearrangement(string[] inputArray)
        {
            prune(inputArray);
            index = 0;
            dic = new Dictionary<int, HashSet<string>>();
            //load them!
            for (index = 0; index < inputArray.Length; index++)
            {
                dic.Add(index, new HashSet<string>());
                dic[index].Add(inputArray[index]);
                //permute(inputArray[index]);
            }
            //compare all of them
           
            bool first = true;

            //TO DO search through dic to find matches
            // show key and link to what other key
            // like key1 to key 3 to key2 to key 4
            /*
             collect keey
             */
            var _ = Enumerable.Range(0, dic.Count).ToArray();
            var mutationsPointer = GetPermutations(_, dic.Count);
            var tempH = new HashSet<string>();
            foreach (var keyPermutation in mutationsPointer)
            {
                if (tempH.Count > 0)
                {
                    return true;
                }
                if (keyPermutation == null)
                {
                    continue;
                }
                List<int> kp = keyPermutation.ToList();
               
                first = true;
                for (int i = 1; i < kp.Count; i++)
                {
                    var h0 = dic[kp[i-1]];
                    if (!first)
                    {
                        if (tempH.Count > 0)
                        {
                            h0.Clear();
                            foreach (var item in tempH)
                            {
                                h0.Add(item);
                            }
                            tempH.Clear();
                        }
                        else
                        {
                            i = kp.Count;
                            continue;
                        }
                    }

                    first = false;
                    var h1 = dic[kp[i]];

                    foreach (var string1 in h0)
                    {
                        foreach (var string2Link in h1)
                        {
                            if (CompareStrings(string1, string2Link))
                            {
                                tempH.Add(string2Link);
                                //links with 0 and 1 now st
                            }
                        }
                    }

                }
              

            }
            if (tempH.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }
        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }


        private static void prune(string[] inputArray)
        {
            StringBuilder sb = new StringBuilder();
            if (inputArray[0].Length > 2)
            {
                bool found = false;
                for (int i = 1; i < inputArray.Length; i++)
                {
                    if (found)
                    {
                        i = inputArray.Length;
                        continue;
                    }
                    for (int j = 0; j < inputArray[i].Length; j++)
                    {
                        if (inputArray.Length <= (i + 1))
                        {

                        }
                        if (inputArray[i - 1][j] == inputArray[i][j])//  && inputArray[i][j] == inputArray[i + 1][j])
                        {
                            sb.Append(inputArray[i][j]);
                        }
                        else
                        {
                            j = inputArray[i].Length;
                            found = true;

                        }

                    }


                }
                if (sb.Length > 2 && sb.Length < inputArray[0].Length)
                {
                    for (int i = 0; i < inputArray.Length; i++)
                    {
                        inputArray[i] = inputArray[i].Substring(sb.Length - 1);
                    }
                    bool revFound = false;
                    sb.Clear();
                    for (int i = inputArray.Length - 2; i >= 0; i--)
                    {
                        if (revFound)
                        {
                            i = -1;
                            continue;
                        }
                        for (int j = inputArray[i].Length - 1; j >= 0; j--)
                        {
                            //(inputArray[i - 1][j] == inputArray[i][j] && inputArray[i][j] == inputArray[i + 1][j])
                            if (inputArray[i][j] == inputArray[i + 1][j])
                            {
                                sb.Insert(0, inputArray[i][j]);
                            }
                            else
                            {
                                j = -1;
                                revFound = true;

                            }
                        }
                    }
                    if (sb.Length > 2 && sb.Length < inputArray[0].Length)
                    {
                        for (int i = 0; i < inputArray.Length; i++)
                        {
                            inputArray[i] = inputArray[i].Substring(0, sb.Length - 2);
                        }
                    }

                }
            }

        }

        static int index = 0;
        static Dictionary<int, HashSet<string>> dic = new Dictionary<int, HashSet<string>>();

        static bool CompareStrings(string a, string b)
        {

            int sum = 0, len = a.Length < b.Length? a.Length : b.Length;
            for (int i = 0; i < len; i++)
                if (a[i] != b[i]) sum++;
            return sum ==1;
        }

        private static void permute(string str)
        {
            permute("", str);
        }

        private static void permute(string prefix, string s)
        {
            int length = s.Length;
            if (length == 0)
            {
                dic[index].Add(prefix);
                //add here
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    permute(prefix + s[i], s.Substring(0, i) + s.Substring(i + 1, length - (i + 1)));
                }
            }
        }
        public static int possibleSums(int[] coins, int[] quantity)
        {
            HashSet<int> h = new HashSet<int>();

            calculateCombinations(0, quantity, new int[coins.Length], coins, h);

            return h.Count;
        }

        static void calculateCombinations(int index, int[] maxValues, int[] currentCombo, int[] values, HashSet<int> htest)
        {


            for (int count = 0; count <= maxValues[index]; count++)
            {
                currentCombo[index] = count;
                if (index < currentCombo.Length - 1)
                {
                    calculateCombinations(index + 1, maxValues, currentCombo, values, htest);
                }
                else
                {
                    int sum = Sum(currentCombo, values);
                    if (sum > 0)
                    {
                        htest.Add(sum);
                        int[] copy = new int[currentCombo.Length];
                        Array.Copy(currentCombo, copy, copy.Length);
                        //results.Add(copy);
                    }
                }
            }
        }

        static int Sum(int[] combo, int[] values)
        {
            int sum = 0;
            for (int i = 0; i < combo.Length; i++)
            {
                sum += combo[i] * values[i];
            }
            return sum;
        }



        //int outerTotal = 0;

        //for (int outerCoin = 0; outerCoin < coins.Length; outerCoin++)
        //{
        //    outerTotal += coins[outerCoin]; //10 + 50 // 10 + 50 +100   
        //    /*
        //     covers 10, 50, 100, 60, 160
        //     */
        //    h.Add(outerTotal);
        //    h.Add(coins[outerCoin]);//single 10, 50, 100

        //    int innerTotal = coins[outerCoin];
        //    int innerTotalQuanity = 0;
        //    for (int innerCoin = 0; innerCoin < coins.Length; innerCoin++)
        //    {
        //        /*
        //    covers 10+ 50, 10+ 100
        //           50+100
        //         */

        //        if (coins[outerCoin] != coins[innerCoin])
        //        {
        //            innerTotal += coins[innerCoin];
        //            h.Add(innerTotal);
        //            h.Add(coins[outerCoin] + coins[innerCoin]);


        //            /*
        //             look at quanity list of each item starting with outer    

        //            */
        //            int n = 1;
        //            while (quantity[outerCoin] >= n)
        //            {
        //                if (n > 1)
        //                {
        //                    innerTotal += coins[outerCoin];
        //                }
        //                h.Add(coins[outerCoin] * n);
        //                h.Add(coins[outerCoin] * n + coins[innerCoin]);
        //                int innN = 1;
        //                while (quantity[innerCoin] >= innN)
        //                {
        //                    if (innN > 1)
        //                    {
        //                        innerTotalQuanity = coins[outerCoin] * n + coins[innerCoin] * innN;
        //                    }
        //                    innerTotalQuanity = coins[outerCoin] * n + coins[innerCoin] * innN;
        //                    h.Add(coins[outerCoin] * n + coins[innerCoin] * innN);
        //                    h.Add(innerTotalQuanity);
        //                    innN++;
        //                }
        //                n++;
        //            }



        //        }

        //    }

        //}



        //    return h.Count;

        //}




        public static bool containsCloseNums(int[] nums, int k)
        {
            Dictionary<int, int> n = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (n.ContainsKey(nums[i]))
                {
                    if (i - n[nums[i]] <= k)
                    {
                        return true;
                    }
                    else
                    {
                        n[nums[i]] = i;
                    }
                }
                else
                {
                    n.Add(nums[i], i);
                }
            }

            return false;

        }


        public static bool areFollowingPatterns(string[] strings, string[] patterns)
        {

            HashSet<string> s = new HashSet<string>();
            HashSet<string> p = new HashSet<string>();

            for (int i = 0; i < strings.Length; i++)
            {
                if (s.Contains(strings[i]) && p.Contains(patterns[i]))
                {
                    continue;
                }
                else if (!s.Contains(strings[i]) && !p.Contains(patterns[i]))
                {
                    s.Add(strings[i]);
                    p.Add(patterns[i]);
                }
                else
                {
                    return false;
                }

                if (i + 1 >= strings.Length)
                {
                    return true;
                }

                if (strings[i] == strings[i + 1] && patterns[i] == patterns[i + 1])
                {
                    continue;
                }

                if (strings[i] != strings[i + 1] && patterns[i] != patterns[i + 1])
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public static string[][] groupingDishes(string[][] dishes)
        {
            SortedSet<string> ingredients = new SortedSet<string>(StringComparer.Ordinal);
            Dictionary<string, HashSet<string>> dic = new Dictionary<string, HashSet<string>>();
            for (int i = 0; i < dishes.Length; i++)
            {
                for (int j = 1; j < dishes[i].Length; j++)
                {
                    ingredients.Add(dishes[i][j]);
                    if (dic.ContainsKey(dishes[i][0]))
                    {
                        dic[dishes[i][0]].Add(dishes[i][j]);
                    }
                    else
                    {
                        dic.Add(dishes[i][0], new HashSet<string>() { dishes[i][j] });
                    }
                }


            }
            List<string[]> returnValues = new List<string[]>();
            foreach (var item in ingredients)
            {
                var matchingKeys = dic.Where(kvp => kvp.Value.Contains(item)).OrderBy(kvp => kvp.Key, StringComparer.Ordinal).Select(kvp => kvp.Key).ToList();
                if (matchingKeys != null && matchingKeys.Count > 1)
                {
                    matchingKeys.Insert(0, item);

                    returnValues.Add(matchingKeys.ToArray());
                }
            }
            //returnValues.Sort(StringComparer.Ordinal);

            return returnValues.ToArray();
            //string[][] dishes = new string[][]{ new string[] { "Salad", "Tomato", "Cucumber", "Salad", "Sauce" },
            //new string[] {"Pizza", "Tomato", "Sausage", "Sauce", "Dough"},
            //new string[] { "Quesadilla", "Chicken", "Cheese", "Sauce"},
            //new string[] { "Sandwich", "Salad", "Bread", "Tomato", "Cheese"}};

            /*
            groupingDishes(dishes) = [["Cheese", "Quesadilla", "Sandwich"],
                            ["Salad", "Salad", "Sandwich"],
                            ["Sauce", "Pizza", "Quesadilla", "Salad"],
                            ["Tomato", "Pizza", "Salad", "Sandwich"]]
             */



        }

    }
}
