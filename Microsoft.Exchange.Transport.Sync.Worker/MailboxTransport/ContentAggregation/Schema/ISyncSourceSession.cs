using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x0200005A RID: 90
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncSourceSession
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000426 RID: 1062
		string Protocol { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000427 RID: 1063
		string SessionId { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000428 RID: 1064
		string Server { get; }
	}
}
