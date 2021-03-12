using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F1 RID: 1777
	internal class CalendarExceptionInvalidDayForTimeChangePattern : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003626 RID: 13862 RVA: 0x000C1F5A File Offset: 0x000C015A
		public CalendarExceptionInvalidDayForTimeChangePattern(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorCalendarInvalidDayForTimeChangePattern, propertyPath)
		{
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000C1F6D File Offset: 0x000C016D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
