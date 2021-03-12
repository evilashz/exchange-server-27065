using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000211 RID: 529
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PreviewPhotoUploadHandler : IPhotoUploadHandler
	{
		// Token: 0x06001341 RID: 4929 RVA: 0x0004F67C File Offset: 0x0004D87C
		public PreviewPhotoUploadHandler(IMailboxSession session, IMailboxPhotoReader reader, IMailboxPhotoWriter writer, IPhotoEditor editor, ITracer upstreamTracer)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (editor == null)
			{
				throw new ArgumentNullException("editor");
			}
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.session = session;
			this.reader = reader;
			this.writer = writer;
			this.editor = editor;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004F714 File Offset: 0x0004D914
		public PhotoResponse Upload(PhotoRequest request, PhotoResponse response)
		{
			switch (request.UploadCommand)
			{
			case UploadCommand.Upload:
				if (request.Preview)
				{
					return this.UploadPreview(request, response);
				}
				return this.LoadPreview(request, response);
			case UploadCommand.Clear:
				return this.ClearPreview(request, response);
			default:
				return response;
			}
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0004F75C File Offset: 0x0004D95C
		public IPhotoUploadHandler Then(IPhotoUploadHandler next)
		{
			return new CompositePhotoUploadHandler(this, next);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0004F768 File Offset: 0x0004D968
		private PhotoResponse UploadPreview(PhotoRequest request, PhotoResponse response)
		{
			if (request.RawUploadedPhoto == null || request.RawUploadedPhoto.Length == 0L)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Preview photo upload handler: skipped because no photo was uploaded in the request.");
				return response;
			}
			request.RawUploadedPhoto.Seek(0L, SeekOrigin.Begin);
			int num = PhotoThumbprinter.Default.Compute(request.RawUploadedPhoto);
			this.tracer.TraceDebug<string, int>((long)this.GetHashCode(), "Preview photo upload handler: uploading preview photo of {0}.  Its thumbprint is {1:X8}.", request.TargetPrimarySmtpAddress, num);
			try
			{
				this.writer.UploadPreview(num, this.CropAndScaleRawPhoto(request.RawUploadedPhoto));
				response.PreviewUploadHandlerProcessed = true;
			}
			catch (StoragePermanentException arg)
			{
				this.tracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "Preview photo upload handler: hit a permanent storage exception uploading photo to mailbox.  Exception: {0}", arg);
				throw;
			}
			catch (StorageTransientException arg2)
			{
				this.tracer.TraceError<StorageTransientException>((long)this.GetHashCode(), "Preview photo upload handler: hit a transient storage exception uploading photo to mailbox.  Exception: {0}", arg2);
				throw;
			}
			return response;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0004F85C File Offset: 0x0004DA5C
		private PhotoResponse LoadPreview(PhotoRequest request, PhotoResponse response)
		{
			Dictionary<UserPhotoSize, byte[]> dictionary = new Dictionary<UserPhotoSize, byte[]>();
			try
			{
				int num = this.reader.ReadAllPreviewSizes(this.session, dictionary);
				this.tracer.TraceDebug<string, int>((long)this.GetHashCode(), "Preview photo upload handler: read preview photo of {0} with thumbprint {1:X8}.", request.TargetPrimarySmtpAddress, num);
				response.UploadedPhotos = dictionary;
				response.Thumbprint = new int?(num);
				response.PreviewUploadHandlerProcessed = true;
			}
			catch (ObjectNotFoundException arg)
			{
				this.tracer.TraceError<ObjectNotFoundException>((long)this.GetHashCode(), "Preview photo upload handler: preview photo does NOT exist in mailbox.  Exception: {0}", arg);
				throw;
			}
			catch (StoragePermanentException arg2)
			{
				this.tracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "Preview photo upload handler: hit a permanent storage exception loading preview photo from mailbox.  Exception: {0}", arg2);
				throw;
			}
			catch (StorageTransientException arg3)
			{
				this.tracer.TraceError<StorageTransientException>((long)this.GetHashCode(), "Preview photo upload handler: hit a transient storage exception loading preview photo from mailbox.  Exception: {0}", arg3);
				throw;
			}
			return response;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0004F93C File Offset: 0x0004DB3C
		private IDictionary<UserPhotoSize, byte[]> CropAndScaleRawPhoto(Stream rawPhoto)
		{
			rawPhoto.Seek(0L, SeekOrigin.Begin);
			return this.editor.CropAndScale(rawPhoto);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004F954 File Offset: 0x0004DB54
		private PhotoResponse ClearPreview(PhotoRequest request, PhotoResponse response)
		{
			response.PreviewUploadHandlerProcessed = true;
			this.writer.ClearPreview();
			return response;
		}

		// Token: 0x04000AB3 RID: 2739
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x04000AB4 RID: 2740
		private readonly IMailboxSession session;

		// Token: 0x04000AB5 RID: 2741
		private readonly IMailboxPhotoReader reader;

		// Token: 0x04000AB6 RID: 2742
		private readonly IMailboxPhotoWriter writer;

		// Token: 0x04000AB7 RID: 2743
		private readonly IPhotoEditor editor;
	}
}
