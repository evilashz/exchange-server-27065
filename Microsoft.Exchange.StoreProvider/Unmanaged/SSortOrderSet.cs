using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002E6 RID: 742
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SSortOrderSet
	{
		// Token: 0x04001236 RID: 4662
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SSortOrderSet));

		// Token: 0x04001237 RID: 4663
		internal int cSorts;

		// Token: 0x04001238 RID: 4664
		internal int cCategories;

		// Token: 0x04001239 RID: 4665
		internal int cExpanded;

		// Token: 0x0400123A RID: 4666
		internal SSortOrder aSorts;
	}
}
