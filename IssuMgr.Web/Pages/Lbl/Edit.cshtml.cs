using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace IssuMgr.Web.Pages.Lbl {
    public class EditModel: PageModel {
        private readonly ILblBO LblBO;
        public EditModel(ILblBO lblBO) {
            LblBO = lblBO;
        }

        [BindProperty]
        public LblModel Lbl { get; set; }
        public async Task<ActionResult> OnGet(int id) {
            var lxRslt = await LblBO.Get(id);
            if(lxRslt.Err != null) {
                TempData["MsgErr"] = lxRslt.Err.Message;

                return Page();
            }

            Lbl = lxRslt.Sngl;

            return Page();
        }

        public async Task<IActionResult> OnPost(int id) {
            //if(!ModelState.IsValid) {
            //    return Page();
            //}

            ExeRslt lxRslt = await LblBO.Update(id, Lbl);

            if(lxRslt.Err != null) {
                TempData["MsgErr"] = lxRslt.Err.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}