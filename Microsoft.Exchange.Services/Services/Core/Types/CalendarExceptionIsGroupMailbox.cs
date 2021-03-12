using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F9 RID: 1785
	internal class CalendarExceptionIsGroupMailbox : ServicePermanentException
	{
		// Token: 0x06003639 RID: 13881 RVA: 0x000C20EC File Offset: 0x000C02EC
		static CalendarExceptionIsGroupMailbox()
		{
			CalendarExceptionIsGroupMailbox.errorMappings.Add(typeof(AcceptItemType).Name, CoreResources.IDs.ErrorCalendarIsGroupMailboxForAccept);
			CalendarExceptionIsGroupMailbox.errorMappings.Add(typeof(DeclineItemType).Name, CoreResources.IDs.ErrorCalendarIsGroupMailboxForDecline);
			CalendarExceptionIsGroupMailbox.errorMappings.Add(typeof(TentativelyAcceptItemType).Name, (CoreResources.IDs)3187786876U);
			CalendarExceptionIsGroupMailbox.errorMappings.Add(typeof(SuppressReadReceiptType).Name, CoreResources.IDs.ErrorCalendarIsGroupMailboxForSuppressReadReceipt);
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000C218F File Offset: 0x000C038F
		public CalendarExceptionIsGroupMailbox(string operation) : base(CalendarExceptionIsGroupMailbox.errorMappings[operation])
		{
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000C21A2 File Offset: 0x000C03A2
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}

		// Token: 0x04001E3D RID: 7741
		private static Dictionary<string, Enum> errorMappings = new Dictionary<string, Enum>();
	}
}
