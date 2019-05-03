using System;
using System.Collections.Generic;

namespace IssuMgr.Model {
    public class LstRslt<T> where T : class {
        private Exception _Err;

        public LstRslt() { }
        public LstRslt(List<T> lst, Exception err) {
            Lst = lst;
            Err = err;
        }
        public LstRslt(List<T> lst) {
            Lst = lst;
            Err = null;
        }
        public Exception Err {
            get { return _Err; }
            set {
                _Err = value;
                EsVld = _Err == null;
            }
        }

        public bool EsVld { get; set; }
        public List<T> Lst { get; set; }
    }
}
