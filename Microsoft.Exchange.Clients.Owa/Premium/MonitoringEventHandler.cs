using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004BC RID: 1212
	[OwaEventNamespace("Monitoring")]
	internal sealed class MonitoringEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002E4C RID: 11852 RVA: 0x00108AD6 File Offset: 0x00106CD6
		[OwaEvent("Ping")]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void Ping()
		{
		}

		// Token: 0x04001FE7 RID: 8167
		public const string EventNamespace = "Monitoring";
	}
}
