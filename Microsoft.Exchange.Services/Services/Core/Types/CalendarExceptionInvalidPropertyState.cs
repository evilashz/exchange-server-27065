using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F3 RID: 1779
	internal class CalendarExceptionInvalidPropertyState : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x0600362A RID: 13866 RVA: 0x000C1F8E File Offset: 0x000C018E
		public CalendarExceptionInvalidPropertyState(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorCalendarInvalidPropertyState, propertyPath)
		{
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000C1FA1 File Offset: 0x000C01A1
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
