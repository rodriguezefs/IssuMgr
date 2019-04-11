using IssuMgr.API.DM.Interfaces;
using IssuMgr.API.Util;
using IssuMgr.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IssuMgr.API.DM {
    public class LblDM: ILblDM {
        private readonly IConfiguration Cfg;
        public LblDM(IConfiguration cfg) {
            Cfg = cfg;
        }
        public async Task<ExeRslt> Create(LblModel Lbl) {
            string lxQry =
                "INSERT INTO [Lbl] " +
                "(Lbl, Clr) " +
                "OUTPUT Inserted.LblId " +
                "VALUES " +
                "(@Lbl, @Clr) ";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Lbl", Lbl.Lbl);
                        cmd.Parameters.AddWithValue("@Clr", Lbl.Clr);

                        await cnx.OpenAsync();
                        var id = await cmd.ExecuteScalarAsync();
                        Lbl.LblId = Convert.ToInt32(id);

                        return new ExeRslt(Lbl.LblId);
                    }
                }
            } catch(Exception ex) {
                ex.Data.Add("Qry", lxQry);
                ex.Data.Add("Method", Ext.GetCaller());
                return new ExeRslt(-1, ex);
            }

        }

        public async Task<ExeRslt> Delete(int id) {
            string lxQry = "DELETE [Lbl] WHERE Id = @Id";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.Parameters.AddWithValue("@Id", id);
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
                "  FROM [LblModel]" +
                " WHERE Id = @id";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.Parameters.AddWithValue("Id", id);

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
                "SELECT Id, Cod_LblModel, Nom_LblModel " +
                "  FROM [LblModel]" +
                " WHERE Id = @id";

            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.Parameters.AddWithValue("Id", id);

                        SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                        lxDA.Fill(lxDT);
                        lxDT.TableName = "Lbl";
                    }
                }
                if(lxDT.Rows.Count > 0) {
                    LblModel lbl = lxDT.Rows[0].ToRow<LblModel>();
                    return await Task.FromResult(new SnglRslt<LblModel>(lbl));
                } else {
                    return null;
                }
            } catch(Exception ex) {
                return new SnglRslt<LblModel>(new LblModel(), ex);
            }
        }

        public async Task<IEnumerable<LblModel>> GetAll() {
            DataTable lxDT = new DataTable();
            string lxQry =
                "SELECT LblId, Lbl, Clr " +
                "  FROM [Lbl]";
            using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                    SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                    lxDA.Fill(lxDT);
                    lxDT.TableName = "Lbl";
                }
            }
            var Lbls = lxDT.ToList<LblModel>();
            return await Task.FromResult(Lbls);
            //TODO: Manejo de error
        }

        public string GetCnxStr() {
            string lxCnxStr = Cfg.GetConnectionString("DefaultConnection");
            return lxCnxStr;
        }

        public async Task<ExeRslt> Update(int id, LblModel Lbl) {
            string lxQry = "UPDATE [Lbl] " +
                           "   SET " +
                           "       Lbl = @Lbl," +
                           "       Clr = @Clr " +
                           " WHERE LblId = @id";
            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Lbl", Lbl.Lbl);
                        cmd.Parameters.AddWithValue("@Clr", Lbl.Clr);

                        cmd.Parameters.AddWithValue("@Id", id);

                        await cnx.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        return new ExeRslt(Lbl.LblId);
                    }
                }
            } catch(Exception ex) {
                ex.Data.Add("Qry", lxQry);
                ex.Data.Add("Method", Ext.GetCaller());
                return new ExeRslt(-1, ex);
            }
        }
    }
}
