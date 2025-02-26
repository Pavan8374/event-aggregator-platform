namespace Event.Domain.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string folder = null);
        Task<bool> DeleteFilesAsync(List<string> fileIds);
        Task<string> GetFileUrlAsync(string fileId);
    }
}
