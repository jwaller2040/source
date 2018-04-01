using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public class CodeWarsBeta
    {
        public static int GetScienceScore(string symbols)
        {
            if (string.IsNullOrEmpty(symbols))
            {
                return 0;
            };
            int c = 0, g = 0, t = 0;

            Dictionary<char, int> dic = new Dictionary<char, int>();
            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbols[i] == 'C' || symbols[i] == 'G' || symbols[i] == 'T' || symbols[i] == 'W')
                {
                    if (dic.ContainsKey(symbols[i]))
                    {
                        dic[symbols[i]] += 1;
                    }
                    else
                    {
                        dic.Add(symbols[i], 1);
                    }
                }


            }


            int total = 0;

            int lowest = 0;
            bool first = true;
            if (!dic.ContainsKey('C') && dic.ContainsKey('W'))
            {
                dic.Add('C', 1);
                dic['W'] -= 1;
            }

            if (!dic.ContainsKey('G') && dic.ContainsKey('W'))
            {
                dic.Add('G', 1);
                if (dic['W'] > 0) dic['W'] -= 1;
            }
            if (!dic.ContainsKey('T') && dic.ContainsKey('W'))
            {
                dic.Add('T', 1);
                if (dic['W'] > 0) dic['W'] -= 1;
            }
            if (dic.ContainsKey('C') && dic.ContainsKey('G') && dic.ContainsKey('T') && dic.ContainsKey('W'))
            {
                for (int i = 0; i < dic['W']; i++)
                {
                    if (dic['C'] == dic['G'] && dic['C'] == dic['T'])
                    {
                        dic['C'] += 1;
                    }

                    char key = 'x';
                    var min = Int32.MaxValue;
                    foreach (var item in dic)
                    {
                        if (item.Key != 'W' && item.Value < min)
                        {
                            key = item.Key;
                            min = item.Value;
                        }
                    }
                    if (dic.ContainsKey(key))
                    {
                        dic[key] += 1;
                    }



                }

            }
            foreach (var item in dic)
            {
                if (item.Key != 'W')
                {
                    total += (int)Math.Pow((double)item.Value, (double)2);
                }

            }

            foreach (var item in dic)
            {
                if (item.Key != 'W')
                {
                    if (lowest > item.Value || first)
                    {
                        first = false;
                        lowest = item.Value;
                    }
                }
            }
            if (dic.ContainsKey('C') && dic.ContainsKey('G') && dic.ContainsKey('T')){
                total += (lowest * 7);
            }


            return total;
        }

    }
}
