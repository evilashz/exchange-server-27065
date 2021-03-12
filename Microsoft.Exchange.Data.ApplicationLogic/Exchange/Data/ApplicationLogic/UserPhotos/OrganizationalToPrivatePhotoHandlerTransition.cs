using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001EE RID: 494
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationalToPrivatePhotoHandlerTransition : IPhotoHandler
	{
		// Token: 0x0600122B RID: 4651 RVA: 0x0004CE23 File Offset: 0x0004B023
		public OrganizationalToPrivatePhotoHandlerTransition(ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.tracer = upstreamTracer;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0004CE40 File Offset: 0x0004B040
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("response", response);
			if (!response.Served)
			{
				return response;
			}
			response.OrganizationalToPrivateHandlerTransitionProcessed = true;
			HttpStatusCode status = response.Status;
			if (status <= HttpStatusCode.NotModified)
			{
				if (status != HttpStatusCode.OK)
				{
					switch (status)
					{
					case HttpStatusCode.Found:
					case HttpStatusCode.NotModified:
						break;
					case HttpStatusCode.SeeOther:
						goto IL_6C;
					default:
						goto IL_6C;
					}
				}
				return response;
			}
			if (status != HttpStatusCode.NotFound && status != HttpStatusCode.InternalServerError)
			{
			}
			IL_6C:
			this.tracer.TraceDebug<HttpStatusCode>((long)this.GetHashCode(), "ORGANIZATIONAL to PRIVATE HANDLER TRANSITION: resetting response.  Original status: {0}", response.Status);
			response.Served = false;
			response.Status = HttpStatusCode.NotFound;
			response.ContentLength = -1L;
			response.ContentType = null;
			response.Thumbprint = null;
			response.HttpExpiresHeader = string.Empty;
			response.PhotoUrl = string.Empty;
			response.ServerCacheHit = false;
			return response;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0004CF24 File Offset: 0x0004B124
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x040009AE RID: 2478
		private readonly ITracer tracer;
	}
}
