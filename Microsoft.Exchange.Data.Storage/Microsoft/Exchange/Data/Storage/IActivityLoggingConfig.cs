using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F0F RID: 3855
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IActivityLoggingConfig
	{
		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x060084B0 RID: 33968
		TimeSpan MaxLogFileAge { get; }

		// Token: 0x1700232F RID: 9007
		// (get) Token: 0x060084B1 RID: 33969
		ByteQuantifiedSize MaxLogDirectorySize { get; }

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x060084B2 RID: 33970
		ByteQuantifiedSize MaxLogFileSize { get; }

		// Token: 0x17002331 RID: 9009
		// (get) Token: 0x060084B3 RID: 33971
		bool IsDumpCollectionEnabled { get; }
	}
}
