using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003E5 RID: 997
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IMonthlyThPatternInfo : IMonthlyPatternInfo
	{
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06002D81 RID: 11649
		RecurrenceOrderType Order { get; }
	}
}
