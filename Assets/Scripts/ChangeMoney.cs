using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMoney : MonoBehaviour
{
    public static string ChangeMoneys(string haveGold) 
    {
        string[] unit = new string[] { "", "a", "b", "c", "d", "e", "f", "g", "h","i","j","k","l","n","m","o",
                "p","q","r","s","t","u","v","w","x","y","z"};
        int[] cVal = new int[27];

        int index = 0;

        while (true)
        {
            string last4 = "";
            if (haveGold.Length >= 4)   
            {
                last4 = haveGold.Substring(haveGold.Length - 4);  
                double intLast4 = int.Parse(last4); 

                cVal[index] = (int)intLast4 % 1000;

                haveGold = haveGold.Remove(haveGold.Length - 3);
            }
            else
            {
                cVal[index] = int.Parse(haveGold);
                break;
            }
            index++;
        }
        if (index > 0)
        {
            int r = cVal[index] * 1000 + cVal[index - 1];
            return string.Format("{0:#.###}{1}", (double)r / 1000f, unit[index]);
        }
        return haveGold;
    }
}
