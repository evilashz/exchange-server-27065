using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200022B RID: 555
	internal struct EnhancedDnsRequestContext
	{
		// Token: 0x060018CB RID: 6347 RVA: 0x00063F70 File Offset: 0x00062170
		public EnhancedDnsRequestContext(SmtpSendConnectorConfig destinationConnector)
		{
			this = new EnhancedDnsRequestContext(null, destinationConnector, null);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00063F7B File Offset: 0x0006217B
		public EnhancedDnsRequestContext(IEnumerable<INextHopServer> destinationServers, SmtpSendConnectorConfig destinationConnector, SmtpSendConnectorConfig proxyConnector)
		{
			this.DestinationServers = destinationServers;
			this.DestinationConnector = destinationConnector;
			this.ProxyConnector = proxyConnector;
		}

		// Token: 0x04000BDF RID: 3039
		public readonly IEnumerable<INextHopServer> DestinationServers;

		// Token: 0x04000BE0 RID: 3040
		public readonly SmtpSendConnectorConfig DestinationConnector;

		// Token: 0x04000BE1 RID: 3041
		public readonly SmtpSendConnectorConfig ProxyConnector;
	}
}
