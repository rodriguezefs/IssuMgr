using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssuMgr.Web.Pages.Issu {
    public class EditModel : PageModel {
        private readonly IIssuBO IssuBO;
        public EditModel(IIssuBO issuBO) {
            IssuBO = issuBO;
        }

        [BindProperty]
        public IssuModel Issu { get; set; }

        [BindProperty]
        public List<LblModel> LstLbl { get; set; }

        public async Task<ActionResult> OnGet(int id) {
            var lxRsltLbl = await IssuBO.GetAllLbl();

            if(lxRsltLbl.EsVld == false) {
                TempData["ExErr"] = lxRsltLbl.Err;
                return RedirectToPage("./Index");
            }
            LstLbl = lxRsltLbl.Lst;

            var lxRslt = await IssuBO.Get(id);
            if(lxRslt.EsVld == false) {
                TempData["ExErr"] = lxRslt.Err;
                return RedirectToPage("./Index");
            }
            Issu = lxRslt.Sngl;

            return Page();
        }

        public async Task<IActionResult> OnPost(int id) {
            var lxRslt = await IssuBO.Update(id, Issu);

            if(lxRslt.EsVld == false) {
                TempData["ExErr"] = lxRslt.Err;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}