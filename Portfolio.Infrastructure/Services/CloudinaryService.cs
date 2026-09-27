using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Application.DTOs.Admin;

namespace Portfolio.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary? _cloudinary;
    private readonly ILogger<CloudinaryService> _logger;
    private readonly bool _isConfigured;

    public CloudinaryService(IConfiguration configuration, ILogger<CloudinaryService> logger)
    {
        _logger = logger;

        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        if (!string.IsNullOrWhiteSpace(cloudName) && !string.IsNullOrWhiteSpace(apiKey) && !string.IsNullOrWhiteSpace(apiSecret))
        {
            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
            _isConfigured = true;
            _logger.LogInformation("Cloudinary service initialized with cloud name '{CloudName}'.", cloudName);
        }
        else
        {
            _isConfigured = false;
            _logger.LogInformation("Cloudinary credentials not set. Falling back to local filesystem storage in wwwroot/uploads.");
        }
    }

    public async Task<UploadMediaResponseDto> UploadImageAsync(Stream fileStream, string fileName, string folder = "portfolio", CancellationToken cancellationToken = default)
    {
        if (_isConfigured && _cloudinary != null)
        {
            try
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, fileStream),
                    Folder = folder,
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = false
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

                if (uploadResult.Error != null)
                {
                    _logger.LogError("Cloudinary image upload error: {Error}", uploadResult.Error.Message);
                    return new UploadMediaResponseDto { Success = false, Message = uploadResult.Error.Message };
                }

                return new UploadMediaResponseDto
                {
                    Success = true,
                    Url = uploadResult.SecureUrl?.ToString() ?? uploadResult.Url?.ToString() ?? string.Empty,
                    PublicId = uploadResult.PublicId,
                    Format = uploadResult.Format,
                    Bytes = uploadResult.Bytes,
                    Message = "Image uploaded successfully to Cloudinary."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during Cloudinary image upload.");
                return new UploadMediaResponseDto { Success = false, Message = ex.Message };
            }
        }

        // Local filesystem fallback
        return await SaveToLocalAsync(fileStream, fileName, folder, cancellationToken);
    }

    public async Task<UploadMediaResponseDto> UploadRawFileAsync(Stream fileStream, string fileName, string folder = "portfolio/docs", CancellationToken cancellationToken = default)
    {
        if (_isConfigured && _cloudinary != null)
        {
            try
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(fileName, fileStream),
                    Folder = folder,
                    UseFilename = true,
                    UniqueFilename = true
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams, "raw", cancellationToken);

                if (uploadResult.Error != null)
                {
                    _logger.LogError("Cloudinary raw upload error: {Error}", uploadResult.Error.Message);
                    return new UploadMediaResponseDto { Success = false, Message = uploadResult.Error.Message };
                }

                return new UploadMediaResponseDto
                {
                    Success = true,
                    Url = uploadResult.SecureUrl?.ToString() ?? uploadResult.Url?.ToString() ?? string.Empty,
                    PublicId = uploadResult.PublicId,
                    Format = uploadResult.Format,
                    Bytes = uploadResult.Bytes,
                    Message = "File uploaded successfully to Cloudinary."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during Cloudinary raw file upload.");
                return new UploadMediaResponseDto { Success = false, Message = ex.Message };
            }
        }

        // Local filesystem fallback
        return await SaveToLocalAsync(fileStream, fileName, folder, cancellationToken);
    }

    public async Task<bool> DeleteMediaAsync(string publicId, CancellationToken cancellationToken = default)
    {
        if (_isConfigured && _cloudinary != null)
        {
            try
            {
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);
                return result.Result == "ok";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete Cloudinary media with publicId '{PublicId}'", publicId);
                return false;
            }
        }

        return true;
    }

    private async Task<UploadMediaResponseDto> SaveToLocalAsync(Stream fileStream, string fileName, string folder, CancellationToken cancellationToken)
    {
        try
        {
            var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var targetDir = Path.Combine(webRoot, "uploads", folder.Replace('/', Path.DirectorySeparatorChar));

            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            var extension = Path.GetExtension(fileName);
            var safeName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid():N}{extension}";
            var filePath = Path.Combine(targetDir, safeName);

            using (var fileDestination = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.CopyToAsync(fileDestination, cancellationToken);
            }

            var relativeUrl = $"/uploads/{folder.Trim('/')}/{safeName}";
            var fileInfo = new FileInfo(filePath);

            return new UploadMediaResponseDto
            {
                Success = true,
                Url = relativeUrl,
                PublicId = safeName,
                Format = extension.TrimStart('.'),
                Bytes = fileInfo.Length,
                Message = "File saved to local server storage."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save file to local directory.");
            return new UploadMediaResponseDto { Success = false, Message = ex.Message };
        }
    }
}
