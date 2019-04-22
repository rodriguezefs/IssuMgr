using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
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

        public ActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPost() {
            var lxRslt = await IssuBO.Create(Issu);

            if(lxRslt.Err != null) {
                TempData["MsgErr"] = lxRslt.Err.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}