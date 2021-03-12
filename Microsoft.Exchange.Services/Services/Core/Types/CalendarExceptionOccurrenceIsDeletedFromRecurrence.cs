using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000700 RID: 1792
	internal class CalendarExceptionOccurrenceIsDeletedFromRecurrence : ServicePermanentException
	{
		// Token: 0x06003649 RID: 13897 RVA: 0x000C22E5 File Offset: 0x000C04E5
		public CalendarExceptionOccurrenceIsDeletedFromRecurrence() : base((CoreResources.IDs)3335161738U)
		{
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x000C22F7 File Offset: 0x000C04F7
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
