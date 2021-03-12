using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A27 RID: 2599
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarSharingPermissionsResponse : CalendarActionResponse
	{
		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x00102541 File Offset: 0x00100741
		// (set) Token: 0x0600493E RID: 18750 RVA: 0x00102549 File Offset: 0x00100749
		[DataMember]
		public CalendarSharingPermissionInfo[] Recipients { get; set; }

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x00102552 File Offset: 0x00100752
		// (set) Token: 0x06004940 RID: 18752 RVA: 0x0010255A File Offset: 0x0010075A
		[DataMember]
		public string AnonymousSharingMaxDetailLevel { get; set; }

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x00102563 File Offset: 0x00100763
		// (set) Token: 0x06004942 RID: 18754 RVA: 0x0010256B File Offset: 0x0010076B
		[DataMember]
		public bool IsAnonymousSharingEnabled { get; set; }

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x00102574 File Offset: 0x00100774
		// (set) Token: 0x06004944 RID: 18756 RVA: 0x0010257C File Offset: 0x0010077C
		[DataMember]
		public DeliverMeetingRequestsType CurrentDeliveryOption { get; set; }
	}
}
