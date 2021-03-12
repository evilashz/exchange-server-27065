using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A0A RID: 2570
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddSharedCalendarResponse : CalendarActionResponse
	{
		// Token: 0x0600488C RID: 18572 RVA: 0x00101993 File Offset: 0x000FFB93
		public AddSharedCalendarResponse(CalendarActionError errorCode) : base(errorCode)
		{
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x0010199C File Offset: 0x000FFB9C
		public AddSharedCalendarResponse(ItemId calendarEntryId)
		{
			this.NewCalendarEntryId = calendarEntryId;
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x0600488E RID: 18574 RVA: 0x001019AB File Offset: 0x000FFBAB
		// (set) Token: 0x0600488F RID: 18575 RVA: 0x001019B3 File Offset: 0x000FFBB3
		[DataMember]
		public ItemId NewCalendarEntryId { get; set; }
	}
}
