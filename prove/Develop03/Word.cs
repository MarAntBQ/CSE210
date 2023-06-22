class Word
{
    private string text;
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        this.text = text;
        IsHidden = false;
    }

    public string GetDisplayText()
    {
        if (IsHidden)
            return GetHiddenRepresentation();
        else
            return text;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    private string GetHiddenRepresentation()
    {
        string hiddenText = "";
        foreach (char c in text)
        {
            if (char.IsLetter(c))
                hiddenText += "_";
            else
                hiddenText += c;
        }
        return hiddenText;
    }
}