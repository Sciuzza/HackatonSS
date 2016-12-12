using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextNewsRead : MonoBehaviour {

    public string[] newsParagraphs;
    public TextAsset ciccio;

    public string temp;

    void Awake()
    {
        ciccio = Resources.Load("DelittoCat") as TextAsset;

        temp = ciccio.text;

        for (int i = 0; i < temp.Length; i++)
        {
            if (i % 134 == 0)
                AppendingPageSeparator(i);
                
        }

        newsParagraphs = ciccio.text.Split('*');
    }

    private void AppendingPageSeparator(int i)
    {
        int j;

        for (j = i; j < temp.Length; j++)
        {
            if (temp[j] != ' ')
                continue;
            else
                break;
        }

        if (j < temp.Length)
            temp.Insert(j, "*");

    }
}
