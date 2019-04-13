using System;

namespace IssuMgr.Model {
    public class ExeRslt {
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
        public int NewId { get; set; }
        public Exception Err { get; set; }
    }
}