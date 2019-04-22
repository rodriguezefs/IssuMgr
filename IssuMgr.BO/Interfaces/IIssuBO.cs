using IssuMgr.Model;
using System.Threading.Tasks;

namespace IssuMgr.BO.Interfaces {
    public interface IIssuBO {
        Task<LstRslt<IssuModel>> GetAll();
        Task<LstRslt<LblModel>> GetAllLbl();
        Task<SnglRslt<IssuModel>> Get(int id);
        Task<ExeRslt> Create(IssuModel Issu);
        Task<bool> Exists(int id);
        Task<ExeRslt> Delete(int id);
        Task<ExeRslt> Update(int id, IssuModel Issu);
    }
}
