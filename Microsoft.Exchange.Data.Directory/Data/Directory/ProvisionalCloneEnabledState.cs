using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200025D RID: 605
	[AttributeUsage(AttributeTargets.Property)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ProvisionalCloneEnabledState : ProvisionalCloneBase
	{
		// Token: 0x06001D3A RID: 7482 RVA: 0x0007916B File Offset: 0x0007736B
		public ProvisionalCloneEnabledState(CloneSet contexts = CloneSet.CloneExtendedSet) : base(contexts)
		{
		}
	}
}
