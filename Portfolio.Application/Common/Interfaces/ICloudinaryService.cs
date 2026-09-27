using Portfolio.Application.DTOs.Admin;

namespace Portfolio.Application.Common.Interfaces;

public interface ICloudinaryService
{
    Task<UploadMediaResponseDto> UploadImageAsync(Stream fileStream, string fileName, string folder = "portfolio", CancellationToken cancellationToken = default);
    Task<UploadMediaResponseDto> UploadRawFileAsync(Stream fileStream, string fileName, string folder = "portfolio/docs", CancellationToken cancellationToken = default);
    Task<bool> DeleteMediaAsync(string publicId, CancellationToken cancellationToken = default);
}
