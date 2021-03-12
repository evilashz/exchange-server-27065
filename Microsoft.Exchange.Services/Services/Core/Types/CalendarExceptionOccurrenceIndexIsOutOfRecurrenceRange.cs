using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006FF RID: 1791
	internal class CalendarExceptionOccurrenceIndexIsOutOfRecurrenceRange : ServicePermanentException
	{
		// Token: 0x06003647 RID: 13895 RVA: 0x000C22CC File Offset: 0x000C04CC
		public CalendarExceptionOccurrenceIndexIsOutOfRecurrenceRange() : base(CoreResources.IDs.ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange)
		{
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x000C22DE File Offset: 0x000C04DE
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
