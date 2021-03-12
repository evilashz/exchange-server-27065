using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A2A RID: 2602
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarSharingRecipientInfoResponse : CalendarActionResponse
	{
		// Token: 0x06004965 RID: 18789 RVA: 0x00102812 File Offset: 0x00100A12
		public GetCalendarSharingRecipientInfoResponse(CalendarActionError errorCode) : base(errorCode)
		{
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x0010281B File Offset: 0x00100A1B
		public GetCalendarSharingRecipientInfoResponse()
		{
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06004967 RID: 18791 RVA: 0x00102823 File Offset: 0x00100A23
		// (set) Token: 0x06004968 RID: 18792 RVA: 0x0010282B File Offset: 0x00100A2B
		[DataMember]
		public ItemId CalendarId { get; set; }

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x00102834 File Offset: 0x00100A34
		// (set) Token: 0x0600496A RID: 18794 RVA: 0x0010283C File Offset: 0x00100A3C
		[DataMember]
		public CalendarSharingRecipientInfo[] Recipients { get; set; }
	}
}
