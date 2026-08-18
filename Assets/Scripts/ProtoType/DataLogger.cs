using System.IO;
using UnityEngine;

namespace Spidy.XRDataShowcase{
public class DataLogger : MonoBehaviour
{
    /// <summary>
    /// Base class for saving data into CSV files.
    /// Other scripts can inherit from this class.
    /// </summary>
    protected string fileName = "GameData.csv";

    private string filePath;

        protected virtual void Awake()
        {
            
            filePath = $"SimulationData/{fileName}";

            string directory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Creates the CSV file and writes the header.
        /// Existing file will be overwritten.
        /// </summary>
        protected void CreateCSV(string header)
        {
            File.WriteAllText(filePath, header + "\n");

            Debug.Log("CSV Created: " + filePath);
        }

        /// <summary>
        /// Adds one row of data to the CSV file.
        /// </summary>
        protected void AddRow(string row)
        {   
            File.AppendAllText(filePath, row + "\n");
        }

        /// <summary>
        /// Returns the current CSV file path.
        /// </summary>
        protected string GetFilePath()
        {
            return filePath;
        }
    
}
}