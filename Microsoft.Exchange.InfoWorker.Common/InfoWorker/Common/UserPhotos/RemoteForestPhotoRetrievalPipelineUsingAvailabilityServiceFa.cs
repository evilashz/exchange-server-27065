using System;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.UserPhotos
{
	// Token: 0x02000142 RID: 322
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoteForestPhotoRetrievalPipelineUsingAvailabilityServiceFactory : IRemoteForestPhotoRetrievalPipelineFactory
	{
		// Token: 0x060008BE RID: 2238 RVA: 0x00025FA1 File Offset: 0x000241A1
		public RemoteForestPhotoRetrievalPipelineUsingAvailabilityServiceFactory(PhotosConfiguration configuration, IRecipientSession recipientSession, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.configuration = configuration;
			this.recipientSession = recipientSession;
			this.tracer = upstreamTracer;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00025FDF File Offset: 0x000241DF
		public IPhotoHandler Create()
		{
			return new RemoteForestPhotoRetrievalPipeline(this.configuration, this.recipientSession, this.tracer);
		}

		// Token: 0x040006D5 RID: 1749
		private readonly PhotosConfiguration configuration;

		// Token: 0x040006D6 RID: 1750
		private readonly IRecipientSession recipientSession;

		// Token: 0x040006D7 RID: 1751
		private readonly ITracer tracer;
	}
}
