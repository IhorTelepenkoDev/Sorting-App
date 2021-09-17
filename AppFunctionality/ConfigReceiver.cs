using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFunctionality
{
    public class ConfigReceiver
    {
        Dictionary<string, string> confValues;
        public ConfigReceiver(string configFilePath, string section = null)
        {
            string sectionTitle = $"[{section}]";
            var allConfigData = File.ReadAllLines(configFilePath);
            List<string> wholeSectionData = new List<string>();

            if (section != null && allConfigData.Contains(sectionTitle) == true)
            {
                bool isSectionFound = false;
                foreach (var line in allConfigData)
                {
                    if (line[0] == Convert.ToChar("[") && line[line.Length - 1] == Convert.ToChar("]")) // if there is another section started
                        isSectionFound = false;

                    if (isSectionFound)
                        wholeSectionData.Add(line);

                    if (line == sectionTitle)
                        isSectionFound = true;
                }
            }
            else wholeSectionData = allConfigData.ToList<string>();

            confValues = wholeSectionData
           .Where(line => (!String.IsNullOrWhiteSpace(line) && !line.StartsWith("#")))
           .Select(line => line.Split(new char[] { '=' }, 2, 0))
           .ToDictionary(parts => parts[0].Trim(), parts => parts.Length > 1 ? parts[1].Trim() : null);
        }

        public string GetValue(string keyName, string defaultValue = null)
        {
            if (confValues != null && confValues.ContainsKey(keyName))
            {
                return confValues[keyName];
            }
            return defaultValue;
        }
    }
}
