using IssuMgr.Model.Models;
using IssuMgr.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssuMgr.DM.Identity {
    public class RoleStore : IRoleStore<ApplicationRole> {
        private readonly string _connectionString;

        public RoleStore(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();

            using(var cnx = new SqlConnection(_connectionString)) {
                await cnx.OpenAsync(cancellationToken);

                string lxQry =
                    $@"INSERT INTO [ApplicationRole] ([Name], [NormalizedName])
                       VALUES (@{nameof(ApplicationRole.Name)}, 
                               @{nameof(ApplicationRole.NormalizedName)});
                       SELECT CAST(SCOPE_IDENTITY() as int)";

                using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue($"@{nameof(ApplicationRole.Name)}", role.Name);
                    cmd.Parameters.AddWithValue($"@{nameof(ApplicationRole.NormalizedName)}", role.NormalizedName);

                    var id = await cmd.ExecuteScalarAsync();
                    role.Id = Convert.ToInt32(id);
                }
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();

            using(var cnx = new SqlConnection(_connectionString)) {
                await cnx.OpenAsync(cancellationToken);

                string lxQry =
                    $@"UPDATE [ApplicationRole] 
                          SET
                           [Name] = @{nameof(ApplicationRole.Name)},
                           [NormalizedName] = @{nameof(ApplicationRole.NormalizedName)}
                        WHERE [Id] = @{nameof(ApplicationRole.Id)}";

                using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue($"@{nameof(ApplicationRole.Name)}", role.Name);
                    cmd.Parameters.AddWithValue($"@{nameof(ApplicationRole.NormalizedName)}", role.NormalizedName);
                    cmd.Parameters.AddWithValue($"@{nameof(ApplicationRole.Id)}", role.Id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();

            using(var cnx = new SqlConnection(_connectionString)) {
                await cnx.OpenAsync(cancellationToken);

                string lxQry = $"DELETE FROM [ApplicationRole] WHERE [Id] = @{nameof(ApplicationRole.Id)}";

                using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue($"@{nameof(ApplicationRole.Id)}", role.Id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken) {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken) {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken) {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken) {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken) {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();
            DataTable lxDT = new DataTable();

            using(var cnx = new SqlConnection(_connectionString)) {
                string lxQry =
                    $@"SELECT * FROM [ApplicationRole] WHERE [Id] = @{nameof(roleId)}";

                await cnx.OpenAsync(cancellationToken);

                using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue($"@{nameof(roleId)}", roleId);

                    SqlDataAdapter lxDA = new SqlDataAdapter  (cmd);
                    lxDA.Fill(lxDT);

                }
                var lxAppUsr = lxDT.Rows[0].ToRow<ApplicationRole>();
                return lxAppUsr;
            }
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();
            DataTable lxDT = new DataTable();

            using(var cnx = new SqlConnection(_connectionString)) {
                string lxQry =
                    $@"SELECT * FROM [ApplicationRole] WHERE [NormalizedName] = @{nameof(normalizedRoleName)}";

                await cnx.OpenAsync(cancellationToken);
                using(SqlCommand cmd = new SqlCommand(lxQry, cnx)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue($"@{nameof(normalizedRoleName)}", normalizedRoleName);

                    SqlDataAdapter lxDA = new SqlDataAdapter  (cmd);
                    lxDA.Fill(lxDT);
                }
                var lxAppUsr = lxDT.Rows[0].ToRow<ApplicationRole>();
                return lxAppUsr;
            }
        }

        public void Dispose() {
            // Nothing to dispose.
        }
    }
}
