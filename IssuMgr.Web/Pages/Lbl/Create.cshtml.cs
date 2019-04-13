using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace IssuMgr.Web.Pages.Lbl {
    public class CreateModel: PageModel {
        private readonly ILblBO LblBO;
        public CreateModel(ILblBO lblBO) {
            LblBO = lblBO;
        }

        [BindProperty]
        public LblModel Lbl { get; set; }
        public ActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPost() {
            //if(!ModelState.IsValid) {
            //    return Page();
            //}

            var lxRslt = await LblBO.Create(Lbl);

            if(lxRslt.Err != null) {
                TempData["MsgErr"] = lxRslt.Err.Message;

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}