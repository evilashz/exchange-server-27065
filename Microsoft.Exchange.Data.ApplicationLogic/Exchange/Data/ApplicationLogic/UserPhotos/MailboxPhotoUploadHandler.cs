using System;
using System.Net;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001EB RID: 491
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxPhotoUploadHandler : IPhotoHandler, IPhotoUploadHandler
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x0004C300 File Offset: 0x0004A500
		public MailboxPhotoUploadHandler(IMailboxSession session, IMailboxPhotoReader reader, IMailboxPhotoWriter writer, ITracer upstreamTracer)
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
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.session = session;
			this.reader = reader;
			this.writer = writer;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0004C380 File Offset: 0x0004A580
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
					result = this.SavePhotoToMailbox(request, response);
					break;
				case UploadCommand.Clear:
					result = this.ClearPhotoFromMailbox(request, response);
					break;
				default:
					result = response;
					break;
				}
			}
			catch (ObjectNotFoundException arg)
			{
				this.tracer.TraceDebug<ObjectNotFoundException>((long)this.GetHashCode(), "Mailbox photo upload handler: photo not found.  Exception: {0}", arg);
				throw;
			}
			catch (StorageTransientException arg2)
			{
				this.tracer.TraceError<StorageTransientException>((long)this.GetHashCode(), "Mailbox photo upload handler: transient exception saving or clearing photo.  Exception: {0}", arg2);
				throw;
			}
			catch (StoragePermanentException arg3)
			{
				this.tracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "Mailbox photo upload handler: permanent exception saving or clearing photo.  Exception: {0}", arg3);
				throw;
			}
			return result;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0004C444 File Offset: 0x0004A644
		public IPhotoUploadHandler Then(IPhotoUploadHandler next)
		{
			return new CompositePhotoUploadHandler(this, next);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0004C450 File Offset: 0x0004A650
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			if (response.Served)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo upload handler: skipped because photo has already been served by an upstream handler.");
				return response;
			}
			PhotoResponse result;
			try
			{
				response.MailboxUploadHandlerProcessed = true;
				this.tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "Mailbox photo upload handler: reading photo of user {0}.", this.session.MailboxOwner);
				this.reader.Read(this.session, request.Size, request.Preview, response.OutputPhotoStream, request.PerformanceLogger);
				response.Thumbprint = new int?(this.reader.ReadThumbprint(this.session, request.Preview));
				response.Served = true;
				response.Status = HttpStatusCode.OK;
				result = response;
			}
			catch (ObjectNotFoundException arg)
			{
				this.tracer.TraceDebug<bool, ObjectNotFoundException>((long)this.GetHashCode(), "Mailbox photo upload handler: photo not found.  Preview? {0}.  Exception: {1}", request.Preview, arg);
				result = response;
			}
			catch (StorageTransientException arg2)
			{
				this.tracer.TraceError<StorageTransientException>((long)this.GetHashCode(), "Mailbox photo upload handler: transient exception at reading photo.  Exception: {0}", arg2);
				throw;
			}
			catch (StoragePermanentException arg3)
			{
				this.tracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "Mailbox photo upload handler: permanent exception at reading photo.  Exception: {0}", arg3);
				throw;
			}
			return result;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0004C58C File Offset: 0x0004A78C
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0004C595 File Offset: 0x0004A795
		private PhotoResponse SavePhotoToMailbox(PhotoRequest request, PhotoResponse response)
		{
			response.MailboxUploadHandlerProcessed = true;
			this.writer.Save();
			return response;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0004C5AA File Offset: 0x0004A7AA
		private PhotoResponse ClearPhotoFromMailbox(PhotoRequest request, PhotoResponse response)
		{
			response.MailboxUploadHandlerProcessed = true;
			this.writer.Clear();
			return response;
		}

		// Token: 0x0400099D RID: 2461
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x0400099E RID: 2462
		private readonly IMailboxSession session;

		// Token: 0x0400099F RID: 2463
		private readonly IMailboxPhotoReader reader;

		// Token: 0x040009A0 RID: 2464
		private readonly IMailboxPhotoWriter writer;
	}
}
