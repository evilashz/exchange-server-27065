using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006AF RID: 1711
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SinglePropertyQuery : Query
	{
		// Token: 0x0600454D RID: 17741 RVA: 0x0012722E File Offset: 0x0012542E
		protected SinglePropertyQuery(int index)
		{
			this.Index = index;
		}

		// Token: 0x040025F4 RID: 9716
		protected readonly int Index;
	}
}
