using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002FC RID: 764
	internal interface IAgentRuntime
	{
		// Token: 0x06002191 RID: 8593
		ISmtpAgentSession NewSmtpAgentSession(ISmtpInSession smtpInSession, INetworkConnection networkConnection, bool isExternalConnection);

		// Token: 0x06002192 RID: 8594
		ISmtpAgentSession NewSmtpAgentSession(SmtpInSessionState sessionState, IIsMemberOfResolver<RoutingAddress> isMemberOfResolver, AcceptedDomainCollection firstOrgAcceptedDomains, RemoteDomainCollection remoteDomains, ServerVersion adminDisplayVersion, out IMExSession mexSession);

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002193 RID: 8595
		AcceptedDomainCollection FirstOrgAcceptedDomains { get; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002194 RID: 8596
		RemoteDomainCollection RemoteDomains { get; }
	}
}
