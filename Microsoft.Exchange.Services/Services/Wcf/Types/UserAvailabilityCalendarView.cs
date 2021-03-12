using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A3D RID: 2621
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UserAvailabilityCalendarView
	{
		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06004A02 RID: 18946 RVA: 0x00103111 File Offset: 0x00101311
		// (set) Token: 0x06004A03 RID: 18947 RVA: 0x00103119 File Offset: 0x00101319
		[DataMember]
		public string FreeBusyViewType { get; set; }

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06004A04 RID: 18948 RVA: 0x00103122 File Offset: 0x00101322
		// (set) Token: 0x06004A05 RID: 18949 RVA: 0x0010312A File Offset: 0x0010132A
		[DataMember]
		public string MergedFreeBusy { get; set; }

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06004A06 RID: 18950 RVA: 0x00103133 File Offset: 0x00101333
		// (set) Token: 0x06004A07 RID: 18951 RVA: 0x0010313B File Offset: 0x0010133B
		[DataMember]
		public EwsCalendarItemType[] Items { get; set; }

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06004A08 RID: 18952 RVA: 0x00103144 File Offset: 0x00101344
		// (set) Token: 0x06004A09 RID: 18953 RVA: 0x0010314C File Offset: 0x0010134C
		[DataMember]
		public WorkingHoursType WorkingHours { get; set; }
	}
}
