using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

       public static int shortestSolutionLength(string[] source)
        {
            var pattern = @"(/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/)|(//.*)";
            Regex rgx = new Regex(pattern, RegexOptions.Multiline);

            StringBuilder sb = new StringBuilder();
            foreach (var item in source)
            {
                //if (item.StartsWith("//")) { continue; }
                sb.AppendLine(item);
            }

            string s = rgx.Replace(sb.ToString(), "").Trim();
           
            int lineCount = 0;
            foreach (var item in s)
            {

                if (!char.IsWhiteSpace(item))
                {
                    lineCount++;
                }

            }

            return lineCount;
        }




       public static int[][] opponentMatching(int[] xp)
        {
            if (xp.Length < 2) return new int[][] { };
            var xpList = xp.Zip(Enumerable.Range(0, xp.Length), Tuple.Create).ToList();
            
            xpList.Sort((a,b) => a.Item1 - b.Item1);

           
            List<List<int>> returnList = new List<List<int>>();
           

            while (xpList.Count >= 2)
            {
                int ps = 0, ds = -1;
                for (int i = 1; i < xpList.Count; i++)
                {
                    int d = xpList[i].Item1 - xpList[i - 1].Item1;
                    if (ds == -1 || d < ds)
                    {
                        ps = i;
                        ds = d;
                    }
                }

                var np = new List<int> { xpList[ps-1].Item2, xpList[ps].Item2 };
                np.Sort((a,b)=>a-b);
                returnList.Add(np);
                xpList.RemoveRange(ps-1, 2);
            }

            return returnList.Select(x => x.ToArray()).ToArray();



        }

        /*
         * xp: [200, 100, 70, 130, 100, 800, 810]
Output:
Run the code to see output
Expected Output:
[[1,4], 
 [5,6], 
 [2,3]]


xpxp::  [1, 1000000000]
Output:
Run the code to see output
Expected Output:
[[0,1]]

            xp: [1000000000, 100000000, 1]
Output:
Run the code to see output
Expected Output:
[[1,2]]

            xp: [239]
Output:
Run the code to see output
Expected Output:
[]

            Input:
xp: [1, 5, 11, 3, 1, 16, 100]
Output:
Run the code to see output
Expected Output:
[[0,4], 
 [1,3], 
 [2,5]]

         * 
         When you click the VS Fight button on CodeFights, the system tries to match you with the best opponent possible. The matching algorithm has become more complex over time,but initially it was a simple search for someone whose xp is as close to yours as possible.

        The easiest way to understand how it used to conduct the search is as follows:



        Imagine that each user looking for an opponent is standing at the center of a search circle on a horizontal xp axis.
        All the search circles have the same radius (the search radius), and initially search radius is equal to 0.
        At each step, the search radius is increased by 1.
        A match is found as soon as two search circles intersect. These circles are then removed immediately.
        For the sake of simplicity, assume that on each step no more than one pair of circles can intersect.
        Given a list of requests as user xps, match them up using the algorithm described above.

        Example

        For xp = [200, 100, 70, 130, 100, 800, 810], the output should be
        opponentMatching(xp) = [[1, 4], [5, 6], [2, 3]].

        Initially, search ranges for users 1 and 4 (these are their IDs equal to 0-based indices) coincide, so they form the first pair.
        After 5 steps search circles of users 5 and 6 intersect. Thus, they form the second pair.
        After 25 more steps search circles of users 2 and 3 intersect. Thus, they form the third pair.
        Finally, user 0 remains without an opponent.
        Input/Output

        [execution time limit] 3 seconds (cs)

        [input] array.integer xp

        Array of positive integers.
        xp[i] equals XP points earned by the user with ID = i.

        Guaranteed constraints:
        1 ≤ xp.length ≤ 15,
        1 ≤ xp[i] ≤ 109.

        [output] array.array.integer

        Array of pairs of opponents. Pairs should be stored in the same order as they were formed by the above-described algorithm. 
        Elements in pairs should be sorted according to their IDs.
             
             */


        public static int marathonTaskScore(int marathonLength, int maxScore, int submissions, int successfulSubmissionTime)
        {

            //if (successfulSubmissionTime < 0)
            //{
            //    return 0;
            //}
            //if (successfulSubmissionTime == 0 || successfulSubmissionTime == 1)
            //{
            //    return maxScore;
            //}
            //else
            //{
            //    int p = (10 * (submissions - 1));
            //    int q = successfulSubmissionTime * maxScore / 2 * 1 / marathonLength;
            //    int t = maxScore - p - q;

            //    if (t < (maxScore / 2))
            //    {
            //        return maxScore / 2;
            //    }
            //    return t;

            //}

            int pen = 0;
            pen += (maxScore / 2 * 1 / marathonLength) * successfulSubmissionTime;
            pen += 10 * (submissions - 1);
            var ret = Math.Max(maxScore - pen, maxScore / 2);
            return (successfulSubmissionTime < 0 ? 0 : ret);

        }

        /*
         In CodeFights marathons, each task score is calculated independently. For a specific task, you get some amount of points if you solve it correctly,
         or you get a 0. Here is how the exact number of points is calculated:

        If you solve a task on your first attempt within the first minute, you get maxScore points.
        Each additional minute you spend on the task adds a penalty of (maxScore / 2) * (1 / marathonLength) to your final score.
        Each unsuccessful attempt adds a penalty of 10 to your final score.
        After all the penalties are deducted, if the score is less than maxScore / 2, you still get maxScore / 2 points.
        Implement an algorithm that calculates this score given some initial parameters.

        Example

        For
        marathonLength = 100,
        maxScore = 400,
        submissions = 4 and
        successfulSubmissionTime = 30, the output should be

        marathonTaskScore(marathonLength, maxScore, 
                          submissions, successfulSubmissionTime) = 310
        Three unsuccessful attempts cost 10 * 3 = 30 points. 30 minutes adds 30 * (400 / 2) * (1 / 100) = 60 more points to the total penalty. So the final score is 400 - 30 - 60 = 310.

        Keeping the same input parameters as above but changing the number of attempts to 95 we get:
        marathonTaskScore(marathonLength, maxScore, submissions, successfulSubmissionTime) = 200;

        400 - 10 * 94 - 30 * (400 / 2) * (1 / 100) = -600. But the score for this task cannot be less than 400 / 2 = 200, so the final score is 200 points.

        For marathonLength = 100, maxScore = 400, submissions = 4 and successfulSubmissionTime = -1, the output should be
        marathonTaskScore(marathonLength, maxScore, submissions, successfulSubmissionTime) = 0.

        The task wasn't solved, so it doesn't give any points.

        Input/Output

        [execution time limit] 3 seconds (cs)

        [input] integer marathonLength

        A positive integer representing the length of the marathon in minutes.

        Guaranteed constraints:
        100 ≤ marathonLength ≤ 1000.

        [input] integer maxScore

        A positive integer. It is guaranteed that maxScore is divisible by 2 * marathonLength.

        Guaranteed constraints:
        400 ≤ maxScore ≤ 2000.

        [input] integer submissions

        A positive integer equal to the number of submissions made by the user for the specific task.

        Guaranteed constraints:
        1 ≤ submissions ≤ 100.

        [input] integer successfulSubmissionTime

        An integer equal to the time of successful submission in minutes since the beginning of the marathon (for example, if a successful submission was made on the first minute then successfulSubmissionTime = 0). If all submissions were unsuccessful then successfulSubmissionTime = -1.

        Guaranteed constraints:
        -1 ≤ successfulSubmissionTime < marathonLength.

        [output] integer

        The final score for the task.
             
             
             */



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


        public static bool plagiarismCheck2(string[] code1, string[] code2)
        {
            return false;
        }

        public static bool plagiarismCheckFailsWithIndexOf(string[] code1, string[] code2)
        {
            int e = 0;
            // Number of lines do not match
            if (code1.Length != code2.Length)
            {
                return false;
            }
            // removing variable names and comparing.
            // Better to get the variable name and replace them throughout the code.
            for (int i = 0; i < code1.Length; i++)
            {
                remove(code1, i);
                remove(code2, i);
                if (code1[i].Equals(code2[i]))
                {
                    e++;
                }
            }
            if (e == code1.Length)
            {
                return true;
            }
            return false;
        }

        private static void remove(string[] code1, int i)
        {
            int b = code1[i].IndexOf("(");
            if (code1[i].IndexOf("(") > -1)
            {
                code1[i] = code1[i].Substring(0, b + 1)
                        + code1[i].Substring(code1[i].IndexOf(")"));
                b = code1[i].IndexOf("(");
            }
        }


        public static bool plagiarismCheckFromPython(string[] code1, string[] code2)
        {
            if (code1.Length != code2.Length)
            {
                return false;
            }
            Regex var = new Regex(@"([_a-zA-Z]\w*)");
            List<string> map = new List<string>();
            HashSet<string> var_set = new HashSet<string>();
             

            foreach (var nw in code1.Zip(code2, Tuple.Create))
            {

                var m1 = (from Match m in var.Matches(nw.Item1) select m.Value).ToList();
                var n1 = var.Replace("", nw.Item1);
                var m2 = (from Match m in var.Matches(nw.Item2) select m.Value).ToList();
                var n2 = var.Replace("", nw.Item2);
                if (n1 != n2 || m1.Count != m2.Count) {
                    return false;
                }
                foreach (var v in m1.Zip(m2, Tuple.Create))
                {
                    if (v.Item1 != v.Item2)
                    {
                       //What is map????
                    }
                }

            }

            return true;
        }

        /*
         * python uses set we use HasSet
         * 
         * tuples are used when there is no design significance and all you want is a lightweight Data Transfer Object (DTO) to move information around.
         * A Tuple will save you time from having to allocate memory on the heap using new for such a simple operation.
         * Another time when I find tuples useful is when performing multiple mathematical operations on some operands
         * 
         * 
         def plagiarismCheck(code1, code2):
    var = re.compile(r"([_a-zA-Z]\w*)")
    if len(code1) != len(code2):
        return False
    map = {}
    var_set = set()

    for l1, l2 in zip(code1, code2):
        m1 = var.findall(l1)
        n1 = var.sub("", l1)
        m2 = var.findall(l2)
        n2 = var.sub("", l2)
        if n1 != n2 or len(m1) != len(m2):
            return False
        for v1, v2 in zip(m1, m2):
            if v1 != v2:
                if v1 in map and map[v1] != v2:
                    return False
                else:
                    if v1 in var_set:
                        return False
                    else:
                        map[v1] = v2
            else:
                if v1 in map:
                    return False
                var_set.add(v1)
    return True

             
             */


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
