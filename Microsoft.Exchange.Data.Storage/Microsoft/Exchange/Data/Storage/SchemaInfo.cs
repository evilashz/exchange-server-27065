using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000816 RID: 2070
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SchemaInfo
	{
		// Token: 0x06004DB3 RID: 19891 RVA: 0x00144E89 File Offset: 0x00143089
		internal SchemaInfo(CalendarPropertyId calendarPropertyId, object promotionMethod, object demotionMethod, CalendarMethod methods, IcalFlags flags)
		{
			this.CalendarPropertyId = calendarPropertyId;
			this.PromotionMethod = promotionMethod;
			this.DemotionMethod = demotionMethod;
			this.Methods = methods;
			this.Flags = flags;
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x00144EB6 File Offset: 0x001430B6
		public bool IsMultiValue
		{
			get
			{
				return (this.Flags & IcalFlags.MultiValue) == IcalFlags.MultiValue;
			}
		}

		// Token: 0x04002A2D RID: 10797
		internal readonly CalendarPropertyId CalendarPropertyId;

		// Token: 0x04002A2E RID: 10798
		internal readonly object PromotionMethod;

		// Token: 0x04002A2F RID: 10799
		internal readonly object DemotionMethod;

		// Token: 0x04002A30 RID: 10800
		internal readonly CalendarMethod Methods;

		// Token: 0x04002A31 RID: 10801
		internal readonly IcalFlags Flags;
	}
}
