using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006FA RID: 1786
	internal class CalendarExceptionIsNotOrganizer : ServicePermanentException
	{
		// Token: 0x0600363C RID: 13884 RVA: 0x000C21A9 File Offset: 0x000C03A9
		public CalendarExceptionIsNotOrganizer() : base(CoreResources.IDs.ErrorCalendarIsNotOrganizer)
		{
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000C21BB File Offset: 0x000C03BB
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
