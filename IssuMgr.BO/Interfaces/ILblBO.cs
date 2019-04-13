using IssuMgr.Model;
using System.Threading.Tasks;

namespace IssuMgr.BO.Interfaces {
    public interface ILblBO {
        Task<LstRslt<LblModel>> GetAll();
        Task<SnglRslt<LblModel>> Get(int id);
        Task<ExeRslt> Create(LblModel Lbl);
        Task<bool> Exists(int id);
        Task<ExeRslt> Delete(int id);
        Task<ExeRslt> Update(int id, LblModel Lbl);
    }
}
