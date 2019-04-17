using IssuMgr.API.DM.Interfaces;
using IssuMgr.BO.Interfaces;
using IssuMgr.Model;
using System.Threading.Tasks;

namespace IssuMgr.BO {
    public class IssuBO: IIssuBO {
        private readonly IIssuDM IssuDM;
        public IssuBO(IIssuDM lblDM) {
            IssuDM = lblDM;
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

        public Task<ExeRslt> Update(int id, IssuModel Issu) {
            return IssuDM.Update(id, Issu);
        }
    }
}
