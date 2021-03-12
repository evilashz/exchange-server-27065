using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E8 RID: 1768
	internal class CalendarExceptionCannotUpdateDeletedItem : ServicePermanentException
	{
		// Token: 0x0600360C RID: 13836 RVA: 0x000C1CF0 File Offset: 0x000BFEF0
		public CalendarExceptionCannotUpdateDeletedItem() : base((CoreResources.IDs)3843271914U)
		{
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000C1D02 File Offset: 0x000BFF02
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
