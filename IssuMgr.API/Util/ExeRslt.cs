using System;

namespace IssuMgr.API.Util {
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