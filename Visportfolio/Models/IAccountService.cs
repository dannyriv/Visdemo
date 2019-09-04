using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Visportfolio.Models
{
    public interface IAccountService
    {
        int ValidateUserLogin(string UserName, string Password);
        string GetRandomPassword(int iLength);
        string ValidateUserCode(string ConfirmationCode);
        int UpdateUserValidFlag(string email);
    }
    public class AccountService : IAccountService
    {
        string connectionString = "Data Source=DANNY\\SQLEXPRESS; UID=sa; Password=Chingo123; Database=Vispires;";

        public string ValidateUserCode(string ConfirmationCode)
        {
            string email;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ValidateUserCode", con))
                {
                    //creates paramater with direction as output
                    SqlParameter ReturnEmail = new SqlParameter("@ReturnEmail", SqlDbType.VarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(("@ConfirmationCode"), SqlDbType.VarChar).Value = ConfirmationCode;
                    cmd.Parameters.Add(ReturnEmail);


                    con.Open();
                    cmd.ExecuteNonQuery();

                    email = (string)ReturnEmail.Value;

                    con.Close();
                }
            }
            return email;
        }

        public int UpdateUserValidFlag(string email)
        {
            int Valid = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUserValidFlag", con))
                {
                    SqlParameter IsValid = new SqlParameter("@IsValid", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(("@Email"), SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add(IsValid);


                    con.Open();
                    cmd.ExecuteNonQuery();

                    Valid = IsValid.Value as int? ?? default(int);

                    con.Close();
                }
            }
            return Valid;
        }

        public int ValidateUserLogin(string UserName, string Password)
        {
            int valid = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ValidateUserLogin", con))
                {
                    //creates paramater with direction as output
                    SqlParameter IsValid = new SqlParameter("@IsValid", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(("@UserName"), SqlDbType.VarChar).Value = UserName;
                    cmd.Parameters.Add(("@Password"), SqlDbType.VarChar).Value = Password;
                    cmd.Parameters.Add(IsValid);


                    con.Open();
                    cmd.ExecuteNonQuery();

                    valid = IsValid.Value as int? ?? default(int);

                    con.Close();
                }
            }
            return valid;
        }

        public string GetRandomPassword(int iLength)
        {
            if (iLength < 1) iLength = 1;
            if (iLength > 40) iLength = 40;
            String sSeed = Guid.NewGuid().ToString().Replace("-", "");
            return sSeed.Substring(0, iLength);
        }
    }
}
