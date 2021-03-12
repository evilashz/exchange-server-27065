using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200050C RID: 1292
	internal interface ISmtpOutConnectionHandler
	{
		// Token: 0x06003C73 RID: 15475
		void HandleConnection(NextHopConnection connection);

		// Token: 0x06003C74 RID: 15476
		void HandleProxyConnection(NextHopConnection connection, IEnumerable<INextHopServer> proxyDestinations, bool internalDestination, string connectionContextString);

		// Token: 0x06003C75 RID: 15477
		SmtpOutConnection NewBlindProxyConnection(NextHopConnection connection, IEnumerable<INextHopServer> proxyDestinations, bool clientProxy, SmtpSendConnectorConfig connector, TlsSendConfiguration tlsSendConfiguration, RiskLevel riskLevel, int outboundIPPool, int connectionAttempts, ISmtpInSession inSession, string connectionContextString);

		// Token: 0x06003C76 RID: 15478
		void HandleShadowConnection(NextHopConnection connection, IEnumerable<INextHopServer> shadowHubs);
	}
}
