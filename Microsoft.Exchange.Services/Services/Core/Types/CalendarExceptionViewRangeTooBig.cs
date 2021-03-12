using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000703 RID: 1795
	internal class CalendarExceptionViewRangeTooBig : ServicePermanentException
	{
		// Token: 0x0600364F RID: 13903 RVA: 0x000C2336 File Offset: 0x000C0536
		public CalendarExceptionViewRangeTooBig() : base((CoreResources.IDs)2945703152U)
		{
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x000C2348 File Offset: 0x000C0548
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
