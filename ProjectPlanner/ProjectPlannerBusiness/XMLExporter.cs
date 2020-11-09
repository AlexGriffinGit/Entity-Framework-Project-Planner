using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ProjectPlannerModel;
using System.IO;

namespace ProjectPlannerBusiness
{
    public class XMLExporter
    {
        private CRUDManager _crudManager = new CRUDManager();
        string path = "";

        public string SerialiseProjects()
        {
            System.Xml.Serialization.XmlSerializer _projectSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(Project));
            string filePath = Path.Combine(path, "Projects.xml");
            FileStream file = File.Create(filePath);

            foreach (var item in _crudManager.RetrieveAllProjects())
            {
                _projectSerialiser.Serialize(file, item);
            }

            file.Close();

            return $"Your Projects XML file is located at { filePath }";
        }

        public string SerialiseFeatures()
        {
            System.Xml.Serialization.XmlSerializer _featureSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(Feature));
            string filePath = Path.Combine(path, "Features.xml");
            FileStream file = File.Create(filePath);

            foreach (var item in _crudManager.RetrieveAllFeatures())
            {
                _featureSerialiser.Serialize(file, item);
            }

            file.Close();

            return $"Your Features XML file is located at { filePath }";
        }

        public string SerialiseIssues()
        {
            System.Xml.Serialization.XmlSerializer _issueSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(Issue));
            string filePath = Path.Combine(path, "Issues.xml");
            FileStream file = File.Create(filePath);

            foreach (var item in _crudManager.RetrieveAllIssues())
            {
                _issueSerialiser.Serialize(file, item);
            }

            file.Close();

            return $"Your Issuse XML file is located at { filePath }";
        }

        public string SerialiseNotes()
        {
            System.Xml.Serialization.XmlSerializer _noteSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(Note));
            string filePath = Path.Combine(path, "Notes.xml");
            FileStream file = File.Create(filePath);

            foreach (var item in _crudManager.RetrieveAllNotes())
            {
                _noteSerialiser.Serialize(file, item);
            }

            file.Close();

            return $"Your Notes XML file is located at { filePath }";
        }

        public void InitSerialisation()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "XML Export");
            Directory.CreateDirectory(Path.Combine(path));
        }
    }
}