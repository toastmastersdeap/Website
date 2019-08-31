using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Toast.Models
{
    public class DBStoredProcedure
    {
        private readonly string _connectionString;

        public DBStoredProcedure()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DevConnectionString"].ConnectionString;
        }

        // TODO:
        public bool GetMemberAccountType()
        {
            return true;
        }

        public void InsertAspUserLogonTime(string id, string ip, string userCountry, string userCity, string device, string javascriptVersion, bool? isMobile)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("spInsertUserLogonTime", con) { CommandType = CommandType.StoredProcedure };

                var paramId = new SqlParameter
                {
                    ParameterName = "@UserID",
                    Value = id
                };
                cmd.Parameters.Add(paramId);

                var paramIP = new SqlParameter
                {
                    ParameterName = "@IP",
                    Value = ip
                };
                cmd.Parameters.Add(paramIP);

                var deviceUse = new SqlParameter
                {
                    ParameterName = "@Device",
                    Value = device
                };
                cmd.Parameters.Add(deviceUse);

                var javscript = new SqlParameter
                {
                    ParameterName = "@JavascriptVersion",
                    Value = !string.IsNullOrEmpty(javascriptVersion) ? javascriptVersion : "Not Found"
                };
                cmd.Parameters.Add(javscript);

                var mobile = new SqlParameter
                {
                    ParameterName = "@IsMobileDevice",
                    Value = isMobile ?? false
                };
                cmd.Parameters.Add(mobile);

                var country = new SqlParameter
                {
                    ParameterName = "@Country",
                    Value = !string.IsNullOrEmpty(userCountry) ? userCountry : "Not Found"
                };
                cmd.Parameters.Add(country);

                var city = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = !string.IsNullOrEmpty(userCity) ? userCity : "Not Found"
                };
                cmd.Parameters.Add(city);

                var dateStamp = new SqlParameter
                {
                    ParameterName = "@LoginDateTime",
                    Value = DateTimeOffset.UtcNow
                };
                cmd.Parameters.Add(dateStamp);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertExceptionLog(string message, string stackTrace, string lineNumber, string userId, string ip = "Not Available", string userCountry = "Not Available", string userCity = "Not Available", string device = "Not Available", string javascriptVersion = "Not Available", bool? isMobile = false)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("spInsertExceptionLog", con) { CommandType = CommandType.StoredProcedure };

                var user = new SqlParameter
                {
                    ParameterName = "@UserID",
                    Value = string.IsNullOrEmpty(userId) ? "" : userId
                };
                cmd.Parameters.Add(user);

                var messagenLog = new SqlParameter
                {
                    ParameterName = "@Message",
                    Value = string.IsNullOrEmpty(message) ? "Not available." : message
                };
                cmd.Parameters.Add(messagenLog);

                var stackTraceLog = new SqlParameter
                {
                    ParameterName = "@StackTrace",
                    Value = string.IsNullOrEmpty(stackTrace) ? "Not available." : stackTrace
                };
                cmd.Parameters.Add(stackTraceLog);

                var lineNum = new SqlParameter
                {
                    ParameterName = "@LineNumber",
                    Value = string.IsNullOrEmpty(lineNumber) ? "Not available." : lineNumber
                };
                cmd.Parameters.Add(lineNum);

                var paramIP = new SqlParameter
                {
                    ParameterName = "@IP",
                    Value = ip
                };
                cmd.Parameters.Add(paramIP);

                var deviceUse = new SqlParameter
                {
                    ParameterName = "@Device",
                    Value = device
                };
                cmd.Parameters.Add(deviceUse);

                var javscript = new SqlParameter
                {
                    ParameterName = "@JavascriptVersion",
                    Value = !string.IsNullOrEmpty(javascriptVersion) ? javascriptVersion : "Not Found"
                };
                cmd.Parameters.Add(javscript);

                var mobile = new SqlParameter
                {
                    ParameterName = "@IsMobileDevice",
                    Value = isMobile ?? false
                };
                cmd.Parameters.Add(mobile);

                var country = new SqlParameter
                {
                    ParameterName = "@Country",
                    Value = !string.IsNullOrEmpty(userCountry) ? userCountry : "Not Found"
                };
                cmd.Parameters.Add(country);

                var city = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = !string.IsNullOrEmpty(userCity) ? userCity : "Not Found"
                };
                cmd.Parameters.Add(city);

                var dateStamp = new SqlParameter
                {
                    ParameterName = "@DateTime",
                    Value = DateTimeOffset.UtcNow
                };
                cmd.Parameters.Add(dateStamp);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}