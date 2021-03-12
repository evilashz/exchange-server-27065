using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E8 RID: 488
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LocalServerPhotoRetrievalPipeline : IPhotoHandler
	{
		// Token: 0x060011E2 RID: 4578 RVA: 0x0004B1F4 File Offset: 0x000493F4
		public LocalServerPhotoRetrievalPipeline(PhotosConfiguration configuration, string clientInfo, IRecipientSession recipientSession, IXSOFactory xsoFactory, ITracer upstreamTracer)
		{
			this.pipeline = new FileSystemPhotoHandler(configuration, new FileSystemPhotoReader(upstreamTracer), upstreamTracer).Then(new MailboxPhotoHandler(configuration, clientInfo, new MailboxPhotoReader(upstreamTracer), recipientSession, upstreamTracer, xsoFactory)).Then(new ADPhotoHandler(new ADPhotoReader(upstreamTracer), recipientSession, upstreamTracer)).Then(new CachingPhotoHandler(new FileSystemPhotoWriter(upstreamTracer), configuration, upstreamTracer)).Then(new DiagnosticsPhotoHandler(upstreamTracer));
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004B268 File Offset: 0x00049468
		public PhotoResponse Retrieve(PhotoRequest request, Stream outputStream)
		{
			return this.pipeline.Retrieve(request, new PhotoResponse(outputStream));
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004B27C File Offset: 0x0004947C
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			return this.pipeline.Retrieve(request, response);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0004B28B File Offset: 0x0004948B
		public IPhotoHandler Then(IPhotoHandler next)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000988 RID: 2440
		private readonly IPhotoHandler pipeline;
	}
}
