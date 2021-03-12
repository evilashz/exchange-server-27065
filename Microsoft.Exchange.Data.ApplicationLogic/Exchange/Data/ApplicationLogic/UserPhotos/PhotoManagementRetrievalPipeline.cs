using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001FA RID: 506
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoManagementRetrievalPipeline
	{
		// Token: 0x06001267 RID: 4711 RVA: 0x0004DCF4 File Offset: 0x0004BEF4
		public PhotoManagementRetrievalPipeline(PhotosConfiguration configuration, IMailboxSession mailboxSession, IRecipientSession recipientSession, ITracer upstreamTracer)
		{
			MailboxPhotoReader reader = new MailboxPhotoReader(upstreamTracer);
			MailboxPhotoWriter writer = new MailboxPhotoWriter(mailboxSession, upstreamTracer);
			ADPhotoReader reader2 = new ADPhotoReader(upstreamTracer);
			ADPhotoWriter writer2 = new ADPhotoWriter(recipientSession, upstreamTracer);
			this.pipeline = new MailboxPhotoUploadHandler(mailboxSession, reader, writer, upstreamTracer).Then(new ADPhotoUploadHandler(recipientSession, configuration, reader2, writer2, upstreamTracer));
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004DD49 File Offset: 0x0004BF49
		public PhotoResponse Retrieve(PhotoRequest request, Stream outputStream)
		{
			return this.pipeline.Retrieve(request, new PhotoResponse(outputStream));
		}

		// Token: 0x040009E3 RID: 2531
		private readonly IPhotoHandler pipeline;
	}
}
