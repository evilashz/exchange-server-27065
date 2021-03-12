using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000702 RID: 1794
	internal class CalendarExceptionSeekToConditionNotSupported : ServicePermanentException
	{
		// Token: 0x0600364D RID: 13901 RVA: 0x000C2318 File Offset: 0x000C0518
		public CalendarExceptionSeekToConditionNotSupported() : base(ResponseCodeType.ErrorCalendarSeekToConditionNotSupported, CoreResources.ErrorCalendarSeekToConditionNotSupported("SeekToConditionPageView"))
		{
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x000C232F File Offset: 0x000C052F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
