using System;
using System.Collections.Generic;
using System.Text;
using Timelogger.Entities;

namespace Timelogger.Abstract
{
    public interface IProjectsRepositories : IDisposable
    {
        //crud
        Project GetProject(int id);
        void AddProject(Project project, bool autosave = true);
        void UpdateProject(Project project, bool autosave = true);
        void DeleteProject(int id);
        List<Tempo> GetTemposByProjectid(int projectId);
        Project GetProjectByName(string name);
        List<Project> GetAllProjects(bool orderDesc);
    }
}
