using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IssuMgr.API.Controllers {
    /// <summary>
    /// Labels etiques para los Issues
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LblController: ControllerBase {
        private readonly ILblBO LblBO;
        public LblController(ILblBO lblBO) {
            LblBO = lblBO;
        }

        // GET: api/Lbl
        /// <summary>
        /// Retorna todos los "Labels" disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<LstRslt<LblModel>> GetAll() {
            var lxLstLbl = await LblBO.GetAll();

            return lxLstLbl;
        }

        // GET: api/Lbl/5
        /// <summary>
        /// Retorna un Label con un Id específico
        /// </summary>
        /// <param name="id">Id del Lable a consultar</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<LblModel>> Get(int id) {
            bool lxLblExists = await LblBO.Exists(id);

            if(!lxLblExists) {
                return NotFound();
            }

            var lxRslt = await LblBO.Get(id);

            if(lxRslt.Err == null) {
                var lxLbl = lxRslt.Sngl;
                return Ok(lxLbl);
            } else {
                return BadRequest(lxRslt.Err);
            }
        }

        // POST: api/Lbl
        /// <summary>
        /// Crear (Insertar) un Label
        /// </summary>
        /// <param name="lbl">Información de Model del Label</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LblModel>> Post([FromBody] LblModel lbl) {
            var lxRslt = await LblBO.Create(lbl);

            if(lxRslt.Err != null) {
                return BadRequest(lbl);
            }

            return CreatedAtAction(nameof(Get), new { id = lbl.LblId }, lbl);
        }

        // PUT: api/Lbl/5
        /// <summary>
        /// Actualizar (Update) la información de un Label específico
        /// </summary>
        /// <param name="id">Id del Label</param>
        /// <param name="lbl">Información de Model del Label</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<LblModel>> Put(int id, [FromBody] LblModel lbl) {
            var lxRslt = await LblBO.Update(id, lbl);

            if(lxRslt.Err != null) {
                return BadRequest(lxRslt);
            }

            return CreatedAtAction(nameof(Get), new { id = lbl.LblId }, lbl);
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Borrar un Label según su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
