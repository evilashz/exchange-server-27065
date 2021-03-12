using System;

namespace Microsoft.Exchange.MailboxLoadBalance.Clients
{
	// Token: 0x02000032 RID: 50
	internal interface IWcfClient
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000198 RID: 408
		bool IsValid { get; }

		// Token: 0x06000199 RID: 409
		void Disconnect();
	}
}
