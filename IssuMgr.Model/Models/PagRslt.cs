using System;
using System.Collections.Generic;

namespace IssuMgr.Model {
    public class PagRslt<T> where T : class {
        public PagRslt() {
        }

        public PagRslt(List<T> lst, int pag, int tam,  int cnt, Exception err) {
            Lst = lst;
            Err = err;
            CalcPag(pag, tam, cnt);
        }

        public PagRslt(List<T> lst, int pag, int tam, int cnt) {
            Lst = lst;
            Err = null;
            CalcPag(pag, tam, cnt);
        }

        public PagRslt(List<T> lst) {
            Lst = lst;
            Err = null;
            CalcPag(0, 0, 0);
        }

        public PagRslt(List<T> lst, Exception err) {
            Lst = lst;
            Err = err;
            CalcPag(0, 0, 0);
        }

        public Exception Err { get; set; }

        public List<T> Lst { get; set; }

        public int PrmPag { get; private set; } = 1;

        public int PrvPag { get; private set; }

        public int SigPag { get; private set; }

        public int TotPag { get; private set; }

        public int UltPag { get; private set; }

        private void CalcPag(int pag, int tam, int cnt) {
            TotPag = (int)Math.Ceiling(decimal.Divide(cnt, tam));
            PrmPag = 1;
            UltPag = TotPag;
            PrvPag = Math.Max(pag - 1, PrmPag);
            SigPag = Math.Min(pag + 1, SigPag);
        }
    }
}
