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
    public class CreateModel: PageModel {
        private readonly IIssuBO IssuBO;
        public CreateModel(IIssuBO issuBO) {
            IssuBO = issuBO;
        }

        [BindProperty]
        public IssuModel Issu { get; set; }

        [BindProperty]
        public List<LblModel> LstLbl { get; set; }

        public async Task<ActionResult> OnGet() {
            var lxRslt = await IssuBO.GetAllLbl();

            if(lxRslt.Err == null) {
                LstLbl = lxRslt.Lst;
            }

            return Page();
        }

        public async Task<IActionResult> OnPost() {

            Issu.LstLbl = LstLbl;

            var lxRslt = await IssuBO.Create(Issu);

            if(lxRslt.Err != null) {
                TempData["MsgErr"] = lxRslt.Err.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}