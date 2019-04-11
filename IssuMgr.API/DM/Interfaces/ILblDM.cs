using IssuMgr.API.Util;
using IssuMgr.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssuMgr.API.DM.Interfaces {
    public interface ILblDM {
        string GetCnxStr();
        Task<IEnumerable<LblModel>> GetAll();
        Task<SnglRslt<LblModel>> Get(int id);
        Task<ExeRslt> Create(LblModel Lbl);
        Task<bool> Exists(int id);
        Task<ExeRslt> Delete(int id);
        Task<ExeRslt> Update(int id, LblModel Lbl);
    }
}
