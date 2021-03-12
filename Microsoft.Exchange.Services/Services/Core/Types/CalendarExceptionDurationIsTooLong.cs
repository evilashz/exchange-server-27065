using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006EB RID: 1771
	internal class CalendarExceptionDurationIsTooLong : ServicePermanentException
	{
		// Token: 0x06003612 RID: 13842 RVA: 0x000C1D3B File Offset: 0x000BFF3B
		public CalendarExceptionDurationIsTooLong() : base((CoreResources.IDs)2484699530U)
		{
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000C1D4D File Offset: 0x000BFF4D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
