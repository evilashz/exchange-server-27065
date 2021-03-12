using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E5 RID: 485
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LocalForestOtherServerPhotoRetrievalPipeline : IPhotoHandler
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x0004AD69 File Offset: 0x00048F69
		public LocalForestOtherServerPhotoRetrievalPipeline(PhotosConfiguration configuration, string certificateValidationComponentId, IPhotoServiceLocator serviceLocator, IRecipientSession recipientSession, IPhotoRequestOutboundWebProxyProvider outgoingRequestProxyProvider, ITracer upstreamTracer)
		{
			this.pipeline = new HttpPhotoHandler(configuration, this.CreateOutboundSender(certificateValidationComponentId, upstreamTracer), serviceLocator, outgoingRequestProxyProvider, upstreamTracer).Then(new ADPhotoHandler(new ADPhotoReader(upstreamTracer), recipientSession, upstreamTracer));
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004ADA0 File Offset: 0x00048FA0
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("response", response);
			return this.pipeline.Retrieve(request, response);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004ADC5 File Offset: 0x00048FC5
		public IPhotoHandler Then(IPhotoHandler next)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004ADCC File Offset: 0x00048FCC
		private IPhotoRequestOutboundSender CreateOutboundSender(string certificateValidationComponentId, ITracer tracer)
		{
			return new PhotoRequestOutboundSender(this.CreateOutboundAuthenticator(certificateValidationComponentId, tracer));
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004ADDB File Offset: 0x00048FDB
		private IPhotoRequestOutboundAuthenticator CreateOutboundAuthenticator(string certificateValidationComponentId, ITracer tracer)
		{
			return new LocalForestOtherServerOutboundAuthenticator(certificateValidationComponentId, tracer);
		}

		// Token: 0x0400097B RID: 2427
		private readonly IPhotoHandler pipeline;
	}
}
