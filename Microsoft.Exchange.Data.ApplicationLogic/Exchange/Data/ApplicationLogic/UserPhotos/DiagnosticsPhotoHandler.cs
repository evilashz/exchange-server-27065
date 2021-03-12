using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001CF RID: 463
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DiagnosticsPhotoHandler : IPhotoHandler
	{
		// Token: 0x06001173 RID: 4467 RVA: 0x00048A9C File Offset: 0x00046C9C
		public DiagnosticsPhotoHandler(ITracer upstreamTracer)
		{
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00048AC4 File Offset: 0x00046CC4
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			if (request.Trace)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Diagnostics photo handler: skipped because request is already being traced.");
				return response;
			}
			if (this.ShouldTraceRequest(response))
			{
				request.Trace = true;
				this.tracer.TraceDebug((long)this.GetHashCode(), "Diagnostics photo handler: enabled tracing on this request.");
			}
			return response;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00048B1A File Offset: 0x00046D1A
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00048B23 File Offset: 0x00046D23
		private bool ShouldTraceRequest(PhotoResponse response)
		{
			return response.PhotoWrittenToFileSystem;
		}

		// Token: 0x04000940 RID: 2368
		private readonly ITracer tracer;
	}
}
