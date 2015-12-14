using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextDEmo
{
    public class Data
    {
        internal string txt;
        internal string fileName;
        internal ICollection<string> history
            //= new List<string>();
//            = new HashSet<string>();
              = new SortedSet<string>();
        internal IDictionary<string, int> firstLetterCount;
        public void loadConfig()
        {
            try {
                using (
                    StreamReader sr =
                    new StreamReader("config.txt"))
                {
                    fileName = sr.ReadLine();
                    int n = Convert.ToInt32(sr.ReadLine());
                    for (int i = 0; i < n; i++)
                    {
                        history.Add(sr.ReadLine());
                    }
                }
            } catch (IOException ex)
            {
              
            }
        }
        public void readFromFile()
        {
            using (StreamReader sr
                = new StreamReader(fileName))
            { 
                txt = sr.ReadToEnd();
                
            }
        }

        public void saveConfig()
        {
            using (
                StreamWriter sw = new StreamWriter("config.txt"))
            {
                sw.WriteLine(fileName);
                sw.WriteLine(history.Count);
                foreach (string s in history)
                {
                    sw.WriteLine(s);
                }
            }
        }
        public void firstLetters()
        {
            Regex r = new Regex(@"\s([а-я])",
                RegexOptions.IgnoreCase);
            // r.Matches.
            firstLetterCount 
                = new SortedDictionary<string, int>();
            foreach (Match m in  r.Matches(txt))
            {
                string b = m.Groups[1].Value.ToUpper();
                if (firstLetterCount.ContainsKey(b))
                {
                    firstLetterCount[b]++;
                } else
                {
                    firstLetterCount[b] = 1;
                }

            }
        }
    }
}