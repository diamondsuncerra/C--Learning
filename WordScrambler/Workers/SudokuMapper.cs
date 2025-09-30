using WordScrambler.Data;

namespace WordScrambler.Workers
{
    class SudokuMapper
    {
        public SudokuMap Find(int givenRow, int givenCol)
        {
            SudokuMap sudokuMap = new SudokuMap();

            if (givenRow >= 0 && givenRow <= 2)
            {
            if (givenCol >= 0 && givenCol <= 2)
            {
                sudokuMap.StartRow = 0;
                sudokuMap.StartCol = 0;
            }
            else if (givenCol >= 3 && givenCol <= 5)
            {
                sudokuMap.StartRow = 0;
                sudokuMap.StartCol = 3;
            }
            else if (givenCol >= 6 && givenCol <= 8)
            {
                sudokuMap.StartRow = 0;
                sudokuMap.StartCol = 6;
            }
            }
            else if (givenRow >= 3 && givenRow <= 5)
            {
            if (givenCol >= 0 && givenCol <= 2)
            {
                sudokuMap.StartRow = 3;
                sudokuMap.StartCol = 0;
            }
            else if (givenCol >= 3 && givenCol <= 5)
            {
                sudokuMap.StartRow = 3;
                sudokuMap.StartCol = 3;
            }
            else if (givenCol >= 6 && givenCol <= 8)
            {
                sudokuMap.StartRow = 3;
                sudokuMap.StartCol = 6;
            }
            }
            else if (givenRow >= 6 && givenRow <= 8)
            {
            if (givenCol >= 0 && givenCol <= 2)
            {
                sudokuMap.StartRow = 6;
                sudokuMap.StartCol = 0;
            }
            else if (givenCol >= 3 && givenCol <= 5)
            {
                sudokuMap.StartRow = 6;
                sudokuMap.StartCol = 3;
            }
            else if (givenCol >= 6 && givenCol <= 8)
            {
                sudokuMap.StartRow = 6;
                sudokuMap.StartCol = 6;
            }
            }

            return sudokuMap;
        }
    }
}