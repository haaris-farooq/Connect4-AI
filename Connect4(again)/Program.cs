using System;
using System.Collections.Generic;
using System.Linq;
namespace Connect4_again_
{

    class Program
    {
        public Dictionary<int, int> Colour = new Dictionary<int, int>() {
                { 1, Convert.ToInt32(Spaces.Red) },
                { 2, Convert.ToInt32(Spaces.Yellow) }
            };
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Initialise();
        }
        private void Initialise()
        {
            while (true)
            {
                int[,] grid = new int[6, 7];
                for (int i = 0; i <= 5; i++)
                {
                    for (int j = 0; j <= 6; j++)
                    {
                        grid[i, j] = Convert.ToInt32(Spaces.Empty);
                    }
                }
                PlayAgainstComputer(grid);
            }
        }
        private void Play(int[,] grid)
        {
            bool win = false;
            bool draw = false;
            int column = 0;
            int playernum = 1;
            bool move = false;
            do
            {
                Display(grid);
                do
                {
                    column = GetMove(grid);
                    move = CanMove(grid, column);
                } while (move == false);
                grid = PlacePiece(grid, column, playernum);
                win = CheckForWin(grid, playernum);
                draw = CheckForDraw(grid);
                if (win == false)
                {
                    playernum = SwitchPlayer(playernum);
                }
            } while (win == false && draw == false);
            if (playernum == 1 && win == true)
            {
                Console.Clear();
                Console.WriteLine("Red Wins");
            }
            else if (playernum == 2 && win == true)
            {
                Console.Clear();
                Console.WriteLine("Yellow Wins");
            }
            else
            {
                Console.WriteLine("It was a Draw");
            }
            Console.WriteLine("Press any key to play again");
        }
        private void PlayAgainstComputer(int[,] grid)
        {
            int playernum = 2;
            int compnum = 1;
            int curr_player = 0;
            bool win = false;
            bool draw = false;
            bool move = false;
            int column = 0;
            int turn = 1;
            do
            {
                if (turn % 2 == compnum % 2)
                {
                    curr_player = compnum;
                }
                else
                {
                    curr_player = playernum;
                }
                Display(grid);
                Console.WriteLine("player {0} turn", curr_player);
                if (curr_player == playernum)
                {
                    do
                    {
                        column = GetMove(grid);
                        move = CanMove(grid, column);
                    } while (move == false);
                    grid = PlacePiece(grid, column, playernum);
                    win = CheckForWin(grid, playernum);
                    draw = CheckForDraw(grid);
                    if (win == false)
                    {
                        turn += 1;
                    }
                }
                else
                {
                    if (turn == 2)
                    {
                        column = 4;
                    }
                    else if (turn <= 6)
                    {
                        column = ComputerDecision(grid, 2, compnum);
                    }
                    else
                    {
                        column = ComputerDecision(grid, 5, compnum);
                    }
                    grid = PlacePiece(grid, column, compnum);
                    win = CheckForWin(grid, compnum);
                    draw = CheckForDraw(grid);
                    if (win == false)
                    {
                        turn += 1;
                    }
                }
            } while (win == false && draw == false);
            Display(grid);
            Console.ReadKey();

        }
        private static int[,] CloneGrid(int[,] grid)
        {
            return (int[,])grid.Clone();
        }
        private static int SwitchPlayer(int num)
        {
            if (num == 1)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        private static int[,] PlacePiece(int[,] grid, int column, int playernum)
        {
            Program p = new Program();
            int[,] newgrid = CloneGrid(grid);
            bool empty = false;
            int y = 5;
            do
            {
                if (newgrid[y, column] == Convert.ToInt32(Spaces.Empty))
                {
                    newgrid[y, column] = p.Colour[playernum];
                    empty = true;
                }
                y -= 1;
            } while (empty == false);
            return newgrid;
        }
        private static int[,] UndoMove(int[,] grid, int column)
        {
            if (CanMove(grid, column) != true || IsColumnEmpty(grid, column))
            {
                return null;
            }
            else
            {
                int y = 5;
                bool notempty = false;
                do
                {
                    if (grid[y, column] != Convert.ToInt32(Spaces.Empty))
                    {
                        notempty = true;
                        grid[y, column] = Convert.ToInt32(Spaces.Empty);
                    }
                    y -= 1;
                } while (notempty == false);
            }
            return grid;
        }
        private static bool IsColumnEmpty(int[,] grid, int column)
        {
            for (int i = 0; i < 6; i++)
            {
                if (grid[i, column] != Convert.ToInt32(Spaces.Empty))
                {
                    return false;
                }
            }
            return true;
        }
        private void Display(int[,] grid)
        {
            Console.Clear();
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    Console.Write("|");
                    if (grid[i, j] == Convert.ToInt32(Spaces.Empty))
                    {
                        Console.Write("_");
                    }
                    else if (grid[i, j] == Convert.ToInt32(Spaces.Red))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (grid[i, j] == Convert.ToInt32(Spaces.Yellow))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.WriteLine();

            }
        }
        private static int GetMove(int[,] grid)
        {
            Program p = new Program();
            int userposition = 1;
            int column = 0;
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            Console.SetCursorPosition(1, 5);
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.RightArrow && userposition < 12)
                {
                    userposition += 2;
                    column += 1;
                    Console.SetCursorPosition(userposition, 5);
                }
                else if (key.Key == ConsoleKey.LeftArrow && userposition > 1)
                {
                    userposition -= 2;
                    column -= 1;
                    Console.SetCursorPosition(userposition, 5);
                }
            } while (key.Key != ConsoleKey.Enter);
            return column;
        }
        private static bool CanMove(int[,] grid, int column)
        {
            bool move = false;
            for (int x = 0; x <= 5; x++)
            {
                if (grid[x, column] == Convert.ToInt32(Spaces.Empty))
                {
                    move = true;
                }
            }
            return move;
        }
        private static bool CheckForDraw(int[,] grid)
        {
            bool draw = true;
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    if (grid[i, j] == Convert.ToInt32(Spaces.Empty))
                    {
                        return false;
                    }
                }
            }
            return draw;
        }
        private static bool CheckForWin(int[,] grid, int playernum)
        {
            Program p = new Program();
            bool win = false;
            int num = p.Colour[playernum];
            //Horizontal Check
            for (int j = 0; j <= 3; j++)
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (grid[i, j] == num && grid[i, j + 1] == num && grid[i, j + 2] == num && grid[i, j + 3] == num)
                    {
                        return true;
                    }
                }
            }
            //Vertical check
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    if (grid[i, j] == num && grid[i + 1, j] == num && grid[i + 2, j] == num && grid[i + 3, j] == num)
                    {
                        return true;
                    }
                }
            }

            //Ascending Diagonal Check
            for (int i = 3; i <= 5; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (grid[i, j] == num && grid[i - 1, j + 1] == num && grid[i - 2, j + 2] == num && grid[i - 3, j + 3] == num)
                    {
                        return true;
                    }
                }
            }
            //Descending Diagonal Check
            for (int i = 3; i <= 5; i++)
            {
                for (int j = 3; j <= 6; j++)
                {
                    if (grid[i, j] == num && grid[i - 1, j - 1] == num && grid[i - 2, j - 2] == num && grid[i - 3, j - 3] == num)
                    {
                        return true;
                    }
                }
            }
            return win;
        }
        public static int[] GetLegalMoves(int[,] grid)
        {
            List<int> moves = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                if (CanMove(grid, i))
                {
                    moves.Add(i);
                }
            }
            return moves.ToArray();
        }
        public static int ComputerDecision(int[,] grid, int depth, int playerid)
        {
            Program p = new Program();
            int opponent = 0;
            if (playerid == 1)
            {
                opponent = 2;
            }
            else
            {
                opponent = 1;
            }
            int[,] newgrid;
            int[] moves = GetLegalMoves(grid);
            //If only one valid move then play that move
            //if (moves.Length == 1)
            //{
            //    return moves[0];
            //}
            int win = -1;
            int stopwin = -1;
            //heuristic: no need to search if there is a clear move
            for (int i = 0; i < moves.Length; i++)
            {
                newgrid = PlacePiece(grid, moves[i], playerid);
                //winning move
                if (CheckForWin(newgrid, playerid) == true)
                {
                    win = moves[i];
                }
                newgrid = PlacePiece(grid, moves[i], opponent);
                //Stops opponent from winning
                if (CheckForWin(newgrid, opponent) == true)
                {
                    stopwin = moves[i];
                }
            }
            //winning is prioritised over stopping opponent winning in one move
            if (win != -1)
            {
                return win;
            }
            else if (stopwin != -1)
            {
                return stopwin;
            }
            //else keep on going and search for best move
            return p.MiniMaxAlphaBeta(grid, depth, playerid, moves);
        }
        public int MiniMaxAlphaBeta(int[,] grid, int depth, int playerid, int[] validmoves)
        {
            Random rnd = new Random();
            validmoves = validmoves.OrderBy(x => rnd.Next()).ToArray();
            int bestmove = validmoves[0];
            int bestscore = -9999999;
            int alpha = -9999999;
            int beta = 9999999;
            int opponent = 0;
            int boardscore = 0;

            if (playerid == 1)
            {
                opponent = 2;
            }
            else
            {
                opponent = 1;
            }
            for (int i = 0; i < validmoves.Length; i++)
            {
                int[,] tempboard = PlacePiece(grid, validmoves[i], playerid);
                boardscore = MinimizeBeta(tempboard, depth - 1, alpha, beta, playerid, opponent);
                if (boardscore > bestscore)
                {
                    bestscore = boardscore;
                    bestmove = validmoves[i];
                }
            }
            return bestmove;
        }
        public int MinimizeBeta(int[,] grid, int depth, int a, int b, int playerid, int opponent)
        {
            int[] moves = GetLegalMoves(grid);
            Evaluation eval = new Evaluation(grid, playerid, opponent);
            if (depth == 0 || moves.Length == 0 || CheckForWin(grid, playerid) == true || CheckForWin(grid, opponent) == true)
            {
                return eval.Eval();
            }
            int beta = b;
            for (int i = 0; i < moves.Length - 1; i++)
            {
                int boardscore = 9999999;
                if (a < beta)
                {
                    int[,] tempboard = PlacePiece(grid, moves[i], opponent);
                    boardscore = MaximiseAlpha(tempboard, depth - 1, a, beta, playerid, opponent);
                }
                if (boardscore < beta)
                {
                    beta = boardscore;
                }
            }
            return beta;
        }
        public int MaximiseAlpha(int[,] grid, int depth, int a, int b, int playerid, int opponent)
        {
            int[] moves = GetLegalMoves(grid);
            Evaluation eval = new Evaluation(grid, playerid, opponent);
            if (depth == 0 || moves.Length == 0 || CheckForWin(grid, playerid) == true || CheckForWin(grid, opponent) == true)
            {
                return eval.Eval();
            }
            int alpha = a;
            for (int i = 0; i < moves.Length - 1; i++)
            {
                int boardscore = -9999999;
                if (alpha < b)
                {
                    int[,] tempboard = PlacePiece(grid, moves[i], playerid);
                    boardscore = MinimizeBeta(tempboard, depth - 1, alpha, b, playerid, opponent);
                }
                if (boardscore > alpha)
                {
                    alpha = boardscore;
                }
            }
            return alpha;
        }
    }
}
