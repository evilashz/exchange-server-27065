using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class EasConstants
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000025C6 File Offset: 0x000007C6
		internal static int MaxWorkerThreadsPerProc
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x0400000D RID: 13
		internal const string UserAgentString = "MRS-EASConnection-UserAgent";

		// Token: 0x0400000E RID: 14
		internal const EasProtocolVersion DefaultEasProtocolVersion = EasProtocolVersion.Version140;
	}
}
