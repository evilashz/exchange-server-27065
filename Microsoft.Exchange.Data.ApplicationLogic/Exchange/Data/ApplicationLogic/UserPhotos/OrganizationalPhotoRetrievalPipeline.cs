using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001ED RID: 493
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OrganizationalPhotoRetrievalPipeline : IPhotoHandler
	{
		// Token: 0x06001227 RID: 4647 RVA: 0x0004CD3C File Offset: 0x0004AF3C
		public OrganizationalPhotoRetrievalPipeline(PhotosConfiguration configuration, string certificateValidationComponentId, string clientInfo, IRecipientSession recipientSession, IPhotoRequestOutboundWebProxyProvider outgoingRequestProxyProvider, IRemoteForestPhotoRetrievalPipelineFactory remoteForestPipelineFactory, IXSOFactory xsoFactory, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNullOrEmpty("certificateValidationComponentId", certificateValidationComponentId);
			ArgumentValidator.ThrowIfNullOrEmpty("clientInfo", clientInfo);
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("outgoingRequestProxyProvider", outgoingRequestProxyProvider);
			ArgumentValidator.ThrowIfNull("remoteForestPipelineFactory", remoteForestPipelineFactory);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.router = new PhotoRequestRouter(configuration, certificateValidationComponentId, clientInfo, recipientSession, new PhotoServiceLocatorFactory(tracer), outgoingRequestProxyProvider, remoteForestPipelineFactory, xsoFactory, tracer);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0004CDBF File Offset: 0x0004AFBF
		public PhotoResponse Retrieve(PhotoRequest request, Stream outputStream)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("outputStream", outputStream);
			return this.router.Route(request).Retrieve(request, new PhotoResponse(outputStream));
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0004CDEF File Offset: 0x0004AFEF
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("response", response);
			return this.router.Route(request).Retrieve(request, response);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004CE1A File Offset: 0x0004B01A
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x040009AD RID: 2477
		private readonly PhotoRequestRouter router;
	}
}
