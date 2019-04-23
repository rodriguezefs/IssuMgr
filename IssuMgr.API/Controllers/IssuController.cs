using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc;

namespace IssuMgr.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class IssuController: ControllerBase {
        private readonly IIssuBO IssuBO;
        public IssuController(IIssuBO issuBO) {
            IssuBO = issuBO;
        }
        // GET: api/Issu
        /// <summary>
        /// Retorna todos los "Issues" disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<LstRslt<IssuModel>> GetAll() {
            var lxLstIssu = await IssuBO.GetAll();

            return lxLstIssu;
        }

        // GET: api/Issu/5
        /// <summary>
        /// Retorna un Issue con un Id específico
        /// </summary>
        /// <param name="id">Id del Lable a consultar</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<IssuModel>> Get(int id) {
            bool lxIssuExists = await IssuBO.Exists(id);

            if(!lxIssuExists) {
                return NotFound();
            }

            var lxRslt = await IssuBO.Get(id);

            if(lxRslt.Err == null) {
                var lxIssu = lxRslt.Sngl;
                return Ok(lxIssu);
            } else {
                return BadRequest(lxRslt.Err);
            }
        }

        // POST: api/Issu
        /// <summary>
        /// Crear (Insertar) un Issue
        /// </summary>
        /// <param name="issu">Información de Model del Issue</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IssuModel>> Post([FromBody] IssuModel issu) {
            var lxRslt = await IssuBO.Create(issu);

            if(lxRslt.Err != null) {
                return BadRequest(issu);
            }

            return CreatedAtAction(nameof(Get), new { id = issu.IssuId }, issu);
        }

        // PUT: api/Issu/5
        /// <summary>
        /// Actualizar (Update) la información de un Issue específico
        /// </summary>
        /// <param name="id">Id del Issue</param>
        /// <param name="issu">Información de Model del Issue</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<IssuModel>> Put(int id, [FromBody] IssuModel issu) {
            var lxRslt = await IssuBO.Update(id, issu);

            if(lxRslt.Err != null) {
                return BadRequest(lxRslt);
            }

            return CreatedAtAction(nameof(Get), new { id = issu.IssuId }, issu);
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Borrar un Issue según su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            bool lxIssuExists = await IssuBO.Exists(id);

            if(!lxIssuExists) {
                return NotFound();
            }
            var lxRslt = await IssuBO.Delete(id);
            var lxId = lxRslt.NewId;

            return NoContent();
        }
    }
}