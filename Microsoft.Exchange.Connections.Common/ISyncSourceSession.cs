using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncSourceSession
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008D RID: 141
		string Protocol { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008E RID: 142
		string SessionId { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008F RID: 143
		string Server { get; }
	}
}
