using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200040C RID: 1036
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RegeneratingPattern : IntervalRecurrencePattern
	{
		// Token: 0x06002EFF RID: 12031 RVA: 0x000C1474 File Offset: 0x000BF674
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			return value is RegeneratingPattern && ((RegeneratingPattern)value).RecurrenceInterval == base.RecurrenceInterval;
		}
	}
}
