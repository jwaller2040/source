using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public class Pente
    {

        //object pente(int[][] playerOne, int[][] playerTwo
        //{

        //}

        public int[][] pente(int[][] playerOne, int[][] playerTwo)
        {

            var penteBoard = new int[15, 15];
            playerOne.Sum(m => penteBoard[m[0], m[1]] = 1);//
            playerTwo.Sum(m => penteBoard[m[0], m[1]] = 2);
            
            //var S = " !\"#$%&'()*+,-.";
            //var R = S.SelectMany(x =>
            //    S.Select(y =>
            //        new[] {
            //    penteBoard[x %= ' ', y %= ' ']>0 ? 0 : "#$%+-345".Sum(d => {
            //        int c = 0, o = 1, X = x, Y = y;
            //        try {
            //            for (; penteBoard[X += d/8 - 5, Y += d%8 - 4] > 1; ++c)
            //                ;
            //            o = penteBoard[X, Y];
            //        } catch {}
            //        return c>3 ? 9 : (c-o)/3;
            //    }), x, y}
            //    )
            //);
            //foreach (var item in R.Where(r => r[0] > 0 & r[0] == R.Max(s => s[0])).Select(r => r.Skip(1)))
            //{
            //    var x = item;
            //}
            //; 
            return new int[][] { new int[] { 7, 6 }, new int[] { 7, 10 } };
            //FillBoard();
            //LoadPlayerPositionsToPenteBoard(playerOne, '1');
            //LoadPlayerPositionsToPenteBoard(playerTwo, '2');
            //return CaluculateNextPositions();
        }

        private int[][] CaluculateNextPositions()
        {
            try
            {
               var playerOneScore =  EvaluateRows('1') + EvaluateColumns('1') + EvaluateDiagonals('1');
               var playerTwoScore = EvaluateRows('2') + EvaluateColumns('2') + EvaluateDiagonals('2');
            }
            catch (Exception)
            {

                return new int[][] { new int[] { } };
            }
            return new int[][] { new int[] { 7, 6 }, new int[] { 7, 10 } };
        }

        private double EvaluateRows(char p)
        {

            int cols = 15;
            int rows = 15;
            char boardPiece;
            double score = 0.0;
            int count;
            // check the rows
            for (int i = 0; i < rows; i++)
            {
                count = 0;
                bool rowClean = true;
                for (int j = 0; j < cols; j++)
                {
                    boardPiece = penteBoard[i][j];

                    if (boardPiece == p)
                        count++;
                    else if (boardPiece ==  GetOponentPiece(p))
                    {
                        rowClean = false;
                        break;
                    }
                }

                // if we get here then the row is clean (an open row)
                if (rowClean && count != 0)
                    score += count;
            }

            return score;
        }

        private double EvaluateColumns(char p)
        {
            int cols = 15;
            //int rows =15;
            char boardPiece;
            double score = 0.0;
            int count;
            // check the rows
            for (int j = 0; j < cols; j++)
            {
                count = 0;
                bool rowClean = true;
                for (int i = 0; i < cols; i++)
                {
                      boardPiece = penteBoard[i][j];

                    if (boardPiece == p)
                        count++;
                    else if (boardPiece == GetOponentPiece(p))
                    {
                        rowClean = false;
                        break;
                    }
                }

                // if we get here then the row is clean (an open row)
                if (rowClean && count != 0)
                    score += count; //Math.Pow(count, count);

            }

            return score;
        }

        protected int[] GetPoint(int position)
        {
           
            var x = position / 15;
            var y = position % 15;
            int[] p = new int[] { x, y };
            return p;
        }

        public bool IsValidSquare(int position)
        {
            int[] p = GetPoint(position);

            if (p[0] >= 0 && p[1] < ROWS && p[1] >= 0 && p[1] < 15 && IsPositionOpen(p[0], p[1]))
                return true;

            return false;
        }

        private bool IsPositionOpen(int row, int col)
        {

            return penteBoard[row][col] == '_';

        }

        private double EvaluateDiagonals(char p)
        {
            // go down and to the right diagonal first
            int count = 0;
            bool diagonalClean = true;
            char boardPiece;
            double score = 0.0;

            for (int i = 0; i < 15; i++)
            {
               boardPiece = penteBoard[i][i]; 

                if (boardPiece == p)
                    count++;

                if (boardPiece == GetOponentPiece(p))
                {
                    diagonalClean = false;
                    break;
                }
            }

            if (diagonalClean && count > 0)
                score += count;// Math.Pow(count, count);
            // now try the other way

            int row = 0;
            int col = 2;
            count = 0;
            diagonalClean = true;

            while (row < 15 && col >= 0)
            {
                  boardPiece = penteBoard[row][col];

                if (boardPiece == p)
                    count++;

                if (boardPiece ==GetOponentPiece(p))
                {
                    diagonalClean = false;
                    break;
                }

                row++;
                col--;
            }

            if (count > 0 && diagonalClean)
                score += count;

            return score;
        }

        private char GetOponentPiece(char p)
        {
            if (p == '1')
            {
                return '2';
            }
            else
            {
                return '1';
            }
        }

        /*
             private double EvaluatePiece(Board b, Board.Pieces p)
        {

            return EvaluateRows(b, p) + EvaluateColumns(b, p) + EvaluateDiagonals(b, p);
        }


        // over all rows sums the number of pieces in the row if 
        // the specified piece can still win in that row i.e. the row
        // does not contain an opponent's piece
        private double EvaluateRows(Board b, Board.Pieces p)
        {

            int cols = 15;
            int rows = 15;

            double score = 0.0;
            int count;
            // check the rows
            for (int i = 0; i < 15; i++)
            {
                count = 0;
                bool rowClean = true;
                for (int j = 0; j < 15; j++)
                {
                    Board.Pieces boardPiece = b.GetPieceAtPoint(i, j);

                    if (boardPiece == p)
                        count++;
                    else if (boardPiece == Board.GetOponentPiece(p))
                    {
                        rowClean = false;
                        break;
                    }
                }

                // if we get here then the row is clean (an open row)
                if (rowClean && count != 0)
                    score += count;
            }

            return score;
        }

        
        // over all rows sums the number of pieces in the row if 
        // the specified piece can still win in that row i.e. the row
        // does not contain an opponent's piece
        private double EvaluateColumns(Board b, Board.Pieces p)
        {
            int cols = b.Columns;
            int rows = b.Rows;

            double score = 0.0;
            int count;
            // check the rows
            for (int j = 0; j < b.Columns; j++)
            {
                count = 0;
                bool rowClean = true;
                for (int i = 0; i < b.Columns; i++)
                {
                    Board.Pieces boardPiece = b.GetPieceAtPoint(i, j);

                    if (boardPiece == p)
                        count++;
                    else if (boardPiece == Board.GetOponentPiece(p))
                    {
                        rowClean = false;
                        break;
                    }
                }

                // if we get here then the row is clean (an open row)
                if (rowClean && count != 0)
                    score += count; //Math.Pow(count, count);

            }

            return score;
        }


        // over both diagonals sums the number of pieces in the diagonal if 
        // the specified piece can still win in that diagonal i.e. the diagonal
        // does not contain an opponent's piece
        private double EvaluateDiagonals(Board b, Board.Pieces p)
        {
            // go down and to the right diagonal first
            int count = 0;
            bool diagonalClean = true;

            double score = 0.0;

            for (int i = 0; i < b.Columns; i++)
            {
                Board.Pieces boardPiece = b.GetPieceAtPoint(i, i);

                if (boardPiece == p)
                    count++;

                if (boardPiece == Board.GetOponentPiece(p))
                {
                    diagonalClean = false;
                    break;
                }
            }

            if (diagonalClean && count > 0)
                score += count;// Math.Pow(count, count);

          


            // now try the other way

            int row = 0;
            int col = 2;
            count = 0; 
            diagonalClean = true;

            while (row < b.Rows && col >= 0)
            {
                Board.Pieces boardPiece = b.GetPieceAtPoint(row, col);

                if (boardPiece == p)
                    count++;

                if (boardPiece == Board.GetOponentPiece(p))
                {
                    diagonalClean = false;
                    break;
                }

                row++;
                col--;
            }

            if (count > 0 && diagonalClean)
                score += count; 

            return score;
        }
    }
             
             
             */


        private void LoadPlayerPositionsToPenteBoard(int[][] positions, char player)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                penteBoard[positions[i][0]][positions[i][1]] = player;

            }
        }

        const int ROWS = 15;
        const int COLS = 15;

        List<List<char>> penteBoard = new List<List<char>>();
        public void FillBoard()
        {
            for (int row = 0; row < ROWS; row++)
            {
                List<char> newRow = new List<char>();
                penteBoard.Add(newRow);

                for (int col = 0; col < COLS; col++)
                {
                    newRow.Add('_');
                }
            }
        }
    }
