using System.Collections.Generic;
using System;

public static class Extensions
{
    private static Random random = new Random();
    
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        for(int i = n - 1; i > 0; --i)
        {
            int j = random.Next(i + 1);
            T temp = list[j];
            list[i] = list[j];
            list[j] = temp;
        }
    }    
}
