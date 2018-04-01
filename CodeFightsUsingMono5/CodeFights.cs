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
                    dic.Add(lines[0], new Folders(lines[0]) { Total =1});
                    CheckUpdate(dic[lines[0]], lines, 0);
                }

            }

            List<string> returnList = new List<string>();

            foreach (var item in dic)
            {
                returnList.Add(string.Concat("--",item.Value.Name, $" ({item.Value.Total})"));
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
                    folders.childFolders.Add(lines[index], new Folders(lines[index]) { Total =1});
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
            Total =0;
            childFolders = new Dictionary<string, Folders>();
        }


        public string Name { get; set; }
        public int Total { get; set; }
    }

    //public static NodeClass BuildTree(string tree)
    //{
    //    var lines = tree.Split(new[] { Environment.NewLine },
    //                           StringSplitOptions.RemoveEmptyEntries);

    //    var result = new NodeClass("TreeRoot");
    //    var list = new List<NodeClass> { result };

    //    foreach (var line in lines)
    //    {
    //        var trimmedLine = line.Trim();
    //        var indent = line.Length - trimmedLine.Length;

    //        var child = new NodeClass(trimmedLine);
    //        list[indent].Add(child);

    //        if (indent + 1 < list.Count)
    //        {
    //            list[indent + 1] = child;
    //        }
    //        else
    //        {
    //            list.Add(child);
    //        }
    //    }

    //    return result;
    //}

    //public static string BuildString(NodeClass tree)
    //{
    //    var sb = new StringBuilder();

    //    BuildString(sb, tree, 0);

    //    return sb.ToString();
    //}

    //private static void BuildString(StringBuilder sb, NodeClass node, int depth)
    //{
    //    sb.AppendLine(string.Concat(node.ID.PadLeft(node.ID.Length + depth), $" ({node.Total})"));

    //    foreach (var child in node)
    //    {
    //        BuildString(sb, child, depth + 1);
    //    }
    //}



}
