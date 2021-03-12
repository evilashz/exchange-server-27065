using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E7 RID: 1767
	internal class CalendarExceptionCannotMoveOrCopyOccurrence : ServicePermanentException
	{
		// Token: 0x0600360A RID: 13834 RVA: 0x000C1CD7 File Offset: 0x000BFED7
		public CalendarExceptionCannotMoveOrCopyOccurrence() : base(CoreResources.IDs.ErrorCalendarCannotMoveOrCopyOccurrence)
		{
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000C1CE9 File Offset: 0x000BFEE9
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
