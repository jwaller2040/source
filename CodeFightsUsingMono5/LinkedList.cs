using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public partial class Fundamentals
    {

        public class IsSubtree
        {
            class Tree<T>
            {
                //public Tree(T x)
                //{
                //    value = x;
                //}
                public T value;
                public Tree<T> left;
                public Tree<T> right;
            }
            bool isSubtree(Tree<int> t1, Tree<int> t2)
            {
                if (t2 == null) return true;

                return isSubtree2(t1, t2);
            }

            bool isSubtree2(Tree<int> t1, Tree<int> t2)
            {
                if (t1 != null && t2 == null) return false;
                if (t2 != null && t1 == null) return false;
                if (t1 == null && t2 == null) return true;
                if (t1.value == t2.value)
                {
                    return isSubtree2(t1.left, t2.left) && isSubtree2(t1.right, t2.right);
                }
                else
                {
                    return isSubtree2(t1.left, t2) || isSubtree2(t1.right, t2);
                }
            }

            Tree<int> restoreBinaryTree(int[] inorder, int[] preorder)
            {
                return helper(0, 0, inorder.Length - 1, preorder, inorder);
            }

            Tree<int> helper(int preStart, int inStart, int inEnd, int[] preorder, int[] inorder)
            {
                if (preStart > preorder.Length - 1 || inStart > inEnd)
                {
                    return null;
                }
                Tree<int> root = new Tree<int>(); 
                root.value = preorder[preStart];
                int inIndex = 0; // Index of current root in inorder
                for (int i = inStart; i <= inEnd; i++)
                {
                    if (inorder[i] == root.value)
                    {
                        inIndex = i;
                    }
                }
                root.left = helper(preStart + 1, inStart, inIndex - 1, preorder, inorder);
                root.right = helper(preStart + inIndex - inStart + 1, inIndex + 1, inEnd, preorder, inorder);
                return root;
            }

        }


        /*
         You are given an array parent of length n specifying a tree. The vertices of the tree are numbered from 0 to n - 1 and parent[i] is 
         the parent of the ith node. The root of the tree is the vertex v, the parent of which is the same vertex (i.e. parent[v] = v if and only if v is a root).

        What will the parent array look like if the edges remain the same but tree is rooted at the other vertex newRoot?

        Example

        For parent = [0, 0, 0, 1] and newRoot = 1, the output should be
        changeRoot(parent, newRoot) = [1, 1, 0, 1].

        Check out the image below for better understanding:



        For parent = [0, 0, 0, 1, 1, 1, 2, 2, 7] and newRoot = 7, the output should be
        changeRoot(parent, newRoot) = [2, 0, 7, 1, 1, 1, 2, 7, 7].

        This is what the tree looks like in the beginning:



        And this is what it looks when we change the root to the vertex 7:



        Input/Output

        [execution time limit] 3 seconds (cs)

        [input] array.integer parent

        The array of parents.

        Guaranteed constraints:
        4 ≤ parent.length ≤ 50,
        0 ≤ parent[i] < parent.length.

 
             */
        public static int[] changeRoot(int[] parent, int newRoot)
        {
            string test = "asdfasdf";
            string test1 = "";
            test1 = test.Substring(0,0).ToLower();

            List<int> answer = new List<int>();
            List<List<int>> graph = new List<List<int>>();
            for (var i = 0; i < parent.Length; i++)
            {
                answer.Add(0);
                graph.Add(new List<int>());
            }
            for (var i = 0; i < parent.Length; i++)
            {
                if (parent[i] != i)
                {
                    graph[i].Add(parent[i]);
                    graph[parent[i]].Add(i);
                }
            }
            return dfs(newRoot, newRoot, graph, answer.ToArray());
        }

        private static int[] dfs(int cur,int prev, List<List<int>> graph, int[] answer)
        {

            answer[cur] = prev;
            for (var i = 0; i < graph[cur].Count; i++)
            {
                var to = graph[cur][i];
                if (to != prev)
                {
                    answer = dfs(to, cur, graph, answer);
                }
            }
            return answer;
        }

        public static string findProfession(int level, int pos)
        {
            if (level == 1)
            {
                return "Engineer";
            }

            if (findProfession(level - 1, (pos + 1) / 2) == "Doctor")
            {
                if (pos % 2 != 0)
                {
                    return "Doctor";
                }
                else
                {
                    return "Engineer";
                }
            }

            if (pos % 2 != 0)
            {
                return "Engineer";
            }
            else
            {
                return "Doctor";
            }
        }

        private static Tree<string> root;
        void insertTree(string profession)
        {
            Tree<string> tempNode = new Tree<string>();
            Tree<string> current;
            Tree<string> parent;
            tempNode.value = profession;
            
            if(root == null)
            {
                root = tempNode;
            }
            else
            {
                current = root;
                parent = null;
                while (true)
                {
                    parent = current;
                    if (!parent.value.Equals(profession))
                    {
                        current = current.left;

                        if (current == null)
                        {
                            parent.left = tempNode;
                            return;
                        }
                        else
                        {
                            current = current.right;
                            if (current == null)
                            {
                                parent.right = tempNode;
                                return;
                            }
                        }
                    }
                }
            }

        }
       
        //
        // Definition for binary tree:
        // class Tree<T> {
        //   public T value { get; set; }
        //   public Tree<T> left { get; set; }
        //   public Tree<T> right { get; set; }
        // }
        int kthSmallestInBST(Tree<int> t, int k)
        {
            index = k;
            return getKthValue(t);
        }

        int index = 0;
        private int getKthValue(Tree<int> node)
        {
            if (node == null) {
                return -1;
            } 
            int result = getKthValue(node.left);
            index--;

            if (index == 0) {
                return node.value;
            }
            if (index < 0) {
                return result;
            }
            return getKthValue(node.right);
        }

        public static bool isTreeSymmetric(Tree<int> t)
        {
            if (t == null)
            {
                return true;
            }

            return isSymmetric(t,t);
        }

        private static bool isSymmetric(Tree<int> root1, Tree<int> root2)
        {
           if(root1==null && root2 == null)
            {
                return true;
            }
           else if (root1 == null || root2 == null)
            {
                return false;
            }

           if (root1.value == root2.value)
            {
                if (isSymmetric(root1.left, root2.right))
                {
                    return isSymmetric(root1.right, root2.left);
                }
            }
            return false;
        }

        bool isTreeSibling(Tree<int> root, Tree<int> a, Tree<int> b)
        {
            if (root == null) {
                return false;
            }
            return ((root.left == a && root.right == b)||
                (root.left == b && root.right == a)||
                isTreeSibling(root.left,a,b)||
                isTreeSibling(root.right,a,b));
            
        }
        //public static bool hasPathWithGivenSum(Tree<int> t, int s)
        //{
        //    total = 0;
        //    foundMatch = false;
        //    firstRun = true;
        //    if (t != null && t.value == s && t.left == null && t.right == null) return false;
        //    PreorderTraversal(t, s);

        //    return total == s;

        //}

        //static int total = 0;
        //static bool foundMatch = false;
        //static bool firstRun = true;
        //static void PreorderTraversal(Tree<int> current, int s)
        //{

        //    if (current == null && total == s)
        //    {
        //        foundMatch = true;
        //    }
        //    if (current != null && !foundMatch)
        //    {
        //        total += current.value;
        //        if (total == s)
        //        {
        //            foundMatch = !firstRun;
        //        }
        //        firstRun = false;
        //        if (current.left != null)
        //        {
        //            PreorderTraversal(current.left, s);
        //        }
        //        if (current.right != null)
        //        {
        //            PreorderTraversal(current.right, s);
        //        }

        //    }

        //}


        /// <summary>
        ///problem doesn't go to the correct left or right !!!
        ///
        ///  012345678901234567890123456789012345678901234567890123456789012345
        /// "(2 (7 (2 () ()) (6 (5 () ()) (11 () ()))) (5 () (9 (4 () ()) ())))";
        ///  1  L  L  L  R   R  L  L  R   R   L  R     R  L  R  L  R  L   R
        ///     2  3  44 443 3  4  55 554 4   55 55432 2  33 3  4  55 554 44321
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static int[] treeBottom(string tree)
        {
            
            MyList<int> root = new MyList<int>();
            int pointer = 0;
            HashSet<int> h = new HashSet<int>();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            string value = string.Empty;
            for (int i = 0; i < tree.Length; i++)
            {
                if (tree[i] == '(')
                {
                    pointer += 1;
                    if (h.Contains(pointer))
                    {
                        for (int j = i + 1; tree[j] != ')' && tree[j] != '('; j++)
                        {
                            if (Char.IsDigit(tree[j]))
                            {
                                value += tree[j];
                            }
                            i += 1;
                        }

                        if (!string.IsNullOrEmpty(value))
                        {
                            if (root.headNode == null)
                            {
                                root.headNode = new ListNode<int> { value = int.Parse(value) };
                                dic[pointer] = int.Parse(value);
                            }
                            else
                            {
                                root.NodeLevel = pointer;
                                root.parentValue = dic[pointer - 1];
                                dic[pointer] = int.Parse(value);
                                var parent = root.getParentNodeRight(root.headNode);
                                root.RightAddToEnd(ref parent, int.Parse(value));
                            }
                        }
                        h.Remove(pointer);
                    }
                    else
                    {
                        h.Add(pointer);
                        
                        for (int j = i + 1; tree[j] != ')' && tree[j] != '('; j++)
                        {
                            if (Char.IsDigit(tree[j]))
                            {
                                value += tree[j];
                            }
                            i += 1;
                        }

                        if (!string.IsNullOrEmpty(value))
                        {
                            if (root.headNode == null)
                            {
                                root.headNode = new ListNode<int> { value = int.Parse(value) };
                                dic.Add(pointer, int.Parse(value));
                            }
                            else
                            {
                                root.NodeLevel = pointer;
                                root.parentValue = dic[pointer - 1];
                                dic[pointer] = int.Parse(value);
                                var parent = root.getParentNodeLeft(root.headNode);
                                root.LeftAddToEnd(ref parent, int.Parse(value));
                            }
                        }
                    }
                }
                else if (tree[i] == ')')
                {
                    pointer -= 1;
               
                }

                value = string.Empty;

            }
            root.TraversalValues(root.headNode);


            return root.ts.ToArray();
        }

        /*
         You are given a recursive notation of a binary tree: each node of a tree is represented as a set of three elements:

         value of the node;
         left subtree;
         right subtree.
         So, a tree can be written as (value left_subtree right_subtree). If a node doesn't exist then it is represented as an empty set: ().
         For example, here is a representation of a tree in the given picture:

         (2 (7 (2 () ()) (6 (5 () ()) (11 () ()))) (5 () (9 (4 () ()) ())))


         Your task is to obtain a list of nodes, that are the most distant from the tree root, in the order from left to right.

         In the notation of a node its value and subtrees are separated by exactly one space character.

         Example

         For

         tree = "(2 (7 (2 () ()) (6 (5 () ()) (11 () ()))) (5 () (9 (4 () ()) ())))"
         the output should be
         treeBottom(tree) = [5, 11, 4].
         */

        List<int> asd = new List<int>();
        //void buildTree(int[] array, int start, int end)
        //{
        //    if (end - start > 1)
        //    {
        //        int mid = (start + end) >> 1;
        //        left = buildTree(array, start, mid);
        //        right = buildTree(array, mid, end);
        //        return new InternalNode(left, right);
        //    }
        //    else
        //    {
        //        return new LeafNode(array[start]);
        //    }
        //}

        public static bool hasPathWithGivenSum(Tree<int> t, int s)
        {
            total = 0;
            foundMatch = false;
            firstRun = true;
            if (t != null && t.value == s && t.left == null && t.right == null) return false;
            PreorderTraversal(t, s);

            return total == s;

        }

        static int total = 0;
        static bool foundMatch = false;
        static bool firstRun = true;
        static void PreorderTraversal(Tree<int> current, int s)
        {

            if (current == null && total == s)
            {
                foundMatch = true;
            }
            if (current != null && !foundMatch)
            {
                total += current.value;
                if (total == s)
                {
                    foundMatch = !firstRun;
                }
                firstRun = false;
                if (current.left != null)
                {
                    PreorderTraversal(current.left, s);
                }
                if (current.right != null)
                {
                    PreorderTraversal(current.right, s);
                }

            }

        }

        public static ListNode<int> rearrangeLastN(ListNode<int> l, int n)
        {
            List<int> firstValueCollection = new List<int>();

            ListNode<int> firstListNode = l;

            while (firstListNode != null)
            {

                firstValueCollection.Add(firstListNode.value);

                firstListNode = firstListNode.right;
            }

            ListNode<int> rearrangedResult = new ListNode<int>();
            ListNode<int> current = rearrangedResult;

            if (n > firstValueCollection.Count)
            {
                n = n % firstValueCollection.Count;

            }

            if (n == 0 || n == firstValueCollection.Count || firstValueCollection.Count == 1)
            {
                for (int i = 0; i < firstValueCollection.Count; i++)
                {
                    ListNode<int> toAdd = new ListNode<int> { value = firstValueCollection[i] };
                    while (current.right != null)
                    {
                        current = current.right;
                    }
                    current.right = toAdd;
                }
                return rearrangedResult.right;
            }

            List<int> shiftResultCollection = firstValueCollection.GetRange(firstValueCollection.Count - n, n);
            shiftResultCollection.AddRange(firstValueCollection.GetRange(0, firstValueCollection.Count - n));

            for (int i = 0; i < shiftResultCollection.Count; i++)
            {
                ListNode<int> toAdd = new ListNode<int> { value = shiftResultCollection[i] };
                while (current.right != null)
                {
                    current = current.right;
                }
                current.right = toAdd;
            }
            return rearrangedResult.right;

        }

        public static ListNode<int> reverseNodesInKGroups(ListNode<int> l, int k)
        {
            List<int> firstValueCollection = new List<int>();
            List<int> toReverseCollection = new List<int>();
            ListNode<int> firstListNode = l;
            int counter = 0;
            int index = 0;
            while (firstListNode != null)
            {

                firstValueCollection.Add(firstListNode.value);
                if (counter % k == 0)
                {
                    if (index - k > -1)
                    {
                        firstValueCollection.Reverse(index - k, k);
                    }


                    counter = 0;


                }
                counter++;
                index++;
                firstListNode = firstListNode.right;
            }

            if (counter % k == 0)
            {
                if (index - k > -1)
                {
                    firstValueCollection.Reverse(index - k, k);
                }


                counter = 0;


            }

            ListNode<int> returnReversedByK = new ListNode<int>();
            ListNode<int> current = returnReversedByK;

            for (int i = 0; i < firstValueCollection.Count; i++)
            {
                ListNode<int> toAdd = new ListNode<int> { value = firstValueCollection[i] };
                while (current.right != null)
                {
                    current = current.right;
                }
                current.right = toAdd;
            }

            return returnReversedByK.right;

        }

        /// <summary>
        /// BOOOOOOM didn't have to test this one!!!
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode<int> mergeTwoLinkedLists(ListNode<int> l1, ListNode<int> l2)
        {
            List<int> firstValueCollection = new List<int>();

            ListNode<int> firstListNode = l1;
            while (firstListNode != null)
            {
                firstValueCollection.Add(firstListNode.value);
                firstListNode = firstListNode.right;
            }

            ListNode<int> secondListNode = l2;
            while (secondListNode != null)
            {
                firstValueCollection.Add(secondListNode.value);
                secondListNode = secondListNode.right;
            }


            firstValueCollection.Sort((a, b) => -1 * a.CompareTo(b)); // descending sort

            ListNode<int> returningHugeValue = new ListNode<int>();
            ListNode<int> current = returningHugeValue;
            for (int i = firstValueCollection.Count - 1; i >= 0; i--)
            {
                ListNode<int> toAdd = new ListNode<int> { value = firstValueCollection[i] };
                while (current.right != null)
                {
                    current = current.right;
                }
                current.right = toAdd;
            }
            return returningHugeValue.right;


        }

        public static ListNode<int> addTwoHugeNumbers(ListNode<int> a, ListNode<int> b)
        {
            List<int> firstValueCollection = new List<int>();
            List<int> secondValuesCollection = new List<int>();
            ListNode<int> firstListNode = a;
            while (firstListNode != null)
            {
                firstValueCollection.Add(firstListNode.value);
                firstListNode = firstListNode.right;
            }

            ListNode<int> secondListNode = b;
            while (secondListNode != null)
            {
                secondValuesCollection.Add(secondListNode.value);
                secondListNode = secondListNode.right;
            }

            int maxLengthOfCollections = (firstValueCollection.Count > secondValuesCollection.Count ? firstValueCollection.Count : secondValuesCollection.Count);
            if (maxLengthOfCollections > firstValueCollection.Count)
            {
                int firstLength = firstValueCollection.Count();
                firstValueCollection.Capacity = maxLengthOfCollections;
                firstValueCollection.InsertRange(0, Enumerable.Repeat(0, maxLengthOfCollections - firstLength));
            }
            if (maxLengthOfCollections > secondValuesCollection.Count)
            {
                int secondLength = secondValuesCollection.Count();
                secondValuesCollection.Capacity = maxLengthOfCollections;
                secondValuesCollection.InsertRange(0, Enumerable.Repeat(0, maxLengthOfCollections - secondLength));
            }

            List<int> combinedCollections = new List<int> { };
            int remainder = 0;
            for (int i = (firstValueCollection.Count - 1); i >= 0; i--)
            {
                int addedFirstAndSecondValue = firstValueCollection[i] + secondValuesCollection[i] + remainder;
                if (addedFirstAndSecondValue >= 10000)
                {
                    remainder = 1;
                    addedFirstAndSecondValue = addedFirstAndSecondValue % 10000;
                }
                else
                {
                    remainder = 0;
                }
                combinedCollections.Add(addedFirstAndSecondValue);
            }

            if (remainder > 0)
            {
                combinedCollections.Add(remainder);
            }


            ListNode<int> returningHugeValue = new ListNode<int>();
            ListNode<int> current = returningHugeValue;
            for (int i = combinedCollections.Count - 1; i >= 0; i--)
            {
                ListNode<int> toAdd = new ListNode<int> { value = combinedCollections[i] };
                while (current.right != null)
                {
                    current = current.right;
                }
                current.right = toAdd;
            }
            return returningHugeValue.right;
        }

        /// <summary>
        /// Adds two "integers".  The integers can be of any size string.
        /// </summary>
        /// <param name="BigInt1">The first integer</param>
        /// <param name="BigInt2">The second integer</param>
        /// <returns>A string that is the addition of the two integers passed.</returns>
        /// <exception cref="Exception">Can throw an exception when parsing the individual parts     of the number.  Callers should handle. </exception>
        static string AddBigInts(string BigInt1, string BigInt2)
        {
            string result = string.Empty;

            //Make the strings the same length, pre-pad the shorter one with zeros
            int length = (BigInt1.Length > BigInt2.Length ? BigInt1.Length : BigInt2.Length);
            BigInt1 = BigInt1.PadLeft(length, '0');
            BigInt2 = BigInt2.PadLeft(length, '0');

            int remainder = 0;

            //Now add them up going from right to left
            for (int i = (BigInt1.Length - 1); i >= 0; i--)
            {
                //If we don't encounter a number, this will throw an exception as indicated.
                int int1 = int.Parse(BigInt1[i].ToString());
                int int2 = int.Parse(BigInt2[i].ToString());

                //Add
                int add = int1 + int2 + remainder;

                //Check to see if we need a remainder;
                if (add >= 10)
                {
                    remainder = 1;
                    add = add % 10;
                }
                else
                {
                    remainder = 0;
                }

                //Add this to our "number"
                result = string.Concat(add, result);
            }

            //Handle when we have a remainder left over at the end
            if (remainder == 1)
            {
                result = string.Concat(remainder, result);
            }

            return result;
        }
        public static ListNode<int> removeKFromList(ListNode<int> l, int k)
        {

            ListNode<int> current = new ListNode<int>();
            current.right = l;

            ListNode<int> node = current;
            while (node.right != null)
            {
                if (node.right.value == k)
                {
                    node.right = node.right.right;
                }
                else
                {
                    node = node.right;
                }
            }

            return current.right;

            //Works but too slow...
            //ListNode<int> current = null;//= new ListNode<int>();

            //while (l != null)
            //{
            //    if (l.value == k)
            //    {
            //        l = l.next;

            //    }
            //    else
            //    {
            //        if (current == null)
            //        {
            //            current = new ListNode<int>() { value = l.value };
            //            current.next = null;
            //        }
            //        else
            //        {

            //            ListNode<int> node = current;
            //            while (node.next != null)
            //            {
            //                node = node.next;
            //            }
            //            node.next = new ListNode<int>() { value = l.value };

            //        }

            //        l = l.next;
            //    }
            //}
            //return current;
        }

        //
        public static bool isListPalindrome(ListNode<int> l)
        {
            if (l == null) { return false; }

            List<int> values = new List<int>();

            while (l != null)
            {
                values.Add(l.value);
                l = l.right;

            }
            List<int> reversedList = new List<int>();
            reversedList.AddRange(values);
            reversedList = reversedList.Reverse<int>().ToList();
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] != reversedList[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckListNode(ListNode<int> l)
        {
            return true;
        }
    }


    public class ListNode<T>
    {
        public T value { get; set; }
        public ListNode<T> right { get; set; }
        public ListNode<T> left { get; set; }
    }
    public class MyList<T>
    {
        public ListNode<T> headNode;

        //public static int NodeLevel;
        public int NodeLevel { get; set; }
        public int parentValue { get; set; }

        public MyList()
        {
            NodeLevel = 0;
            headNode = null;
        }

        private void getToNodePosition(ref ListNode<T> n)
        {
            ListNode<T> current = n;
            NodeLevel -= 1;
            ListNode<T> node = current;
            while (node != null || NodeLevel == 0)
            {
                if (node.right != null)
                {
                    ListNode<T> r = node.right;
                    getToNodePosition(ref r);

                }
                if (node.left != null)
                {
                    ListNode<T> l = node.left;
                    getToNodePosition(ref l);

                }
            }
        }


        public ListNode<int> getParentNodeRight(ListNode<int> node)
        {
            NodeLevel -= 1;
            if (node != null)
            {
                if (node.value.Equals(parentValue) && NodeLevel <= 1)
                {
                    return node;
                }
                else
                {
                    ListNode<int> foundNode = getParentNodeRight(node.right);
                    if (foundNode == null)
                    {
                        foundNode = getParentNodeRight(node.left);
                    }
                    return foundNode;
                }
            }
            else
            {
                return null;
            }
        }

        public ListNode<int> getParentNodeLeft(ListNode<int> node)
        {
            NodeLevel -= 1;
            if (node != null)
            {
                if (node.value.Equals(parentValue) && NodeLevel <= 1)
                {
                    return node;
                }
                else
                {
                    ListNode<int> foundNode = getParentNodeLeft(node.left);
                    if (foundNode == null)
                    {
                        foundNode = getParentNodeLeft(node.right);
                    }
                    return foundNode;
                }
            }
            else
            {
                return null;
            }
        }

        public void RightAddToEnd(ref ListNode<T> node, T data)
        {
            if (node == null)
            {
                node = new ListNode<T>() { value = data };

            }
            else
            {
                ListNode<T> n = node.right;
                RightAddToEnd(ref n, data);
                node.right = n;
            }
        }


        public void RightAddToBeginning(T data)
        {
            if (headNode == null)
            {
                headNode = new ListNode<T>() { value = data };

            }
            else
            {
                ListNode<T> temp = new ListNode<T>() { value = data };
                temp.right = headNode;
                headNode = temp;
            }
        }

        public void LeftAddToEnd(ref ListNode<T> node, T data)
        {
            if (node == null)
            {
                node = new ListNode<T>() { value = data };

            }
            else
            {
                var l = node.left;
                LeftAddToEnd(ref l, data);
                node.left = l;

            }
        }


        public void LeftAddToBeginning(T data)
        {
            if (headNode == null)
            {
                headNode = new ListNode<T>() { value = data };

            }
            else
            {
                ListNode<T> temp = new ListNode<T>() { value = data };
                temp.left = headNode;
                headNode = temp;
            }
        }

        public List<T> ts = new List<T>();


        private Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();


        public void TraversalValues(ListNode<T> current)
        {
            int dist = 0;
            findLeafDown(current, 0, ref dist);



        }

        void findLeafDown(ListNode<T> root, int lev, ref int maxDistance)
        {
            // base case
            if (root == null)
                return;

            // If this is a leaf node, then check if it is closer
            // than the closest so far
            if (root.left == null && root.right == null)
            {
                if (lev > maxDistance)
                    ts.Clear();
                maxDistance = lev;
                ts.Add(root.value);
                return;
            }

            // Recur for left and right subtrees
            findLeafDown(root.left, lev + 1, ref maxDistance);
            findLeafDown(root.right, lev + 1, ref maxDistance);
        }

        class Node
        {
            public Node[] nodes = new Node[256];
            public bool bit;
        }

        class Tries
        {
            Node root = null;
            public void put(string word)
            {
                root = _put(root, word, 0);
            }

            public Node _put(Node x, string word, int d)
            {


                if (x == null)
                {
                    x = new Node();
                }

                if (d == word.Length)
                {
                    x.bit = true;
                    return x;
                }

                int c = (int)word[d];

                x.nodes[c] = _put(x.nodes[c], word, d + 1);
                return x;
            }

            public Dictionary<string, string> getWords(string part)
            {
                Dictionary<string, string> queue = new Dictionary<string, string>();
                collect(root, part, "", "", 0, queue);
                return queue;
            }

            void collect(Node x, string part, string prefix, string prefix2, int d, Dictionary<string, string> queue)
            {

                if (x == null)
                {
                    return;
                }

                if (x.bit && part.Length == d)
                {
                    queue[prefix] =  prefix2;
                }


                for (int i = 0; i < 256; ++i)
                {
                    if (d <= part.Length - 1 && i == part[d])
                    {
                        string p = prefix + (char)i;
                        string p2 = prefix2 + (char)i;
                        if (d == 0)
                        {
                            p2 = prefix2 + "[" + (char)i;
                        }

                        if (d == part.Length - 1)
                        {
                            p2 = p2 + "]";
                        }

                        collect(x.nodes[i], part, p, p2, d + 1, queue);
                    }
                    else if (d <= part.Length - 1)
                    {
                        collect(x.nodes[i], part, prefix + (char)i, prefix + (char)i, 0, queue);
                    }
                    else
                    {
                        collect(x.nodes[i], part, prefix + (char)i, prefix2 + (char)i, d, queue);
                    }
                }
            }
        }


        public static string[] findSubstrings(string[] words, string[] parts)
        {

            Tries t = new Tries();

            foreach (var word in words)
            {
                t.put(word);

            }

            Dictionary<string, string> selected = new Dictionary<string, string>();

            foreach (var w in words)
            {
                if (!selected.ContainsKey(w))
                {
                    selected.Add(w, w);
                }
                
            }

            foreach (var p in parts.OrderBy(x=>x.Length))
            {
                Dictionary<string, string> found = t.getWords(p);
                foreach (var f in found)
                {
                    string word = f.Key;
                    string formatted = f.Value;
                    if (selected.ContainsKey(word))
                    {
                        if (selected[word].Length <= formatted.Length)
                        {
                            selected[word] = formatted;
                        }
                    }
                    else
                    {
                        selected.Add(word, formatted);
                    }
                }


            }


            string[] result = new string[selected.Count];
            int index = 0;

            foreach (var entry in selected)
            {
                result[index++] = entry.Value;
            }


            return result;



        }



    }



    /*
            Given a binary tree t and an integer s, determine whether there is a root to leaf path in t such that the sum of vertex values equals s.

            Example

            For

            t = {
                "value": 4,
                "left": {
                    "value": 1,
                    "left": {
                        "value": -2,
                        "left": null,
                        "right": {
                            "value": 3,
                            "left": null,
                            "right": null
                        }
                    },
                    "right": null
                },
                "right": {
                    "value": 3,
                    "left": {
                        "value": 1,
                        "left": null,
                        "right": null
                    },
                    "right": {
                        "value": 2,
                        "left": {
                            "value": -2,
                            "left": null,
                            "right": null
                        },
                        "right": {
                            "value": -3,
                            "left": null,
                            "right": null
                        }
                    }
                }
            }
            and
            s = 7,
            the output should be hasPathWithGivenSum(t, s) = true.

            This is what this tree looks like:

                    4
                    / \
                   1   3
                  /   / \
                -2   1   2
                  \     / \
                    3  -2 -3
            Path 4 -> 3 -> 2 -> -2 gives us 7, the required sum.

            For

            t = {
                "value": 4,
                "left": {
                    "value": 1,
                    "left": {
                        "value": -2,
                        "left": null,
                        "right": {
                            "value": 3,
                            "left": null,
                            "right": null
                        }
                    },
                    "right": null
                },
                "right": {
                    "value": 3,
                    "left": {
                        "value": 1,
                        "left": null,
                        "right": null
                    },
                    "right": {
                        "value": 2,
                        "left": {
                            "value": -4,
                            "left": null,
                            "right": null
                        },
                        "right": {
                            "value": -3,
                            "left": null,
                            "right": null
                        }
                    }
                }
            }
            and
            s = 7,
            the output should be hasPathWithGivenSum(t, s) = false.

            This is what this tree looks like:

                    4
                   / \
                  1   3
                 /   / \
               -2   1   2
                 \     / \
                  3  -4 -3
            There is no path from root to leaf with the given sum 7.

            Input/Output

            [execution time limit] 3 seconds (cs)

            [input] tree.integer t

            A binary tree of integers.

            Guaranteed constraints:
            0 ≤ tree size ≤ 5 · 104,
            -1000 ≤ node value ≤ 1000.

            [input] integer s

            An integer.

            Guaranteed constraints:
            -4000 ≤ s ≤ 4000.

            [output] boolean

            Return true if there is a path from root to leaf in t such that the sum of node values in it is equal to s, otherwise return false.
         */
    // Definition for binary tree:
    public class Tree<T>
    {
        public T value { get; set; }


        public Tree<T> left { get; set; }
        public Tree<T> right { get; set; }
    }


  
      

 

}
