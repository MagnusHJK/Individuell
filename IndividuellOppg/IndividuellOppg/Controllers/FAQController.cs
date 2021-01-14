using Castle.Core.Internal;
using Castle.Core.Logging;
using IndividuellOppg.DAL;
using IndividuellOppg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IFAQRepository _db;
        private readonly ILogger<FAQController> _log;

        public FAQController(IFAQRepository db, ILogger<FAQController> log)
        {
            _db = db;
            _log = log;
        }


        [HttpGet]
        public async Task<ActionResult> HentAlle()
        {
            List<FAQ> alleFAQs = await _db.HentAlle();

            if (alleFAQs.IsNullOrEmpty())
            {
                return NotFound("Ingen spørsmål funnet");
            }
            return Ok(alleFAQs);
        }

        [HttpPost]
        public async Task<ActionResult> Lag(FAQ innFAQ)
        {
            if (ModelState.IsValid)
            {
                var returnOK = await _db.Lag(innFAQ);
                if (!returnOK)
                {
                    return NotFound("FAQ kunne ikke opprettes");
                }
                return Ok("FAQ ble opprettet");
            }
            return BadRequest("Feil i inputvalidering");
        }


        [HttpPut]
        public async Task<ActionResult> Endre(FAQ endretFAQ)
        {
            if (ModelState.IsValid)
            {
                var returnOK = await _db.Endre(endretFAQ);

                if (!returnOK)
                {
                    return NotFound("FAQ kunne ikke endres");
                }
                return Ok("FAQ ble endret");
            }
            return BadRequest("Feil i inputvalidering");
        }

        [HttpDelete("{FAQId}")]
        public async Task<ActionResult> Slett(int FAQId)
        {
            var returnOK = await _db.Slett(FAQId);

            if(!returnOK)
            {
                return NotFound("FAQ kunne ikke slettes");
            }
            return Ok("FAQ ble slettet");
        }
    }
}
