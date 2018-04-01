using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFightsUsingMono5
{
    public class ChatBotChallenge
    {
        public static string[] chatBot(string[][] conversations, string[] currentConversation)
        {
            if (conversations == null) return currentConversation;
            if (currentConversation == null) return currentConversation;
            if (conversations.Length < 1 || conversations.Length > 10000) return currentConversation;
            if (currentConversation.Length < 1 || currentConversation.Length > 100) return currentConversation;

            for (int i = 0; i < conversations.Length; i++)
            {
                if (conversations[i].Length > 100 ) return currentConversation;
            }

            List<string[]> conversationsList = new List<string[]>();
            conversationsList.AddRange(conversations);
            List<string> currentConversationList = new List<string>();
            currentConversationList.AddRange(currentConversation);

            string[] bestMatch = null;
            int tempCount = 0;
            foreach (var item in conversationsList)
            {
                if (item.Length < 1 || item.Length > 15) return currentConversation;

                var resultSet = currentConversation.Intersect<string>(item).Count();
                if (resultSet > tempCount)
                {
                    tempCount = resultSet;
                    bestMatch = item;
                }
            }

            if (tempCount == 0) return currentConversation;

            foreach (var item in currentConversationList)
            {
                if (item.Length < 1 || item.Length > 15) return currentConversation;
            }
            var lastMatchingWord = bestMatch.Intersect<string>(currentConversation).Last<string>();//currentConversation.Intersect<string>(bestMatch).Last<string>();
            List<string> appendingList = new List<string>();
            appendingList.AddRange(bestMatch);
            var pointAfterMatch = appendingList.LastIndexOf(lastMatchingWord) + 1;
            
            for (int i = pointAfterMatch; i < appendingList.Count; i++)
            {
                currentConversationList.Add(appendingList[i]);
            }
           

            return currentConversationList.ToArray();
        }

    }
}