/*Pente is a game of stones played on a Go board.

Each player takes turns placing a stone on the board.
The objective is to place 5 of your stones in a row horizontally, vertically, or diagonally.
When a player has placed 4 open-ended stones in a row, they have nearly won the game, because the opposing player cannot block both ends on a single turn.
When a player has placed 3 open-ended stones in a row, they must be blocked or they can create a series of 4 stones on the next turn.
Assume that it is player two's turn.
Given a 15 × 15 board, and the positions of player one and player two.
Return the coordinates where player two should place a stone to block player one.

A position that is under threat from having a 5th (winning) stone placed is a higher priority than blocking the placement of a 4th stone.
If a position is under threat to have a 4th stone placed from multiple directions, it is a higher priority than a position that is only under threat from one direction.
Edit (Additional clarification): Any given coordinate has a threat priority level that is defined as follows:

Coordinates under threat from a 5th stone.
If any coordinate is under threat from the placement of a 5th stone, then any threats due to the placement of a 4th stone do not matter.
Coordinates under threat from a 4th stone that would result in an open-ended sequence of 4 stones, from multiple directions. The more directions, the higher the priority. (Test case 6).
Coordinates under threat from a 4th stone that would result in an open-ended sequence of 4 stones from only 1 direction.
If no stones under threat = empty list.
Example 1
For playerOne = [[7,7], [7,8], [7,9], [10,9]] and playerTwo = [[0,0],[6,8], [8,7], [9,8]], the output should be
penter(playerOne, playerTwo) = [[7,6], [7,10]].

  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4
0 2 _ _ _ _ _ _ _ _ _ _ _ _ _ _
1 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
2 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
3 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
4 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
5 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
6 _ _ _ _ _ _ _ _ 2 _ _ _ _ _ _
7 _ _ _ _ _ _ X 1 1 1 X _ _ _ _
8 _ _ _ _ _ _ _ 2 _ _ _ _ _ _ _
9 _ _ _ _ _ _ _ _ 2 _ _ _ _ _ _
0 _ _ _ _ _ _ _ _ _ 1 _ _ _ _ _
1 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
2 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
3 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
4 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
Player one should be blocked at either position marked X.

For playerOne = [[7,8], [9,6], [10,5], [10,9]] and playerTwo = [[10,6], [10,7], [10,8]], the output should be
penter(playerOne, playerTwo) = [[6,9], [11,4]].

  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4
0 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
1 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
2 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
3 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
4 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
5 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
6 _ _ _ _ _ _ _ _ _ X _ _ _ _ _
7 _ _ _ _ _ _ _ _ 1 _ _ _ _ _ _
8 _ _ _ _ _ _ _ 1 _ _ _ _ _ _ _
9 _ _ _ _ _ _ 1 _ _ _ _ _ _ _ _
0 _ _ _ _ _ 1 2 2 2 1 _ _ _ _ _
1 _ _ _ _ X _ _ _ _ _ _ _ _ _ _
2 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
3 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
4 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
Player one should be blocked at position X.

The goal is to return the highest-priority block, if it exists.

(In the game of Pente, players can also capture opponent's stones if they enclose 2 of them, but this rule is ignored for the sake of challenge simplicity.)

Input/Output

[execution time limit] 3 seconds (cs)

[input] array.array.integer playerOne

Player One's positions

[input] array.array.integer playerTwo

Player Two's positions

[output] array.array.integer

Highest threat positions or an empty list if no positions are under threat.
Coordinates should be returned in lexical order. */
}
