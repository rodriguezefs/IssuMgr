using IssuMgr.API.DM.Interfaces;
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
            string lxQry =
                "INSERT INTO [Issu] " +
                "(Tit, Txt, St, StmCre, StMdf) " +
                "OUTPUT Inserted.IssuId " +
                "VALUES " +
                "(@Tit, @Txt, @St, GetDates(), GetDate()) ";
            //TODO Procesar los labels asignados
            //TODO Manejo de transacciones
            try {
                using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                    using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Tit", Issu.Tit);
                        cmd.Parameters.AddWithValue("@Txt", Issu.Txt);
                        cmd.Parameters.AddWithValue("@St", Issu.St);

                        await cnx.OpenAsync();
                        var id = await cmd.ExecuteScalarAsync();
                        Issu.IssuId = Convert.ToInt32(id);

                        return new ExeRslt(Issu.IssuId);
                    }
                }
            } catch(Exception ex) {
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
            string lxQry =
                "SELECT Id, Cod_IssuModel, Nom_IssuModel " +
                "  FROM [Issu]" +
                " WHERE Id = @id";

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
                    IssuModel lbl = lxDT.Rows[0].ToRow<IssuModel>();
                    return await Task.FromResult(new SnglRslt<IssuModel>(lbl));
                } else {
                    return null;
                }
            } catch(Exception ex) {
                return new SnglRslt<IssuModel>(new IssuModel(), ex);
            }
        }

        public async Task<IEnumerable<IssuModel>> GetAll() {
            DataTable lxDT = new DataTable();
            string lxQry =
                "SELECT IssuId, Tit, Txt, St, StmCre, StMdf " +
                "  FROM [Issu]";
            //TODO Subir los Labels Asignados
            using(SqlConnection cnx = new SqlConnection(GetCnxStr())) {
                using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                    SqlDataAdapter lxDA = new SqlDataAdapter(cmd);
                    lxDA.Fill(lxDT);
                    lxDT.TableName = "Issu";
                }
            }
            var Issus = lxDT.ToList<IssuModel>();
            return await Task.FromResult(Issus);
            //TODO: Manejo de error Issu GetAll
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
    }
}
