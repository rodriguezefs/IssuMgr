﻿using IssuMgr.API.BO.Interfaces;
using IssuMgr.API.DM.Interfaces;
using IssuMgr.API.Util;
using IssuMgr.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssuMgr.API.BO {
    public class LblBO: ILblBO {
        private readonly ILblDM LblDM;
        public LblBO(ILblDM lblDM) {
            LblDM = lblDM;
        }
        public Task<ExeRslt> Create(LblModel Lbl) {
            return LblDM.Create(Lbl);
        }

        public Task<ExeRslt> Delete(int id) {
            return LblDM.Delete(id);
        }

        public Task<bool> Exists(int id) {
            return LblDM.Exists(id);
        }

        public Task<SnglRslt<LblModel>> Get(int id) {
            return LblDM.Get(id);
        }

        public Task<LstRslt<LblModel>> GetAll() {
            return LblDM.GetAll();
        }

        public Task<ExeRslt> Update(int id, LblModel Lbl) {
            return LblDM.Update(id, Lbl);
        }
    }
}
