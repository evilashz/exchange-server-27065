using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000052 RID: 82
	internal interface IHistoryFacade
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002F7 RID: 759
		RecipientP2Type RecipientType { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002F8 RID: 760
		List<IHistoryRecordFacade> Records { get; }
	}
}
