using System;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Agent.SystemProbeDrop
{
	// Token: 0x02000003 RID: 3
	internal sealed class SystemProbeDropRoutingAgent : RoutingAgent
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021D1 File Offset: 0x000003D1
		public SystemProbeDropRoutingAgent()
		{
			base.OnCategorizedMessage += this.OnCategorizedMessageHandler;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021EB File Offset: 0x000003EB
		public void OnCategorizedMessageHandler(CategorizedMessageEventSource source, QueuedMessageEventArgs args)
		{
			if (SystemProbeDropHelper.IsAgentEnabled() && SystemProbeDropHelper.CheckMailItemHeaders(args.MailItem) && SystemProbeDropHelper.ShouldDropMessage(args.MailItem.MimeDocument.RootPart.Headers, "OnCategorizedMessage"))
			{
				source.Delete();
			}
		}
	}
}
