using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001D0 RID: 464
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ExternalRequestorPhotoRetrievalPipeline : IPhotoHandler
	{
		// Token: 0x06001177 RID: 4471 RVA: 0x00048B2B File Offset: 0x00046D2B
		public ExternalRequestorPhotoRetrievalPipeline(IRecipientSession recipientSession, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.pipeline = new ADPhotoHandler(new ADPhotoReader(upstreamTracer), recipientSession, upstreamTracer);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00048B5C File Offset: 0x00046D5C
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			return this.pipeline.Retrieve(request, response);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00048B6B File Offset: 0x00046D6B
		public IPhotoHandler Then(IPhotoHandler next)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000941 RID: 2369
		private readonly IPhotoHandler pipeline;
	}
}
