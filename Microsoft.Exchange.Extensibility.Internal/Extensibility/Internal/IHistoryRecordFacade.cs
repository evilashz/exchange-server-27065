using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000051 RID: 81
	internal interface IHistoryRecordFacade
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002F5 RID: 757
		HistoryType Type { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002F6 RID: 758
		RoutingAddress Address { get; }
	}
}
