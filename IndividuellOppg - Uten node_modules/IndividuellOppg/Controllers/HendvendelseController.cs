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
    public class HendvendelseController : ControllerBase
    {
        private readonly IHendvendelseRepository _db;
        private readonly ILogger<HendvendelseController> _log;

        public HendvendelseController(IHendvendelseRepository db, ILogger<HendvendelseController> log)
        {
            _db = db;
            _log = log;
        }


        [HttpGet]
        public async Task<ActionResult> HentAlle()
        {
            List<Hendvendelse> alleHendvendelser = await _db.HentAlle();

            return Ok(alleHendvendelser);
        }


        [HttpPost("/Hendvendelse")]
        [Route("Hendvendelse")]
        public async Task<ActionResult> LagHendvendelse(Hendvendelse innHendvendelse)
        {
            if (ModelState.IsValid)
            {
                var resultOK = await _db.LagHendvendelse(innHendvendelse);

                if (!resultOK)
                {
                    return BadRequest("Hendvendelsen kunne ikke opprettes");
                }
                return Ok("Hendvendelsen ble opprettet");
            }
            return BadRequest("Feil i inputvalidering");
        }

        [HttpPost("/Mail")]
        [Route("Mail")]
        public ActionResult SendMail(Mail mail)
        {
            if (ModelState.IsValid)
            {
                var resultOK = _db.SendMail(mail);
                if (!resultOK)
                {
                    return BadRequest("Mail kunne ikke bli sendt");
                }
                return Ok("Mail sendt");
            }
            return BadRequest("Feil i inputvalidering");
        }

        [HttpPut]
        public async Task<ActionResult> EndreHendvendelse(Hendvendelse innHendvendelse)
        {
            if (ModelState.IsValid)
            {
                var resultOK = await _db.EndreHendvendelse(innHendvendelse);

                if (!resultOK)
                {
                    return NotFound("Hendvendelse kunne ikke endres");
                }
                return Ok("Hendvendelse endret");
            }
            return BadRequest("Feil i inputvalidering");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettHendvendelse(int id)
        {
            var resultOK = await _db.SlettHendvendelse(id);

            if (!resultOK)
            {
                return NotFound("Hendvendelsen ble ikke slettet");
            }
            return Ok("Hendvendelsen ble slettet");
        }
    }
}
