using System;

namespace IssuMgr.Model {
    public class SnglRslt<T> where T : class {

        public SnglRslt() { }

        public SnglRslt(T sngl, Exception err) {
            Sngl = sngl;
            Err = err;
        }
        public SnglRslt(T sngl) {
            Sngl = sngl;
            Err = null;
        }
        public bool EsVld { get; set; }

        private Exception _Err;

        public Exception Err {
            get { return _Err; }
            set {
                _Err = value;
                EsVld = _Err == null;
            }
        }


        public T Sngl { get; set; }
    }
}
