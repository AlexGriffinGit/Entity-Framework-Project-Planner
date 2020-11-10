using System.Collections.Generic;
using System.Linq;
using ProjectPlannerModel;

namespace ProjectPlannerBusiness
{
    public class CRUDFeatureManager
    {
        public Feature SelectedFeature { get; set; }

        public List<Feature> RetrieveAllFeatures()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                return pc.Features.ToList();
            }
        }

        public List<Feature> RetrieveProjectFeatures(CRUDProjectManager currentProj)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Feature> features = new List<Feature>();

                var _featureIDs =
                    from f in pc.Features
                    where f.ProjectId == currentProj.SelectedProject.ProjectId
                    orderby f.Priority
                    select f;

                foreach (var item in _featureIDs)
                {
                    features.Add(item);
                }

                return features;
            }
        }

        public List<Feature> RetrieveCompleteFeatures(CRUDProjectManager currentProj)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Feature> features = new List<Feature>();

                var _featureIDs =
                    from f in pc.Features
                    where f.ProjectId == currentProj.SelectedProject.ProjectId && f.Status == 3
                    select f;

                if (_featureIDs.Count() > 0)
                {
                    foreach (var item in _featureIDs)
                    {
                        features.Add(item);
                    }
                }

                return features;
            }
        }

        public List<Feature> RetrieveToDoFeatures(CRUDProjectManager currentProj)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                List<Feature> features = new List<Feature>();

                var _featureIDs =
                    from f in pc.Features
                    where f.ProjectId == currentProj.SelectedProject.ProjectId && f.Status != 3
                    orderby f.Priority
                    select f;

                foreach (var item in _featureIDs)
                {
                    features.Add(item);
                }

                return features;
            }
        }

        public void SetSelectedFeature(object selected)
        {
            SelectedFeature = (Feature)selected;
        }

        public void CreateNewFeature(string title, string description, int status, int priority, string notes, CRUDProjectManager currentProj)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                Feature _newFeature = new Feature()
                {
                    Title = title,
                    Description = description,
                    Status = status,
                    Priority = priority,
                    Notes = notes,

                    ProjectId = currentProj.SelectedProject.ProjectId
                };

                pc.Features.Add(_newFeature);

                pc.SaveChanges();
            }
        }

        public void UpdateFeature(string title, string description, int status, int priority, string notes)
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _updateFeature =
                    from f in pc.Features
                    where f.FeatureId == SelectedFeature.FeatureId
                    select f;

                foreach (var feature in _updateFeature)
                {
                    feature.Title = title;
                    feature.Description = description;
                    feature.Status = status;
                    feature.Priority = priority;
                    feature.Notes = notes;

                    SelectedFeature = feature;
                }

                pc.SaveChanges();
            }
        }

        public void DeleteFeature()
        {
            using (PlannerContext pc = new PlannerContext())
            {
                var _deleteFeature =
                    from f in pc.Features
                    where f.FeatureId == SelectedFeature.FeatureId
                    select f;

                foreach (var item in _deleteFeature)
                {
                    pc.Features.Remove(item);
                }

                pc.SaveChanges();
            }
        }
    }
}
