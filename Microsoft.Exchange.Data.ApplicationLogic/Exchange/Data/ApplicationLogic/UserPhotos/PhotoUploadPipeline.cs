using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000210 RID: 528
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoUploadPipeline
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		public PhotoUploadPipeline(PhotosConfiguration configuration, IMailboxSession mailboxSession, IRecipientSession recipientSession, ITracer upstreamTracer)
		{
			MailboxPhotoReader reader = new MailboxPhotoReader(upstreamTracer);
			MailboxPhotoWriter writer = new MailboxPhotoWriter(mailboxSession, upstreamTracer);
			ADPhotoReader reader2 = new ADPhotoReader(upstreamTracer);
			ADPhotoWriter writer2 = new ADPhotoWriter(recipientSession, upstreamTracer);
			this.pipeline = new PreviewPhotoUploadHandler(mailboxSession, reader, writer, PhotoEditor.Default, upstreamTracer).Then(new ADPhotoUploadHandler(recipientSession, configuration, reader2, writer2, upstreamTracer)).Then(new MailboxPhotoUploadHandler(mailboxSession, reader, writer, upstreamTracer)).Then(new FileSystemPhotoUploadHandler(configuration, new FileSystemPhotoWriter(upstreamTracer), upstreamTracer));
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0004F665 File Offset: 0x0004D865
		public PhotoResponse Upload(PhotoRequest request, Stream outputStream)
		{
			return this.pipeline.Upload(request, new PhotoResponse(outputStream));
		}

		// Token: 0x04000AB2 RID: 2738
		private readonly IPhotoUploadHandler pipeline;
	}
}
