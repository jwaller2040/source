using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public class Edge
    {
        public int source, dest;

        public Edge(int source, int dest)
        {
            this.source = source;
            this.dest = dest;
        }
    };

    // Class to represent a graph object
   public class Graph
    {
        // An array of Lists to represent adjacency list
        public List<List<int>> adjList = null;

        // Constructor
        public Graph(List<Edge> edges, int N)
        {
            adjList = new List<List<int>>(N);
            for (int i = 0; i < N; i++)
            {
                adjList.Add(new List<int>());
            }

            // add edges to the directed graph
            for (int i = 0; i < edges.Count; i++)
            {
                int src = edges[i].source;
                int dest = edges[i].dest;

                adjList[src].Add(dest);
            }

          
        }

    }

   public class DFSUtil
    {
        // Function to perform DFS Traversal
        private static void DFS(Graph graph, int v, bool[] visited)
        {
            // mark current node as visited
            visited[v] = true;

            // do for every edge (v -> u)
            for (int u = 0; u < graph.adjList.Count; u++)
            {
                if (!visited[u])
                {
                    DFS(graph, u, visited);
                }
                   

            }
        }

        // Check if graph is strongly connected or not
        public static bool check(Graph graph, int N)
        {
            // do for every vertex
            for (int i = 0; i < N; i++)
            {
                // stores vertex is visited or not
                bool[] visited = new bool[N];

                // start DFS from first vertex
                DFS(graph, i, visited);

                // If DFS traversal doesn’t visit all vertices,
                // then graph is not strongly connected
                for (int j = 0; j < visited.Length; j++)
                {
                    if (!visited[j])
                        return false;
                }
               
                   
            }
            return true;
        }

        public static void main(String[] args)
        {
            // vector of graph edges as per above diagram
            List<Edge> edges = new List<Edge>{
                    new Edge(0, 4), new Edge(1, 0), new Edge(1, 2),
                    new Edge(2, 1), new Edge(2, 4), new Edge(3, 1),
                    new Edge(3, 2) };

            // Number of vertices in the graph
           int N = 5;

            // construct graph
            Graph graph = new Graph(edges, N);

            // check if graph is not strongly connected or not
            if (check(graph, N))
            {
                Console.WriteLine("Graph is Strongly Connected");
            }
            else
            {
                Console.WriteLine("Graph is not Strongly Connected");
            }
        }
    }

}
