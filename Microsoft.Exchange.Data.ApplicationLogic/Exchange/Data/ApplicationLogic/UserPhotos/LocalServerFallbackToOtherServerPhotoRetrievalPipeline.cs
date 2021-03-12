using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E7 RID: 487
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LocalServerFallbackToOtherServerPhotoRetrievalPipeline : IPhotoHandler
	{
		// Token: 0x060011DC RID: 4572 RVA: 0x0004B0D8 File Offset: 0x000492D8
		public LocalServerFallbackToOtherServerPhotoRetrievalPipeline(PhotosConfiguration configuration, string clientInfo, IRecipientSession recipientSession, IXSOFactory xsoFactory, string certificateValidationComponentId, IPhotoServiceLocatorFactory serviceLocatorFactory, IPhotoRequestOutboundWebProxyProvider outgoingRequestProxyProvider, ITracer upstreamTracer)
		{
			this.configuration = configuration;
			this.certificateValidationComponentId = certificateValidationComponentId;
			this.recipientSession = recipientSession;
			this.serviceLocatorFactory = serviceLocatorFactory;
			this.outgoingRequestProxyProvider = outgoingRequestProxyProvider;
			this.tracer = upstreamTracer;
			this.localServerPipeline = new LocalServerPhotoRetrievalPipeline(configuration, clientInfo, recipientSession, xsoFactory, upstreamTracer);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004B12B File Offset: 0x0004932B
		public PhotoResponse Retrieve(PhotoRequest request, Stream outputStream)
		{
			return this.Retrieve(request, new PhotoResponse(outputStream));
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004B13C File Offset: 0x0004933C
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			PhotoResponse result;
			try
			{
				result = this.localServerPipeline.Retrieve(request, response);
			}
			catch (WrongServerException)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "LOCAL SERVER WITH FALLBACK TO OTHER SERVER PIPELINE: target mailbox is NOT on this server.  Falling back to other server.");
				result = this.FallbackToOtherServer(request, response);
			}
			return result;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0004B190 File Offset: 0x00049390
		public IPhotoHandler Then(IPhotoHandler next)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0004B197 File Offset: 0x00049397
		private PhotoResponse FallbackToOtherServer(PhotoRequest request, PhotoResponse response)
		{
			request.PerformanceLogger.Log("WrongRoutingDetectedThenFallbackToOtherServer", string.Empty, 1U);
			return this.CreateOtherServerPipeline(request).Retrieve(request, response);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004B1BD File Offset: 0x000493BD
		private LocalForestOtherServerPhotoRetrievalPipeline CreateOtherServerPipeline(PhotoRequest request)
		{
			return new LocalForestOtherServerPhotoRetrievalPipeline(this.configuration, this.certificateValidationComponentId, this.serviceLocatorFactory.CreateForLocalForest(request.PerformanceLogger), this.recipientSession, this.outgoingRequestProxyProvider, this.tracer);
		}

		// Token: 0x04000981 RID: 2433
		private readonly LocalServerPhotoRetrievalPipeline localServerPipeline;

		// Token: 0x04000982 RID: 2434
		private readonly PhotosConfiguration configuration;

		// Token: 0x04000983 RID: 2435
		private readonly string certificateValidationComponentId;

		// Token: 0x04000984 RID: 2436
		private readonly IRecipientSession recipientSession;

		// Token: 0x04000985 RID: 2437
		private readonly IPhotoServiceLocatorFactory serviceLocatorFactory;

		// Token: 0x04000986 RID: 2438
		private readonly IPhotoRequestOutboundWebProxyProvider outgoingRequestProxyProvider;

		// Token: 0x04000987 RID: 2439
		private readonly ITracer tracer;
	}
}
