using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;

namespace GCloudStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            //ListBuckets();//working
            //CreateBucket();//working
            UploadFiles();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        public static void CreateBucket()
        {
            string projectid = "applied-talon-290111";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Users\ravic\OneDrive\Documents\gcloud svc account\applied-talon-290111-44a1e9c3ad40.json");

            StorageClient storageClient = StorageClient.Create();
            string bucketName = "nrctnkill-testbkt";
            try
            {
                storageClient.CreateBucket(projectid, bucketName);
            }
            catch (Exception e)//Google.GoogleApiException
            //when (e.Error.Code == 409)
            {
                // The bucket already exists.
                Console.WriteLine(e.ToString());
            }
        }

        public static void ListBuckets()
        {
            string projectid = "applied-talon-290111";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Users\ravic\OneDrive\Documents\gcloud svc account\applied-talon-290111-44a1e9c3ad40.json");

            var storage = StorageClient.Create();
            var buckets = storage.ListBuckets(projectid);
            Console.WriteLine("Buckets:");
            foreach (var bucket in buckets)
            {
                Console.WriteLine(bucket.Name);
            }
            //return buckets;
        }

        public static void UploadFiles()
        {
            string projectid = "applied-talon-290111";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Users\ravic\OneDrive\Documents\gcloud svc account\applied-talon-290111-44a1e9c3ad40.json");

            string bucketName = "nrctnkill-testbkt";
            int count = 0;
            string directory = "C:\\upload";
            var files = Directory.GetFiles(directory);
            List<string> lst = new List<string>();
            foreach (var fileName in files)
            {
                //lst.Add(fileName.Substring(fileName.LastIndexOf("\\") + 1));
                StorageClient storage = StorageClient.Create();
                UploadFile(bucketName, fileName, ref storage);
            }
        }

        public static void UploadFile(string bucketName, string localPath, ref StorageClient storage, string objectName = null)
        {
            
            try
            {
                using (var f = File.OpenRead(localPath))
                {
                    objectName = objectName ?? Path.GetFileName(localPath);
                    storage.UploadObject(bucketName, objectName, null, f);
                    Console.WriteLine($"Uploaded { objectName}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
