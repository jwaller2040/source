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

        public static ListNode<int> addTwoHugeNumbers(ListNode<int> a, ListNode<int> b)
        {
            List<int> firstValueCollection = new List<int>();
            List<int> secondValuesCollection = new List<int>();
            ListNode<int> firstListNode = a;
            while (firstListNode != null)
            {
                firstValueCollection.Add(firstListNode.value);
                firstListNode = firstListNode.next;
            }
            
            ListNode<int> secondListNode = b;
            while (secondListNode != null)
            {
                secondValuesCollection.Add(secondListNode.value);
                secondListNode = secondListNode.next;
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
                ListNode<int> toAdd =  new ListNode<int> { value = combinedCollections[i] };
                while (current.next != null)
                {
                  current = current.next;
                }
                current.next = toAdd;
            }
            return returningHugeValue.next;
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
                result = string.Concat(remainder , result);
            }

            return result;
        }
        public static ListNode<int> removeKFromList(ListNode<int> l, int k)
        {

            ListNode<int> current = new ListNode<int>();
            current.next = l;

            ListNode<int> node = current;
            while (node.next != null)
            {
                if (node.next.value == k)
                {
                    node.next = node.next.next;
                }
                else
                {
                    node = node.next;
                }
            }

            return current.next;

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
                l = l.next;

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
        public ListNode<T> next { get; set; }
    }



    public class MyList<T>
    {
        public ListNode<T> headNode;

        public MyList()
        {
            headNode = null;
        }

        public void AddToEnd(ListNode<T> node, T data)
        {
            if (node == null)
            {
                node = new ListNode<T>() { value = data };

            }
            else
            {
                AddToEnd(node.next, data);
            }
        }


        public void AddToBeginning(T data)
        {
            if (headNode == null)
            {
                headNode = new ListNode<T>() { value = data };

            }
            else
            {
                ListNode<T> temp = new ListNode<T>() { value = data };
                temp.next = headNode;
                headNode = temp;
            }
        }

        public void print(ListNode<T> node)
        {
            if (node != null)
            {
                Console.WriteLine("|" + node.value + "|->");
                if (node.next != null)
                {
                    print(node.next);
                }
            }

        }
    }


}
