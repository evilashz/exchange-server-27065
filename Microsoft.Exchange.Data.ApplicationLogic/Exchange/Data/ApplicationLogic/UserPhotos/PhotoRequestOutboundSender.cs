using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000205 RID: 517
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PhotoRequestOutboundSender : IPhotoRequestOutboundSender
	{
		// Token: 0x060012B9 RID: 4793 RVA: 0x0004E43C File Offset: 0x0004C63C
		public PhotoRequestOutboundSender(IPhotoRequestOutboundAuthenticator authenticator)
		{
			ArgumentValidator.ThrowIfNull("authenticator", authenticator);
			this.authenticator = authenticator;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004E456 File Offset: 0x0004C656
		public HttpWebResponse SendAndGetResponse(HttpWebRequest request)
		{
			return this.authenticator.AuthenticateAndGetResponse(request);
		}

		// Token: 0x04000A5B RID: 2651
		private readonly IPhotoRequestOutboundAuthenticator authenticator;
	}
}
