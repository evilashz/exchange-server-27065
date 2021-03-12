using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200025B RID: 603
	[AttributeUsage(AttributeTargets.Property)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ProvisionalClone : ProvisionalCloneBase
	{
		// Token: 0x06001D38 RID: 7480 RVA: 0x00079159 File Offset: 0x00077359
		public ProvisionalClone(CloneSet contexts = CloneSet.CloneExtendedSet) : base(contexts)
		{
		}
	}
}
