using WordScrambler.Workers;
using WordScrambler.Data;
using System.Runtime.CompilerServices;

namespace WordScrambler.Strategies
{
    class NakedPairStrategy(SudokuMapper sudokuMapper) : ISudokuStrategy
    {

        private readonly SudokuMapper _sudokuMapper = sudokuMapper;

        public int[,] Solve(int[,] sudokuBoard)
        {

            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    if (sudokuBoard[row, col] == 0 || sudokuBoard[row, col].ToString().Length > 1)
                    {
                        EliminateNakedPairFromOthersInRow(sudokuBoard, row, col);
                        EliminateNakedPairFromOthersInCol(sudokuBoard, row, col);
                        EliminateNakedPairFromOthersInBlock(sudokuBoard, row, col);
                    }
                }
            }
            return sudokuBoard;

        }

        private void EliminateNakedPairFromOthersInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInRow(sudokuBoard, givenRow, givenCol)) return;
            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (sudokuBoard[givenRow, givenCol] != sudokuBoard[givenRow, col] && sudokuBoard[givenRow, col].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], givenRow, col);
                }
            }
        }

        private void EliminateNakedPair(int[,] sudokuBoard, int nakedPair, int row, int col)
        {
            var valuesToEliminateArr = nakedPair.ToString().ToCharArray();
            foreach (var valueToEliminate in valuesToEliminateArr)
            {
                sudokuBoard[row, col] = Convert.ToInt32(sudokuBoard[row, col].ToString().Replace(valueToEliminate.ToString(), string.Empty));
            }
        }


        private void EliminateNakedPairFromOthersInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInCol(sudokuBoard, givenRow, givenCol)) return;
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (sudokuBoard[givenRow, givenCol] != sudokuBoard[row, givenCol] && sudokuBoard[row, givenCol].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, givenCol);
                }
            }
        }

        private void EliminateNakedPairFromOthersInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInBlock(sudokuBoard, givenRow, givenCol)) return;

            var startRow = _sudokuMapper.Find(givenRow, givenCol).StartRow;
            var startCol = _sudokuMapper.Find(givenRow, givenCol).StartCol;

            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    if (sudokuBoard[givenRow, givenCol] != sudokuBoard[row, col] && sudokuBoard[row, col].ToString().Length > 1)
                    {
                        EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, col);
                    }
                }
            }
        }

        private bool HasNakedPairInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (givenCol != col && IsNakedPair(sudokuBoard[givenRow, col], sudokuBoard[givenRow, givenCol]))
                    return true;

            }
            return false;
        }
        private bool HasNakedPairInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (givenRow != row && IsNakedPair(sudokuBoard[row, givenCol], sudokuBoard[givenRow, givenCol]))
                    return true;
            }
            return false;
        }


        private bool HasNakedPairInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            var startRow = _sudokuMapper.Find(givenRow, givenCol).StartRow;
            var startCol = _sudokuMapper.Find(givenRow, givenCol).StartCol;

            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    if (givenRow != row && givenCol != col && IsNakedPair(sudokuBoard[row, givenCol], sudokuBoard[givenRow, givenCol]))
                        return true;
                }
            }
            return false;
        }

        private bool IsNakedPair(int v1, int v2)
        {
            return v1.ToString().Length == 2 && v1.Equals(v2);
        }

    }
}