using IssuMgr.Model;
using System.Threading.Tasks;

namespace IssuMgr.DM.Interfaces {
    public interface ILblDM {
        Task<ExeRslt> Create(LblModel Lbl);

        Task<ExeRslt> Delete(int id);

        Task<bool> Exists(int id);

        Task<SnglRslt<LblModel>> Get(int id);

        Task<LstRslt<LblModel>> GetAll();

        Task<PagRslt<LblModel>> GetPag(int numPag, int tamPag);

        string GetCnxStr();
        Task<ExeRslt> Update(int id, LblModel Lbl);
    }
}
