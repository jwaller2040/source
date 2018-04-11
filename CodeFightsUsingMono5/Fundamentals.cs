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

       public static string reverseParentheses(string s)
        {
           
            bool start = false;
            StringBuilder sb = new StringBuilder();
            StringBuilder mainBuilder = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
               
                if (s[i] == '(')
                {
                  
                    start = true;
                    continue;
                }
                if (s[i] == ')')
                {
                   
                    start = false;
                    char[] charArray = sb.ToString().ToCharArray();
                    Array.Reverse(charArray);                   
                    mainBuilder.Append(new string(charArray));
                    sb.Clear();
                    continue;
                }
                if (start)
                {
                    sb.Append(s[i]);
                }
                else
                {
                    mainBuilder.Append(s[i]);
                }
               
            }

            return mainBuilder.ToString(); ;
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
              if (h.Contains(i)) {
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

            int total1 =0, total2 =0;

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
                    if (h.Contains(j))  continue; 

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
            //10ms
            //if (n == 1)
            //{
            //    return 1;
            //}
            //else
            //{
            //    return ((n * 4) - 4) + shapeArea(n - 1);
            //}

            //10ms
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

    }


}
