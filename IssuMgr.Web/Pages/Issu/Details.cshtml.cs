using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssuMgr.Web.Pages.Issu {
    public class DetailsModel: PageModel {
        private readonly IIssuBO IssuBO;

        public DetailsModel(IIssuBO issuBO) {
            IssuBO = issuBO;
        }

        [BindProperty(SupportsGet=true)]
        public IssuModel Issu { get; set; }

        public async void OnGet(int id) {
            var lxRslt = await IssuBO.Get(id);

            if(lxRslt.EsVld == false) {
                TempData["ExErr"] = lxRslt.Err;
            }

            Issu = lxRslt.Sngl;
        }
    }
}
