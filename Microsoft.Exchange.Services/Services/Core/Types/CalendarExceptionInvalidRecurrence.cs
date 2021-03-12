using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F5 RID: 1781
	internal class CalendarExceptionInvalidRecurrence : ServicePermanentException
	{
		// Token: 0x0600362F RID: 13871 RVA: 0x000C1FD6 File Offset: 0x000C01D6
		public CalendarExceptionInvalidRecurrence() : base(CoreResources.IDs.ErrorCalendarInvalidRecurrence)
		{
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000C1FE8 File Offset: 0x000C01E8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
