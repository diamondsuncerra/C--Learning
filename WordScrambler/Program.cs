using WordScrambler.Strategies;
using WordScrambler.Workers;

namespace WorkdScrambler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SudokuMapper sudokuMapper = new SudokuMapper();
                SudokuBoardStateManager sudokuBoardStateManager = new SudokuBoardStateManager();
                SudokuSolverEngine engine = new SudokuSolverEngine(sudokuBoardStateManager, sudokuMapper);
                SudokuFileReader fileReader = new SudokuFileReader();
                SUdokuBoardDisplayer displayer = new SUdokuBoardDisplayer();

                Console.Write("Please enter file for Sudoku game: ");
                var filename = Console.ReadLine() ?? string.Empty;

                var sudokuBoard = fileReader.ReadFile(filename);

                displayer.Display("Initial State", sudokuBoard);
                bool isSolved = engine.Solve(sudokuBoard);
                displayer.Display("Final state", sudokuBoard);

                Console.WriteLine(isSolved ? "The algorithm has successfully solved the provided puzzle." 
                : "Unfortunately, the algorithm has not succeeded in solving the puzzle.");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} : {1}", "Sudoku Puzzle cannot be solved due to errors: ", e.Message);
            }
        }
    }
}