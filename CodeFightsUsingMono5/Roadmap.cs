using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    /// <summary>
    ///You have a roadmap, which is the list of tasks that your team needs to complete. Each task in this list has a 
    ///title, a start date, an end date, and a list of the people who will be working on it. You are given some 
    ///queries, each of which contains a specific person's name and a date. For each query that is made, you need to 
    ///return the list of tasks on which that person will be working on the date specified in the query, sorted by 
    ///the tasks' end dates. If their end dates are equal, then sort by the tasks' titles.
    /// </summary>
    public class Roadmap
    {
        public static string[][] roadmap(string[][] tasks, string[][] queries)
        {
            List<Task> tasksList = new List<Task>();
            for (int i = 0; i < tasks.Length; i++)
            {
                tasksList.Add(new Task(tasks[i]));
            }
            List<Query> queriesList = new List<Query>();
            for (int pointer = 0; pointer < queries.Length; pointer++)
            {
                queriesList.Add(new Query(queries[pointer]));
                queriesList[pointer].LoadTaks(tasksList);

            }
      
            List<string[]> returnValue = new List<string[]>();

            foreach (var item in queriesList)
            {
                returnValue.Add(item.AssignedList());
            }
           
            return returnValue.ToArray();

        }    

    }

    public class Task
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<string> People { get; set; }

        public Task(string[] t)
        {
            Title = t[0];
            DateTime d;
            DateTime.TryParse(t[1], out d);
            if (d == System.DateTime.MinValue)
            {
                StartDate = DateTime.MaxValue;
            }
            StartDate = d;
            DateTime.TryParse(t[2], out d);
            EndDate = d;
            People = new List<string>();
            for (int i = 3; i < t.Length; i++)
            {
                People.Add(t[i]);
            }
        }

        public Task(string title, DateTime startDate, DateTime endDate, IList<string> people)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            StartDate = startDate;
            EndDate = endDate;
            People = people ?? throw new ArgumentNullException(nameof(people));
        }

        public bool Contains(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            if (People == null || People.Count == 0)
            {
                return false;
            }
            return People.Contains(Name);
        }

        public bool InRange(DateTime target)
        {
            if (target >= StartDate && target <= EndDate)
            {
                return true;
            }
            else
            {
                return false;

            }

        }


    }

    public class Query {
        public Query(string[] q)
        {
            DateTime d;
            DateTime.TryParse(q[1], out d);
            if (d == System.DateTime.MinValue)
            {
                QueryDate = DateTime.MaxValue;
            }
            QueryDate = d;

            Name = q[0];

            Tasks = new List<Task>();
        }

        public string Name { get; set; }
        public DateTime QueryDate { get; set; }
        public List<Task> Tasks { get; set; }

        public String[] AssignedList()
        {
            bool useOderbyTitle = Tasks.GroupBy(l => l.EndDate).ToList().Count == 1;
            if (useOderbyTitle)
            {
                //Tasks.OrderBy(t => t.Title);
                return (from t in Tasks orderby t.Title select t.Title).ToArray();
            }
            else
            {
                return (from t in Tasks orderby t.EndDate select t.Title).ToArray();
            }
           
        }
        public void LoadTaks(List<Task> taskCollection)
        {
            Tasks.AddRange((from e in taskCollection
                            where e.Contains(this.Name) && e.InRange((this.QueryDate))                            
                            select e).ToList());
          

        }
    }
}
