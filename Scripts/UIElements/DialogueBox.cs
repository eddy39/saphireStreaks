using Godot;
using System;

public class DialogueBox : TextureRect
{

    public Label TextLabel;
    public Tween tween;

    // vars
    public BaseDialogue dialogue;
    public float writingSpeed = 12; // chars/s

    public bool currentLineFinished = false;
    public bool dialogueRunning = false;
    public override void _Ready()
    {
        TextLabel = (Label) FindNode("Label");
        tween = (Tween) FindNode("Tween");

        tween.Connect("tween_all_completed",this,"LineFinished");

    }
    public void StartDialogue()
    {
        if (dialogueRunning)
        {
            // if line finished Next line
            if (currentLineFinished)
            {
                // check if dialogue done
                if (dialogue.currendInd>= dialogue.TextList.Count)
                {
                    Visible = false;
                    dialogueRunning = false;
                    dialogue.currendInd  = 0;
                    return;
                }
                NextLine();
            } else {
                // if not finished , finish line instantly
                TextLabel.PercentVisible = 1;
                tween.StopAll();
                currentLineFinished = true;
            }
            return;
        }
        dialogueRunning = true;
        Visible = true;
        
        NextLine();
    }
    public void NextLine()
    {
        TextLabel.Text = dialogue.TextList[dialogue.currendInd];
        float duration = TextLabel.Text.Length / writingSpeed;
        TextLabel.PercentVisible = 0;
        tween.InterpolateProperty(TextLabel,"percent_visible",0,1,duration);
        tween.Start();

        dialogue.currendInd+=1;
        currentLineFinished = false;
    }
    public void LineFinished()
    {
        currentLineFinished = true;
    }

}
