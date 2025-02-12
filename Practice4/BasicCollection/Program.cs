﻿using System;
using System.Collections;

namespace BasicCollection
{
    internal class Program
    {
        static void Main()
        {
            ArrayList myList = new ArrayList();
            myList.Add("First");
            myList.Add("Second");
            myList.Add("Third");
            myList.Add("Fourth");

            foreach (string item in myList)
            {
                Console.WriteLine("Unsorted: {0}", item);
            }

            myList.Sort();

            foreach (string item in myList)
            {
                Console.WriteLine("Sorted: {0}", item);
            }
        }
    }
}