using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000152 RID: 338
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class FastTransferObject : BaseObject
	{
		// Token: 0x06000635 RID: 1589 RVA: 0x000113E3 File Offset: 0x0000F5E3
		protected FastTransferObject(bool isTopLevel)
		{
			this.isTopLevel = isTopLevel;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x000113F2 File Offset: 0x0000F5F2
		protected bool IsTopLevel
		{
			get
			{
				return this.isTopLevel;
			}
		}

		// Token: 0x04000336 RID: 822
		private readonly bool isTopLevel;
	}
}
