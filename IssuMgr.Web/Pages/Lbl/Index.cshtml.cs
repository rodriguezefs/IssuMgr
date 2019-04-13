using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace IssuMgr.Web.Pages.Lbl {
    public class IndexModel: PageModel {
        private readonly ILblBO LblBO;
        public IndexModel(ILblBO lblBO) {
            LblBO = lblBO;
        }

        public List<LblModel> Lbls { get; set; }
        public async void OnGet() {
            var lxRslt = await LblBO.GetAll();

            if(lxRslt.Err != null) {
                TempData["MsgErr"] = lxRslt.Err.Message;
            }
            Lbls = lxRslt.Lst;
        }
    }
}