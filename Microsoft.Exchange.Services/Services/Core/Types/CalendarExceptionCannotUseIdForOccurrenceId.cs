using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E9 RID: 1769
	internal class CalendarExceptionCannotUseIdForOccurrenceId : ServicePermanentException
	{
		// Token: 0x0600360E RID: 13838 RVA: 0x000C1D09 File Offset: 0x000BFF09
		public CalendarExceptionCannotUseIdForOccurrenceId() : base((CoreResources.IDs)4180336284U)
		{
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000C1D1B File Offset: 0x000BFF1B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
