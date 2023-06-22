using System;
using System.Collections.Generic;

class Scripture
{
    private Reference reference;
    private List<Word> words;

    public bool AllWordsHidden { get; private set; }

    public Scripture(string referenceText, string text)
    {
        reference = new Reference(referenceText);
        words = new List<Word>();

        string[] wordStrings = text.Split(' ');
        foreach (string wordString in wordStrings)
        {
            words.Add(new Word(wordString));
        }

        AllWordsHidden = false;
    }

    public void Display()
    {
        Console.WriteLine(reference.GetDisplayText());

        foreach (Word word in words)
        {
            Console.Write(word.GetDisplayText() + " ");
        }
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        int wordsToHide = random.Next(1, words.Count - GetHiddenWordCount() + 1);

        for (int i = 0; i < wordsToHide; i++)
        {
            int index = random.Next(0, words.Count);
            while (words[index].IsHidden)
                index = random.Next(0, words.Count);

            words[index].Hide();
        }

        if (GetHiddenWordCount() == words.Count)
            AllWordsHidden = true;
    }

    private int GetHiddenWordCount()
    {
        int count = 0;
        foreach (Word word in words)
        {
            if (word.IsHidden)
                count++;
        }
        return count;
    }
}