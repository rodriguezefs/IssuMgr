//using IssuMgr.BO.Interfaces;
//using IssuMgr.Model;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Threading.Tasks;

//namespace IssuMgr.Web.Pages.Lbl {
//    public class DeleteModel: PageModel {
//        private readonly ILblBO LblBO;
//        public DeleteModel(ILblBO lblBO) {
//            LblBO = lblBO;
//        }

//        [BindProperty]
//        public LblModel Lbl { get; set; }
//        public async Task<ActionResult> OnGet(int id) {
//            var lxRslt = await LblBO.Get(id);

//            if(lxRslt.Err != null) {
//                TempData["MsgErr"] = lxRslt.Err.Message;

//                return Page();
//            }
//            return Page();
//        }

//        public async Task<ActionResult> OnPost(int id) {
//            var lxRslt = await LblBO.Delete(id);

//            if(lxRslt.Err != null) {
//                TempData["MsgErr"] = lxRslt.Err.Message;

//                return Page();
//            }

//            return RedirectToPage("./Index");
//        }
//    }
//}