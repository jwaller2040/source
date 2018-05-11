using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeFightsUsingMono5
{

    public partial class Fundamentals
    {


        /*
        Last night you partied a little too hard. Now there's a black and white photo of you that's about to go viral! You can't let this ruin your reputation, 
        so you want to apply the box blur algorithm to the photo to hide its content.

        The pixels in the input image are represented as integers. The algorithm distorts the input image in the following way: 
        Every pixel x in the output image has a value equal to the average value of the pixel values from the 3 × 3 square that has its center at x, 
        including x itself. All the pixels on the border of x are then removed.
        Return the blurred image as an integer, with the fractions rounded down.
        Example
        For
        image = [[1, 1, 1], 
                 [1, 7, 1], 
                 [1, 1, 1]]
        the output should be boxBlur(image) = [[1]].

        To get the value of the middle pixel in the input 3 × 3 square: (1 + 1 + 1 + 1 + 7 + 1 + 1 + 1 + 1) = 15 / 9 = 1.66666 = 1. The border pixels are cropped from the final result.
        For
        image = [[7, 4, 0, 1], 
                 [5, 6, 2, 2], 
                 [6, 10, 7, 8], 
                 [1, 4, 2, 0]]
        the output should be
        boxBlur(image) = [[5, 4], 
                          [4, 4]]
        There are four 3 × 3 squares in the input image, so there should be four integers in the blurred output. 
        To get the first value: (7 + 4 + 0 + 5 + 6 + 2 + 6 + 10 + 7) = 47 / 9 = 5.2222 = 5. 
        The other three integers are obtained the same way, then the surrounding integers are cropped from the final result.
             
             */
        public static int[][] boxBlur(int[][] image)
        {
            int row = 0, column = 0;
            List<List<int>> listOfblurred = new List<List<int>>();
           bool foundAllSquares = false; 
            while (!foundAllSquares)
            {
               var _ =   image[row][column];
                listOfblurred.Add(new List<int>());
               if (image[row].Length >= 3)
                {
                   
                    bool foundAllColumns = false;
                    while (!foundAllColumns)
                    {
                        List<int> calc = new List<int>();
                        for (int i = 0; i < 3; i++)
                        {

                            calc.Add(image[row + i][column]);
                            calc.Add(image[row + i][column + 1]);
                            calc.Add(image[row + i][column + 2]);

                        }
                        listOfblurred[row].Add(calc.Sum() / 9);
                        column++;
                        if (image[row].Length - column < 3)
                        {
                            foundAllColumns = true;
                            column = 0;
                        }

                    }

                }

                row++;
                if (image.Length - row < 3)
                {
                    foundAllSquares = true;
                }
                
            }


            return listOfblurred.Select(l => l.ToArray()).ToArray();
        }
        /*
         * (36+0+18+27+54+9+81+63+72)/9 = 40
         * 9-45 
           * image: [[36,0,18,9], 
                   [27,54,9,0], 
                   [81,63,72,45]]
                  Output:
                  Run the code to see output
                  Expected Output:
                  [[40,30]]
                               * 
                               * 
                               Test 5
                  Input:
                  image: [[36,0,18,9,9,45,27], 
                   [27,0,54,9,0,63,90], 
                   [81,63,72,45,18,27,0], 
                   [0,0,9,81,27,18,45], 
                   [45,45,27,27,90,81,72], 
                   [45,18,9,0,9,18,45], 
                   [27,81,36,63,63,72,81]]
                  Output:
                  Run the code to see output
                  Expected Output:
                  [[39,30,26,25,31], 
                   [34,37,35,32,32], 
                   [38,41,44,46,42], 
                   [22,24,31,39,45], 
                   [37,34,36,47,59]]
                  Console Output:

           */



        /*
         Just past the prob. Here is my pseudo solution, and hope it helps.

         Find all the candidates in range(1, value of MaxObstacle); candidate definition: a number which all obstacles can not be divided by.
         Get the minimum one from the candidate list
           ps. If candidate list is empty, it means no number between range(1, value of MaxObstacle) meet requirements. The result would be (value of MaxObstacle + 1); the case of Test 2.
             
             
             */
        int addTwoDigits(int n, int m)
        {
            StringBuilder sb = new StringBuilder(n);
            for (int i = 0; i < n; i++)
            {
                sb.Append(9);
            }
            return int.Parse(sb.ToString());
            var amount = n.ToString();
            int tot = 0;
            for (int i = 0; i < amount.Length; i++)
            {
                tot += int.Parse(amount[i].ToString());
            }
            return tot;
        }

        public static int avoidObstacles(int[] inputArray)
        {

            var myList = inputArray.ToList();
            myList.Sort();
            var jumpstart = myList[0];
            var max = myList.Max();
            bool found = false;
            jumpstart += 1;
            bool pastMax = false;

            while (!found)
            {
                for (int i = 0; i < myList.Count; i++)
                {
                    if (jumpstart > myList[i])
                    {
                        continue;
                    }
                    if (jumpstart == myList[i])
                    {
                        jumpstart += 1;
                    }
                    if (myList[i] % jumpstart == jumpstart || myList[i] % jumpstart == 0)
                    {
                        jumpstart += 1;
                        i = 0;
                    }
                    else
                    {
                        found = true;
                    }
                }
                if (jumpstart >= max)
                {
                    pastMax = true;
                    //jumpstart += 1;
                    found = true;
                }

            }

            if (jumpstart > myList.Count && !pastMax)// WHY?! Because you must find the minimal length allowed for jumps in between.
                                                      // If they are even jumps and didn't go over max...
            {
                int innerJumps = jumpstart / myList.Count;
                if (jumpstart % myList.Count == 0)
                {
                    jumpstart = innerJumps;
                }
            }

            return jumpstart;
 


        }
        /*         
            You are given an array of integers representing coordinates of obstacles situated on a straight line.
            Assume that you are jumping from the point with coordinate 0 to the right. You are allowed only to make jumps of the same length represented by some integer.
            Find the minimal length of the jump enough to avoid all the obstacles.
            Example
            For inputArray = [5, 3, 6, 7, 9], the output should be
            avoidObstacles(inputArray) = 4.
            Check out the image below for better understanding:
         */


        public static bool isIPv4Address(string inputString)
        {
            if (inputString.Length <5 || inputString.Split('.').Length != 4 ) {
                return false;
            }
            string value = string.Empty;
            foreach (var item in inputString.Split('.'))
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (Char.IsDigit(item[i]))
                    {
                        value += item[i];
                    }
                    else
                    {
                        return false;
                    }
                }
                
                if (string.IsNullOrEmpty(value) ||value.Length > 3 ||int.Parse(value) < 0 || int.Parse(value) > 255) 
                {
                    return false;
                }
                value = string.Empty;

            }

            return true;
        }

        public static string whatIsLove(int n)
        {
            string[] whatislove = "What is love Baby don't hurt me Don't hurt me no more".Split();// Baby, don't hurt me Don't hurt, me no more What is love? Yeah".Split(); ;
            if (n>whatislove.Length)
            {
                n = n % whatislove.Length;
            }
            int c = 0;
            int index = 0;
            while (c<n)
            {

                index++;
                if (index -1 > whatislove.Length)
                {
                    index = 0;
                }
                c++;
                
                
            }
            index -= 1;
           

            return whatislove[index];
        }


        public static int arrayMaximalAdjacentDifference(int[] inputArray)
        {
            
            if (inputArray.Length == 1) { return 0; }
            int diff = 0;
            for (int i = 1; i < inputArray.Length; i++)
            {
                //           
                var tempDiff  = Math.Abs(inputArray[i - 1] - inputArray[i]);

                if (tempDiff > diff)
                {
                    diff = tempDiff;
                }
            }
            return diff;

            /*
              int max_diff =  Math.Abs(inputArray[1]) -  Math.Abs(inputArray[0]);
        int i, j;
        for (i = 0; i < inputArray.Length; i++) {
            for (j = i + 1; j < inputArray.Length; j++) {
                if ( Math.Abs(inputArray[j]) -  Math.Abs(inputArray[i]) > max_diff)
                    max_diff =  Math.Abs(inputArray[j]) -  Math.Abs(inputArray[i]);
            }
        }
        return max_diff;
             
             */


        }

        /*
         Given an array of integers, find the maximal absolute difference between any two of its adjacent elements.
         Example
         For inputArray = [2, 4, 1, 0], the output should be
             arrayMaximalAdjacentDifference(inputArray) = 3.
             
         Input: inputArray: [-1, 4, 10, 3, -2]
         Expected Output:7

             */

        public static bool areEquallyStrong(int yourLeft, int yourRight, int friendsLeft, int friendsRight)
        {
            if (yourLeft + yourRight != friendsLeft + friendsLeft) return false;
           
            //        10           5            10            
             return ((yourLeft == friendsLeft && yourRight == friendsRight) || (yourRight == friendsLeft && yourLeft == friendsRight));
        }

        /*
         * Input:yourLeft: 15 yourRight: 10 friendsLeft: 15 friendsRight: 9 Expected Output:false
         * 
         * 
         * Input:yourLeft: 10 yourRight: 5 friendsLeft: 10 friendsRight: 6 Output:true Expected Output: false
         Call two arms equally strong if the heaviest weights they each are able to lift are equal.
         Call two people equally strong if their strongest arms are equally strong (the strongest arm can be both the right and the left), and so are their weakest arms.
         Given your and your friend's arms' lifting capabilities find out if you two are equally strong.
         Example
         For yourLeft = 10, yourRight = 15, friendsLeft = 15 and friendsRight = 10, the output should be
          areEquallyStrong(yourLeft, yourRight, friendsLeft, friendsRight) = true;
         For yourLeft = 15, yourRight = 10, friendsLeft = 15 and friendsRight = 10, the output should be
          areEquallyStrong(yourLeft, yourRight, friendsLeft, friendsRight) = true;
         For yourLeft = 15, yourRight = 10, friendsLeft = 15 and friendsRight = 9, the output should be
          areEquallyStrong(yourLeft, yourRight, friendsLeft, friendsRight) = false.
             
             */


        public static bool palindromeRearranging(string inputString)
        {
            int[] count = new int[256];
            for (int i = 0; i < count.Length; i++)
            {
                count[i] = 0;
            }
            for (int i = 0; i < inputString.Length; i++)
            {
              count[(int)(inputString[i])]++;
            }
            int odd = 0;
            for (int i = 0; i < 256; i++)
            {
                if ((count[i] & 1) == 1)
                    odd++;

                if (odd > 1)
                    return false;
            }
            return true;
        }
        /*
         Given a string, find out if its characters can be rearranged to form a palindrome.
         Example
         For inputString = "aabb", the output should be
         palindromeRearranging(inputString) = true.
         We can rearrange "aabb" to make "abba", which is a palindrome.
         
         For inputString: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaabc"
         Expected Output:false
           
         For inputString: "zaa"
         Expected Output:true
             
             */

        public static int arrayChange(int[] inputArray)
        {
           
            int changesMade = 0;
            for (int i = 1; i < inputArray.Length; i++)
            {
                    // 1                 1
                    // 2                 1
                    while (inputArray[i - 1] >= inputArray[i])
                    {
                        changesMade += 1;
                        inputArray[i] += 1;
                    }
               
            }

            return changesMade;

        }
        /*
         You are given an array of integers. On each move you are allowed to increase exactly one of its element by one. 
         Find the minimal number of moves required to obtain a strictly increasing sequence from the input.

Example

For inputArray = [1, 1, 1], the output should be
                     +1  +2
arrayChange(inputArray) = 3.


For inputArray: [-1000, 0, -2, 0]
                   nope +4 +1
Expected Output:5

For inputArray: [2, 1, 10, 1]
                    +2 no +10
Expected Output:12

Guaranteed constraints:
3 ≤ inputArray.length ≤ 105,
-105 ≤ inputArray[i] ≤ 105.

[output] integer

The minimal number of moves needed to obtain a strictly increasing sequence from inputArray.
It's guaranteed that for the given test cases the answer always fits signed 32-bit integer type.
             
             */


        /*
         As Description says : return the lexicographically largest (this makes descending order of alphabets in the string) and since swapping is allowed for few combination pairs, 
         this makes few characters are remains in same position.And because of few connected pairs, here is the changes increasing the combination of swapping, 
         plus it will have many sub-graph. If we go by brute force for all the combination which leads to complexity to make highest lexicographical.

         Which can be easily done once we find the connected pairs(possible swapping) and since we need lexicographically largest value, 
         we just need to find the connected graph(possible swapping) 
         and sort it to descending, then replace to their respective index. [that we will be the lexicographically largest ]

         If pairs are not connected, no swapping :) no possible string combination

         Take same example :
          a c x r a b d z
          0 1 2 3 4 5 6 7

         Allowed swapping (0,2)(5,7)(2,7),(1,6) as per this 3, 4 remains in same position (index)

         connected graphs

          a x , b z, x z ——> axbz (0,2,5,7)

          c d ——> cd (1,6)

          after sorting(descending) axbz ——> zxba (0,2,5,7) [ Description : return the lexicographically largest (this makes descending order of alphabets)]

          after sorting cd ——> dc (1,6)

          replace “zxba to (0,2,5,7)” and “dc to (1,6)”

          z d x r b c a
          0 1 2 3 4 5 7
             
             */
        public static string swapLexOrder(string str, int[][] pairs)
        {
            var swaps = new List<HashSet<int>>();
            foreach (var pair in pairs)
            {
                var swap = new HashSet<int>();
                swap.Add(pair[0] - 1);
                swap.Add(pair[1] - 1);
                swaps.Add(swap);
            }
            int i = 0;

            while (i < swaps.Count)
            {
                bool merge = false;
                for (int j = i + 1; j < swaps.Count; j++)
                {
                    foreach (var x in swaps[j])
                    {
                        if (swaps[i].Contains(x))
                        {
                            foreach (var y in swaps[j])
                            {
                                swaps[i].Add(y);
                            }
                          
                            merge = true;
                            break;
                        }
                    }
                    if (merge)
                    {
                        swaps.RemoveAt(j);
                        i = 0;
                        break;
                    }
                }
                if (!merge) i++;
            }
 
            char[] rs = str.ToCharArray();
            for (i = 0; i < str.Length; i++)
            {
                foreach (var swap in swaps)
                {
                    if (swap.Contains(i))
                    {
                        int largest = i;
                        foreach (int j in swap)
                        {
                            if (StringComparer.Ordinal.Compare(rs[largest], rs[j]) < 0)
                            {
                                largest = j;
                            }
                        }
                        var t = rs[i];
                        rs[i] = rs[largest];
                        rs[largest] = t;
                        swap.Remove(i);
                    }
                    else
                    {
                        rs[i] = rs[i];
                    }
                }
            }
            return new String(rs);
        }
        /*
        if (str.Length == 1) return str;
        for (int i = 0; i < pairs.Length; i++)
        {
            pairs[i][0] -= 1;
            pairs[i][1] -= 1;
        }

        Dictionary<int, HashSet<int>> dic = new Dictionary<int, HashSet<int>>();
        bool found = false;
        for (int i = 0; i < pairs.Length; i++)
        {
            if (dic.Count == 0)
            {
                dic.Add(i, new HashSet<int>() { pairs[i][0], pairs[i][1] });
                found = true;
            }
            else if (dic.FirstOrDefault(x => x.Value.Contains(pairs[i][0])).Value != null)
            {
                var key = dic.FirstOrDefault(x => x.Value.Contains(pairs[i][0])).Key;
                dic[key].Add(pairs[i][0]);
                dic[key].Add(pairs[i][1]);
                found = true;
            }
            else if (dic.FirstOrDefault(x => x.Value.Contains(pairs[i][1])).Value != null)
            {
                var key = dic.FirstOrDefault(x => x.Value.Contains(pairs[i][1])).Key;
                dic[key].Add(pairs[i][0]);
                dic[key].Add(pairs[i][1]);
                found = true;
            }

            if (!found)
            {
                for (int j = 0; j < pairs.Length; j++)
                {
                    if (j == i)
                    { continue; }
                    if (pairs[i][0] == pairs[j][0] ||
                        pairs[i][1] == pairs[j][0] ||
                        pairs[i][0] == pairs[j][1] ||
                        pairs[i][1] == pairs[j][1])
                    {
                        if (dic.FirstOrDefault(x => x.Value.Contains(pairs[i][0])).Key > -1)
                        {
                            var key = dic.FirstOrDefault(x => x.Value.Contains(pairs[i][0])).Key;
                            dic[key].Add(pairs[i][0]);
                            dic[key].Add(pairs[i][1]);
                            found = true;

                        }
                        else if (dic.FirstOrDefault(x => x.Value.Contains(pairs[i][1])).Key > -1)
                        {
                            var key = dic.FirstOrDefault(x => x.Value.Contains(pairs[i][1])).Key;
                            dic[key].Add(pairs[i][0]);
                            dic[key].Add(pairs[i][1]);
                            found = true;
                        }
                    }
                }
            }

            if (found)
            {
                found = false;
            }
            else
            {
                dic.Add(i, new HashSet<int>() { pairs[i][0], pairs[i][1] });
            }
        }
        var maxCount = dic.Values.Max(list => list.Count);

        if (dic.Count > 1 && maxCount > 2)
        {
            char[] strChar = str.ToCharArray();
            Dictionary<int, List<char>> strDic = new Dictionary<int, List<char>>();
            foreach (var item in dic)
            {
                strDic.Add(item.Key, new List<char>());
                foreach (var v in item.Value)
                {
                    strDic[item.Key].Add(str[v]);
                }
            }

            foreach (var item in strDic)
            {
                int index = 0;
                foreach (var d in dic[item.Key])
                {
                    strChar[d] = item.Value.OrderByDescending(x => x).ToList()[index];
                    index++;
                }
            }
            return new string(strChar);
        }
        else
        {
            SortedSet<string> h = new SortedSet<string>(StringComparer.Ordinal);

            while (true)
            {
                char[] charsToSwap = str.ToCharArray();
                char first;
                char second;
                for (int i = 0; i < pairs.Length; i++)
                {
                    first = charsToSwap[pairs[i][0]];//0
                    second = charsToSwap[pairs[i][1]];//3
                    charsToSwap[pairs[i][1]] = first;
                    charsToSwap[pairs[i][0]] = second;
                    str = new string(charsToSwap);
                    if (h.Contains(str))
                    {
                        return h.Max();
                    }

                    h.Add(str);
                }

            }
        }
    }
    /*
                    Take same example :
    a c x r a b d z
    0 1 2 3 4 5 6 7

    Allowed swapping (0,2)(5,7)(2,7),(1,6) as per this 3, 4 remains in same position (index)

    connected graphs

    a x , b z, x z ——> axbz (0,2,5,7)

    c d ——> cd (1,6)

    after sorting(descending) axbz ——> zxba (0,2,5,7) [ Description : return the lexicographically largest (this makes descending order of alphabets)]

    after sorting cd ——> dc (1,6)

    replace “zxba to (0,2,5,7)” and “dc to (1,6)”

    z d x r b c a
    0 1 2 3 4 5 7


                     */
        //string str = "acxrabdz";

        // Console.WriteLine(finalString);





        // return h.Max();



        public static bool areSimilar(int[] a, int[] b)
        {
            HashSet<int> h = new HashSet<int>();
            if (Enumerable.SequenceEqual(a, b))
            {
                return true;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a.Length >= i + 2)
                {
                    if (a[i] == b[i])
                    {
                        continue;
                    }
                    int index = i;
                    int valueToFind = b[i];
                    if (h.Count == 0)
                    {
                        h.Add(valueToFind);
                    }
                    else if (!h.Contains(valueToFind))
                    {
                        return false;
                    }

                    for (int j = index; j < a.Length; j++)
                    {
                        if (a[j] == valueToFind && a[i] != b[i])
                        {
                            int toSwap = a[i];
                            a[j] = toSwap;
                            a[i] = valueToFind;
                            if (Enumerable.SequenceEqual(a, b))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        string[] addBorder(string[] picture)
        {
            int columnLength = picture[0].Length + 2;
            List<string> pList = new List<string>();
            pList.Add(new string('*', columnLength));
            for (int i = 0; i < picture.Length; i++)
            {
                pList.Add(string.Concat("*", picture[i], "*"));
            }
            pList.Add(new string('*', columnLength));


            return pList.ToArray();
        }


        public static int[] alternatingSums(int[] a)
        {
            int team1 = 0, team2 = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (i % 2 == 0)
                {
                    team1 += a[i];
                }
                else
                {
                    team2 += a[i];
                }
            }

            return new int[] { team1, team2 };
        }



        public static string reverseParentheses(string s)
        {
            StringBuilder sb = new StringBuilder();

            int startIndex = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    startIndex = 0;
                    var s1 = s.Substring(i);
                    int level = 0;
                    int length = s.Substring(i).Length;
                    for (int j = 0; j < s1.Length; j++)
                    {
                        if (s1[j] == '(')
                        {

                            level += 1;
                            continue;
                        }
                        if (s1[j] == ')')
                        {

                            level -= 1;
                            continue;
                        }
                        if (level == 0)
                        {
                            length = j--;
                            j = s1.Length;
                        }
                    }

                    while (length >= s.Substring(i).Length)
                    {
                        length -= 1;
                    }


                    sb.Append(Parse(s.Substring(i + 1, length), ref startIndex));
                    i = startIndex + i;
                }
                else
                {
                    sb.Append(s[i]);
                }
            }


            return sb.ToString();

        }

        static string Parse(String value, ref Int32 index)
        {
            StringBuilder result = new StringBuilder();
            int startIndex = index; // Used to get substrings

            while (index < value.Length)
            {
                Char current = value[index];

                if (current == '(')
                {
                    var s1 = value.Substring(index);
                    int level = 0;
                    int length = value.Substring(index).Length;
                    for (int j = 0; j < s1.Length; j++)
                    {
                        if (s1[j] == '(')
                        {

                            level += 1;
                            continue;
                        }
                        if (s1[j] == ')')
                        {

                            level -= 1;
                            continue;
                        }
                        if (level == 0)
                        {
                            length = j--;
                            j = s1.Length;
                        }
                    }

                    while (length >= s1.Length)
                    {
                        length -= 1;
                    }
                    index++;
                    result.Append(current);
                    startIndex = 0;




                    result.Append(Parse(value.Substring(index, length), ref startIndex));
                    index = startIndex + index;
                    continue;
                }
                if (current == ')')
                {
                    // Push last result
                    result.Append(current);
                    index++;

                    var s1 = value.Substring(index);
                    int level = 0;
                    int length = value.Substring(index).Length;

                    for (int j = 0; j < s1.Length; j++)
                    {
                        if (s1[j] == '(')
                        {

                            level += 1;
                            continue;
                        }
                        if (s1[j] == ')')
                        {

                            level -= 1;
                            continue;
                        }
                        if (level == 0)
                        {
                            length = j--;
                            j = s1.Length;
                        }
                    }


                    char[] reverseMe = result.ToString().ToCharArray();
                    reverseMe.Reverse();
                    Array.Reverse(reverseMe);
                    string returnValue = new string(reverseMe);
                    returnValue = returnValue.Replace("(", "").Replace(")", "");
                    return returnValue;


                    // return result.ToString();
                }
                result.Append(current);
                index++;

                // Process all other chars here
            }
            return result.ToString();
            // We can't find the closing bracket
            //  throw new Exception("String is not valid");
        }

        /// <summary>
        /// Some people are standing in a row in a park. There are trees between them which cannot be moved. 
        /// Your task is to rearrange the people by their heights in a non-descending order without moving the trees.
        ///        Example
        ///        For a = [-1, 150, 190, 170, -1, -1, 160, 180], the output should be
        ///sortByHeight(a) = [-1, 150, 160, 170, -1, -1, 180, 190].
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int[] sortByHeight(int[] a)
        {
            HashSet<int> h = new HashSet<int>();
            List<int> l = new List<int>();
            List<int> result = new List<int>();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == -1)
                {
                    h.Add(i);
                }
                else
                {
                    l.Add(a[i]);
                }

            }
            l.Sort();
            int index = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (h.Contains(i))
                {
                    result.Add(-1);
                    continue;
                }
                else
                {
                    result.Add(l[index]);
                    index++;
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Ticket numbers usually consist of an even number of digits. A ticket number is considered lucky if 
        /// the sum of the first half of the digits is equal to the sum of the second half.
        /// Given a ticket number n, determine if it's lucky or not.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool isLucky(int n)
        {
            if (n.ToString().Length % 2 != 0) { return false; }

            int mid = n.ToString().Length / 2;

            int total1 = 0, total2 = 0;

            for (int i = 0; i < mid; i++)
            {
                total1 += (int)n.ToString()[i];
            }
            for (int i = mid; i < n.ToString().Length; i++)
            {
                total2 += (int)n.ToString()[i];
            }

            //For n = 1230, the output should be
            // isLucky(n) = true;
            // For n = 239017, the output should be
            //isLucky(n) = false.
            return total1 == total2;
        }

        /// <summary>
        /// Given two strings, find the number of common characters between them.
        ///        Example
        ///        For s1 = "aabcc" and s2 = "adcaa", the output should be
        ///        commonCharacterCount(s1, s2) = 3.
        ///Strings have 3 common characters - 2 "a"s and 1 "c".
        /// 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static int commonCharacterCount(string s1, string s2)
        {
            int total = 0;
            Dictionary<char, int> dic1 = new Dictionary<char, int>();
            Dictionary<char, int> dic2 = new Dictionary<char, int>();
            //foreach (var outer in s1)
            //{
            //    foreach (var inner in s2)
            //    { 
            //        if (outer.Equals(inner))
            //        {

            //            //a, a, b, c, c
            //            //aaa aaa x c c 
            //        }

            //    }
            //}
            foreach (var letter in s1)
            {
                if (dic1.ContainsKey(letter))
                {
                    dic1[letter] += 1;
                }
                else
                {
                    dic1.Add(letter, 1);
                }
            }
            foreach (var letter in s2)
            {
                if (dic2.ContainsKey(letter))
                {
                    dic2[letter] += 1;
                }
                else
                {
                    dic2.Add(letter, 1);
                }
            }

            foreach (var item in dic1)
            {
                if (dic2.ContainsKey(item.Key))
                {
                    if (dic2[item.Key] > item.Value)
                    {
                        total += item.Value;
                    }
                    else
                    {
                        total += dic2[item.Key];
                    }
                }
            }

            return total;
        }

        public static string[] allLongestStrings(string[] inputArray)
        {
            if (inputArray.Length == 1) return inputArray;


            int minLength = inputArray.Max(w => w.Length);

            List<string> returnList = new List<string>();
            foreach (var item in inputArray)
            {
                if (item.Length == minLength)
                {
                    returnList.Add(item);
                }
            }
            //string[] inputArray = new string[] { "aba", "aa", "ad", "vcd", "aba" };
            //string[] expected = new string[] { "aba", "vcd", "aba" };


            return returnList.ToArray();
        }

        /// <summary>
        /// any room that is free or is located anywhere 
        /// below a free room in the same column is not considered suitable for the bots to live in.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int matrixElementsSum(int[][] matrix)
        {
            int total = 0;
            HashSet<int> h = new HashSet<int>();
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (h.Contains(j)) continue;

                    if (matrix[i][j] == 0)
                    {
                        h.Add(j);
                    }
                    else
                    {
                        total += matrix[i][j];
                    }
                }
            }
            return total;
        }


        /// <summary>
        /// Given a sequence of integers as an array, determine whether it is possible to obtain a strictly increasing 
        /// sequence by removing no more than one element from the array.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool almostIncreasingSequence(int[] sequence)
        {
            //40, 50, 60, 10, 20, 30
            //  j>i 
            //    j>i
            //         j>i wrong
            //         j> i+ found 2 failed

            // 1, 3, 2
            // j>i
            //    j>i xx found 1
            //    found 1 passed
            //
            //-1, 10, 1, 2, 3, 4, 5
            // j>i
            //     j>ix
            //        j>i
            //             j>i
            //WORKS BUT TOOO SLOW!!!
            //int temp = int.MaxValue;
            bool increasing = true;
            int skipsFound = 0;

            for (int i = 1; i < sequence.Length; i++)
            {  //1,2,1,2     2       1
                if (sequence[i] > sequence[i - 1])
                {//            0            1
                    //10 > -1
                    // 1>10xxx
                    continue;
                }             //1        2
                else if (sequence[i] <= sequence[i - 1])
                {
                    skipsFound += 1;
                    if (skipsFound > 1)
                    {
                        return false;
                    }

                    if (i == 1) continue; //first run don't know anything...

                    ///look ahead or look backward and compare those
                    /// 4, 5, 6, 1, 2, 3
                    /// 1,2,1,2
                    /// 2 
                    /// 
                    /// 2
                    ///    
                    if (i + 1 < sequence.Length)
                    {
                        // i-2  i-1  i   i+1  i+2
                        // -1,  10,  1,  2,   3,  4, 5
                        //  1,   2,  1,  2
                        //2                1
                        if ((sequence[i] > sequence[i - 2]))
                        {
                            continue;
                        }

                        //          2           2
                        if (sequence[i + 1] <= sequence[i - 1])
                        {
                            return false;
                        }

                        if (i + 2 < sequence.Length)
                        {             //not sure i need this... 
                            if (sequence[i + 2] <= sequence[i - 2])
                            {
                                return false;
                            }
                        }
                    }

                }
            }



            //for (int i = 0; i < sequence.Length; i++)
            //{
            //    temp = int.MaxValue;
            //    for (int j = 0; j < sequence.Length; j++)
            //    {
            //        if (j == i)
            //        {
            //            continue;
            //        }

            //        if (temp == int.MaxValue)
            //        {
            //            temp = sequence[j];
            //            continue;
            //        }

            //        if (temp >= sequence[j])
            //        {
            //            increasing = false;
            //            j = sequence.Length;
            //        }
            //        else
            //        {
            //            increasing = true;
            //            temp = sequence[j];
            //        }
            //        if (increasing && j == sequence.Length - 1)
            //        {
            //            return increasing;
            //        }
            //    }
            //}
            return increasing;
        }

        public static int makeArrayConsecutive2(int[] statues)
        {
            List<int> sortedStatues = statues.ToList();
            sortedStatues.Sort();
            int higestValue = sortedStatues.Last();//status[status.Length -1]
            int firstValue = sortedStatues.First();
            List<int> totalAmount = new List<int>();
            int x = firstValue;
            do
            {
                totalAmount.Add(x);
                x++;
            } while (higestValue > x);

            return totalAmount.Except(sortedStatues).ToList().Count;

        }



        public static int[] bankRequests(int[] accounts, string[] requests)
        {
            List<int> accountList = accounts.ToList();
            int err = 0;
            for (int i = 0; i < requests.Length; i++)
            {


                string[] message = requests[i].Split(null);
                //for (int i = 0; i < message.Length; i++)
                //{

                //}
                switch (message[0])
                {
                    case "withdraw":
                        if (int.Parse(message[1]) - 1 < 0 || accountList.Count - 1 < int.Parse(message[1]) - 1)
                        {
                            err = (i + 1) * -1;
                            return new int[] { err };
                        }

                        accountList[int.Parse(message[1]) - 1] -= int.Parse(message[2]);
                        if (accountList[int.Parse(message[1]) - 1] < 0)
                        {
                            err = (i + 1) * -1;
                            return new int[] { err };
                        }

                        break;
                    case "transfer":
                        //"transfer 5 1 20": 
                        if (int.Parse(message[1]) - 1 < 0 || accountList.Count - 1 < int.Parse(message[1]) - 1)
                        {
                            err = (i + 1) * -1;
                            return new int[] { err };
                        }
                        if (int.Parse(message[2]) - 1 < 0 || accountList.Count - 1 < int.Parse(message[2]) - 1)
                        {
                            err = (i + 1) * -1;
                            return new int[] { err };
                        }
                        accountList[int.Parse(message[1]) - 1] -= int.Parse(message[3]);
                        if (accountList[int.Parse(message[1]) - 1] < 0)
                        {
                            err = (i + 1) * -1;
                            return new int[] { err };
                        }
                        accountList[int.Parse(message[2]) - 1] += int.Parse(message[3]);
                        break;
                    case "deposit":
                        //"deposit 5 20": 
                        if (int.Parse(message[1]) - 1 < 0 || accountList.Count - 1 < int.Parse(message[1]) - 1)
                        {
                            err = (i + 1) * -1;
                            return new int[] { err };
                        }

                        accountList[int.Parse(message[1]) - 1] += int.Parse(message[2]);

                        break;
                    default:
                        break;
                }

            }


            return accountList.ToArray();
        }

        #region solved test

        public static int[][] constructSubmatrix(int[][] matrix, int[] rowsToDelete, int[] columnsToDelete)
        {
            var m = matrix.ToList();
            var rowdel = rowsToDelete.ToList();
            var colmdel = columnsToDelete.ToList();
            List<int[]> newMatrix = new List<int[]>();
            List<int> innerColumn = new List<int>();
            for (int i = 0; i < m.Count; i++)
            {
                if (rowdel.Contains(i)) { continue; }
                innerColumn = new List<int>();
                for (int j = 0; j < m[i].Length; j++)
                {
                    if (colmdel.Contains(j)) { continue; }
                    innerColumn.Add(m[i][j]);
                }
                newMatrix.Add(innerColumn.ToArray());
                //{ 1, 0, 0, 2 },{ 1, 3, 1,  4 } },{ 1 }, { 0,2});
                //{{0,2 },{ 0, 5 }};
            }
            return newMatrix.ToArray();

        }



        public static int zigzag(int[] a)
        {
            if (a.Length == 1) return 0;
            if (a.Length == 2) return 1;

            Dictionary<int, int> zigZagCollection = new Dictionary<int, int>();

            bool zigZagPos = false;
            bool zigZagNeg = false;
            bool reset = true;

            int indexKey = 0;

            for (int i = 0; i < a.Length; i++)
            {
                indexKey = i;
                zigZagNeg = false;
                zigZagPos = false;
                reset = true;
                zigZagCollection.Add(indexKey, 1);
                for (int j = i; j < a.Length - 1; j++)
                {
                    if (reset)
                    {
                        reset = false;
                        if (a[j] < a[j + 1])
                        {
                            zigZagNeg = true;
                            zigZagPos = false;
                            zigZagCollection[indexKey] += 1;
                        }
                        else if (a[j] > a[j + 1])
                        {
                            zigZagNeg = false;
                            zigZagPos = true;
                            zigZagCollection[indexKey] += 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (a[j] < a[j + 1] && !zigZagNeg)
                    {
                        zigZagNeg = true;
                        zigZagPos = false;
                        zigZagCollection[indexKey] += 1;
                    }
                    else if (a[j] > a[j + 1] && !zigZagPos)
                    {
                        zigZagNeg = false;
                        zigZagPos = true;
                        zigZagCollection[indexKey] += 1;
                    }
                    else
                    {
                        reset = true;
                        break;
                    }

                }

            }

            return zigZagCollection.Values.Max();
        }


        #endregion




        public static int shapeArea(int n)
        {
           
            var area = 1;

            for (int i = 1; i <= n; i++)
            {
                area += ((i * 4) - 4);
            }
            return area;

        }


        public static int adjacentElementsProduct(int[] inputArray)
        {
            int highestValue = int.MinValue;
            int currentValue = 0;
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (i + 1 < inputArray.Length)
                {
                    currentValue = inputArray[i] * inputArray[i + 1];
                    if (currentValue > highestValue)
                    {
                        highestValue = currentValue;
                    }
                }
            }
            return highestValue;
        }




        public static bool isCryptSolution(string[] crypt, char[][] solution)
        {


            for (int word = 0; word < crypt.Length; word++)
            {
                for (int letter = 0; letter < crypt[word].Length; letter++)
                {
                    for (int keyLetter = 0; keyLetter < solution.Length; keyLetter++)
                    {
                        crypt[word] = crypt[word].Replace(solution[keyLetter][0], solution[keyLetter][1]);
                    }

                }
            }

            for (int wordToNumber = 0; wordToNumber < crypt.Length; wordToNumber++)
            {
                if (crypt[wordToNumber].Length > 1 && crypt[wordToNumber].StartsWith("0"))
                {
                    return false;
                }
            }

            int cryptarithmSolution = int.Parse(crypt[0]) + int.Parse(crypt[1]);

            return cryptarithmSolution == int.Parse(crypt[2]);

        }



        /// <summary>
        /// 9x9 no dupes up and down... Go!!!
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool sudoku2(char[][] grid)
        {
            //Sudoku s = new Sudoku(grid);
            //return s.IsValid();
            HashSet<char> h = new HashSet<char>();
            HashSet<char> v = new HashSet<char>();

            for (int row = 0; row < grid.Length; row++)
            {
                h.Clear();
                v.Clear();

                ///check vert and horiz
                for (int column = 0; column < grid.Length; column++)
                {
                    if (char.IsNumber(grid[row][column]))
                    {
                        if (h.Contains(grid[row][column]))
                        {
                            return false;
                        }
                        else
                        {
                            h.Add(grid[row][column]);
                        };
                    }
                    if (char.IsNumber(grid[column][row]))
                    {
                        if (v.Contains(grid[column][row]))
                        {
                            return false;
                        }
                        else
                        {
                            v.Add(grid[column][row]);
                        };
                    }

                }
                h.Clear();
                v.Clear();
                ///check 3 x 3
                for (int column = 0; column < grid.Length; column++)
                {

                    var index = 3 * (row % 3) + column % 3;
                    var block = column / 3 + 3 * (row / 3);
                    if (char.IsNumber(grid[block][index]))
                    {
                        if (h.Contains(grid[block][index]))
                        {
                            return false;
                        }
                        else
                        {
                            h.Add(grid[block][index]);
                        };
                    }

                }
            }

            return true;
        }

        public class Sudoku
        {
            char[][] _grid;

            public Sudoku(char[][] grid)
            {
                _grid = grid;
            }

            public bool IsValid()
            {
                return RowsAreValid()
                    && ColumnsAreValid()
                    && SquaresAreValid();
            }

            bool RowsAreValid()
            {
                return Validate(GetNumberFromRow);
            }

            bool ColumnsAreValid()
            {
                return Validate(GetNumberFromColumn);
            }

            bool SquaresAreValid()
            {
                return Validate(GetNumberFromSquare);
            }

            bool Validate(Func<int, int, int> numberGetter)
            {
                for (var row = 0; row < 9; row++)
                {
                    var usedNumbers = new bool[10];
                    for (var column = 0; column < 9; column++)
                    {
                        var number = numberGetter(row, column);
                        if (number != 0 && usedNumbers[number] == true)
                        {
                            return false;
                        }

                        usedNumbers[number] = true;
                    }
                }

                return true;
            }

            int GetNumberFromRow(int row, int column)
            {
                return ToNumber(_grid[row][column]);
            }

            int GetNumberFromColumn(int row, int column)
            {
                return ToNumber(_grid[column][row]);
            }

            int GetNumberFromSquare(int block, int index)
            {
                var column = 3 * (block % 3) + index % 3;
                var row = index / 3 + 3 * (block / 3);
                return ToNumber(_grid[row][column]);
            }

            int ToNumber(char c)
            {
                if (c == '.')
                    return 0;
                return (int)(c - '0');
            }
        }

        public static int[][] rotateImage(int[][] a)
        {
            List<List<int>> temp = new List<List<int>>();
            List<int> subList = new List<int>();
            int subLength = a[0].Length;
            bool f = true;
            for (int i = a.Length - 1; i >= 0; i--)
            {
                subList.Clear();
                for (int j = 0; j < a[i].Length; j++)
                {
                    subList.Add(a[i][j]);
                }

                for (int loadIndex = 0; loadIndex < subLength; loadIndex++)
                {
                    if (f)
                    {
                        temp.Add(new List<int>() { subList[loadIndex] });
                    }
                    else
                    {
                        temp[loadIndex].Add(subList[loadIndex]);
                    }
                }

                if (f) f = false;

            }
            //[[1,2,3], 
            //[4,5,6], 
            //[7,8,9]]
            //expecting
            //[[7,4,1], 
            // [8,5,2], 
            //[9,6,3]]

            return temp.Select(l => l.ToArray()).ToArray();
        }


        public static char firstNotRepeatingCharacter(string s)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (dic.ContainsKey(s[i]))
                {
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


        public static int centuryFromYear(int year)
        {
            if (year % 100 == 0)
            {
                if (year < 100) { return 1; }
                return year / 100;
            }
            else
            {
                if (year < 100) { return 1; }
                return (year / 100) + 1;
            }

        }


    }


    public class MatryoshkaDoll
    {
        private readonly MatryoshkaDoll containedDoll;

        public MatryoshkaDoll() { }

        public MatryoshkaDoll(MatryoshkaDoll containedDoll)
        {
            this.containedDoll = containedDoll;
        }


        public int NumberOfSmallerDolls
        {
            get
            {
                var x = containedDoll;
                if (x == null)
                {
                    return 0;
                }
                else
                {
                    return 1 + NumberOfSmallerDolls;
                }

            }
        }

        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(new MatryoshkaDoll(new MatryoshkaDoll()).NumberOfSmallerDolls);
        //}
    }

    //<p unselectable = "on" > When < b style="display:none;" unselectable="on"> 3 </b> the<b style="display:none;" unselectable="on"> 5 </b> character<b style="display:none;" unselectable="on"> 3 </b> moves,<b style = "display:none;" unselectable="on"> z</b> the<b style="display:none;" unselectable="on"> 3 </b> tile<b style="display:none;" unselectable="on"> m </b> at<b style="display:none;" unselectable="on"> 5 </b> the<b style="display:none;" unselectable="on"> 3 </b> previous<b style="display:none;" unselectable="on"> 8 </b> position<b style="display:none;" unselectable="on"> 9 </b> disappears.<b style = "display:none;" unselectable= "on" > a </ b > The < b style= "display:none;" unselectable= "on" > 4 </ b > character < b style= "display:none;" unselectable= "on" > a </ b > can < b style= "display:none;" unselectable= "on" > 4 </ b > only < b style= "display:none;" unselectable= "on" > 4 </ b > move < b style= "display:none;" unselectable= "on" > a </ b > left < b style= "display:none;" unselectable= "on" > 9 </ b > and < b style= "display:none;" unselectable= "on" > 4 </ b > right,<b style = "display:none;" unselectable= "on" > 5 </ b > and < b style= "display:none;" unselectable= "on" > 7 </ b > always < b style= "display:none;" unselectable= "on" > v </ b > jumps < b style= "display:none;" unselectable= "on" > w </ b > over < b style= "display:none;" unselectable= "on" > n </ b > one < b style= "display:none;" unselectable= "on" > j </ b > tile,<b style = "display:none;" unselectable= "on" > x </ b > and < b style= "display:none;" unselectable= "on" > s </ b > any < b style= "display:none;" unselectable= "on" > f </ b > holes.< b style= "display:none;" unselectable= "on" > m </ b > The < b style= "display:none;" unselectable= "on" > s </ b > character < b style= "display:none;" unselectable= "on" > 5 </ b > will < b style= "display:none;" unselectable= "on" > 5 </ b > not < b style= "display:none;" unselectable= "on" > 7 </ b > move < b style= "display:none;" unselectable= "on" > 2 </ b > if<b style = "display:none;" unselectable= "on" > p </ b > there < b style= "display:none;" unselectable= "on" > 4 </ b > are < b style= "display:none;" unselectable= "on" > r </ b > no < b style= "display:none;" unselectable= "on" > x </ b > tiles < b style= "display:none;" unselectable= "on" > i </ b > left < b style= "display:none;" unselectable= "on" > j </ b > to < b style= "display:none;" unselectable= "on" > j </ b > move < b style= "display:none;" unselectable= "on" > z </ b > to < b style= "display:none;" unselectable= "on" > e </ b > (you < b style= "display:none;" unselectable= "on" > 7 </ b > do<b style = "display:none;" unselectable= "on" > y </ b > not < b style= "display:none;" unselectable= "on" > 5 </ b > need < b style= "display:none;" unselectable= "on" > v </ b > to < b style= "display:none;" unselectable= "on" > m </ b > implement < b style= "display:none;" unselectable= "on" > n </ b > this < b style= "display:none;" unselectable= "on" > j </ b > in<b style = "display:none;" unselectable= "on" > p </ b > the < b style= "display:none;" unselectable= "on" > f </ b > code).</p>
    public class Platformer
    {
        public Platformer(int numberOfTiles, int position)
        {
            for (int i = 0; i < numberOfTiles; i++)
            {
                numberOfTilesList.Add(i);
            }
            PositionRow = position;
        }

        private static List<int> numberOfTilesList = new List<int>();
        public void JumpLeft()
        {
            try
            {
                if (PositionRow - 2 > 0)
                {
                    numberOfTilesList.Remove(PositionRow);
                }
                PositionRow = PositionRow - 2;

            }
            catch (Exception)
            {


            }


        }

        public void JumpRight()
        {
            try
            {
                if (PositionRow + 2 <= numberOfTilesList.Count - 1)
                {
                    numberOfTilesList.Remove(PositionRow);
                }
                PositionRow = PositionRow + 2;

            }
            catch (Exception)
            {


            }
        }


        public int PositionRow { get; set; }

        public int Position()
        {
            return PositionRow;
        }

        public static void Main(string[] args)
        {
            Platformer platformer = new Platformer(2, 3);
            //Platformer platformer = new Platformer(100, 3);
            Console.WriteLine(platformer.Position());

            platformer.JumpLeft();
            Console.WriteLine(platformer.Position());

            platformer.JumpRight();
            Console.WriteLine(platformer.Position());
            Console.ReadKey();
        }
    }
    public class Path
    {
        public string CurrentPath { get; private set; }

        private static LinkedList<string> folders = new LinkedList<string>();
        private int folderCount;
        private string driveName;
        public Path(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            string[] d = path.Split(':');
            if (d.Length > 1)
            {
                driveName = d[0] + ":";
                path = d[1];
            }
            if (AnyPathHasIllegalCharacters(path))
            {
                throw new ArgumentException("Argument Invalid Path Chars");
            }

            string[] f = path.Split('/');
            for (int i = 0; i < f.Length; i++)
            {
                if (!string.IsNullOrEmpty(f[i]))
                {
                    folders.AddLast(f[i]);
                }

            }
            folderCount = folders.Count;
            this.CurrentPath = path;
        }
        static bool AnyPathHasIllegalCharacters(string path)
        {
            return path.IndexOfAny(InvalidPathChars) >= 0;
        }
        private static string CombineNoChecks(string path1, string path2)
        {
            StringBuilder sb = new StringBuilder(path1.Length + path2.Length);
            foreach (var item in folders)
            {
                sb.AppendFormat("/{0}", item);
            }
            return sb.ToString();
        }

        public void Cd(string newPath)
        {
            if (newPath == null)
            {
                throw new ArgumentNullException("newPath");
            }
            if (AnyPathHasIllegalCharacters(newPath))
            {
                throw new ArgumentException("Argument Invalid newPath Chars");
            }
            //if (newPath.StartsWith(".."))
            //{
            //    newPath = newPath.Replace("../", "/");
            //    string[] f = newPath.Split('/');
            //    folders.RemoveLast();
            //    for (int i = 0; i < f.Length; i++)
            //    {
            //        if (!string.IsNullOrEmpty(f[i]))
            //        {
            //            folders.AddLast(f[i]);
            //        }

            //    }
            //    CurrentPath = driveName + CombineNoChecks(CurrentPath, newPath);
            //}
            //else
            //{
            //    CurrentPath = driveName + newPath;
            //}



            String[] newP = newPath.Split('/');
            String[] oldP = CurrentPath.Split('/');
            int lnCount = 0;
            foreach (String str in newP)
            {
                if (str.Equals(".."))
                {
                    lnCount++;
                }
            }

            int len = oldP.Length;
            String strOut = "";
            for (int i = 0; i < len - lnCount; i++)
            {
                strOut = strOut + oldP[i] + "/";
            }

            len = newP.Length;
            for (int i = 0; i < len; i++)
            {
                if (!newP[i].Equals(".."))
                {
                    strOut = strOut + newP[i] + "/";
                }
            }
            CurrentPath = strOut.Substring(0, strOut.Length - 1);
            //Console.WriteLine(CurrentPath);
            if (CurrentPath.IndexOf("/") < 0)
                throw new Exception("Directory not found");
            //return this;

        }
        static bool IsDirectorySeparator(char c)
        {
            return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
        }
        public static readonly char DirectorySeparatorChar = '\\';
        public static readonly char AltDirectorySeparatorChar = '/';
        public static readonly char VolumeSeparatorChar = ':';
        internal static readonly char[] InvalidPathChars = new char[]
{
    '"',
    '<',
    '>',
    '|',
    '\a',
    '\b',
    '\t',
    '\n',
    '\v',
    '\f',
    '\r',
    '0',
    '1',
    '2',
    '3',
    '4',
    '5',
    '6',
    '7',
    '8',
    '9'
};


        //Write a function that provides change directory(cd) function for an abstract file system.

        //Notes:

        //Root path is '/'.
        //Path separator is '/'.
        //Parent directory is addressable as "..".
        //Directory names consist only of English alphabet letters (A-Z and a-z).
        //The function should support both relative and absolute paths.
        //The function will not be passed any invalid paths.
        //Do not use built-in path-related functions.
        //public static void Main(string[] args)
        //{
        //    Path path = new Path("C:/a/b/c/d");
        //    path.Cd("x");

        //    Console.WriteLine(path.CurrentPath);
        //    Console.ReadKey();
        //}
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello World!");
    //    }
    //}
    public class TrainComposition
    {
        //A TrainComposition is built by attaching and detaching wagons from the left and the right sides.
        //For example, if we start by attaching wagon 7 from the left followed by attaching wagon 13, again from the left, 
        //we get a composition of two wagons(13 and 7 from left to right). Now the first wagon that can be detached from the right is 7 and the first that can be detached from the left is 13.
        //Implement a TrainComposition that models this problem.
        LinkedList<int> trainStack = new LinkedList<int>();

        public void AttachWagonFromLeft(int wagonId)
        {
            trainStack.AddFirst(wagonId);
        }

        public void AttachWagonFromRight(int wagonId)
        {
            if (trainStack.Count == 0)
            {
                AttachWagonFromLeft(wagonId);
            }
            else
            {
                trainStack.AddLast(wagonId);
            }


        }

        public int DetachWagonFromLeft()
        {
            if (trainStack.Count == 0)
            {
                return 0;
            }
            int returnvalue = trainStack.First.Value;
            trainStack.RemoveFirst();
            return returnvalue;

        }

        public int DetachWagonFromRight()
        {
            if (trainStack.Count == 0)
            {
                return 0;
            }
            int returnvalue = trainStack.Last.Value;
            trainStack.RemoveLast();
            return returnvalue;
        }

        //public static void Main(string[] args)
        //{

        //    TrainComposition tree = new TrainComposition();
        //    tree.AttachWagonFromLeft(7);
        //    tree.AttachWagonFromLeft(13);
        //    Console.WriteLine(tree.DetachWagonFromRight()); // 7 
        //    Console.WriteLine(tree.DetachWagonFromLeft()); // 13
        //    Console.ReadKey();
        //}
    }



    class Palindrome
    {
        public static bool IsPalindrome(string word)
        {
            if (string.IsNullOrEmpty(word)) return true;
            word = word.Trim();
            word = word.ToLower();
            string origWord = word;
            char[] characters = word.ToCharArray();

            Array.Reverse(characters);
            string backword = new string(characters);
            char[] backwordcharacters = backword.ToCharArray();
            characters = word.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] != backwordcharacters[i])
                {
                    return false;
                }
                //Console.WriteLine(characters[i] == backwordcharacters[i]);
            }
            return true;
            //throw new NotImplementedException("Waiting to be implemented.");
        }

        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(Palindrome.IsPalindrome("Deleveled"));
        //    Console.WriteLine(Palindrome.IsPalindrome("Dele veled"));
        //    Console.WriteLine(Palindrome.IsPalindrome(""));
        //    Console.WriteLine(Palindrome.IsPalindrome("Noel sees Leon"));
        //    Console.WriteLine(Palindrome.IsPalindrome("fake"));
        //    Console.ReadKey();
        //}
    }




    public class Node
    {
        public int Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(int value, Node left, Node right)
        {
            Value = value;
            Left = left;
            Right = right;
        }
    }

    public class BinarySearchTree
    {
        public static bool Contains(Node root, int value)
        {

            if (root == null)
                return false;

            if (root.Value == value)
            {
                return true;
            }
            else if (root.Value < value)
            {
                return Contains(root.Right, value);
            }
            else if (root.Value > value)
            {
                return Contains(root.Left, value);
            }

            return false;
        }

        //Binary search tree(BST) is a binary tree where the value of each node is larger or equal to the values in all the nodes in that node's left subtree and is 
        //smaller than the values in all the nodes in that node's right subtree.
        //Write a function that checks if a given binary search tree contains a given value.
        //For example, for the following tree:

        //n1(Value: 1, Left: null, Right: null)
        //n2(Value: 2, Left: n1, Right: n3)
        //n3(Value: 3, Left: null, Right: null)
        //Call to Contains(n2, 3) should return true since a tree with root at n2 contains number 3.

        //public static void Main(string[] args)
        //{
        //    Node n1 = new Node(1, null, null);
        //    Node n3 = new Node(3, null, null);
        //    Node n2 = new Node(2, n1, n3);

        //    Console.WriteLine(Contains(n2, 3));
        //    Console.ReadKey();
        //}
    }

    public class TextInput
    {
        public virtual void Add(char c)
        {
            returnString += c;
        }

        public string returnString { get; set; }

        public string GetValue()
        {
            return returnString;
        }
    }

    public class NumericInput : TextInput
    {
        public override void Add(char c)
        {
            if (char.IsNumber(c))
            {
                base.Add(c);
            }

        }
    }

    public class UserInput
    {
        //public static void Main(string[] args)
        //{
        //    TextInput input = new NumericInput();
        //    input.Add('1');
        //    input.Add('a');
        //    input.Add('0');
        //    Console.WriteLine(input.GetValue());
        //    Console.ReadKey();

        //}
    }

    public class TwoSum
    {

        // Nested for loops can iterate over the list and calculate a sum in O(N^2) time.
        public static Tuple<int, int> FindTwoSum(IList<int> list, int sum)
        {
            if (list.Count < 2)
            {
                return null;
            }

            var indexByValue = new Dictionary<int, int>();
            for (var i = 0; i < list.Count; i++)
            {
                var value = list[i];
                if (!indexByValue.ContainsKey(value))
                {
                    indexByValue.Add(value, i);
                }
            }

            for (var j = 0; j < list.Count; j++)
            {
                var remainder = sum - list[j];
                if (indexByValue.ContainsKey(remainder))
                {
                    return new Tuple<int, int>(j, indexByValue[remainder]);
                }
            }

            return null;
        }

        int c, t;
        object median(string[] o)
        {
            var L = new SortedList<int, int>();
            return o.Select(s =>
                s[0] > 50
                    ? 0 * (t = ~-c / 2) + L.Sum(p =>
                              (t >= 0 & (t -= p.Value) < 0 ? p.Key : 0d) + (t >= -p.Value - ~-c % 2 & t < -~-c % 2 ? p.Key : 0)
                    ) / 2
                    : .1 + 0 * (c += s[0] % 2) *
                        (s[0] > 49 ? L[Int32.Parse(s.Split()[1])]-- : 0) *
                        (L.ContainsKey(t = Int32.Parse(s.Split()[s[0] - 48])) ? L[t]++ : L[t] = 1)
            ).Where(x => x != .1);
        }

    }

}
