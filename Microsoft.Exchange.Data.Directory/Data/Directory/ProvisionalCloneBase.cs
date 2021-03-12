using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200025A RID: 602
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[AttributeUsage(AttributeTargets.Property)]
	internal class ProvisionalCloneBase : Attribute
	{
		// Token: 0x06001D37 RID: 7479 RVA: 0x0007914A File Offset: 0x0007734A
		public ProvisionalCloneBase(CloneSet contexts)
		{
			this.CloneSet = contexts;
		}

		// Token: 0x04000DE2 RID: 3554
		public CloneSet CloneSet;
	}
}
