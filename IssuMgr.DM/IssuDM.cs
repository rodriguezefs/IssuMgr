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
    public class IssuDM: IIssuDM {
        private readonly IConfiguration Cfg;
        public IssuDM(IConfiguration cfg) {
            Cfg = cfg;
        }
        public async Task<ExeRslt> Create(IssuModel Issu) {
            SqlTransaction trns = null;

            string lxQry =
                "INSERT INTO [Issu] " +
                "(Tit, Txt, St, StmCre, StmMdf) " +
                "OUTPUT Inserted.IssuId " +
                "VALUES " +
                "(@Tit, @Txt, @St, GetDate(), GetDate()) ";

            string lxQryLbl =
                "INSERT INTO [LblxIssu] " +
                "(IssuId, LblId) " +
                "VALUES " +
                "(@IssuId, @LblId)";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    await cnx.OpenAsync();
                    trns = cnx.BeginTransaction();

                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx, trns)) {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Tit", Issu.Tit);
                        cmd.Parameters.AddWithValue("@Txt", Issu.Txt);
                        cmd.Parameters.AddWithValue("@St", Issu.St);

                        var id = await cmd.ExecuteScalarAsync();
                        Issu.IssuId = Convert.ToInt32(id);

                        foreach(var lxLbl in Issu.LstLbl) {
                            using(SqlCommand cmdL = new SqlCommand(lxQryLbl, cnx, trns)) {
                                cmdL.CommandType = CommandType.Text;
                                cmdL.Parameters.AddWithValue("@IssuId", Issu.IssuId);
                                cmdL.Parameters.AddWithValue("@LblId",  lxLbl.LblId);

                                await cmdL.ExecuteScalarAsync();
                            }
                        }

                        trns.Commit();

                        return new ExeRslt(Issu.IssuId);
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
            string lxQry = "DELETE [Issu] WHERE IssuId = @Id";
            //TODO Borrar Labels asignados
            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.Parameters.AddWithValue("@IsuuId", id);
                        cnx.Open();
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return new ExeRslt(rows);
                    }
                }
            } catch(Exception ex) {
                ex.Data.Add("Qry", lxQry);
                ex.Data.Add("Method", Ext.GetCaller());
                return new ExeRslt(-1, ex);
            }
        }
        public async Task<bool> Exists(int id) {
            DataTable lxDT = new DataTable();
            string lxQry =
                "SELECT Id " +
                "  FROM [Issu]" +
                " WHERE IssuId = @id";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.Parameters.AddWithValue("Id", id);

                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDT);
                        lxDT.TableName = "Issu";
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
        public async Task<SnglRslt<IssuModel>> Get(int id) {
            DataTable lxDT = new DataTable();
            DataTable lxDTL = new DataTable();

            string lxQry =
                "SELECT Id, Cod_IssuModel, Nom_IssuModel " +
                "  FROM [Issu]" +
                " WHERE Id = @id";

            string lxQryLxI =
              "SELECT LxI.IssuId, LxI.LblId, L.Lbl, L.BkClr, L.Clr " +
              " FROM [LblxIssu] LxI " +
              " LEFT JOIN [Lbl] L " +
              "   ON L.LblId = LxI.LblId " +
              "WHERE LxI.IssuId = @IssuId";
            
            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.Parameters.AddWithValue("Id", id);

                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDT);
                        lxDT.TableName = "Issu";
                    }

                    using(SqlCommand cmd = new SqlCommand(lxQryLxI, cnx)) {
                        cmd.Parameters.AddWithValue("@IssuId", id);
                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDTL);
                        lxDT.TableName = "LblxIssu";
                    }

                }
                if(lxDT.Rows.Count > 0) {
                    IssuModel lxIssu = lxDT.Rows[0].ToRow<IssuModel>();
                    FillLbl(ref lxIssu, lxDTL);

                    return await Task.FromResult(new SnglRslt<IssuModel>(lxIssu));
                } else {
                    return null;
                }
            } catch(Exception ex) {
                return new SnglRslt<IssuModel>(new IssuModel(), ex);
            }
        }
        public async Task<LstRslt<IssuModel>> GetAll() {
            DataSet lxDS = new DataSet();

            string lxQryI =
                "SELECT IssuId, Tit, Txt, St, StmCre, StmMdf " +
                "  FROM [Issu] I" +
                " ORDER BY StmMdf DESC";

            string lxQryLxI =
                "SELECT LxI.IssuId, LxI.LblId, L.Lbl, L.BkClr, L.Clr " +
                " FROM [LblxIssu] LxI " +
                " LEFT JOIN [Lbl] L " +
                "   ON L.LblId = LxI.LblId";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQryI, cnx)) {
                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDS, "Issu");
                    }
                    using(SqlCommand cmd = new SqlCommand(lxQryLxI, cnx)) {
                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDS, "LblxIssu");
                    }

                    DataColumn lxColIssuIdM = lxDS.Tables["Issu"].Columns["IssuId"];
                    DataColumn lxColIssuIdD = lxDS.Tables["LblxIssu"].Columns["IssuId"];
                    DataRelation lxI_LxI = new DataRelation("Issu_LblxIssu", lxColIssuIdM, lxColIssuIdD);
                    lxDS.Relations.Add(lxI_LxI);

                    //DataColumn lxColLblIdM = lxDS.Tables["Lbl"].Columns["LblId"];
                    //DataColumn lxColLblIdD = lxDS.Tables["LblxIssu"].Columns["LblId"];
                    //DataRelation lxL_LxI = new DataRelation("Lbl_LblxIssu", lxColLblIdM, lxColLblIdD);
                    //lxDS.Relations.Add(lxL_LxI);
                }
                var lxRslt = new LstRslt<IssuModel>();
                if(lxDS.Tables["Issu"].Rows.Count > 0) {
                    List<IssuModel> lxIssus = lxDS.Tables["Issu"].ToList<IssuModel>();
                    FillLbls(ref lxIssus, lxDS);

                    lxRslt = new LstRslt<IssuModel>(lxIssus);
                }
                return await Task.FromResult(lxRslt);
            } catch(Exception ex) {
                return new LstRslt<IssuModel>(new List<IssuModel>(), ex);
            }
        }
        public async Task<LstRslt<LblModel>> GetAllLbl() {
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
        public string GetCnxStr() {
            string lxCnxStr = Cfg.GetConnectionString("DefaultConnection");
            return lxCnxStr;
        }
        public async Task<ExeRslt> Update(int id, IssuModel Issu) {
            string lxQry = "UPDATE [Issu] " +
                           "   SET " +
                           "       Tit = @Tit," +
                           "       Txt = @Txt," +
                           "       St = @St," +
                           "       StmMdf = @StmMdf " +
                           " WHERE IssuId = @id";
            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Tit", Issu.Tit);
                        cmd.Parameters.AddWithValue("@Txt", Issu.Txt);
                        cmd.Parameters.AddWithValue("@St", Issu.St);
                        cmd.Parameters.AddWithValue("@StmMdf", Issu.StmMdf);

                        cmd.Parameters.AddWithValue("@IssuId", id);

                        await cnx.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        return new ExeRslt(Issu.IssuId);
                    }
                }
            } catch(Exception ex) {
                ex.Data.Add("Qry", lxQry);
                ex.Data.Add("Method", Ext.GetCaller());
                return new ExeRslt(-1, ex);
            }
        }
        private void FillLbls(ref List<IssuModel> Issus, DataSet DS) {

            foreach(var lxIssu in Issus) {
                string lxFltStr = $"[IssuId] = {lxIssu.IssuId}";
                DataRow[] lxRows = DS.Tables["LblxIssu"].Select(lxFltStr);
                lxIssu.LstLbl = new List<LblModel>();
                foreach(var lxRow in lxRows) {
                    var lxLbl = lxRow.ToRow<LblModel>();
                    lxIssu.LstLbl.Add(lxLbl);
                }
            }
        }

        private void FillLbl(ref IssuModel Issu, DataTable DT) {
            DataRow[] lxRows = DT.Select();
            Issu.LstLbl = new List<LblModel>();
            foreach(var lxRow in lxRows) {
                var lxLbl = lxRow.ToRow<LblModel>();
                Issu.LstLbl.Add(lxLbl);
            }
        }
    }
}
