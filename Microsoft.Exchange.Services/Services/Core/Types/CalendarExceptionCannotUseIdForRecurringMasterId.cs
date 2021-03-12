using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006EA RID: 1770
	internal class CalendarExceptionCannotUseIdForRecurringMasterId : ServicePermanentException
	{
		// Token: 0x06003610 RID: 13840 RVA: 0x000C1D22 File Offset: 0x000BFF22
		public CalendarExceptionCannotUseIdForRecurringMasterId() : base(CoreResources.IDs.ErrorCalendarCannotUseIdForRecurringMasterId)
		{
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000C1D34 File Offset: 0x000BFF34
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
