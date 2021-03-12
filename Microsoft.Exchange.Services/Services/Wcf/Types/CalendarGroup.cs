using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A11 RID: 2577
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarGroup
	{
		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x060048AC RID: 18604 RVA: 0x00101AD2 File Offset: 0x000FFCD2
		// (set) Token: 0x060048AD RID: 18605 RVA: 0x00101ADA File Offset: 0x000FFCDA
		[DataMember]
		public ItemId ItemId { get; set; }

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x00101AE3 File Offset: 0x000FFCE3
		// (set) Token: 0x060048AF RID: 18607 RVA: 0x00101AEB File Offset: 0x000FFCEB
		[DataMember]
		public string GroupId { get; set; }

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x060048B0 RID: 18608 RVA: 0x00101AF4 File Offset: 0x000FFCF4
		// (set) Token: 0x060048B1 RID: 18609 RVA: 0x00101AFC File Offset: 0x000FFCFC
		[DataMember]
		public string GroupName { get; set; }

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x060048B2 RID: 18610 RVA: 0x00101B05 File Offset: 0x000FFD05
		// (set) Token: 0x060048B3 RID: 18611 RVA: 0x00101B0D File Offset: 0x000FFD0D
		[DataMember]
		public CalendarGroupType GroupType { get; set; }

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x00101B16 File Offset: 0x000FFD16
		// (set) Token: 0x060048B5 RID: 18613 RVA: 0x00101B1E File Offset: 0x000FFD1E
		[DataMember]
		public CalendarEntry[] Calendars { get; set; }
	}
}
