using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006FC RID: 1788
	internal class CalendarExceptionMeetingIsOutOfDateResponseNotProcessed : ServicePermanentException
	{
		// Token: 0x06003641 RID: 13889 RVA: 0x000C2281 File Offset: 0x000C0481
		public CalendarExceptionMeetingIsOutOfDateResponseNotProcessed() : base((CoreResources.IDs)3573754788U)
		{
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000C2293 File Offset: 0x000C0493
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
