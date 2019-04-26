using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace IssuMgr.Web.Pages.Lbl {
    public class IndexModel: PageModel {
        private readonly ILblBO LblBO;
        public IndexModel(ILblBO lblBO) {
            LblBO = lblBO;
        }

        public List<LblModel> Lbls { get; set; }

        public bool MosPrm => PagAct != 1;
        public bool MosPrv => PagAct > 1;
        public bool MosSig => PagAct < TotPag;
        public bool MosUlt => PagAct != TotPag;

        [BindProperty]
        public int PagAct { get; set; } = 1;
        public int TamPag { get; set; } = 5;
        public int TotPag { get; set; }
        public async void OnGet() {
            var lxRslt = await LblBO.GetPag(PagAct,TamPag);

            if(lxRslt.Err != null) {
                TempData["ExErr"] = lxRslt.Err;
            }
            TotPag = lxRslt.TotPag;

            Lbls = lxRslt.Lst;
        }
    }
}