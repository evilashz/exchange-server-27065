using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200009A RID: 154
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ILifetimeTrackable
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006AC RID: 1708
		DateTime CreateTime { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006AD RID: 1709
		// (set) Token: 0x060006AE RID: 1710
		DateTime LastAccessTime { get; set; }
	}
}
