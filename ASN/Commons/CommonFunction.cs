using ASN.IRepository;
using System.Data;
using Microsoft.AspNetCore.Http;
using ASN.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace ASN.Commons
{
    public class CommonFunction : ICommonFunction
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MySQL> _logger;
        private readonly IMySQL _mySQL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommonFunction(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _logger = logger;
            _mySQL = new MySQL(_configuration, _logger);
            _httpContextAccessor = httpContextAccessor;

           
        }
         public static class AppConfigCommon
        {
            public static string configAWSS3Bucket { get; } //= Convert.ToString(_configuration["AWSS3Bucket"]);
            public static string configAWSProfileName { get; } //= Convert.ToString(ConfigurationManager["AWSProfileName"]);
            public static string configAWSAccessKey { get; } //= Convert.ToString(ConfigurationManager.AppSettings["AWSAccessKey"]);
            public static string configAWSSecretKey { get; } //= Convert.ToString(ConfigurationManager.AppSettings["AWSSecretKey"]);
            public static string configAssetsDomain { get; } //= Convert.ToString(ConfigurationManager.AppSettings["ConfigAssetsDomain"]);

        }
        
        public List<object> DataTableToSerializer(DataTable table)
        {
            var parentRow = new List<object>();
            foreach (DataRow row in table.Rows)
            {
                var childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return parentRow;
        }

        public int? GetUserIdFromSession()
        {
            try
            {
               
                if (_httpContextAccessor.HttpContext != null)
                {
                    ISecureDatails secureDatails = new SecureDatails();
                    string encryptedUserAuth = SessionExtensions.GetString(_httpContextAccessor.HttpContext.Session, "UserSession");

                   
                    if (!string.IsNullOrEmpty(encryptedUserAuth))
                    {
                       
                        string decryptedUserAuth = secureDatails.Decrypt(encryptedUserAuth);

                        
                        UserAuth userAuth = System.Text.Json.JsonSerializer.Deserialize<UserAuth>(decryptedUserAuth);

                       
                        return userAuth?.UserID;
                    }
                }
            }
            catch (Exception ex)
            {
              
                _logger.LogError("Error retrieving UserID from session: {Message}", ex.Message);
                throw;
            }

           
            return null;
        }


        #region AWS
        public static bool UploadFileToAWSS3(System.IO.Stream localFilePath, string subDirectoryInBucket, string fileNameInS3, string prv_fileNameInS3)
        {
            try
            {
                string bucketName = Convert.ToString(AppConfigCommon.configAWSS3Bucket);
                Amazon.S3.IAmazonS3 client = new Amazon.S3.AmazonS3Client(Amazon.RegionEndpoint.APSoutheast1);
                Amazon.S3.Transfer.TransferUtility utility = new Amazon.S3.Transfer.TransferUtility(client);
                Amazon.S3.Transfer.TransferUtilityUploadRequest request = new Amazon.S3.Transfer.TransferUtilityUploadRequest();

                if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
                {
                    request.BucketName = bucketName; //no subdirectory just bucket name  
                }
                else
                {
                    request.BucketName = bucketName + @"/" + subDirectoryInBucket.Trim('/');
                }
                request.Key = fileNameInS3; //file name up in S3  
                request.InputStream = localFilePath;
                utility.Upload(request);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion AWS
    }
}
