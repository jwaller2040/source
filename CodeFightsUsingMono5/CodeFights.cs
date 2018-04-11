using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{

    //public class NodeClass : IEnumerable<NodeClass>
    //{
    //    private readonly Dictionary<string, NodeClass> _children = new Dictionary<string, NodeClass>();
    //    public readonly string ID;
    //    public NodeClass Parent { get; private set; }
    //    public NodeClass(string id)
    //    {
    //        this.ID = id;
    //        this.Total += 1;
    //    }
    //    public NodeClass GetChild(string id)
    //    {
    //        NodeClass returnValue;
    //        if (this._children.TryGetValue(id, out returnValue)) {
    //            return returnValue;
    //        }
    //        else
    //        {
    //            return null;
    //        }

    //    }
    //    public void Add(NodeClass item)
    //    {
    //        if (item.Parent != null)
    //        {
    //            item.Parent._children.Remove(item.ID);
    //        }
    //        item.Parent = this;

    //        if (this._children.ContainsKey(item.ID))
    //        {
    //            this._children[item.ID].Total += 1;
    //            item._children.ToList().ForEach(x => this._children[item.ID]._children[x.Key] = x.Value);
    //            List<string> orignialKeyList = new List<string>(this._children[item.ID]._children.Keys);
    //            List<string> keyList = new List<string>(item._children.Keys);
    //            bool hasMatch = orignialKeyList.Any(x => keyList.Any(y => y == x));
    //            if (hasMatch)
    //            {
    //                foreach (var keys in keyList)
    //                {
    //                    UpdateTotals(this._children[item.ID]._children[keys]);
    //                }
    //            }
    //            else
    //            {
    //                foreach (var x in keyList.Where(b => orignialKeyList.Any(a => !b.Contains(a))))
    //                {
    //                    this._children[item.ID]._children.Add(x, item._children[x]);
    //                }    

    //            }

    //        }
    //        else
    //        {
    //            this._children.Add(item.ID, item);
    //        }

    //    }

    //    private void UpdateTotals(NodeClass item)
    //    {
    //        if (item != null)
    //        {
    //            item.Total += 1;
    //            if (item._children.Count > 0)
    //            {
    //                List<string> keyList = new List<string>(item._children.Keys);
    //                foreach (var keys in keyList)
    //                {
    //                    UpdateTotals(item._children[keys]);
    //                }
    //            }


    //        }

    //    }

    //    public IEnumerator<NodeClass> GetEnumerator()
    //    {
    //        return this._children.Values.GetEnumerator();
    //    }

    //    IEnumerator<NodeClass> IEnumerable<NodeClass>.GetEnumerator()
    //    {
    //        return this._children.Values.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return this.GetEnumerator();
    //    }

    //    public int Total { get; set; }
    //    public int Count
    //    {
    //        get
    //        {
    //            return this._children.Count;
    //        }
    //    }
    //}
    public class TreeClass
    {


        public static string[] countAPI(string[] calls)
        {
            if (calls == null)
            {
                throw new ArgumentNullException(nameof(calls));
            }

            List<string> callsList = new List<string>();
            callsList.AddRange(calls);
            //callsList.Sort();
            Dictionary<string, Folders> dic = new Dictionary<string, Folders>();

            foreach (string item in callsList)
            {
                var lines = item.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (dic.ContainsKey(lines[0]))
                {
                    dic[lines[0]].Total += 1;
                    CheckUpdate(dic[lines[0]], lines, 0);
                }
                else
                {
                    dic.Add(lines[0], new Folders(lines[0]) { Total = 1 });
                    CheckUpdate(dic[lines[0]], lines, 0);
                }

            }

            List<string> returnList = new List<string>();

            foreach (var item in dic)
            {
                returnList.Add(string.Concat("--", item.Value.Name, $" ({item.Value.Total})"));
                SubLevels(returnList, item.Value.childFolders, "--");

            }



            return returnList.ToArray();

        }

        private static void SubLevels(List<string> returnList, Dictionary<string, Folders> childFolders, string level)
        {
            level += "--";

            foreach (var item in childFolders)
            {
                returnList.Add(string.Concat(level, item.Value.Name, $" ({item.Value.Total})"));

                SubLevels(returnList, item.Value.childFolders, level);

            }

        }

        private static void CheckUpdate(Folders folders, string[] lines, int index)
        {
            //starts with proj1 or 2
            //sub1 or 2
            //meth1 or 2
            index += 1;
            if (lines.Length <= index) return;

            if (folders.childFolders != null)
            {
                if (folders.childFolders.ContainsKey(lines[index]))
                {
                    folders.childFolders[lines[index]].Total += 1;
                    CheckUpdate(folders.childFolders[lines[index]], lines, index);
                }
                else
                {
                    folders.childFolders.Add(lines[index], new Folders(lines[index]) { Total = 1 });
                    CheckUpdate(folders.childFolders[lines[index]], lines, index);

                }

            }
            else
            {
                folders.childFolders.Add(lines[index], new Folders(lines[index]) { Total = 1 });
                CheckUpdate(folders.childFolders[lines[index]], lines, index);
            }



        }

    }

    public class Folders
    {
        public Dictionary<string, Folders> childFolders { get; set; }

        public Folders(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Total = 0;
            childFolders = new Dictionary<string, Folders>();
        }


        public string Name { get; set; }
        public int Total { get; set; }
    }

    public class CFBot
    {
        public static bool plagiarismCheck(string[] code1, string[] code2)
        {
            if (code1.Length != code2.Length)
            {
                return false;
            }
            //var pattern = @"([a-zA-Z0-9_-]{1,})";
            var pattern = @"([a-zA-Z0-9_-]{1,})|([^\s\(\)\,\;]{1,})";//([\w+]{1,})"; //need to look at signs too +-*/
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < code1.Length; i++)
            {
                var variableMatches1 = System.Text.RegularExpressions.Regex.Matches(code1[i], pattern);
                var variableMatches2 = System.Text.RegularExpressions.Regex.Matches(code2[i], pattern);

                if (variableMatches1.Count != variableMatches2.Count)
                {
                    return false; //if the matches count don't match get out
                }

                for (int m = 0; m < variableMatches1.Count; m++)
                {
                    if (dic.ContainsKey(variableMatches1[m].Value))
                    {
                        if (dic[variableMatches1[m].Value] != variableMatches2[m].Value)
                        {
                            return false; //if you have a variable in the same position but not the samve value as old it's not a match
                        }
                    }
                    else
                    {
                        //valid variable name not a number or symbol
                        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^(?![0-9]*$)[a-zA-Z0-9_-]+$");
                        if (r.IsMatch(variableMatches1[m].Value))
                        {
                            dic.Add(variableMatches1[m].Value, variableMatches2[m].Value);
                        }
                        else if (variableMatches1[m].Value != variableMatches2[m].Value)
                        {
                            return false;
                        }
                    }
                }
            }
            //string size test...
            StringBuilder code1builder = new StringBuilder();
            StringBuilder code2builder = new StringBuilder();
            foreach (string value in code1)
            {
                code1builder.Append(value);
              
            }
            foreach (string value in code2)
            {
                code2builder.Append(value);

            }
            string changeToMatch = code2builder.ToString();
            foreach (var item in dic)
            {
                changeToMatch = changeToMatch.Replace(item.Value, item.Key);
            }

            return changeToMatch.Length == code1builder.ToString().Length;
        }









        public static string[] taskMaker(string[] source, int challengeId)
        {
            List<string> returnValue = new List<string>();
            foreach (var item in source)
            {
                if (item.Contains(@"//DB"))
                {
                    if (item.Contains(string.Format(@"//DB {0}", challengeId.ToString())))
                    {
                        returnValue.RemoveAt(returnValue.Count - 1);
                        returnValue.Add(item.Replace(string.Format(@"//DB {0}//", challengeId.ToString()), ""));
                    }
                }
                else
                {
                    returnValue.Add(item);
                }
            }
            return returnValue.ToArray();
        }

        public static double companyBotStrategy(int[][] trainingData)
        {
            int totalCorrectnessFound = 0;
            int totalAnswerTime = 0;

            for (int i = 0; i < trainingData.Length; i++)
            {

                if (trainingData[i][1] == 1)
                {
                    totalCorrectnessFound += 1;
                    totalAnswerTime += trainingData[i][0];
                }

            }

            if (totalCorrectnessFound == 0) { return 0.0; }

            if (totalAnswerTime == 0) { return 0.0; }


            return (double)totalAnswerTime / totalCorrectnessFound;
        }


    }


}
