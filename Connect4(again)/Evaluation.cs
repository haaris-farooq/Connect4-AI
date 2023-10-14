using System;
using System.Collections.Generic;
namespace Connect4_again_
{
    public class Evaluation
    {
        int[,] grid;
        int player;
        int opponent;
        public Evaluation(int[,] board, int playerid, int opponentid)
        {
            grid = board;
            player = playerid;
            opponent = opponentid;
        }
        public int Eval()
        {
            int playerfours = CountSequence(4, player);
            int playerthrees = CountSequence(3, player);
            int playertwos = CountSequence(2, player);
            int opponentfours = CountSequence(4, opponent);
            int opponentthrees = CountSequence(3, opponent);
            int opponenttwos = CountSequence(2, opponent);
            if (opponentfours > 0)
            {
                return -9999999;
            }
            else
            {
                int playerscore = (playerfours * 99999) + (playerthrees * 9999) + (playertwos * 99);
                int opponentscore = (opponentfours * 99999) + (opponentthrees * 9999) + (opponenttwos * 99);
                return playerscore - opponentscore;
            }

        }
        public int CountSequence(int length, int playerid)
        {
            int totalcount = 0;
            Dictionary<int, int> Colourt = new Dictionary<int, int>() {
                { 1, Convert.ToInt32(Spaces.Red) },
                { 2, Convert.ToInt32(Spaces.Yellow) }
            };
            for (int row = 0; row < grid.GetUpperBound(0); row++)
            {
                for (int col = 0; col < grid.GetUpperBound(1); col++)
                {
                    if (grid[row, col] == Colourt[playerid])
                    {
                        totalcount += VerticalSequence(row, col, length);
                        totalcount += HorizontalSequence(row, col, length);
                        totalcount += Asc_DiagonalSequence(row, col, length);
                        totalcount += Desc_DiagonalSequence(row, col, length);
                    }
                }
            }
            return totalcount;
        }
        public int VerticalSequence(int row, int col, int length)
        {
            int count = 0;
            int inc = row;
            do
            {

                if (grid[row, col] == grid[inc, col])
                {
                    count += 1;
                }
                else
                {
                    break;
                }
                inc -= 1;
            } while (inc > 0);
            if (count >= length)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int Asc_DiagonalSequence(int row, int col, int length)
        {
            int count = 0;
            int column_index = col;
            int inc = row;
            do
            {
                if (grid[row, col] == grid[inc, column_index])
                {
                    count += 1;
                }
                else
                {
                    break;
                }
                inc -= 1;
                column_index += 1;
            } while (inc > 0 && column_index < grid.GetUpperBound(1));
            if (count >= length)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int Desc_DiagonalSequence(int row, int col, int length)
        {
            int count = 0;
            int column_index = col;
            for (int i = row; i < grid.GetUpperBound(0); i++)
            {
                if (column_index > grid.GetUpperBound(1))
                {
                    break;
                }
                if (grid[row, col] == grid[i, column_index])
                {
                    count += 1;
                }
                else
                {
                    break;
                }
            }
            if (count >= length)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int HorizontalSequence(int row, int col, int length)
        {
            int count = 0;
            for (int i = col; i < grid.GetUpperBound(1); i++)
            {
                if (grid[row, col] == grid[row, i])
                {
                    count += 1;
                }
                else
                {
                    break;
                }
            }
            if (count >= length)
            {
                return 1;
            }
            else
            {
                return 0;
            }


        }
    }
}
