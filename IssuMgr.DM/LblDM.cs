using IssuMgr.DM.Interfaces;
using IssuMgr.Model;
using IssuMgr.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IssuMgr.DM {
    public class LblDM: ILblDM {
        private readonly IConfiguration Cfg;

        public LblDM(IConfiguration cfg) {
            Cfg = cfg;
        }

        public async Task<ExeRslt> Create(LblModel Lbl) {
            SqlTransaction trns = null;

            string lxQry =
                "INSERT INTO [Lbl] " +
                "(Lbl, BkClr, Clr) " +
                "OUTPUT Inserted.LblId " +
                "VALUES " +
                "(@Lbl, @BkClr, @Clr) ";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    await cnx.OpenAsync();
                    trns = cnx.BeginTransaction();

                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx, trns)) {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Lbl", Lbl.Lbl);
                        cmd.Parameters.AddWithValue("@BkClr", Lbl.BkClr);
                        cmd.Parameters.AddWithValue("@Clr", Lbl.Clr);

                        var id = await cmd.ExecuteScalarAsync();
                        Lbl.LblId = Convert.ToInt32(id);

                        trns.Commit();

                        return new ExeRslt(Lbl.LblId);
                    }
                }
            } catch(Exception ex) {
                try {
                    trns.Rollback();
                } catch(Exception exr) {
                    ex.Data.Add("Rollback", exr.Message);
                }

                ex.Data.Add("Qry", lxQry);
                ex.Data.Add("Method", Ext.GetCaller());
                return new ExeRslt(-1, ex);
            }

        }

        public async Task<ExeRslt> Delete(int id) {
            SqlTransaction trns = null;

            string lxQry = "DELETE [Lbl] WHERE LblId = @LblId";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    await cnx.OpenAsync();
                    trns = cnx.BeginTransaction();

                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx, trns)) {
                        cmd.Parameters.AddWithValue("@LblId", id);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        trns.Commit();
                        return new ExeRslt(rows);
                    }
                }
            } catch(Exception ex) {
                try {
                    trns.Rollback();
                } catch(Exception exr) {
                    ex.Data.Add("Rollback", exr.Message);
                }
                ex.Data.Add("Qry", lxQry);
                ex.Data.Add("Method", Ext.GetCaller());
                return new ExeRslt(-1, ex);
            }
        }

        public async Task<bool> Exists(int id) {
            DataTable lxDT = new DataTable();

            string lxQry =
                "SELECT LblId " +
                "  FROM [Lbl] " +
                " WHERE LblId = @LblId";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.Parameters.AddWithValue("LblId", id);

                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDT);
                        lxDT.TableName = "Lbl";
                    }
                }
                if(lxDT.Rows.Count > 0) {
                    return await Task.FromResult(true);
                } else {
                    return await Task.FromResult(false);
                }
            } catch(Exception) {
                return await Task.FromResult(false);
            }
        }

        public async Task<SnglRslt<LblModel>> Get(int id) {
            DataTable lxDT = new DataTable();
            string lxQry =
                "SELECT LblId, Lbl, BkClr, Clr " +
                "  FROM [Lbl]" +
                " WHERE LblId = @LblId";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.Parameters.AddWithValue("LblId", id);

                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDT);
                        lxDT.TableName = "Lbl";
                    }
                }
                if(lxDT.Rows.Count > 0) {
                    LblModel lxLbl = lxDT.Rows[0].ToRow<LblModel>();
                    var lxRslt = new SnglRslt<LblModel>(lxLbl);
                    return await Task.FromResult(lxRslt);
                } else {
                    return new SnglRslt<LblModel>(new LblModel());
                }
            } catch(Exception ex) {
                return new SnglRslt<LblModel>(new LblModel(), ex);
            }
        }

        public async Task<LstRslt<LblModel>> GetAll() {
            DataTable lxDT = new DataTable();

            string lxQry =
                "SELECT LblId, Lbl, BkClr, Clr " +
                "  FROM [Lbl]";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDT);
                        lxDT.TableName = "Lbl";
                    }
                }

                if(lxDT.Rows.Count > 0) {
                    List<LblModel> lxLbls = lxDT.ToList<LblModel>();
                    var lxRslt = new LstRslt<LblModel>(lxLbls);
                    return await Task.FromResult(lxRslt);
                } else {
                    return new LstRslt<LblModel>(new List<LblModel>());
                }
            } catch(Exception ex) {
                return new LstRslt<LblModel>(new List<LblModel>(), ex);
            }
        }

        public async Task<PagRslt<LblModel>> GetPag(int pag, int tam) {
            DataTable lxDT = new DataTable();

            string lxQry =
                "SELECT LblId, Lbl, BkClr, Clr " +
                "  FROM [Lbl]";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDT);
                        lxDT.TableName = "Lbl";
                    }
                }

                if(lxDT.Rows.Count > 0) {
                    var lxLbls = lxDT.ToList<LblModel>().AsQueryable();
                    int lxSkp = (pag - 1) * tam;
                    int lxTke = tam;
                    int cnt = lxDT.Rows.Count;

                    var lxPagLst = lxLbls.Skip(lxSkp).Take(lxTke).ToList();
                    var lxPagRslt = new PagRslt<LblModel>(lxPagLst, pag, tam, cnt);
                    return await Task.FromResult(lxPagRslt);
                } else {
                    return new PagRslt<LblModel>(new List<LblModel>());
                }
            } catch(Exception ex) {
                return new PagRslt<LblModel>(new List<LblModel>(), ex);
            }
        }

        public string GetCnxStr() {
            string lxCnxStr = Cfg.GetConnectionString("DefaultConnection");
            return lxCnxStr;
        }

        public async Task<ExeRslt> Update(int id, LblModel Lbl) {
            SqlTransaction trns = null;
            string lxQry = "UPDATE [Lbl] " +
                           "   SET " +
                           "       Lbl = @Lbl," +
                           "       BkClr = @BkClr, " +
                           "       Clr = @Clr " +
                           " WHERE LblId = @LblId";
            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    await cnx.OpenAsync();
                    trns = cnx.BeginTransaction();

                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx, trns)) {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Lbl", Lbl.Lbl);
                        cmd.Parameters.AddWithValue("@BkClr", Lbl.BkClr);
                        cmd.Parameters.AddWithValue("@Clr", Lbl.Clr);

                        cmd.Parameters.AddWithValue("@LblId", id);

                        await cmd.ExecuteNonQueryAsync();
                        trns.Commit();
                        return new ExeRslt(Lbl.LblId);
                    }
                }
            } catch(Exception ex) {
                try {
                    trns.Rollback();
                } catch(Exception exr) {
                    ex.Data.Add("Rollback", exr.Message);
                }
                ex.Data.Add("Qry", lxQry);
                ex.Data.Add("Method", Ext.GetCaller());
                return new ExeRslt(-1, ex);
            }
        }
    }
}
