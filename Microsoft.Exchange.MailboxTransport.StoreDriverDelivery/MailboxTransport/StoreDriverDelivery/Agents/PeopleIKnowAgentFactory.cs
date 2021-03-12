using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000AC RID: 172
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PeopleIKnowAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x0001EF54 File Offset: 0x0001D154
		public PeopleIKnowAgentFactory()
		{
			this.isAgentEnabled = this.ReadAgentEnabledValueFromConfigFile();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001EF68 File Offset: 0x0001D168
		private bool ReadAgentEnabledValueFromConfigFile()
		{
			return TransportAppConfig.GetConfigBool("PeopleIKnowAgentEnabled", true);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001EF75 File Offset: 0x0001D175
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new PeopleIKnowAgent(this.isAgentEnabled);
		}

		// Token: 0x0400033C RID: 828
		private const string PeopleIKnowAgentEnabled = "PeopleIKnowAgentEnabled";

		// Token: 0x0400033D RID: 829
		private readonly bool isAgentEnabled;
	}
}
