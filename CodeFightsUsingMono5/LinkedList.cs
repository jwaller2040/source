using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public partial class Fundamentals
    {

        public static ListNode<int> removeKFromList(ListNode<int> l, int k)
        {
            //a.x.x.x.x.x.x
            //
            ListNode<int> current = null;


            // recursive loop
            while (l != null)
            {
                if (l.value == k)
                {
                    l = l.next;

                }
                else
                {
                    if (current == null)
                    {
                        current = new ListNode<int>() { value = l.value };
                        current.next = null;
                    }
                    else
                    {
                        // AddToEnd(l.value);
                    }

                    l = l.next;
                }
            }


            return current;
        }


        //
        public static bool isListPalindrome(ListNode<int> l)
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
