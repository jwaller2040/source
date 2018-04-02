using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public class Fundamentals
    {

        // Definition for singly-linked list:
        public class ListNode<T>
        {
            public T value { get; set; }
            public ListNode<T> next { get; set; }
        }
        //
        public static bool isListPalindrome(ListNode<int> l)
        {
            

            return true;

        }

        public static bool isCryptSolution(string[] crypt, char[][] solution)
        {
          
           
            for (int word = 0; word < crypt.Length; word++)
            {
                for (int letter = 0; letter < crypt[word].Length; letter++)
                {
                    for (int keyLetter = 0; keyLetter < solution.Length; keyLetter++)
                    {
                        crypt[word] =  crypt[word].Replace(solution[keyLetter][0], solution[keyLetter][1]);
                    }

                }
            }

            for (int wordToNumber = 0; wordToNumber < crypt.Length; wordToNumber++)
            {
                if (crypt[wordToNumber].Length > 1 &&crypt[wordToNumber].StartsWith("0"))
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


    }
}
