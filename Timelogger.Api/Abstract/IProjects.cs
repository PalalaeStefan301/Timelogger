using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Timelogger.Entities;

namespace Timelogger.Api.Abstract
{
    internal interface IProjects
    {
        #region CRUD
        IActionResult Get(int id = 0, string name = null);
        IActionResult Post([FromBody] Project project);
        IActionResult Put([FromBody] Project project);
        IActionResult Delete(int id);
        #endregion
        IActionResult GetByDeadline(bool orderDesc = false);
    }
}
