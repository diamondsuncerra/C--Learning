using Microsoft.VisualBasic;

namespace WordScrambler.Workers
{
    class SudokuFileReader
    {
        public int[,] ReadFile(string filename)
        {
            int[,] board = new int[9, 9];

            try
            {
                var lines = File.ReadAllLines(filename);
                int row = 0;
                foreach (var line in lines)
                {
                    string[] lineElements = line.Split('|').Skip(1).Take(9).ToArray(); // it begins with |
                                                                                       // input like |9| |7| | |4| etc

                    int col = 0;

                    foreach (var lineElement in lineElements)
                    {
                        var trimmed = lineElement.Trim();
                        board[row, col] = string.IsNullOrEmpty(trimmed) ? 0 : Convert.ToInt16(trimmed);
                        col++;
                    }
                    row++;
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error reading the file: " + e.Message);

            }
            return board;

        }
    }
}