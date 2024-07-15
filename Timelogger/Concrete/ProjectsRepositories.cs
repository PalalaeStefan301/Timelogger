using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timelogger.Abstract;
using Timelogger.Entities;

namespace Timelogger.Concrete
{
    public class ProjectsRepositories : IProjectsRepositories
    {
        private readonly ApiContext _context;
        public ProjectsRepositories(ApiContext context)
        {
            _context = context;
        }

        public Project GetProject(int id)
        {
            return _context.Projects.Find(id);
        }
        public void AddProject(Project project, bool autosave = true)
        {
            _context.Projects.Add(project);
            if(autosave)
            {
                Save();
            }
        }
        public Project GetProjectByName(string name)
        {
            return _context.Projects.FirstOrDefault(x => x.Name == name);
        }
        public List<Project> GetAllProjects(bool orderDesc)
        {
            if (orderDesc)
            {
                return _context.Projects.OrderByDescending(x => x.Deadline).ToList();
            }
            else
            {
                return _context.Projects.OrderBy(x => x.Deadline).ToList();
            }
        }
        public void UpdateProject(Project project, bool autosave = true)
        {
            _context.Entry(project).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            if (autosave)
            {
                Save();
            }
        }
        public void DeleteProject(int id)
        {
            Project project = _context.Projects.Find(id);
            if(project != null)
            {
                _context.Projects.Remove(project);
                Save();
            }
        }
        public List<Tempo> GetTemposByProjectid(int projectId)
        {
            return _context.Tempos.Where(x => x.ProjectId == projectId).ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
