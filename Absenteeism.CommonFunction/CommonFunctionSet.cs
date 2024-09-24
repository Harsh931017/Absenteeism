using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.DirectoryServices;


namespace Absenteeism.CommonFunction
{
    public class CommonFunctionSet
    {
        public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        public static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(computedHash) == storedHash;
            }
        }

        public static bool IsAuthenticated(string UserName, string Password, string ADPath)
        {
            try
            {
                string domainUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string[] paramsLogin = domainUser.Split('\\');
                string domainName = paramsLogin[0].ToString();
                string domainAndUsername = domainName + @"\" + UserName;
                string LDAPPath = ADPath;
                DirectoryEntry entry = new DirectoryEntry(LDAPPath, UserName, Password);
                DirectorySearcher srch = new DirectorySearcher(entry);
                srch.Filter = "(SAMAccountName=" + UserName + ")";
                srch.SearchScope = SearchScope.Subtree;



                // define properties to load
                srch.PropertiesToLoad.Add("objectSid");
                srch.PropertiesToLoad.Add("displayName");
                var userName = "";

                // search the directory
                foreach (SearchResult result in srch.FindAll())
                {
                    // grab the data - if present
                    if (result.Properties["objectSid"] != null && result.Properties["objectSid"].Count > 1)
                    {
                        var sid = result.Properties["objectSid"][0];
                    }
                    if (result.Properties["displayName"] != null && result.Properties["displayName"].Count > 0)
                    {
                        userName = result.Properties["displayName"][0].ToString();
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task ErrorLog(Exception ex, string connectionString,string ClassName,string MethodName)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(connectionString))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();

                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ErrorMessage, ex.Message);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StackTrace, ex.StackTrace);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_MethodName, MethodName);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ClassName, ClassName);
                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_InsertErrorLog, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
