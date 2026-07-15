using UnityEngine;


//makes this script into a custom data type
[System.Serializable]
public class CutsceneLine
{
    
    public string speaker;
    
    [TextArea(2, 5)]
    public string dialogue;


}
