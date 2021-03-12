using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Agent.TrustedMail
{
	// Token: 0x02000003 RID: 3
	public sealed class CentralizedMailFlowAgentFactory : RoutingAgentFactory
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002468 File Offset: 0x00000668
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			bool enabled = CentralizedMailFlowAgentFactory.isEdge && TrustedMailUtils.AcceptAnyRecipientOnPremises && TrustedMailUtils.TrustedMailAgentsEnabled;
			return new CentralizedMailFlowAgent(enabled);
		}

		// Token: 0x04000003 RID: 3
		private static bool isEdge = !(Components.Configuration.LocalServer.IsBridgehead | Components.Configuration.LocalServer.TransportServer.IsFrontendTransportServer);
	}
}
