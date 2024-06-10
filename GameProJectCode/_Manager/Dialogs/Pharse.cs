using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Phrase
{
    public string Speaker;
    public string Text;
    private bool hasVariance = false;
    public List<string> Variances;
    public Phrase(string speaker,string text)
    {
        hasVariance = false;
        Speaker = speaker;
        Text = text;
        Variances = null;
    }
    public Phrase(string speaker, List<string> variance)
    {
        hasVariance = true;
        Speaker = speaker;
        Variances = variance;
        
    }
    public bool HasVariance=> hasVariance;
}

