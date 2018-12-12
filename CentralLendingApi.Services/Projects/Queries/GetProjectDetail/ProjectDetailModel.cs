using CentralLendingApi.Data.Models;
using System;
using System.Linq.Expressions;

namespace CentralLendingApi.Services.Projects.Queries.GetProjectDetail
{
    public class ProjectDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Note { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public int? Term { get; set; }
        public string Link { get; set; }
        public DateTime? PollDate { get; set; }
        public DateTime? StartDate { get; set; }

        public static Expression<Func<Project, ProjectDetailModel>> Projection
        {
            get
            {
                return project => HMapper.Mapper.Map<Project, ProjectDetailModel>(project);
            }
        }

        public static ProjectDetailModel Create(Project project)
        {
            return Projection.Compile().Invoke(project);
        }
    }
}
