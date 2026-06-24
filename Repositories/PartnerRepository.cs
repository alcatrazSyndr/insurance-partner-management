using Dapper;
using InsurancePartnerManagement.Models;
using Microsoft.Data.Sqlite;

namespace InsurancePartnerManagement.Repositories
{
    public class PartnerRepository
    {
        private readonly string _connectionString;

        public PartnerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        public IEnumerable<Partner> GetAllPartners()
        {
            using var connection = GetConnection();

            return connection.Query<Partner>("SELECT * FROM Partners ORDER BY CreatedAtUtdc DESC");
        }

        public Partner? GetPartnerById(int id)
        {
            using var connection = GetConnection();

            return connection.QueryFirstOrDefault<Partner>("SELECT * FROM Partners WHERE Id = @Id", new { Id = id });
        }

        public IEnumerable<Policy> GetPoliciesByPartnerId(int partnerId)
        {
            using var connection = GetConnection();

            return connection.Query<Policy>("SELECT * FROM Policies WHERE PartnerId = @PartnerId", new { PartnerId = partnerId });
        }

        public int CreatePartner(Partner partner)
        {
            using var connection = GetConnection();

            var sql = @"
                INSERT INTO PARTNERS
                    (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreatedByUser, IsForeign, ExternalCode, Gender)
                VALUES
                    (@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPIN, @PartnerTypeId, @CreatedByUser, @IsForeign, @ExternalCode, @Gender);
                SELECT last_insert_rowid();";

            return connection.ExecuteScalar<int>(sql, partner);
        }

        public int CreatePolicy(Policy policy)
        {
            using var connection = GetConnection();

            var sql = @"
                INSERT INTO Policies
                    (PartnerId, PolicyNumber, Amount)
                VALUES
                    (@PartnerId, @PolicyNumber, @Amount);
                SELECT last_insert_rowid();";

            return connection.ExecuteScalar<int>(sql, policy);
        }
    }
}
