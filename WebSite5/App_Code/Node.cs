using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Node
/// </summary>
public class Node
{
    public string letter { get; set; }
    private bool isRoot = false;
    private bool isLastLetter = false;
    private int level;
    private Dictionary<string, Node> childNodes;

    public Node()
    {
        this.isRoot = true;
        this.level = 0;
        this.childNodes = new Dictionary<string, Node>();
    }

    public Node(string letter, int level)
    {
        this.letter = letter;
        this.level = level;
        this.childNodes = new Dictionary<string, Node>();
    }

    public void Insert(string word)
    {
        if (word.Equals(""))
            this.isLastLetter = true;
        else
        {
            string firstLetter = "" + word[0];
            if (!childNodes.ContainsKey(firstLetter))
                childNodes.Add(firstLetter, new Node(firstLetter, level + 1));
            childNodes[firstLetter].Insert(word.Substring(1, word.Length - 1));
        }
    }

    /// <summary>
    /// -1 
    ///  0 Found this sequence in the begin of word (ex: input "aab" - existing word: "aabam" )
    ///  1 Found this word 
    /// </summary>
    public int Contains(string word)
    {
        int response;
        if (word.Equals(""))
        {
            if (this.isLastLetter)
                response = 1;
            else
                response = 0;
        }
        else
        {
            string firstLetter = "" + word[0];
            if (!childNodes.ContainsKey(firstLetter))
                response = -1;
            else
                response = childNodes[firstLetter].Contains(word.Substring(1, word.Length - 1));
        }
        return response;
    }



    public override bool Equals(Object obj)
    {
        Node node = obj as Node;
        if (node == null)
            return false;
        else
            return node.letter == this.letter;
    }

    public override int GetHashCode()
    {
        return letter.GetHashCode();
    }

    public override string ToString()
    {
        string message = "";
        if (!isRoot)
            message += "" + this.letter + level + "|";
        foreach (Node node in childNodes.Values)
            message += node;
        return message;
    }
}