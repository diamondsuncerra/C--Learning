using System.Runtime.CompilerServices;

namespace WordScrambler.Workers
{
    class SUdokuBoardDisplayer
    {
        public void Display(string title, int[,] board)
        {
            if (!title.Equals(string.Empty))
                Console.Write("{0} {1}", title, Environment.NewLine);

            for (int row = 0; row < board.GetLength(0); row++)
            {
                Console.Write("|");
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    Console.Write("{0}{1}", board[row, col], "|");

                }
                Console.WriteLine();
            }
            Console.WriteLine(); 
        }
    }
}