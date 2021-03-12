using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000083 RID: 131
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Message
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x0001964C File Offset: 0x0001784C
		internal Message()
		{
			this.includeField = new Include();
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001965F File Offset: 0x0001785F
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00019667 File Offset: 0x00017867
		internal Include Include
		{
			get
			{
				return this.includeField;
			}
			set
			{
				this.includeField = value;
			}
		}

		// Token: 0x04000311 RID: 785
		private Include includeField;
	}
}
