using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class Pop3Constants
	{
		// Token: 0x04000081 RID: 129
		internal const int DefaultPort = 110;

		// Token: 0x04000082 RID: 130
		internal static readonly TimeSpan PopConnectionTimeout = TimeSpan.FromHours(3.0);
	}
}
