using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuMgr.API.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssuMgr.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LblController: ControllerBase {
        private readonly ILblBO LblBO;
        public LblController(ILblBO lblBO) {
            LblBO = lblBO;
        }
        // GET: api/Lbl
        [HttpGet]
        public async Task<IEnumerable<LblModel>> Get() {
            var lxLstLbl = await LblBO.GetAll();
            
            return lxLstLbl;
        }

        // GET: api/Lbl/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<LblModel>> Get(int id) {
            bool lxLblExists = await LblBO.Exists(id);

            if(!lxLblExists) {
                return NotFound();
            }

            var lxRslt = await LblBO.Get(id);
            var lxLbl = lxRslt.Sngl;
            return lxLbl;
        }

        // POST: api/Lbl
        [HttpPost]
        public async Task<ActionResult<LblModel>> Post([FromBody] LblModel lbl) {
            var lxRslt = await LblBO.Create(lbl);

            if(lxRslt.Err != null) {
                return BadRequest(lbl);
            }

            return CreatedAtAction(nameof(Get), new { id = lbl.LblId }, lbl);
        }

        // PUT: api/Lbl/5
        [HttpPut("{id}")]
        public async Task<ActionResult<LblModel>> Put(int id, [FromBody] LblModel lbl) {
            var lxRslt = await LblBO.Update(id, lbl);

            if(lxRslt.Err != null) {
                return BadRequest(lxRslt);
            }

            return CreatedAtAction(nameof(Get), new { id = lbl.LblId }, lbl);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            bool lxLblExists = await LblBO.Exists(id);

            if(!lxLblExists) {
                return NotFound();
            }
            var lxRslt = await LblBO.Delete(id);
            var lxId = lxRslt.NewId;

            return NoContent();
        }
    }
}
