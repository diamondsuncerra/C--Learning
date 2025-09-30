using WordScrambler.Data;
using WordScrambler.Workers;

namespace WordScrambler.Strategies
{
    class SimpleMarkupStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;

        public SimpleMarkupStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {

            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    if (sudokuBoard[row, col] == 0 || sudokuBoard[row, col].ToString().Length > 1)
                    {
                        var possibilitiesInRowAndCol = GetPossibilitiesInRowAndCol(sudokuBoard, row, col);
                        var possibilitiesInBlock = GetPossibilitiesInBlock(sudokuBoard, row, col);
                        sudokuBoard[row, col] = GetPossibilityIntersection(possibilitiesInBlock, possibilitiesInRowAndCol);
                    }
                }
            }

            return sudokuBoard;
        }

        private static int GetPossibilityIntersection(int possibilitiesInBlock, int possibilitiesInRowAndCol)
        {  
                string s1 = possibilitiesInBlock.ToString();
                string s2 = possibilitiesInRowAndCol.ToString();

                var digits = s1.Intersect(s2)
                            .Where(char.IsDigit)
                            .Distinct()
                            .OrderBy(c => c);

                return digits.Any() ? int.Parse(new string(digits.ToArray())) : 0;
        }

        private int GetPossibilitiesInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
         {
            SudokuMap sudokuMap = _sudokuMapper.Find(givenRow, givenCol);

            int startRow = sudokuMap.StartRow;
            int startCol = sudokuMap.StartCol;

            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int col = startCol; col < startCol + 3; col++)
            {
                for (int row = startRow; row < startRow + 3; row++)
                {
                    if (isValidSingle(sudokuBoard[row, col]))
                        possibilities[sudokuBoard[row, col] - 1] = 0;
                }
            }
            return Convert.ToInt32(String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
        }

        private int GetPossibilitiesInRowAndCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int col = 0; col < 9; col++)
            {
                if (isValidSingle(sudokuBoard[givenRow, col]))
                    possibilities[sudokuBoard[givenRow, col] - 1] = 0;
            }
            for (int row = 0; row < 9; row++)
            {
                if (isValidSingle(sudokuBoard[row, givenCol]))
                    possibilities[sudokuBoard[row, givenCol] - 1] = 0;
            }

            return Convert.ToInt32(String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
        }

        private bool isValidSingle(int cellDigit)
        {
            return cellDigit != 0 && cellDigit.ToString().Length < 2;

        }
    }
}