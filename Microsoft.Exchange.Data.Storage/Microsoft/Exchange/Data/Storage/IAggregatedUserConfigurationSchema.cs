using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001C4 RID: 452
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAggregatedUserConfigurationSchema
	{
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001860 RID: 6240
		IEnumerable<AggregatedUserConfigurationDescriptor> All { get; }
	}
}
