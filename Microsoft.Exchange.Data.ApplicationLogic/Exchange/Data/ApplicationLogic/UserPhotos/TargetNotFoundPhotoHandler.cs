using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000216 RID: 534
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TargetNotFoundPhotoHandler : IPhotoHandler
	{
		// Token: 0x0600135E RID: 4958 RVA: 0x000502D6 File Offset: 0x0004E4D6
		public TargetNotFoundPhotoHandler(PhotosConfiguration configuration, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.configuration = configuration;
			this.tracer = upstreamTracer;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00050304 File Offset: 0x0004E504
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("response", response);
			if (response.Served)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "TARGET NOT FOUND HANDLER: skipped because photo has already been served by an upstream handler.");
				return response;
			}
			this.tracer.TraceDebug((long)this.GetHashCode(), "TARGET NOT FOUND HANDLER: responding request with HTTP 404 Not Found.");
			response.TargetNotFoundHandlerProcessed = true;
			response.Served = true;
			response.Status = HttpStatusCode.NotFound;
			response.HttpExpiresHeader = UserAgentPhotoExpiresHeader.Default.ComputeExpiresHeader(DateTime.UtcNow, HttpStatusCode.NotFound, this.configuration);
			return response;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00050399 File Offset: 0x0004E599
		public IPhotoHandler Then(IPhotoHandler next)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000ACF RID: 2767
		private readonly PhotosConfiguration configuration;

		// Token: 0x04000AD0 RID: 2768
		private readonly ITracer tracer;
	}
}
