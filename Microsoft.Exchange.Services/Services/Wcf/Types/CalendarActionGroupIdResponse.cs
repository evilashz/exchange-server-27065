using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A0E RID: 2574
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarActionGroupIdResponse : CalendarActionResponse
	{
		// Token: 0x06004898 RID: 18584 RVA: 0x00101A0F File Offset: 0x000FFC0F
		public CalendarActionGroupIdResponse(CalendarActionError errorCode) : base(errorCode)
		{
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x00101A18 File Offset: 0x000FFC18
		public CalendarActionGroupIdResponse(Guid groupClassId, ItemId groupItemId)
		{
			this.NewGroupClassId = groupClassId.ToString();
			this.NewGroupItemId = groupItemId;
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x0600489A RID: 18586 RVA: 0x00101A3A File Offset: 0x000FFC3A
		// (set) Token: 0x0600489B RID: 18587 RVA: 0x00101A42 File Offset: 0x000FFC42
		[DataMember]
		public string NewGroupClassId { get; set; }

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x0600489C RID: 18588 RVA: 0x00101A4B File Offset: 0x000FFC4B
		// (set) Token: 0x0600489D RID: 18589 RVA: 0x00101A53 File Offset: 0x000FFC53
		[DataMember]
		public ItemId NewGroupItemId { get; set; }
	}
}
