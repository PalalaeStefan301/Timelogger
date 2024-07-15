using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using Timelogger.Abstract;
using Timelogger.Api.Abstract;
using Timelogger.Concrete;
using Timelogger.Entities;
using Timelogger.Filters;

namespace Timelogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidateModelState))]
    public class TemposController : Controller, ITempos
    {
        private readonly ITemposRepositories temposRepositories;
        public TemposController(ITemposRepositories temposRepositories)
        {
            this.temposRepositories = temposRepositories;
        }

        [HttpGet]
        public IActionResult Get(int id = 0, int projectId = 0)
        {
            if(id != 0)
            {
                Tempo tempo = temposRepositories.GetTempo(id);
                if(tempo == null)
                {
                    return NotFound();
                }
                return Ok(tempo);
            }

            if (projectId != 0)
            {
                List<Tempo> tempos = temposRepositories.GetTempoByProjectId(projectId);
                if (tempos == null || tempos.Count == 0)
                {
                    return NotFound();
                }
                return Ok(tempos);
            }

            //here I choose to let the bad case scenario to be the last resort, because if this got treated first then I'd still had to return a bad request here
            return BadRequest("At least one of the 2 ids must be different than zero, id or projectId");
        }
        [HttpPost]
        //[SkipCustomFilterAttribute]
        public IActionResult Post([FromBody] Tempo tempo)
        {
            if(tempo.Hours == 0)
            {
                //if I don't get here means that the hours is already bigger than 1, respecting the mininum of 30 mins criteria
                //if I get here instead I check the difference in minutes between startDate and endDate
                if(((TimeSpan)(tempo.EndDate - tempo.StartDate)).TotalMinutes < 30)
                {
                    return BadRequest("Minunum value for a tempo must be 30 minutes.");
                }
            }

            if(tempo.Id != 0)
            {
                tempo.Id = 0;
            }
            temposRepositories.AddTempo(tempo);
            return Ok();


        }
        [HttpPut]
        public IActionResult Put([FromBody] Tempo tempo)
        {
            if (tempo.Id != 0)
            {
                tempo.Id = 0;
            }
            var oldTempo = tempo;
            temposRepositories.UpdateTempo(tempo);
            return Ok(new { OldTempo = oldTempo, NewTempo = tempo });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //not sure if I have to be more specific if the tempo exists or not, I choose to say less, if the tempo exists will be deleted
            //and here could be added a more specific way to let freelancers delete tempos, like checking to be theirs tempos, not letting other users to delete something that's not theirs.
            temposRepositories.DeleteTempo(id);
            return Ok();
        }
    }
}
