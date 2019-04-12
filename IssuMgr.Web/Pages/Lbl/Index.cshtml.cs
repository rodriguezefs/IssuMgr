using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuMgr.API.BO;
using IssuMgr.API.BO.Interfaces;
using IssuMgr.API.Util;
using IssuMgr.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssuMgr.Web.Pages.Lbl {
    public class IndexModel: PageModel {
        private readonly ILblBO LblBO;
        public IndexModel(ILblBO lblBO) {
            LblBO = lblBO;
        }

        public LstRslt<LblModel> Lbls { get; set; }
        public async void OnGet() {
            Lbls = await LblBO.GetAll();
        }
    }
}