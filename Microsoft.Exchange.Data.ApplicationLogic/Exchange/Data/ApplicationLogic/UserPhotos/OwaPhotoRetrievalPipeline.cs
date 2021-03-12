using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001EF RID: 495
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OwaPhotoRetrievalPipeline
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x0004CF30 File Offset: 0x0004B130
		public OwaPhotoRetrievalPipeline(PhotosConfiguration configuration, string certificateValidationComponentId, string clientInfo, IRecipientSession recipientSession, IPhotoRequestOutboundWebProxyProvider outgoingRequestProxyProvider, IRemoteForestPhotoRetrievalPipelineFactory remoteForestPipelineFactory, IXSOFactory xsoFactory, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNullOrEmpty("certificateValidationComponentId", certificateValidationComponentId);
			ArgumentValidator.ThrowIfNullOrEmpty("clientInfo", clientInfo);
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("outgoingRequestProxyProvider", outgoingRequestProxyProvider);
			ArgumentValidator.ThrowIfNull("remoteForestPipelineFactory", remoteForestPipelineFactory);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.pipeline = new OrganizationalPhotoRetrievalPipeline(configuration, certificateValidationComponentId, clientInfo, recipientSession, outgoingRequestProxyProvider, remoteForestPipelineFactory, xsoFactory, upstreamTracer).Then(new OrganizationalToPrivatePhotoHandlerTransition(upstreamTracer)).Then(new PrivatePhotoHandler(configuration, xsoFactory, upstreamTracer)).Then(new TransparentImagePhotoHandler(configuration, upstreamTracer));
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0004CFE0 File Offset: 0x0004B1E0
		public PhotoResponse Retrieve(PhotoRequest request, Stream outputStream)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("outputStream", outputStream);
			return this.pipeline.Retrieve(request, new PhotoResponse(outputStream));
		}

		// Token: 0x040009AF RID: 2479
		private readonly IPhotoHandler pipeline;
	}
}
