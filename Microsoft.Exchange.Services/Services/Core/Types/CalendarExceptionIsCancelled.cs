using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F7 RID: 1783
	internal class CalendarExceptionIsCancelled : ServicePermanentException
	{
		// Token: 0x06003634 RID: 13876 RVA: 0x000C2014 File Offset: 0x000C0214
		static CalendarExceptionIsCancelled()
		{
			CalendarExceptionIsCancelled.errorMappings.Add(typeof(AcceptItemType).Name, CoreResources.IDs.ErrorCalendarIsCancelledForAccept);
			CalendarExceptionIsCancelled.errorMappings.Add(typeof(DeclineItemType).Name, (CoreResources.IDs)2997278338U);
			CalendarExceptionIsCancelled.errorMappings.Add(typeof(TentativelyAcceptItemType).Name, CoreResources.IDs.ErrorCalendarIsCancelledForTentative);
			CalendarExceptionIsCancelled.errorMappings.Add(typeof(RemoveItemType).Name, (CoreResources.IDs)4064247940U);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000C20B7 File Offset: 0x000C02B7
		public CalendarExceptionIsCancelled(string operation) : base(CalendarExceptionIsCancelled.errorMappings[operation])
		{
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06003636 RID: 13878 RVA: 0x000C20CA File Offset: 0x000C02CA
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}

		// Token: 0x04001E3C RID: 7740
		private static Dictionary<string, Enum> errorMappings = new Dictionary<string, Enum>();
	}
}
