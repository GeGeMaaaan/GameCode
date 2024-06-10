using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

public class DialogInterpreter
{
    public Dialog Interpret(string filePath)
    {
        Dialog mainDialog = new Dialog();
        Dialog prevDialog = new Dialog();
        Dialog currentDialog= new Dialog();
        List<string> currentKey =new List<string>();
        string[] lines = filePath.Split(new string[] { "\n" }, StringSplitOptions.None);
        lines = lines.Select(line => line.Trim()).ToArray();
        Dictionary<string, string> Speakers = new Dictionary<string, string>();
        bool isResponse = false;
        bool FillVarianceDialog = true;
        List<string> currentResonseList = new List<string>();
        foreach (string line in lines)
        {
            if (line.StartsWith("Type"))
            {
                currentDialog.SetDialogType(line.Split()[2]);
            }
            else if (line.StartsWith("define"))
            {
                Speakers.Add(line.Split("=")[0].Split(" ")[1], line.Split("=")[1]);
            }
            else if (line.StartsWith("Start"))
            {
                currentDialog.SetId(line.Split()[2]);
            }
            else if (line.StartsWith("stats_check"))
            {
                var aboba = line.Split();
                if (Enum.TryParse(aboba[1], true, out MainStats stat))
                {
                    currentDialog.Check[stat] = int.Parse(aboba[2]);
                }
                currentDialog.AddPhrase(new Phrase("System", "Check"));
            }
            else if (line.StartsWith("event_actve"))
            {
                currentDialog.CheckName = line.Split()[1];
                
            }
            else if (line.StartsWith("Event"))
            {
                currentDialog.AddPhrase(new Phrase("System", line));
            }
            else if (line.StartsWith("ph"))
            {
                if (isResponse)
                {
                    currentResonseList.Add(line.Split(":")[1]);
                }
                else
                {
                    var currentLine = line.Split(":");
                    var Speaker = Speakers[currentLine[0].ToString().Split(" ")[1]];
                    var linessa = currentLine[1].Split(new string[] { "\\n" }, StringSplitOptions.None);
                    var curentText = "";
                    foreach(var item in linessa)
                    {
                        curentText += item.Trim()+"\n";
                    }
                    currentDialog.AddPhrase(new Phrase(Speaker, curentText));
                }
            }
            else if (line.StartsWith("start_responses"))
            {
                currentResonseList = new List<string>();
                isResponse = true;
            }
            else if (line.StartsWith("end_responses"))
            {
                currentDialog.AddPhrase(new Phrase("Андрей Левин", currentResonseList));
                isResponse = false;
            }
            else if (line.StartsWith("ans_response")||line.StartsWith("check_res"))
            {
                currentDialog = new Dialog(currentDialog);
                currentKey.Add(line.Split(":")[1]);
            }
            else if (line.StartsWith("end_ans_response")||line.StartsWith("end_check_res"))
            {
                currentDialog.Father.AddDialogVariance(currentKey.Last(),currentDialog);
                currentKey.Remove(currentKey.Last());
                currentDialog = currentDialog.Father;
                if (currentDialog == null)
                {
                    throw new Exception("something Wrong with file");
                }
            }
            else if (line.StartsWith("End"))
            {
                mainDialog = currentDialog;
            }

        }
        return mainDialog;

    }
}