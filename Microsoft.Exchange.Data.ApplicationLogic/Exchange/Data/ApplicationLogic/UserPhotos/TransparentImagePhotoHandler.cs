using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000219 RID: 537
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransparentImagePhotoHandler : IPhotoHandler
	{
		// Token: 0x06001368 RID: 4968 RVA: 0x00050404 File Offset: 0x0004E604
		public TransparentImagePhotoHandler(PhotosConfiguration configuration, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.configuration = configuration;
			this.tracer = upstreamTracer;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00050430 File Offset: 0x0004E630
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("response", response);
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
						goto IL_5B;
					default:
						goto IL_5B;
					}
				}
				return response;
			}
			if (status != HttpStatusCode.NotFound && status != HttpStatusCode.InternalServerError)
			{
			}
			IL_5B:
			response.TransparentImageHandlerProcessed = true;
			this.tracer.TraceDebug((long)this.GetHashCode(), "TRANSPARENT IMAGE HANDLER: responding request with HTTP 200 OK and a transparent image.");
			response.Served = true;
			response.Status = HttpStatusCode.OK;
			response.HttpExpiresHeader = UserAgentPhotoExpiresHeader.Default.ComputeExpiresHeader(DateTime.UtcNow, HttpStatusCode.NotFound, this.configuration);
			response.ContentType = "image/gif";
			response.ContentLength = (long)TransparentImagePhotoHandler.Clear1x1GIF.Length;
			response.OutputPhotoStream.Write(TransparentImagePhotoHandler.Clear1x1GIF, 0, TransparentImagePhotoHandler.Clear1x1GIF.Length);
			return response;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0005051A File Offset: 0x0004E71A
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x04000AD2 RID: 2770
		private static readonly byte[] Clear1x1GIF = new byte[]
		{
			71,
			73,
			70,
			56,
			57,
			97,
			1,
			0,
			1,
			0,
			128,
			0,
			0,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			33,
			249,
			4,
			1,
			0,
			0,
			1,
			0,
			44,
			0,
			0,
			0,
			0,
			1,
			0,
			1,
			0,
			0,
			2,
			1,
			76,
			0,
			59
		};

		// Token: 0x04000AD3 RID: 2771
		private readonly PhotosConfiguration configuration;

		// Token: 0x04000AD4 RID: 2772
		private readonly ITracer tracer;
	}
}
