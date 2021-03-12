using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A3A RID: 2618
	[DataContract]
	public class SyncCalendarResponse
	{
		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x060049E7 RID: 18919 RVA: 0x00102F7B File Offset: 0x0010117B
		// (set) Token: 0x060049E8 RID: 18920 RVA: 0x00102F83 File Offset: 0x00101183
		[DataMember]
		public string SyncState { get; set; }

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x060049E9 RID: 18921 RVA: 0x00102F8C File Offset: 0x0010118C
		// (set) Token: 0x060049EA RID: 18922 RVA: 0x00102F94 File Offset: 0x00101194
		[DataMember]
		public bool IncludesLastItemInRange { get; set; }

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x060049EB RID: 18923 RVA: 0x00102F9D File Offset: 0x0010119D
		// (set) Token: 0x060049EC RID: 18924 RVA: 0x00102FA5 File Offset: 0x001011A5
		[DataMember(EmitDefaultValue = false)]
		public ItemId[] DeletedItems { get; set; }

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x060049ED RID: 18925 RVA: 0x00102FAE File Offset: 0x001011AE
		// (set) Token: 0x060049EE RID: 18926 RVA: 0x00102FB6 File Offset: 0x001011B6
		[DataMember(EmitDefaultValue = false)]
		public EwsCalendarItemType[] UpdatedItems { get; set; }

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x060049EF RID: 18927 RVA: 0x00102FBF File Offset: 0x001011BF
		// (set) Token: 0x060049F0 RID: 18928 RVA: 0x00102FC7 File Offset: 0x001011C7
		[DataMember(EmitDefaultValue = false)]
		public EwsCalendarItemType[] RecurrenceMastersWithInstances { get; set; }

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x00102FD0 File Offset: 0x001011D0
		// (set) Token: 0x060049F2 RID: 18930 RVA: 0x00102FD8 File Offset: 0x001011D8
		[DataMember(EmitDefaultValue = false)]
		public EwsCalendarItemType[] RecurrenceMastersWithoutInstances { get; set; }

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x060049F3 RID: 18931 RVA: 0x00102FE1 File Offset: 0x001011E1
		// (set) Token: 0x060049F4 RID: 18932 RVA: 0x00102FE9 File Offset: 0x001011E9
		[DataMember(EmitDefaultValue = false)]
		public EwsCalendarItemType[] UnchangedRecurrenceMastersWithInstances { get; set; }
	}
}
