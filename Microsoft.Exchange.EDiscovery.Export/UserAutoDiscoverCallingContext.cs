using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000049 RID: 73
	internal sealed class UserAutoDiscoverCallingContext : UserServiceCallingContext<DefaultBinding_Autodiscover>
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x000171B0 File Offset: 0x000153B0
		public UserAutoDiscoverCallingContext(ICredentialHandler credentialHandler, IDictionary<string, ICredentials> cachedCredentials = null) : base(credentialHandler, cachedCredentials)
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000171BC File Offset: 0x000153BC
		public override void SetServiceUrl(DefaultBinding_Autodiscover binding, Uri targetUrl)
		{
			string leftPart = targetUrl.GetLeftPart(UriPartial.Path);
			Uri targetUrl2 = targetUrl;
			if (leftPart.EndsWith("/autodiscover.xml", StringComparison.OrdinalIgnoreCase))
			{
				targetUrl2 = new Uri(targetUrl.GetLeftPart(UriPartial.Authority) + "/autodiscover/autodiscover.svc");
			}
			else if (!leftPart.EndsWith("/autodiscover.svc", StringComparison.OrdinalIgnoreCase))
			{
				Tracer.TraceError("AutoDiscoverClient.GetUserEwsEndpoints: Unexpected redirected URL '{0}'", new object[]
				{
					targetUrl
				});
				throw new ExportException(ExportErrorType.UnexpectedAutoDiscoverServiceUrl, targetUrl.AbsoluteUri);
			}
			base.SetServiceUrl(binding, targetUrl2);
		}
	}
}
