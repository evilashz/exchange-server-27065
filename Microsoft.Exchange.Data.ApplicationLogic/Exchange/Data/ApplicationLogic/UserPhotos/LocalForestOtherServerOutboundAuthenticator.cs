using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E4 RID: 484
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LocalForestOtherServerOutboundAuthenticator : IPhotoRequestOutboundAuthenticator
	{
		// Token: 0x060011CF RID: 4559 RVA: 0x0004AC9C File Offset: 0x00048E9C
		public LocalForestOtherServerOutboundAuthenticator(string certificateValidationComponentId, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("certificateValidationComponentId", certificateValidationComponentId);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.certificateValidationComponentId = certificateValidationComponentId;
			this.tracer = upstreamTracer;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004ACE4 File Offset: 0x00048EE4
		public HttpWebResponse AuthenticateAndGetResponse(HttpWebRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			NetworkServiceImpersonator.Initialize();
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "LOCAL FOREST OTHER SERVER OUTBOUND AUTHENTICATOR: stamping component id '{0}' onto request.", this.certificateValidationComponentId);
			CertificateValidationManager.SetComponentId(request, this.certificateValidationComponentId);
			request.PreAuthenticate = true;
			return HttpAuthenticator.NetworkService.AuthenticateAndExecute<HttpWebResponse>(request, () => (HttpWebResponse)request.GetResponse());
		}

		// Token: 0x04000979 RID: 2425
		private readonly string certificateValidationComponentId;

		// Token: 0x0400097A RID: 2426
		private readonly ITracer tracer;
	}
}
