using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000206 RID: 518
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRequestOutboundWebProxyProviderUsingLocalServerConfiguration : IPhotoRequestOutboundWebProxyProvider
	{
		// Token: 0x060012BB RID: 4795 RVA: 0x0004E464 File Offset: 0x0004C664
		public PhotoRequestOutboundWebProxyProviderUsingLocalServerConfiguration(ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.tracer = upstreamTracer;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0004E480 File Offset: 0x0004C680
		public IWebProxy Create()
		{
			Server localServer = LocalServerCache.LocalServer;
			if (localServer == null || localServer.InternetWebProxy == null)
			{
				return null;
			}
			this.tracer.TraceDebug<Uri>((long)this.GetHashCode(), "OUTBOUND PROXY: a proxy is configured for the local server.  Proxy: {0}", localServer.InternetWebProxy);
			return new WebProxy(localServer.InternetWebProxy);
		}

		// Token: 0x04000A5C RID: 2652
		private readonly ITracer tracer;
	}
}
