using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICoreState
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000415 RID: 1045
		// (set) Token: 0x06000416 RID: 1046
		Origin Origin { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000417 RID: 1047
		ItemLevel ItemLevel { get; }
	}
}
