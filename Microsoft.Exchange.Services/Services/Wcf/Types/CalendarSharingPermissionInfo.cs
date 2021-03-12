using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A28 RID: 2600
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarSharingPermissionInfo
	{
		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x00102585 File Offset: 0x00100785
		// (set) Token: 0x06004946 RID: 18758 RVA: 0x0010258D File Offset: 0x0010078D
		[DataMember]
		public EmailAddressWrapper EmailAddress { get; set; }

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x00102596 File Offset: 0x00100796
		// (set) Token: 0x06004948 RID: 18760 RVA: 0x0010259E File Offset: 0x0010079E
		[DataMember]
		public string CurrentDetailLevel { get; set; }

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x001025A7 File Offset: 0x001007A7
		// (set) Token: 0x0600494A RID: 18762 RVA: 0x001025AF File Offset: 0x001007AF
		[DataMember]
		public string ID { get; set; }

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x001025B8 File Offset: 0x001007B8
		// (set) Token: 0x0600494C RID: 18764 RVA: 0x001025C0 File Offset: 0x001007C0
		[DataMember]
		public string[] AllowedDetailLevels { get; set; }

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600494D RID: 18765 RVA: 0x001025C9 File Offset: 0x001007C9
		// (set) Token: 0x0600494E RID: 18766 RVA: 0x001025D1 File Offset: 0x001007D1
		[DataMember]
		public bool IsInsideOrganization { get; set; }

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x001025DA File Offset: 0x001007DA
		// (set) Token: 0x06004950 RID: 18768 RVA: 0x001025E2 File Offset: 0x001007E2
		[DataMember]
		public bool FromPermissionsTable { get; set; }

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06004951 RID: 18769 RVA: 0x001025EB File Offset: 0x001007EB
		// (set) Token: 0x06004952 RID: 18770 RVA: 0x001025F3 File Offset: 0x001007F3
		[DataMember]
		public string BrowseUrl { get; set; }

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x001025FC File Offset: 0x001007FC
		// (set) Token: 0x06004954 RID: 18772 RVA: 0x00102604 File Offset: 0x00100804
		[DataMember]
		public string ICalUrl { get; set; }

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x0010260D File Offset: 0x0010080D
		// (set) Token: 0x06004956 RID: 18774 RVA: 0x00102615 File Offset: 0x00100815
		[DataMember]
		public string HandlerType { get; set; }

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x0010261E File Offset: 0x0010081E
		// (set) Token: 0x06004958 RID: 18776 RVA: 0x00102626 File Offset: 0x00100826
		[DataMember]
		public bool ViewPrivateAppointments { get; set; }
	}
}
