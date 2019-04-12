using System;
using System.Collections.Generic;

namespace IssuMgr.API.Util {
    public class LstRslt<T> where T : class {
        public LstRslt() { }

        public LstRslt(List<T> lst, Exception err) {
            Lst = lst;
            Err = err;
        }

        public LstRslt(List<T> lst) {
            Lst = lst;
            Err = null;
        }
        public List<T> Lst { get; set; }
        public Exception Err { get; set; }
    }
}
