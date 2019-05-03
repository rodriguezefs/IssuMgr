using System;

namespace IssuMgr.Model {
    public class SnglRslt<T> where T : class {

        private Exception _Err;

        public SnglRslt() { }

        public SnglRslt(T sngl, Exception err) {
            Sngl = sngl;
            Err = err;
        }
        public SnglRslt(T sngl) {
            Sngl = sngl;
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
        public T Sngl { get; set; }
    }
}
