using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssuMgr.API.Util {
    public class SnglRslt<T> where T : class {
        public SnglRslt() {

        }
        public SnglRslt(T sngl, Exception err) {
            Sngl = sngl;
            Err = err;
        }
        public SnglRslt(T sngl) {
            Sngl = sngl;
            Err = null;
        }
        public T Sngl { get; set; }
        public Exception Err { get; set; }
    }
}
