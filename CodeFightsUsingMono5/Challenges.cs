using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
   public class Challenges
    {

        /*
         * Given an encoded string, return its corresponding decoded string.
        The encoding rule is: k[encoded_string], where the encoded_string inside the square brackets is repeated exactly k times. 
        Note: k is guaranteed to be a positive integer.
        Note that your solution should have linear complexity because this is what you will be asked during an interview.

        Example
        For s = "4[ab]", the output should be
        decodeString(s) = "abababab";
        For s = "2[b3[a]]", the output should be
        decodeString(s) = "baaabaaa";
        For s = "z1[y]zzz2[abc]", the output should be
        decodeString(s) = "zyzzzabcabc".
         s: "2[2[2[b]]]"
        Output:
        Run the code to see output
        Expected Output:
        "bbbbbbbb"
        s: "2[a]3[b]4[c]5[d]"
        Output:
        Run the code to see output
        Expected Output:
        "aabbbccccddddd"
             */
       static string decodeValuex = string.Empty;
       public static  string decodeString(string s)
        {
            if (s.Length == 1) { return s; }
            decodeValuex = s;
            StringBuilder sb = new StringBuilder();
            StringBuilder result = new StringBuilder();
            string strungNumber = string.Empty;
            int bracketCount = 0;
            for (int i = 1; i < s.Length; i++)
            {
                if (char.IsDigit(s[i-1]))
                {
                    strungNumber += s[i-1];
                    if (s[i] == '[')
                    {
                        bracketCount++;
                        for (int j = i+1; j<s.Length && bracketCount > 0; j++)
                        {
                           if (char.IsDigit(s[j]))
                            {
                                //recusive loop and apend and add // pass the index, string builder???
                                StringBuilder deepDiveSb = new StringBuilder();
                                decodeStringInner(ref j, deepDiveSb);
                                sb.Append(deepDiveSb);
                                bracketCount++;
                            }
                            else if (s[j] != '[' && s[j] != ']')
                            {
                              sb.Append(s[j]);
                            }
                           else if (s[j] == ']')
                            {
                                bracketCount--;
                            }
                            i = j+1;//how to append the last char?? like codefights missing s and 2[f2[e]g] missing g
                           
                        }
                        int count = int.Parse(strungNumber);
                        while (count > 0)
                        {
                            result.Append(sb.ToString());
                            count--;
                        }
                        sb.Clear();
                        strungNumber = string.Empty;
                    }
                }
                else if (s[i-1] != ']' && s[i - 1] != '[' && !char.IsDigit(s[i - 1]))
                {
                    result.Append(s[i-1]);
                }
                
                if (s.Length == i + 1)
                {
                    if (s[i] != ']' && s[i] != '[' && !char.IsDigit(s[i]))
                    {
                        result.Append(s[i]);
                    }
                }
            }
            return result.ToString();
        }

       private static void decodeStringInner(ref int index, StringBuilder result)
        {
            string strungNumber = string.Empty;
            StringBuilder sb = new StringBuilder();
            for (; index < decodeValuex.Length; index++)
            {
               
                if (char.IsDigit(decodeValuex[index - 1]))
                {
                    strungNumber += decodeValuex[index - 1];
                    if (decodeValuex[index] == '[')
                    {
                        for (int j = index + 1; j < decodeValuex.Length && decodeValuex[j] != ']'; j++)
                        {
                           
                            if (char.IsDigit(decodeValuex[j]))
                            {
                                StringBuilder deepDiveSb = new StringBuilder();
                                decodeStringInner(ref j, deepDiveSb);
                                sb.Append(deepDiveSb);
                            }
                            else if (decodeValuex[j] != '[')
                            {
                                sb.Append(decodeValuex[j]);
                            }
                            index = j;
                        }
                        int count = int.Parse(strungNumber);
                        while (count > 0)
                        {
                            result.Append(sb.ToString());
                            count--;
                        }
                        sb.Clear();
                        strungNumber = string.Empty;
                        
                        break;
                    }
                }
                else
                {
                    if (decodeValuex.Length == index + 1)
                    {
                        if (decodeValuex[index] != ']' && decodeValuex[index] != '[' && !char.IsDigit(decodeValuex[index]))
                        {
                            result.Append(decodeValuex[index]);
                        }
                    }
                   
                }

            }
        }

        public static string teeNine(string message)
        {
            if (string.IsNullOrEmpty(message)) return string.Empty;
            message = message.ToLower();
            char[] m = message.ToCharArray();
            //cat = bt
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(2, "abc");
            dic.Add(3, "def");
            dic.Add(4, "ghi");
            dic.Add(5, "jkl");
            dic.Add(6, "mno");
            dic.Add(7, "pqrs");
            dic.Add(8, "tuv");
            dic.Add(9, "wxyz");
            StringBuilder sb = new StringBuilder();
            int currentKey = -1;
            int priorKey = -1;
            string foundValue = string.Empty;
            for (int i = 0; i < m.Length; i++)
            {
                if (dic.Any(x => x.Value.Contains(m[i])))
                {
                    currentKey = dic.FirstOrDefault(x => x.Value.Contains(m[i])).Key;
                    priorKey = currentKey;
                    int index = 0;
                    while(currentKey == priorKey)
                    {
                        if (index >= dic[currentKey].Length)
                        {
                            index = 0;
                        }
                        foundValue = dic[currentKey][index].ToString();
                        index++;
                        i++;
                        if (i >= m.Length)
                        {
                            sb.Append(foundValue);
                            break;
                        }
                        else if (dic.Any(x => x.Value.Contains(m[i])))
                        {
                            currentKey = dic.FirstOrDefault(x => x.Value.Contains(m[i])).Key;
                            if (priorKey != currentKey)
                            {
                                sb.Append(foundValue);
                            i--;
                            break;
                            }
                             
                        } else
                        {
                            sb.Append(foundValue);
                            sb.Append(m[i]);
                            break;
                        }
                       
                    }
                }
                else
                {
                    sb.Append(m[i]);
                }
               
                }
        
            return sb.ToString();
        }
        /*
        2 = abc
        3 = def
        4 = ghi
        5 = jkl
        6 = mno
        7 = pqrs
        8 = tuv
        9 = wxyz
             */


        static int i, decodeValue;
        static string h = "", t;
        //string[][] instead of IEnumerable!!??
        public static IEnumerable<object> autocompleteHighestCsharp(string[] wordlist, string[] actions)
        {

            var z = wordlist.ToList();
            z.Sort(StringComparer.Ordinal);
            for (; i < actions.Length;)
            {
                t = actions[i++];
                decodeValue = h.Length;
                if (t == "PAUSE")
                {
                    decodeValue = z.BinarySearch(h, StringComparer.Ordinal);
                    if (decodeValue < 0) decodeValue = ~decodeValue;
                    yield return z.GetRange(decodeValue, ~z.BinarySearch(h + '|', StringComparer.Ordinal) - decodeValue);
                }
                else if (t != "BACKSPACE") h += t;
                else if (decodeValue > 0) h = h.Substring(0, decodeValue - 1);
            }
        }

        public static string[][] autocompleteOriginal(string[] wordList, string[] actions)
        {
            var w1 = wordList.ToList();
            w1.Sort(StringComparer.Ordinal);
            StringBuilder js = new StringBuilder(wordList.Length + actions.Length);
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

            List<List<string>> returnList = new List<List<string>>();
            if (actions.Length == 1 && actions[0] == "PAUSE")
            {
                returnList.Add(new List<string>(w1));
                return returnList.Select(x => x.ToArray()).ToArray();
            }
            for (int i = 0; i < actions.Length; i++)
            {

                if (actions[i] == "PAUSE")
                {
                    if (dic.ContainsKey(js.ToString()))
                    {
                        returnList.Add(new List<string>(dic[js.ToString()]));
                    }
                    else
                    {
                        if (js.Length - 1 > 0 && dic.ContainsKey(js.ToString().Substring(0, js.Length - 1)) && dic[js.ToString().Substring(0, js.Length - 1)].Count == 0)
                        {
                            dic.Add(js.ToString(), new List<string>());
                            returnList.Add(new List<string>());
                        }
                        else
                        {

                            dic.Add(js.ToString(), new List<string>(from w in w1 where w.Length > js.Length ? w.Substring(0, js.Length) == js.ToString() : false select w));
                            returnList.Add(new List<string>(dic[js.ToString()]));
                        }

                    }

                }
                else if (actions[i] == "BACKSPACE")
                {
                    if (js.Length == 0)
                    {
                        continue;
                    }
                    js.Length--;
                    if (js.Length == 0)
                    {
                        continue;
                    }
                }
                else
                {
                    js.Append(actions[i]);
                    if (string.IsNullOrEmpty(js.ToString()))
                    {
                        continue;
                    }


                }

            }

            return returnList.Select(x => x.ToArray()).ToArray();
        }


        public static string[][] autocomplete(string[] wordList, string[] actions)
        {
            var w1 = wordList.ToList();
            w1.Sort(StringComparer.Ordinal);
            StringBuilder js = new StringBuilder(wordList.Length + actions.Length);
            //Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            var trie = new Trie();
            trie.InsertRange(w1);
            List<List<string>> returnList = new List<List<string>>();
            if (actions.Length == 1 && actions[0] == "PAUSE")
            {
                returnList.Add(new List<string>(w1));
                return returnList.Select(x => x.ToArray()).ToArray();
            }
            for (int i = 0; i < actions.Length; i++)
            {
 
                if (actions[i]=="PAUSE")
                {
                    List<string> temp = trie.GetList(js.ToString());
                    bool valid = true;
                    if (temp.Count > 0) {
                        valid = w1.BinarySearch(temp[0],StringComparer.Ordinal) > -1;
                    }
                    if (valid)
                    {
                        returnList.Add(temp);
                    }
                    else
                    {
                        returnList.Add(new List<string>());
                    }
                  
                }
                else if (actions[i]=="BACKSPACE")
                {
                    if (js.Length == 0)
                    {
                        continue;
                    }
                    js.Length--;
                    if (js.Length==0)
                    {
                        continue;
                   }
                }
                else {
                    js.Append(actions[i]);
                    if (string.IsNullOrEmpty(js.ToString()))
                    {
                        continue;
                    }

                   
                }
            
            }

            return returnList.Select(x => x.ToArray()).ToArray();
        }


    }

    public class TrieNode
    {
        public char Value { get; set; }
        public List<TrieNode> Children { get; set; }
        public TrieNode Parent { get; set; }
        public int Depth { get; set; }

        public TrieNode(char value, int depth, TrieNode parent)
        {
            Value = value;
            Children = new List<TrieNode>();
            Depth = depth;
            Parent = parent;
        }

        public bool IsLeaf()
        {
            return Children.Count == 0;
        }

        public TrieNode FindChildNode(char c)
        {
            foreach (var child in Children)
                if (child.Value == c)
                    return child;

            return null;
        }

      
    }

    public class Trie
    {
        private readonly TrieNode _root;

        public Trie()
        {
            _root = new TrieNode('^', 0, null);
        }

        public TrieNode Prefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                currentNode = currentNode.FindChildNode(c);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        public List<string> GetList(string s)
        {
            var prefix = Prefix(s);
            if (prefix.Depth == s.Length && prefix.FindChildNode('$') != null) {
               return new List<string>();
            }
            else
            {
                var l = Traverse(prefix, new StringBuilder(s));
                return l.ToList();
                
            }
        }
        public bool Search(string s)
        {
            var prefix = Prefix(s);
            return prefix.Depth == s.Length && prefix.FindChildNode('$') != null;
        }

        public void InsertRange(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
                Insert(items[i]);
        }

        public void Insert(string s)
        {
            var commonPrefix = Prefix(s);
            var current = commonPrefix;

            for (var i = current.Depth; i < s.Length; i++)
            {
                var newNode = new TrieNode(s[i], current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }

            current.Children.Add(new TrieNode('$', current.Depth + 1, current));
        }
        private IEnumerable<string> Traverse(TrieNode trieNode, StringBuilder buffer)
        {
            if (trieNode == null)
            {
                yield break;
            }
            if (trieNode.IsLeaf())
            {
                if (buffer.Length == 1)
                {
                    yield return buffer.ToString();
                }
                yield return buffer.ToString().Substring(0,buffer.Length-1);
            }
            foreach (var child in trieNode.Children)
            {
                //if (child.Value=='$') { continue; }
                buffer.Append(child.Value);
                foreach (var word in Traverse(child, buffer))
                {
                   
                    yield return word;
                }
                buffer.Length--;
            }
        }
        
    }
}
