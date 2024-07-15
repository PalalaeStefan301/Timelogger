using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timelogger.Abstract;
using Timelogger.Entities;

namespace Timelogger.Concrete
{
    public class TemposRepositories : ITemposRepositories
    {
        private readonly ApiContext _context;
        public TemposRepositories(ApiContext context)
        {
            _context = context;
        }

        public Tempo GetTempo(int id)
        {
            return _context.Tempos.Find(id);
        }
        public void AddTempo(Tempo tempo, bool autosave = true)
        {
            _context.Tempos.Add(tempo);
            if(autosave)
            {
                Save();
            }
        }
        public List<Tempo> GetTempoByProjectId(int projectId)
        {
            return _context.Tempos.Where(x => x.ProjectId == projectId).ToList();
        }
        public void UpdateTempo(Tempo tempo, bool autosave = true)
        {
            _context.Entry(tempo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            if (autosave)
            {
                Save();
            }
        }
        public void DeleteTempo(int id)
        {
            Tempo tempo = _context.Tempos.Find(id);
            if(tempo != null)
            {
                _context.Tempos.Remove(tempo);
                Save();
            }
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
