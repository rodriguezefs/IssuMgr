using System;

namespace IssuMgr.Model {
    public class ExeRslt {

        private Exception _Err;

        public ExeRslt() {
        }

        public ExeRslt(int newId) {
            NewId = newId;
            Err = null;
        }

        public ExeRslt(int newId, Exception err) {
            NewId = newId;
            Err = err;
        }
        public Exception Err {
            get { return _Err; }
            set {
                _Err = value;
                EsVld = _Err == null;
            }
        }
        public bool EsVld { get; set; }
        public int NewId { get; set; }
    }
}