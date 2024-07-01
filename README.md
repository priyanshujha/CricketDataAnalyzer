# CricketDataAnalyzer

This is a C# console application that processes T20I cricket match JSON files. It extracts and filters specific match information based on certain conditions related to T20 international cricket matches.
The analyzer was written out of curiosity to find out how many times in history has a chasing team lost T20I after needing less than 30 runs in last 5 overs with atleast 6 wickets remaining, after India beat SA in T20I WC 2024 final (https://www.espncricinfo.com/series/icc-men-s-t20-world-cup-2024-1411166/india-vs-south-africa-final-1415755/full-scorecard)

## Features

- Processes JSON files from a specified directory.
- Filters matches based on outcome conditions (by runs).
- Checks if both teams in the match are within the top 20 T20 international teams.
- Analyzes the second innings to determine if the chasing team required less than 30 runs from 30 balls with more than or equal to 6 wickets remaining.
- Outputs the filenames of matches that meet the criteria.

## Dataset

This analyzer uses the T20I ball-by-ball dataset available at [CricSheet](https://cricsheet.org/matches/).

## Usage

1. **Clone the Repository**:
   ```shell
   git clone https://github.com/your-username/CricketDataAnalyzer.git
   cd CricketDataAnalyzer
   ```
2. **Build the Project**:
    
    Ensure you have .NET installed on your machine. Then, build the project using:
    ```shell
    dotnet build
    ```
3. **Run the Program**:

    Provide the path to the directory containing the JSON files as a command line argument.
    ```shell
    dotnet run -- "C:\path\to\your\json\files"
    ```

## How It Works

The CricketDataAnalyzer processes T20I cricket match JSON files to extract and filter specific match information based on certain criteria. Here’s a detailed breakdown of its workflow:

1. **Command Line Argument**: The program takes a folder path as a command line argument.
   - Usage example:
     ```shell
     dotnet run -- "C:\path\to\your\json\files"
     ```

2. **Directory Check**: It verifies that the provided directory exists.
   - If the directory does not exist, the program outputs an error message and terminates.

3. **Top 20 Teams**: The program maintains a list of the top 20 T20 international teams.
   - It checks if both teams in the match are within this list before proceeding with further analysis.

4. **Outcome Check**: The program filters matches where the outcome was decided by runs.
   - It examines the `outcome` section of the JSON file to determine if the match was won by runs.

5. **Second Innings Analysis**: The program examines the second innings to determine if the chasing team needed less than 30 runs from 30 balls with more than or equal to 6 wickets remaining.
   - It retrieves the target runs and overs for the second innings.
   - It processes each delivery in the second innings, tracking the score and wickets.
   - If the chasing team required less than 30 runs from 30 balls with more than 6 wickets remaining, the match is considered a potential match of interest.

6. **Output**: The program prints the filenames of the matches that meet all criteria.
   - If a match meets the criteria, the filename is output to the console.

By following these steps, the CricketDataAnalyzer efficiently filters and identifies T20I matches of interest based on the specified conditions.

## Dependencies

- **.NET 8**: This project is built using .NET 8. Ensure you have the .NET 8 installed.
- **Newtonsoft.Json**: Used for parsing JSON files. Install it via NuGet:
  ```shell
  dotnet add package Newtonsoft.Json

## Contributing

Feel free to fork this repository, make changes, and submit pull requests. Contributions are welcome!

## License

This project is licensed under the MIT License.
