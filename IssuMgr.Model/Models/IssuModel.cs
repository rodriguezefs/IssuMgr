using System;

namespace IssuMgr.Model {
    public enum IssueSt {
        Abr,
        Crr
    }
    public class IssuModel {
        public int IssuId { get; set; }
        public string Tit { get; set; }
        public string Txt { get; set; }
        public IssueSt St { get; set; }
        public DateTime StmCre { get; set; }
        public DateTime StMdf { get; set; }
    }
}
