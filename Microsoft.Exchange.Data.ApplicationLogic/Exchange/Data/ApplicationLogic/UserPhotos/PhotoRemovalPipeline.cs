using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001FE RID: 510
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRemovalPipeline
	{
		// Token: 0x06001279 RID: 4729 RVA: 0x0004DF2C File Offset: 0x0004C12C
		public PhotoRemovalPipeline(PhotosConfiguration configuration, IMailboxSession mailboxSession, IRecipientSession recipientSession, ITracer upstreamTracer)
		{
			MailboxPhotoReader reader = new MailboxPhotoReader(upstreamTracer);
			MailboxPhotoWriter writer = new MailboxPhotoWriter(mailboxSession, upstreamTracer);
			ADPhotoReader reader2 = new ADPhotoReader(upstreamTracer);
			ADPhotoWriter writer2 = new ADPhotoWriter(recipientSession, upstreamTracer);
			this.pipeline = new MailboxPhotoUploadHandler(mailboxSession, reader, writer, upstreamTracer).Then(new ADPhotoUploadHandler(recipientSession, configuration, reader2, writer2, upstreamTracer)).Then(new FileSystemPhotoUploadHandler(configuration, new FileSystemPhotoWriter(upstreamTracer), upstreamTracer)).Then(new PreviewPhotoUploadHandler(mailboxSession, reader, writer, PhotoEditor.Default, upstreamTracer));
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0004DFA9 File Offset: 0x0004C1A9
		public PhotoResponse Upload(PhotoRequest request, Stream outputStream)
		{
			return this.pipeline.Upload(request, new PhotoResponse(outputStream));
		}

		// Token: 0x04000A2C RID: 2604
		private readonly IPhotoUploadHandler pipeline;
	}
}
