using System;
using System.CodeDom;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ContactsDataAccessLayer
{
    public class clsCountryData
    {
        public static bool FindCountryByID(int ID , ref string CountryName , ref string Code , ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Countries where CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CountryName = (string)reader["CountryName"];

                    if (reader["Code"] != DBNull.Value)
                    {
                        Code = (string)reader["Code"];
                    }
                    else
                    {
                        Code = "";
                    }

                    if (reader["PhoneCode"] != DBNull.Value)
                    {
                        PhoneCode = (string)reader["PhoneCode"];
                    }
                    else
                    {
                        PhoneCode = "";
                    }
                }
                else
                    isFound = false;


                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                isFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return isFound;

        }

        public static bool FindCountryByName( ref int CountryID, string CountryName , ref string Code , ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Countries where CountryName = @CountryName";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CountryID = (int)reader["CountryID"];
                    Code = (string)reader["Code"];
                    PhoneCode = (string)reader["PhoneCode"];

                }
                else
                    isFound = false;

                reader.Close();
            }
            catch(Exception ex)
            {
                isFound = false;
                Console.WriteLine("Error : " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return isFound;

        }

        public static bool IsCountryExistByID(int ID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select Found = 1 from Countries where CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                isFound = Reader.HasRows;

                Reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                isFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return isFound;
        }

        public static bool IsCountryExistsByName(string CountryName)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select found = 1 from Countries where CountryName = @CountryName";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@CountryName" , CountryName);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                isFound = Reader.HasRows;

                Reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error : ", ex.Message);
                isFound = false;
            }
            finally
            {
                Connection.Close();

            }

            return isFound;

        }

        public static int AddNewCountry(string CountryName , string Code , string PhoneCode)
        {
            int CountryID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into Countries(CountryName,Code,PhoneCode) 
                                          values (@CountryName,@Code,@PhoneCode);
                             select scope_identity();";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            if (Code != "")
                command.Parameters.AddWithValue("@Code", Code);
            else
                command.Parameters.AddWithValue("@Code", System.DBNull.Value);

            if (PhoneCode != "")
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            else
                command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                    CountryID = InsertedID;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : ", ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return CountryID;
        }

        public static bool UpdateCountry(int ID , string CountryName , string Code , string PhoneCode)
        {
            int rowAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update Countries set CountryName = @CountryName 
                                                 , Code = @Code 
                                                 , PhoneCode = @PhoneCode
                                               where CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@CountryID", ID);
            Command.Parameters.AddWithValue("@CountryName" , CountryName);
            Command.Parameters.AddWithValue("@Code", Code);
            Command.Parameters.AddWithValue("@PhoneCode", PhoneCode);

            try
            {
                Connection.Open();

                rowAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return (rowAffected > 0);
        }

        public static bool DeleteCountry(int ID)
        {
            int rowAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete Countries where CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                Connection.Open();

                rowAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return (rowAffected > 0);
        }

        public static DataTable ListCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Countries";

            SqlCommand Command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return null;
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }

    }
}
