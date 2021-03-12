using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006FB RID: 1787
	internal class CalendarExceptionIsOrganizer : ServicePermanentException
	{
		// Token: 0x0600363E RID: 13886 RVA: 0x000C21C4 File Offset: 0x000C03C4
		static CalendarExceptionIsOrganizer()
		{
			CalendarExceptionIsOrganizer.errorMappings.Add(typeof(AcceptItemType).Name, (CoreResources.IDs)2633097826U);
			CalendarExceptionIsOrganizer.errorMappings.Add(typeof(DeclineItemType).Name, (CoreResources.IDs)2980490932U);
			CalendarExceptionIsOrganizer.errorMappings.Add(typeof(TentativelyAcceptItemType).Name, (CoreResources.IDs)3371251772U);
			CalendarExceptionIsOrganizer.errorMappings.Add(typeof(RemoveItemType).Name, CoreResources.IDs.ErrorCalendarIsOrganizerForRemove);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000C2267 File Offset: 0x000C0467
		public CalendarExceptionIsOrganizer(string operation) : base(CalendarExceptionIsOrganizer.errorMappings[operation])
		{
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x000C227A File Offset: 0x000C047A
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x04001E3E RID: 7742
		private static Dictionary<string, Enum> errorMappings = new Dictionary<string, Enum>();
	}
}
