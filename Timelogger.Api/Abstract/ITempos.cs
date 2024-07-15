using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Timelogger.Entities;

namespace Timelogger.Api.Abstract
{
    internal interface ITempos
    {
        #region CRUD
        IActionResult Get(int id = 0, int projectid = 0);
        IActionResult Post([FromBody] Tempo tempo);
        IActionResult Put([FromBody] Tempo tempo);
        IActionResult Delete(int id);
        #endregion
    }
}
