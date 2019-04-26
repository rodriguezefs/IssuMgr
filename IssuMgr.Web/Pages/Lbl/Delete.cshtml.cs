using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace IssuMgr.Web.Pages.Lbl {
    public class DeleteModel: PageModel {
        private readonly ILblBO LblBO;
        public DeleteModel(ILblBO lblBO) {
            LblBO = lblBO;
        }

        [BindProperty]
        public LblModel Lbl { get; set; }
        public async Task<ActionResult> OnGet(int id) {
            var lxRslt = await LblBO.Get(id);

            if(lxRslt.Err != null) {
                TempData["ExErr"] = lxRslt.Err;

                return Page();
            }
            Lbl = lxRslt.Sngl;

            return Page();
        }

        public async Task<ActionResult> OnPost(int LblId) {
            var lxRslt = await LblBO.Delete(LblId);

            if(lxRslt.Err != null) {
                TempData["ExErr"] = lxRslt.Err;

                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}