using System;
using System.Collections.Generic;
using System.Text;
using Timelogger.Entities;

namespace Timelogger.Abstract
{
    public interface ITemposRepositories : IDisposable
    {
        //crud
        Tempo GetTempo(int id);
        List<Tempo> GetTempoByProjectId(int projectId);
        void AddTempo(Tempo tempo, bool autosave = true);
        void UpdateTempo(Tempo tempo, bool autosave = true);
        void DeleteTempo(int id);
    }
}
