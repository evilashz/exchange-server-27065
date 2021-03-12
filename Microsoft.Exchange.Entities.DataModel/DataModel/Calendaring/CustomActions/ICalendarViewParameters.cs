using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x0200003F RID: 63
	public interface ICalendarViewParameters
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000143 RID: 323
		ExDateTime EffectiveEndTime { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000144 RID: 324
		ExDateTime EffectiveStartTime { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000145 RID: 325
		bool HasExplicitEndTime { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000146 RID: 326
		bool HasExplicitStartTime { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000147 RID: 327
		bool IsDefaultView { get; }
	}
}
