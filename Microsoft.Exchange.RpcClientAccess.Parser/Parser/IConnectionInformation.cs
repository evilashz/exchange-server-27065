using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001AD RID: 429
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConnectionInformation
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000889 RID: 2185
		ushort SessionId { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600088A RID: 2186
		bool ClientSupportsBackoffResult { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600088B RID: 2187
		bool ClientSupportsBufferTooSmallBreakup { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600088C RID: 2188
		Encoding String8Encoding { get; }
	}
}
