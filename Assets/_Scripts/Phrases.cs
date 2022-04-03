using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Phrases
{
    public static string[] winPhrases =
    {
        "You got away this time!", 
        "Say no, to death!",
        "No time to die!",
        "Nice try, Death!",
        "You've survived, for now..."
    };

    public static string[] losePhrases =
   {
        "Better luck next time!",
        "That was just sad...",
        "You just had to die, didn't you?",
        "In the end, death always gets you.",
        "Ded"
    };

    public static string getRandomWinPhrase()
    {
        string phrase = winPhrases[Random.Range(0, winPhrases.Length)];
        return phrase;
    }

    public static string getRandomLosePhrase()
    {
        string phrase = losePhrases[Random.Range(0, losePhrases.Length)];
        return phrase;
    }
}
