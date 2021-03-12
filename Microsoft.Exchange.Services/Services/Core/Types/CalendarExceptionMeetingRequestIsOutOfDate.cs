using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006FE RID: 1790
	internal class CalendarExceptionMeetingRequestIsOutOfDate : ServicePermanentException
	{
		// Token: 0x06003645 RID: 13893 RVA: 0x000C22B3 File Offset: 0x000C04B3
		public CalendarExceptionMeetingRequestIsOutOfDate() : base((CoreResources.IDs)3227656327U)
		{
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000C22C5 File Offset: 0x000C04C5
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
