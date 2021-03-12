using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000191 RID: 401
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessageChangePartial : IDisposable
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060007E7 RID: 2023
		PropertyGroupMapping PropertyGroupMapping { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007E8 RID: 2024
		IEnumerable<int> ChangedPropGroups { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060007E9 RID: 2025
		IEnumerable<PropertyTag> OtherGroupPropTags { get; }
	}
}
