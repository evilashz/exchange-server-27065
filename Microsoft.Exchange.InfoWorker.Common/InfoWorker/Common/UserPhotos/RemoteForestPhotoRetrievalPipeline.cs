using System;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.UserPhotos
{
	// Token: 0x02000141 RID: 321
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RemoteForestPhotoRetrievalPipeline : IPhotoHandler
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x00025F38 File Offset: 0x00024138
		public RemoteForestPhotoRetrievalPipeline(PhotosConfiguration configuration, IRecipientSession recipientSession, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.pipeline = new RemoteForestPhotoHandler(configuration, upstreamTracer).Then(new ADPhotoHandler(new ADPhotoReader(upstreamTracer), recipientSession, upstreamTracer));
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00025F8B File Offset: 0x0002418B
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			return this.pipeline.Retrieve(request, response);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00025F9A File Offset: 0x0002419A
		public IPhotoHandler Then(IPhotoHandler next)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040006D4 RID: 1748
		private readonly IPhotoHandler pipeline;
	}
}
