using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000214 RID: 532
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SelfPhotoRetrievalPipeline : IPhotoHandler
	{
		// Token: 0x0600135A RID: 4954 RVA: 0x00050277 File Offset: 0x0004E477
		public SelfPhotoRetrievalPipeline(PhotosConfiguration configuration, string clientInfo, IRecipientSession recipientSession, IXSOFactory xsoFactory, ITracer upstreamTracer)
		{
			this.pipeline = new MailboxPhotoHandler(configuration, clientInfo, new MailboxPhotoReader(upstreamTracer), recipientSession, upstreamTracer, xsoFactory).Then(new ADPhotoHandler(new ADPhotoReader(upstreamTracer), recipientSession, upstreamTracer));
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000502AC File Offset: 0x0004E4AC
		public PhotoResponse Retrieve(PhotoRequest request, Stream outputStream)
		{
			return this.pipeline.Retrieve(request, new PhotoResponse(outputStream));
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000502C0 File Offset: 0x0004E4C0
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			return this.pipeline.Retrieve(request, response);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000502CF File Offset: 0x0004E4CF
		public IPhotoHandler Then(IPhotoHandler next)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000AC0 RID: 2752
		private readonly IPhotoHandler pipeline;
	}
}
