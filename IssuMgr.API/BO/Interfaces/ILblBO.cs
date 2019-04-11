using IssuMgr.API.Util;
using IssuMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssuMgr.API.BO.Interfaces {
    public interface ILblBO {
        Task<IEnumerable<LblModel>> GetAll();
        Task<SnglRslt<LblModel>> Get(int id);
        Task<ExeRslt> Create(LblModel Lbl);
        Task<bool> Exists(int id);
        Task<ExeRslt> Delete(int id);
        Task<ExeRslt> Update(int id, LblModel Lbl);
    }
}
