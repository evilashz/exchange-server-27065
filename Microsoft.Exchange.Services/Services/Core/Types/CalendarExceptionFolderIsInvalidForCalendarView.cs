using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006ED RID: 1773
	internal class CalendarExceptionFolderIsInvalidForCalendarView : ServicePermanentException
	{
		// Token: 0x06003616 RID: 13846 RVA: 0x000C1D6D File Offset: 0x000BFF6D
		public CalendarExceptionFolderIsInvalidForCalendarView() : base((CoreResources.IDs)2989650895U)
		{
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000C1D7F File Offset: 0x000BFF7F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
