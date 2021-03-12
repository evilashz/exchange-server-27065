using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000085 RID: 133
	internal class IndexDeliveryAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x000182AC File Offset: 0x000164AC
		public IndexDeliveryAgentFactory()
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("IndexDeliveryAgentFactory", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.IndexDeliveryAgentTracer, (long)this.GetHashCode());
			this.enabled = new SearchConfig().IndexAgentEnabled;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000182EA File Offset: 0x000164EA
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			if (this.enabled)
			{
				return new IndexDeliveryAgent();
			}
			return new IndexDeliveryAgentFactory.NoopDeliveryAgent();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000182FF File Offset: 0x000164FF
		public override void Close()
		{
			this.diagnosticsSession.TraceDebug("Factory closed", new object[0]);
		}

		// Token: 0x0400029D RID: 669
		private const string ComponentName = "IndexDeliveryAgentFactory";

		// Token: 0x0400029E RID: 670
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400029F RID: 671
		private readonly bool enabled;

		// Token: 0x02000086 RID: 134
		private class NoopDeliveryAgent : StoreDriverDeliveryAgent
		{
		}
	}
}
