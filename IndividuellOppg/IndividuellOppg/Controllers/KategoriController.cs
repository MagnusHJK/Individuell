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
    public class KategoriController : ControllerBase
    {
        private readonly IKategoriRepository _db;
        private readonly ILogger<KategoriController> _log;

        public KategoriController(IKategoriRepository db, ILogger<KategoriController> log)
        {
            _db = db;
            _log = log;
        }


        [HttpGet]
        public async Task<ActionResult> HentAlle()
        {
            List<Kategori> alleKategorier = await _db.HentAlle();

            if (alleKategorier.IsNullOrEmpty())
            {
                return NotFound("Ingen kategorier funnet");
            }
            return Ok(alleKategorier);
        }

        [HttpPost]
        public async Task<ActionResult> Lag(Kategori innKategori)
        {
            if (ModelState.IsValid)
            {
                var nyKategori = await _db.Lag(innKategori);

                if (nyKategori == null)
                {
                    return BadRequest("Kategori ble ikke opprettet");
                }
                return Ok(nyKategori);
            }
            return BadRequest("Feil i inputvalidering");
        }
    }
}
