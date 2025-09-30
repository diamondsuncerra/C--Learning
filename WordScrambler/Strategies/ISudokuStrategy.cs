namespace WordScrambler.Strategies
{
    interface ISudokuStrategy
    {
        int[,] Solve(int[,] sudokuBoard)
        {
            
            return sudokuBoard;
        }
    }
}