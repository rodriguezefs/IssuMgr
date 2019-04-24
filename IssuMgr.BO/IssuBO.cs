using IssuMgr.DM.Interfaces;
using IssuMgr.BO.Interfaces;
using IssuMgr.DM;
using IssuMgr.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace IssuMgr.BO {
    public class IssuBO: IIssuBO {
        private readonly IIssuDM IssuDM;
        public IssuBO(IIssuDM issuDM) {
            IssuDM = issuDM;
        }
        public Task<ExeRslt> Create(IssuModel Issu) {
            return IssuDM.Create(Issu);
        }

        public Task<ExeRslt> Delete(int id) {
            return IssuDM.Delete(id);
        }

        public Task<bool> Exists(int id) {
            return IssuDM.Exists(id);
        }

        public Task<SnglRslt<IssuModel>> Get(int id) {
            return IssuDM.Get(id);
        }

        public Task<LstRslt<IssuModel>> GetAll() {
            return IssuDM.GetAll();
        }

        public Task<LstRslt<LblModel>> GetAllLbl() {
            return IssuDM.GetAllLbl();
        }

        public Task<ExeRslt> Update(int id, IssuModel Issu) {
            return IssuDM.Update(id, Issu);
        }
    }
}
