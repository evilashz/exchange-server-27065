using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000213 RID: 531
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoteForestPhotoRetrievalPipelineTooComplex : IRemoteForestPhotoRetrievalPipelineFactory
	{
		// Token: 0x06001358 RID: 4952 RVA: 0x00050250 File Offset: 0x0004E450
		public RemoteForestPhotoRetrievalPipelineTooComplex(ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.tracer = upstreamTracer;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0005026A File Offset: 0x0004E46A
		public IPhotoHandler Create()
		{
			return new TooComplexPhotoHandler(this.tracer);
		}

		// Token: 0x04000ABF RID: 2751
		private readonly ITracer tracer;
	}
}
