using System;
using System.Net;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000070 RID: 112
	internal interface INextHopServer
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600024C RID: 588
		bool IsIPAddress { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600024D RID: 589
		IPAddress Address { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600024E RID: 590
		string Fqdn { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600024F RID: 591
		bool IsFrontendAndHubColocatedServer { get; }
	}
}
