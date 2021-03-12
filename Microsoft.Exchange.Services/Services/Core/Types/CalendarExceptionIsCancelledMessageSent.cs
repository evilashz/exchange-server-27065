using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F8 RID: 1784
	internal class CalendarExceptionIsCancelledMessageSent : ServicePermanentException
	{
		// Token: 0x06003637 RID: 13879 RVA: 0x000C20D1 File Offset: 0x000C02D1
		public CalendarExceptionIsCancelledMessageSent() : base((CoreResources.IDs)3167358706U)
		{
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x000C20E3 File Offset: 0x000C02E3
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
