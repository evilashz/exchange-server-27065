using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006EC RID: 1772
	internal class CalendarExceptionEndDateIsEarlierThanStartDate : ServicePermanentException
	{
		// Token: 0x06003614 RID: 13844 RVA: 0x000C1D54 File Offset: 0x000BFF54
		public CalendarExceptionEndDateIsEarlierThanStartDate() : base((CoreResources.IDs)4006585486U)
		{
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x000C1D66 File Offset: 0x000BFF66
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
