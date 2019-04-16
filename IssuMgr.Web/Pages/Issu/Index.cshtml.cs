using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace IssuMgr.Web.Pages.Issu {
    public class IndexModel: PageModel {
        private readonly IIssuBO IssuBO;

        public IndexModel(IIssuBO issuBO) {
            IssuBO = issuBO;
        }

        public List<IssuModel> Issus { get; set; }

        public async void OnGet() {
            var lxRslt = await IssuBO.GetAll();

            if(lxRslt.Err != null) {
                TempData["MsgErr"] = lxRslt.Err.Message;
            }

            Issus = lxRslt.Lst;
        }
    }
}
