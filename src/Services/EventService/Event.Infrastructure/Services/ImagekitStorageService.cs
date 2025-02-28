using Event.Domain.Entities;
using Event.Domain.Services;
using Imagekit.Models;
using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Event.Infrastructure.Services
{
    public class ImagekitStorageService : IFileStorageService
    {
        private readonly ImagekitClient _imagekitClient;
        private readonly ILogger<ImagekitStorageService> _logger;
        private readonly string _urlEndpoint;
        private readonly ImageKitSetting _imageKitSetting;

        public ImagekitStorageService(
            IConfiguration configuration,
            ILogger<ImagekitStorageService> logger,
            IOptions<ImageKitSetting> options)
        {

            _imageKitSetting = options.Value;
            _imagekitClient = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.URLEndpoint);
            _logger = logger;

        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string folder = null)
        {
            try
            {
                // Convert stream to byte array
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                // Create the upload request
                var uploadRequest = new FileCreateRequest
                {
                    file = fileBytes,
                    fileName = fileName,
                };

                // If folder is specified, add it to the path
                if (!string.IsNullOrEmpty(folder))
                {
                    uploadRequest.folder = folder;
                }

                // Add metadata as needed
                uploadRequest.tags = new List<string> { "event-images" };
                uploadRequest.isPrivateFile = false; // Set to true if you want private files
                uploadRequest.useUniqueFileName = true;
                uploadRequest.overwriteFile = false;
                uploadRequest.responseFields = new List<string> { "tags", "customCoordinates", "metadata" };

                // Upload the file
                Result result = await _imagekitClient.UploadAsync(uploadRequest);

                if (result.HttpStatusCode == 200 && !string.IsNullOrEmpty(result.fileId))
                {
                    _logger.LogInformation($"File uploaded successfully with ID: {result.fileId}", result.fileId);
                    return result.url;
                }
                else
                {
                    _logger.LogError("File upload failed: {Message}", result);
                    throw new Exception("File upload failed: " + result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to Imagekit");
                throw new Exception("Error uploading file to Imagekit", ex);
            }
        }
        public async Task<bool> DeleteFilesAsync(List<string> fileIds)
        {
            try
            {
                var response = await _imagekitClient.BulkDeleteFilesAsync(fileIds);
                if (response.HttpStatusCode == 204)
                {
                    _logger.LogInformation($"Files deleted successfully.");
                    return true;
                }
                else
                {
                    _logger.LogError($"Failed to delete file. Response: {response}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting files");
                return false;
            }
        }
        public async Task<string> GetFileUrlAsync(string fileId)
        {
            try
            {
                var fileDetails = await _imagekitClient.GetFileDetailAsync(fileId);
                return fileDetails.Raw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving URL for file {fileId}");
                return null;
            }
        }
        public async Task<List<string>> GetAllFilesInFolderAsync(string folder)
        {
            try
            {
                var listFilesRequest = new GetFileListRequest
                {
                   
                };

                var result = await _imagekitClient.GetFileListRequestAsync(listFilesRequest);

                if (result.HttpStatusCode == 200)
                {
                    var fileUrls = new List<string>();
                    foreach (var file in result.FileList)
                    {
                        if (!string.IsNullOrEmpty(file.url))
                        {
                            fileUrls.Add(file.url);
                        }
                    }
                    return fileUrls;
                }
                else
                {
                    _logger.LogError("Getting files list failed: {Message}", result);
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting files from Imagekit folder");
                return new List<string>();
            }
        }
        public async Task<Stream> GetFileStreamAsync(string fileId)
        {
            try
            {
                var result = await _imagekitClient.GetFileDetailAsync(fileId);

                if (result.HttpStatusCode == 200 && !string.IsNullOrEmpty(result.Raw))
                {
                    // Create an HTTP client to download the file
                    using var httpClient = new System.Net.Http.HttpClient();
                    return await httpClient.GetStreamAsync(result.Raw);
                }
                else
                {
                    _logger.LogError("Getting file details failed: {Message}", result.Raw);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting file stream from Imagekit");
                return null;
            }
        }
    }
}
