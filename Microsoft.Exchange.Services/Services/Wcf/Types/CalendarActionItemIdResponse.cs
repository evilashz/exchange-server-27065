using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A0C RID: 2572
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarActionItemIdResponse : CalendarActionResponse
	{
		// Token: 0x06004890 RID: 18576 RVA: 0x001019BC File Offset: 0x000FFBBC
		public CalendarActionItemIdResponse(CalendarActionError errorCode) : base(errorCode)
		{
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x001019C5 File Offset: 0x000FFBC5
		public CalendarActionItemIdResponse(ItemId calendarEntryId)
		{
			this.NewCalendarEntryId = calendarEntryId;
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x001019D4 File Offset: 0x000FFBD4
		// (set) Token: 0x06004893 RID: 18579 RVA: 0x001019DC File Offset: 0x000FFBDC
		[DataMember]
		public ItemId NewCalendarEntryId { get; set; }
	}
}
