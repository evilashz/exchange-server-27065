using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200025C RID: 604
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class ProvisionalCloneOnce : ProvisionalCloneBase
	{
		// Token: 0x06001D39 RID: 7481 RVA: 0x00079162 File Offset: 0x00077362
		public ProvisionalCloneOnce(CloneSet contexts = CloneSet.CloneExtendedSet) : base(contexts)
		{
		}
	}
}
