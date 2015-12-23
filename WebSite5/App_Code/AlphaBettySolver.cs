using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for Teste
/// </summary>
public class AlphaBettySolver
{
    private HashSet<string> foundWords;
    private SpanishWords spanishWords;

    public AlphaBettySolver()
    {
        this.spanishWords = new SpanishWords();
    }

    private int i = 0;
    public string FoundWords()
    {
        string foundWords = "";
        foreach (string word in SortByLength(this.foundWords))
            foundWords = foundWords + word + " ";
        System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\paulo\Desktop\UserspauloDesktopsolved.txt");
        file.WriteLine(foundWords); 
        file.Close();
        return foundWords;
    }

    public string FoundWordsSquares()
    {
        return "0 0|1 1|1 6|2 1|6 1";
    }


    public void Solve()
    {
        this.foundWords = new HashSet<string>();

        System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\paulo\Desktop\UserspauloDesktophello.txt");
        string line = file.ReadLine();    // A A B A | P L P L | A N S A |
        file.Close();
        string[] lines = line.Split('|'); // ["A A B A", "P L P L", "A N S A", ""]

        for (int i = 0; i < lines.Length; i++)
            lines[i] = Regex.Replace(lines[i], @"\s+", ""); // ["AABA", "PLPL", "ANSA"]

        int linesNumber = lines.Length - 1;
        int columnsNumber = lines[0].Length;

        string[,] letters = new string[linesNumber, columnsNumber];
        for (int i = 0; i < linesNumber; i++)
        {
            for (int j = 0; j < columnsNumber; j++)
            {
                letters[i, j] = "" + lines[i][j];
            }
        }

        FindWords(letters);
        FoundWords();
        //Console.WriteLine(spanishWords.Contains("dfvdfv"));
        //Console.WriteLine(spanishWords.Contains("aaban"));
    }


    /// <summary>
    /// Checks all board
    /// </summary>
    public void FindWords(string[,] letters)
    {
        int lines = letters.GetLength(0);
        int columns = letters.GetLength(1);
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                FindWords("", i, j, letters, new HashSet<string>());
            }
        }
    }

    /// <summary>
    /// Find the words starting in square [x,y]
    /// </summary>
    public void FindWords(string currentWord, int line, int col, string[,] letters, HashSet<string> analysedSquares)
    {
        currentWord += letters[line, col];
        int existsWord = spanishWords.Contains(currentWord);
        if (existsWord == 1)
            foundWords.Add(currentWord);
        if (existsWord > -1)
        {
            List<int[]> neighbours = GetNeighbours(line, col, analysedSquares, letters);
            foreach (int[] neighbour in neighbours)
            {
                HashSet<string> newAnalysedSquares = new HashSet<string>(analysedSquares);
                newAnalysedSquares.Add(GetSquareId(line, col));
                FindWords(currentWord, neighbour[0], neighbour[1], letters, newAnalysedSquares);
            }
        }
    }

    private List<int[]> GetNeighbours(int line, int col, HashSet<string> analysedSquares, string[,] letters)
    {
        List<int[]> neighbours = new List<int[]>();
        for (int i = line - 1; i <= line + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                bool currentSquare = (line == i && col == j);
                bool insideBounds = (i > -1 && j > -1) && (i < letters.GetLength(0) && j < letters.GetLength(1));
                bool analysed = analysedSquares.Contains(GetSquareId(i, j));
                if (!currentSquare && insideBounds && !analysed && !(letters[i, j].Equals("-")))
                {
                    int[] square = new int[2] { i, j };
                    neighbours.Add(square);
                }
            }
        }
        return neighbours;
    }

    private string GetSquareId(int line, int col)
    {
        return "" + line + " " + col;
    }

    static IEnumerable<string> SortByLength(IEnumerable<string> e)
    {
        // Use LINQ to sort the array received and return a copy.
        var sorted = from s in e
                     orderby s.Length descending
                     select s;
        return sorted;
    }
}