using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SpanishWords
/// </summary>
public class SpanishWords
{
    private Node treeStore = new Node();

    public SpanishWords()
    {
        System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\paulo\Documents\Visual Studio 2015\Projects\WebSite5\words.txt");
        for (int i = 0; i < 247179; i++)
        {
            InsertWord(file.ReadLine());
        }
    }

    private void InsertWord(string word)
    {
        word = word.ToUpper();
        treeStore.Insert(word);
    }

    /// <summary>
    /// -1 
    ///  0 Found this sequence in the begin of word (ex: input "aab" - existing word: "aabam" )
    ///  1 Found this word 
    /// </summary>
    public int Contains(string word)
    {
        word = word.ToUpper();
        return treeStore.Contains(word);
    }

    public override string ToString()
    {
        return treeStore.ToString();
    }
}