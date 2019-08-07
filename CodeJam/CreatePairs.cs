using System;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;

namespace Codejam
{
    class CreatePairs
    {
        int MaximalSum(int[] data)
        {
            int sum = 0;
            //Your code goes here
            if (data.Length = 0)
                Console.WriteLine("Enter More than one Value");
            return sum;
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            CreatePairs createPairs = new CreatePairs();
            do
            {
                int[] data = Array.ConvertAll<string, int>(input.Split(','), delegate (string s) { return Int32.Parse(s); });
                Console.WriteLine(createPairs.MaximalSum(data));
                input = Console.ReadLine();
            } while (input != "*");
        }
        #endregion
    }
}
