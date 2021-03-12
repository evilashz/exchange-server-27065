using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001C9 RID: 457
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADPhotoUploadHandler : IPhotoHandler, IPhotoUploadHandler
	{
		// Token: 0x06001154 RID: 4436 RVA: 0x00047E10 File Offset: 0x00046010
		public ADPhotoUploadHandler(IRecipientSession session, PhotosConfiguration configuration, IADPhotoReader reader, IADPhotoWriter writer, ITracer upstreamTracer)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
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
			this.session = session;
			this.photoSizeToUploadToAD = configuration.PhotoSizeToUploadToAD;
			this.reader = reader;
			this.writer = writer;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00047EAC File Offset: 0x000460AC
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
					result = this.SavePhotoToAD(request, response);
					break;
				case UploadCommand.Clear:
					result = this.ClearPhotoFromAD(request, response);
					break;
				default:
					result = response;
					break;
				}
			}
			catch (ADNoSuchObjectException arg)
			{
				this.tracer.TraceError<ADNoSuchObjectException>((long)this.GetHashCode(), "AD photo upload handler: failed to write photo to AD.  Exception: {0}", arg);
				throw;
			}
			catch (ADOperationException arg2)
			{
				this.tracer.TraceError<ADOperationException>((long)this.GetHashCode(), "AD photo upload handler: operation exception at writing photo.  Exception: {0}", arg2);
				throw;
			}
			return result;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00047F48 File Offset: 0x00046148
		public IPhotoUploadHandler Then(IPhotoUploadHandler next)
		{
			return new CompositePhotoUploadHandler(this, next);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00047F54 File Offset: 0x00046154
		private PhotoResponse SavePhotoToAD(PhotoRequest request, PhotoResponse response)
		{
			byte[] photoToSaveToAD = this.GetPhotoToSaveToAD(response.UploadedPhotos);
			if (photoToSaveToAD == null || photoToSaveToAD.Length == 0)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "AD photo upload handler: skipped because photo to save to AD is not available.");
				return response;
			}
			response.ADUploadHandlerProcessed = true;
			using (MemoryStream memoryStream = new MemoryStream(photoToSaveToAD))
			{
				this.writer.Write(request.UploadTo, memoryStream);
			}
			return response;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00047FCC File Offset: 0x000461CC
		private byte[] GetPhotoToSaveToAD(IDictionary<UserPhotoSize, byte[]> uploadedPhotos)
		{
			if (uploadedPhotos == null)
			{
				return null;
			}
			byte[] result;
			if (!uploadedPhotos.TryGetValue(this.photoSizeToUploadToAD, out result))
			{
				this.tracer.TraceDebug<UserPhotoSize>((long)this.GetHashCode(), "AD photo upload handler: photo of size {0} was not uploaded.", this.photoSizeToUploadToAD);
				return null;
			}
			return result;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0004800E File Offset: 0x0004620E
		private PhotoResponse ClearPhotoFromAD(PhotoRequest request, PhotoResponse response)
		{
			response.ADUploadHandlerProcessed = true;
			this.writer.Write(request.UploadTo, Stream.Null);
			return response;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00048030 File Offset: 0x00046230
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			if (response.Served)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "AD photo upload handler: skipped because photo has already been served by an upstream handler.");
				return response;
			}
			if (request.Preview)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "AD photo upload handler: skipped because cannot serve PREVIEW photo.");
				return response;
			}
			PhotoResponse result;
			try
			{
				response.ADUploadHandlerProcessed = true;
				this.reader.Read(this.session, request.UploadTo, response.OutputPhotoStream);
				response.Served = true;
				response.Status = HttpStatusCode.OK;
				response.Thumbprint = null;
				result = response;
			}
			catch (ADNoSuchObjectException arg)
			{
				this.tracer.TraceDebug<ADNoSuchObjectException>((long)this.GetHashCode(), "AD photo upload handler: no photo.  Exception: {0}", arg);
				result = response;
			}
			catch (ADOperationException arg2)
			{
				this.tracer.TraceError<ADOperationException>((long)this.GetHashCode(), "AD photo upload handler: exception at reading photo.  Exception: {0}", arg2);
				throw;
			}
			catch (IOException arg3)
			{
				this.tracer.TraceError<IOException>((long)this.GetHashCode(), "AD photo upload handler: I/O exception at reading photo.  Exception: {0}", arg3);
				throw;
			}
			return result;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00048148 File Offset: 0x00046348
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x04000931 RID: 2353
		private readonly UserPhotoSize photoSizeToUploadToAD;

		// Token: 0x04000932 RID: 2354
		private readonly IRecipientSession session;

		// Token: 0x04000933 RID: 2355
		private readonly IADPhotoReader reader;

		// Token: 0x04000934 RID: 2356
		private readonly IADPhotoWriter writer;

		// Token: 0x04000935 RID: 2357
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;
	}
}
