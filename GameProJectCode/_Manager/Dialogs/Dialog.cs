using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Dialog
{
    private string id;
    private string type = "Dialog";
    private List<Phrase> phrases;
    private Dictionary<string, Dialog> dialogVariance;
    public Dialog Father;

    public string CheckName;
    public Dictionary<MainStats, int> Check;
    public Dialog(Dialog dialogFather=null) {
        Check = new Dictionary<MainStats, int>();
        Father = dialogFather;
        phrases = new List<Phrase>();
        dialogVariance = new Dictionary<string, Dialog>();
    }
    public List<string> GetVarianceName()
    {
       return dialogVariance.Keys.ToList();
    }
    public Dictionary<MainStats,int> GetVarianceCheck()
    {
        var newDictionary = new Dictionary<MainStats, int>();
        foreach(var dialog in dialogVariance.Values)
        {
            if(dialog.Check.Count> 0)
            {
                newDictionary[dialog.Check.Keys.First()] = dialog.Check.Values.First();
            }
        }
        return newDictionary;
    }
    public void AddPhrase(Phrase phrase)
    {
        phrases.Add(phrase);
    }
    public List<Phrase> GetPhrases()
    {
        return phrases;
    }
    public void AddDialogVariance(string key,Dialog dialog)
    {
        dialogVariance[key] = dialog;
    }
    public void RemoveDialogVariance(string key)
    {
        dialogVariance.Remove(key);
    }
    public Dialog GetDialog(string key)
    {
        dialogVariance.TryGetValue(key, out Dialog dialog);
        return dialog;
    }
    public void SetDialogType(string type1)
    {
        type = type1;
    }
    public void SetId(string id)
    {
        this.id = id;
    }
    public string Type => type;
    public string Id => id;
}

