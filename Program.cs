using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json.Linq;

class Program
{
    static HashSet<string> topTeams = new HashSet<string>
        {
            "India", "England", "Pakistan", "South Africa", "New Zealand",
            "Australia", "Sri Lanka", "West Indies", "Afghanistan", "Bangladesh",
            "Ireland", "Zimbabwe", "Netherlands", "Scotland", "United Arab Emirates",
            "Oman", "Nepal", "Namibia", "United States", "Papua New Guinea"
        };
    static void Main(string[] args)
    {
        string folderPath = @"C:\Users\prijha\t20s_json";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("The provided folder path does not exist.");
            return;
        }
        int isMatchFoundCounter = 1;
        
        var jsonFiles = Directory.GetFiles(folderPath, "*.json");

        foreach (var jsonFile in jsonFiles)
        {
            try
            {
                
                var jsonData = JObject.Parse(File.ReadAllText(jsonFile));
                
                var info = jsonData["info"];
                var outcome = info["outcome"];

                var teams = info["teams"];
                if (teams == null || teams.Count() != 2 ||
                    !topTeams.Contains(teams[0].ToString()) || !topTeams.Contains(teams[1].ToString()))
                {
                    continue; // Skip this file if the teams are not in the top 20 T20 teams
                }

                if (outcome["by"]?["runs"] != null && info["gender"].Value<string>() == "male")
                {
                    var secondInnings = jsonData["innings"]?[1];
                    if (secondInnings == null)
                    {
                        continue; // Skip this file if the second innings does not exist
                    }

                    var target = secondInnings["target"];
                    if (target == null)
                    {
                        continue; // Skip this file if the target does not exist
                    }

                    int targetRuns = target["runs"]?.Value<int>() ?? 0;
                    int targetOvers = target["overs"]?.Value<int>() ?? 0;

                    if (targetOvers < 20)
                    {
                        continue;
                    }
                    

                    int score = 0;
                    int wickets = 0;

                    var overs = jsonData["innings"][1]["overs"];
                    bool isMatchFound = false;

                    foreach (var over in overs)
                    {
                        foreach (var delivery in over["deliveries"])
                        {
                            score += delivery["runs"]["total"].Value<int>();

                            if (delivery["wickets"] != null)
                            {
                                wickets++;
                            }

                            if (wickets > 4 && score < targetRuns - 30)
                            {
                                break;
                            }

                            if (score >= targetRuns - 30 && over["over"].Value<int>() <= targetOvers - 6)
                            {
                                isMatchFound = true;
                                break;
                            }
                        }

                        if (isMatchFound || (wickets > 4 && score < targetRuns - 30))
                        {
                            break;
                        }
                    }

                    if (isMatchFound)
                    {
                        Console.WriteLine(String.Format("{0}.{1} vs {2}, {3}, Chasing Team that lost - {4}, Target Runs {5}, Score {6}, wickets {7}",
                        isMatchFoundCounter,
                            teams[0],
                            teams[1],
                            info["dates"][0],
                            jsonData["innings"]?[1]["team"],
                            targetRuns,
                            score,
                            wickets));
                        Console.WriteLine();
                        isMatchFoundCounter++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {jsonFile}: {ex.Message}");
                continue;
            }
        }
    }
}
