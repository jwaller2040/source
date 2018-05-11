using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public class HashTables
    {


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
