using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F21 RID: 3873
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IUpdatableItem
	{
		// Token: 0x06008539 RID: 34105
		bool UpdateWith(IUpdatableItem newItem);
	}
}
