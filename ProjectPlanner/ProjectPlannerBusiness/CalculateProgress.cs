using System.Collections.Generic;
using System.Linq;
using ProjectPlannerModel;

namespace ProjectPlannerBusiness
{
    public class CalculateProgress
    {
        public int CalculateProjectProgress(Project current)
        {
            List<Feature> _completeFeatures = new List<Feature>();
            List<Feature> _toDoFeatures = new List<Feature>();
            List<Issue> _issues = new List<Issue>();

            using (PlannerContext pc = new PlannerContext())
            {
                var _completeIDs =
                    from f in pc.Features
                    where f.ProjectId == current.ProjectId && f.Status == 3
                    select f;


                foreach (var item in _completeIDs)
                {
                    _completeFeatures.Add(item);
                }

                var _toDoIDs =
                    from f in pc.Features
                    where f.ProjectId == current.ProjectId && f.Status != 3
                    orderby f.Priority
                    select f;

                foreach (var item in _toDoIDs)
                {
                    _toDoFeatures.Add(item);
                }                

                var _issueIDs =
                    from i in pc.Issues
                    where i.ProjectId == current.ProjectId && i.Status != 3
                    orderby i.Priority
                    select i;

                foreach (var item in _issueIDs)
                {
                    _issues.Add(item);
                }

            }

            int progress = 0;
            int numOfFeatures = _completeFeatures.Count + _toDoFeatures.Count;

            if (numOfFeatures == 0)
            {
                return 0;
            }

            int featureWorth = 100 / numOfFeatures;

            foreach (var item in _toDoFeatures)
            {
                if (item.Status == 0)
                {
                    progress += featureWorth / 5;
                }
                else if (item.Status == 1)
                {
                    progress += featureWorth / 2;
                }
                else if (item.Status == 2)
                {
                    progress += (featureWorth / 4) * 3;
                }
            }

            progress += _completeFeatures.Count * featureWorth;

            foreach (var item in _issues)
            {
                if (item.Status == 0 || item.Status == 1)
                {
                    if (item.Priority <= 5)
                    {
                        progress -= 10;
                    }
                    else if (item.Priority <= 10)
                    {
                        progress -= 5;
                    }
                    else
                    {
                        progress -= 3;
                    }
                }
                else if (item.Status == 2)
                {
                    if (item.Priority <= 5)
                    {
                        progress -= 3;
                    }
                    else if (item.Priority <= 10)
                    {
                        progress -= 2;
                    }
                    else
                    {
                        progress -= 1;
                    }
                }
            }

            return progress;
        }
    }
}
