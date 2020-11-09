using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ProjectPlannerModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectPlannerBusiness
{
    public class JSONExporter
    {
        private CRUDManager _crudManager = new CRUDManager();
        string path = "";

        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        public string SerialiseProjects()
        {
            string filePath = Path.Combine(path, "Projects.json");
            string jsonString = JsonSerializer.Serialize(_crudManager.RetrieveAllProjects(), options);

            File.WriteAllText(filePath, jsonString);

            return $"Your Projects JSON file is located at { filePath }";
        }

        public string SerialiseFeatures()
        {
            string filePath = Path.Combine(path, "Features.json");
            string jsonString = JsonSerializer.Serialize(_crudManager.RetrieveAllFeatures(), options);

            File.WriteAllText(filePath, jsonString);

            return $"Your Features JSON file is located at { filePath }";
        }

        public string SerialiseIssues()
        {
            string filePath = Path.Combine(path, "Issues.json");
            string jsonString = JsonSerializer.Serialize(_crudManager.RetrieveAllIssues(), options);

            File.WriteAllText(filePath, jsonString);

            return $"Your Issues JSON file is located at { filePath }";
        }

        public string SerialiseNotes()
        {
            string filePath = Path.Combine(path, "Notes.json");
            string jsonString = JsonSerializer.Serialize(_crudManager.RetrieveAllNotes(), options);

            File.WriteAllText(filePath, jsonString);

            return $"Your Notes JSON file is located at { filePath }";
        }

        public void InitSerialisation()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "JSON Export");
            Directory.CreateDirectory(Path.Combine(path));
        }
    }
}
