using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000217 RID: 535
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TooComplexPhotoHandler : IPhotoHandler
	{
		// Token: 0x06001361 RID: 4961 RVA: 0x000503A0 File Offset: 0x0004E5A0
		public TooComplexPhotoHandler(ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.tracer = upstreamTracer;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000503BA File Offset: 0x0004E5BA
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "TOO COMPLEX HANDLER: rejecting request.");
			throw new TooComplexPhotoRequestException();
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000503D8 File Offset: 0x0004E5D8
		public IPhotoHandler Then(IPhotoHandler next)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000AD1 RID: 2769
		private readonly ITracer tracer;
	}
}
