using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public static class PlagueInc
    {
        static int findHighestIndex(int b, int total, int[][] people)
        {
            var v = new bool[total];
            var s = new int[total];
            var pr = new int[total];
            int x = 0, c = 0;

            s[c++] = b;
            v[b] = true;

            do
            {
                foreach (var t in people[s[x]].Skip(1).Where(i => !v[i]))
                {
                    s[c++] = t;
                    v[t] = true;
                    pr[t] = pr[s[x]] + 1;
                }
            } while (++x < c);
            return (x < total ? -1 : pr.Max());
        }

        public static int plagueInc(int[][] people)
        {
            int min = people.Length + 1, minPos = -1;

            for (int personIndex = 0; personIndex < people.Length; personIndex++) //loop through people
            {
                int res = findHighestIndex(personIndex, people.Length, people); //gets the value... the main part.
                if (res != -1 && min > res)
                {
                    min = res;
                    minPos = personIndex;
                }
            }
            return minPos;
        }




        //public static int plagueInc(int[][] people)
        //{
        //    var swaps = new List<HashSet<int>>();
        //    foreach (var p in people)
        //    {
        //        var swap = new HashSet<int>();
        //        foreach (var item in p)
        //        {
        //            swap.Add(item);
        //        }
        //        swaps.Add(swap);
        //    }

        //    int i = 0;

        //    while (i < swaps.Count)
        //    {
        //        bool merge = false;
        //        for (int j = i + 1; j < swaps.Count; j++)
        //        {
        //            foreach (var x in swaps[j])
        //            {
        //                if (swaps[i].Contains(x))
        //                {
        //                    foreach (var y in swaps[j])
        //                    {
        //                        swaps[i].Add(y);
        //                    }

        //                    merge = true;
        //                    break;
        //                }
        //            }
        //            if (merge)
        //            {
        //                swaps.RemoveAt(j);
        //                i = 0;
        //                break;
        //            }
        //        }
        //        if (!merge) i++;
        //    }

        //    return 1;
        //}

        /*
         *Quick note:
         * Even though this is a shortest solutions challenge, I would also appreciate seeing some fast algorithms here!
         * As an evil mastermind and a casual Plague Inc player, your goal is to destroy all form of life on Earth. 
         * There are many ways to do this, but currently the "cheapest" method is to release a contagious virus to the population.
         * The downside? Nobody is giving you money for this (Why would they lend you money for destroying the world?) and 
         * making the virus is expensive, each day of the outbreak adds up the cost. Not only that, the virus isn't effective, 
         * and can only spread to an infected person's family and friends every day.
         * Thankfully, you have been given a matrix of relationships, where people[i] represents the relationship between person[i][0] and everyone in the list 
         * (ex: [0,1,3,4] for person[0] means 0 is friends with 1,3, and 4)
         * (NOTE: Friendship is both ways, no need to think about pretending to be their friend). Now all you have to do is to find who you should infect first to infect everyone!
         * You must return the number of he person that will take the least amount of days to infect the world. If there are ties, return the smaller value.
         * Note: If not everyone can be infected (ex: Someone with no friends or family :c ), return -1
         * Example
         * For people = [[0,1], [1,0,3], [2,3], [3,1,2]], the output should be plagueInc(people) = 1.
         * In this example person[0] means 0 is friends to 1, person[1] is friends to 0 and 3, person[2] is friends to 3, and person[3] is friends to 1 and 2.
         * -> Day 1:Person 1 gets infected, that person is friends to 0 and 3.
         * -> Day 2:0 and 3 get infected, and one of their friends happens to be person 2.
         * -> Day 3:2 is now infected, making everyone dead. Global domination is complete!
         * Of course getting the person 3 takes 3 days as well, but index 1 is less than 3, making 1 a better option.
         * Input/Output [execution time limit] 3 seconds (cs) 
         * [input] array.array.integer people
         * A list of everyone's relationships from 0 to n. Keep in mind that the array is messy and unsorted.
         * ex: [2,8,5,15,12] means 2 is friends with 8,5,15 and 12 
         * Guaranteed constraints: 2 ≤ people.length ≤ 2 · 103 1 ≤ people[i].length 0 ≤ people[i][j] < people.length.
         */

    }
}
