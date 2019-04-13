using IssuMgr.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssuMgr.API.DM.Interfaces {
    public interface IIssuDM {
        Task<ExeRslt> Create(IssuModel Issu);
        Task<ExeRslt> Delete(int id);
        Task<bool> Exists(int id);
        Task<SnglRslt<IssuModel>> Get(int id);
        Task<IEnumerable<IssuModel>> GetAll();
        string GetCnxStr();
        Task<ExeRslt> Update(int id, IssuModel Issu);
    }
}