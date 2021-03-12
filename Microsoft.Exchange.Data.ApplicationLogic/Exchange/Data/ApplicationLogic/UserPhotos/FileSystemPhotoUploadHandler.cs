using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001D6 RID: 470
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FileSystemPhotoUploadHandler : IPhotoUploadHandler
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x00049960 File Offset: 0x00047B60
		public FileSystemPhotoUploadHandler(PhotosConfiguration configuration, IFileSystemPhotoWriter writer, ITracer upstreamTracer)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (!new DirectoryInfo(configuration.ExchangeInstallPath).Exists)
			{
				throw new ArgumentException("configuration.ExchangeInstallPath");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.upstreamTracer = upstreamTracer;
			this.writer = writer;
			this.photosRootDirectoryFullPath = configuration.PhotosRootDirectoryFullPath;
			this.sizesToCacheOnFileSystem = configuration.SizesToCacheOnFileSystem;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000499FC File Offset: 0x00047BFC
		public PhotoResponse Upload(PhotoRequest request, PhotoResponse response)
		{
			if (request.Preview)
			{
				return response;
			}
			PhotoResponse result;
			try
			{
				switch (request.UploadCommand)
				{
				case UploadCommand.Upload:
					result = this.SavePhotosToFileSystem(request, response);
					break;
				case UploadCommand.Clear:
					result = this.ClearPhotosFromFileSystem(request, response);
					break;
				default:
					result = response;
					break;
				}
			}
			catch (CannotMapInvalidSmtpAddressToPhotoFileException arg)
			{
				this.tracer.TraceError<CannotMapInvalidSmtpAddressToPhotoFileException>((long)this.GetHashCode(), "File system photo upload handler: invalid SMTP address cannot be mapped to file system.  Exception: {0}", arg);
				throw;
			}
			catch (IOException arg2)
			{
				this.tracer.TraceError<string, IOException>((long)this.GetHashCode(), "File system photo upload handler: failed to write photo for {0}.  Exception: {1}", request.TargetPrimarySmtpAddress, arg2);
				result = response;
			}
			catch (UnauthorizedAccessException arg3)
			{
				this.tracer.TraceError<string, UnauthorizedAccessException>((long)this.GetHashCode(), "File system photo upload handler: authorization failure writing photo for {0}.  Exception: {1}", request.TargetPrimarySmtpAddress, arg3);
				result = response;
			}
			catch (Win32Exception arg4)
			{
				this.tracer.TraceDebug<string, Win32Exception>((long)this.GetHashCode(), "File system photo upload handler: Win32 exception writing photo for {0}.  Exception: {1}", request.TargetPrimarySmtpAddress, arg4);
				result = response;
			}
			return result;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00049B10 File Offset: 0x00047D10
		public IPhotoUploadHandler Then(IPhotoUploadHandler next)
		{
			return new CompositePhotoUploadHandler(this, next);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00049B1C File Offset: 0x00047D1C
		private PhotoResponse SavePhotosToFileSystem(PhotoRequest request, PhotoResponse response)
		{
			response.FileSystemUploadHandlerProcessed = true;
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "File system photo upload handler: saving photos of {0} to file system.", request.TargetPrimarySmtpAddress);
			FileSystemPhotoMap map = new FileSystemPhotoMap(this.photosRootDirectoryFullPath, this.upstreamTracer);
			foreach (UserPhotoSize size in this.sizesToCacheOnFileSystem)
			{
				this.SavePhotoToFileSystem(request, response, map, size);
			}
			return response;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00049BA4 File Offset: 0x00047DA4
		private void SavePhotoToFileSystem(PhotoRequest request, PhotoResponse response, FileSystemPhotoMap map, UserPhotoSize size)
		{
			byte[] uploadedPhotoOfSize = this.GetUploadedPhotoOfSize(response.UploadedPhotos, size);
			if (uploadedPhotoOfSize == null || uploadedPhotoOfSize.Length == 0)
			{
				this.tracer.TraceError<UserPhotoSize>((long)this.GetHashCode(), "File system photo upload handler: photo of size {0} NOT available and will NOT be saved to file system.", size);
				return;
			}
			string text = map.Map(request.TargetPrimarySmtpAddress, size);
			using (MemoryStream memoryStream = new MemoryStream(uploadedPhotoOfSize))
			{
				this.tracer.TraceDebug<string>((long)this.GetHashCode(), "File system photo upload handler: writing photo at {0}", text);
				this.DeleteThenWritePhoto(text, response.Thumbprint.Value, memoryStream);
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00049C44 File Offset: 0x00047E44
		private byte[] GetUploadedPhotoOfSize(IDictionary<UserPhotoSize, byte[]> uploadedPhotos, UserPhotoSize size)
		{
			if (uploadedPhotos == null)
			{
				return null;
			}
			byte[] result;
			if (!uploadedPhotos.TryGetValue(size, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00049C64 File Offset: 0x00047E64
		private PhotoResponse ClearPhotosFromFileSystem(PhotoRequest request, PhotoResponse response)
		{
			response.FileSystemUploadHandlerProcessed = true;
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "File system photo upload handler: clearing photos of {0} from file system.", request.TargetPrimarySmtpAddress);
			FileSystemPhotoMap map = new FileSystemPhotoMap(this.photosRootDirectoryFullPath, this.upstreamTracer);
			foreach (UserPhotoSize size in this.sizesToCacheOnFileSystem)
			{
				this.ClearPhotoFromFileSystem(request, response, map, size);
			}
			return response;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00049CEC File Offset: 0x00047EEC
		private void ClearPhotoFromFileSystem(PhotoRequest request, PhotoResponse response, FileSystemPhotoMap map, UserPhotoSize size)
		{
			int num = PhotoThumbprinter.Default.GenerateThumbprintForNegativeCache();
			string text = map.Map(request.TargetPrimarySmtpAddress, size);
			this.tracer.TraceDebug<string, int>((long)this.GetHashCode(), "File system photo upload handler: clearing photo at {0}.  Replacing it with NEGATIVE caching photo with thumbprint = {1:X8}", text, num);
			this.DeleteThenWritePhoto(text, num, Stream.Null);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00049D3C File Offset: 0x00047F3C
		private void DeleteThenWritePhoto(string photoFullPath, int thumbprint, Stream photo)
		{
			try
			{
				File.Delete(photoFullPath);
			}
			catch (DirectoryNotFoundException arg)
			{
				this.tracer.TraceDebug<string, DirectoryNotFoundException>((long)this.GetHashCode(), "File system photo upload handler: cannot delete photo file {0} because directory doesn't exist.  Exception: {1}", photoFullPath, arg);
			}
			catch (IOException arg2)
			{
				this.tracer.TraceDebug<string, IOException>((long)this.GetHashCode(), "File system photo upload handler: cannot delete photo file {0} because of a generic I/O exception.  Exception: {1}", photoFullPath, arg2);
			}
			catch (UnauthorizedAccessException arg3)
			{
				this.tracer.TraceDebug<string, UnauthorizedAccessException>((long)this.GetHashCode(), "File system photo upload handler: cannot delete photo file {0} because of an authorization failure.  Exception: {1}", photoFullPath, arg3);
			}
			this.writer.Write(photoFullPath, thumbprint, photo);
		}

		// Token: 0x0400095E RID: 2398
		private readonly ICollection<UserPhotoSize> sizesToCacheOnFileSystem;

		// Token: 0x0400095F RID: 2399
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x04000960 RID: 2400
		private readonly ITracer upstreamTracer;

		// Token: 0x04000961 RID: 2401
		private readonly string photosRootDirectoryFullPath;

		// Token: 0x04000962 RID: 2402
		private readonly IFileSystemPhotoWriter writer;
	}
}
