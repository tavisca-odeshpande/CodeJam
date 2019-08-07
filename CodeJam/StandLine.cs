using System;
using System.Collections.Generic;
using System.Text;

namespace Codejam
{
    class StandInLine
    {
        int[] Reconstruct(int[] left)
        {
            List<int> finalList = new List<int>();
            for(int i= left.Length-1; i>=0 ; i--)
            {
                finalList.Insert(left[i],i+1);
            }
            return finalList.ToArray();
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            StandInLine standInLine = new StandInLine();
            do
            {
                int[] left = Array.ConvertAll<string, int>(input.Split(','), delegate (string s) { return Int32.Parse(s); });
                Console.WriteLine(string.Join(",", Array.ConvertAll<int, string>(standInLine.Reconstruct(left), delegate (int s) { return s.ToString(); })));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}
