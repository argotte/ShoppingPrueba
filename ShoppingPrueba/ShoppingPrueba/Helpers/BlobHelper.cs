﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ShoppingPrueba.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ShoppingPrueba.Helpers
{
    public class BlobHelper : IBlobHelper
    {
        private readonly Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient _blobClient;

        public BlobHelper(IConfiguration configuration)
        {
            string keys = configuration["Blob:ConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task DeleteBlobAsync(Guid id, string containerName)
        {
            try
            {
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}");
                await blockBlob.DeleteAsync();
            }
            catch { };
        }

        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName)
        {
            Stream stream = file.OpenReadStream();
            return await UploadBlobAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(byte[] file, string containerName)
        {
            MemoryStream stream = new MemoryStream(file);
            return await UploadBlobAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(string image, string containerName)
        {
            Stream stream = File.OpenRead(image);
            return await UploadBlobAsync(stream, containerName);
        }


        private async Task<Guid> UploadBlobAsync(Stream stream, string containerName)
        {
            Guid name = Guid.NewGuid();
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{name}");
            await blockBlob.UploadFromStreamAsync(stream);
            return name;
        }

        Task<Guid> IBlobHelper.UploadBlobAsync(byte[] file, string containerName)
        {
            throw new NotImplementedException();
        }

        Task<Guid> IBlobHelper.UploadBlobAsync(string image, string containerName)
        {
            throw new NotImplementedException();
        }
    }
}
