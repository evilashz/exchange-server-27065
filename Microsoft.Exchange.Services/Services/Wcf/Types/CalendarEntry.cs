using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A0F RID: 2575
	[KnownType(typeof(LinkedCalendarEntry))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(LocalCalendarEntry))]
	public abstract class CalendarEntry
	{
		// Token: 0x0600489E RID: 18590 RVA: 0x00101A5C File Offset: 0x000FFC5C
		public CalendarEntry()
		{
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600489F RID: 18591 RVA: 0x00101A64 File Offset: 0x000FFC64
		// (set) Token: 0x060048A0 RID: 18592 RVA: 0x00101A6C File Offset: 0x000FFC6C
		[DataMember]
		public ItemId ItemId { get; set; }

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060048A1 RID: 18593 RVA: 0x00101A75 File Offset: 0x000FFC75
		// (set) Token: 0x060048A2 RID: 18594 RVA: 0x00101A7D File Offset: 0x000FFC7D
		[DataMember]
		public string CalendarName { get; set; }

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060048A3 RID: 18595 RVA: 0x00101A86 File Offset: 0x000FFC86
		// (set) Token: 0x060048A4 RID: 18596 RVA: 0x00101A8E File Offset: 0x000FFC8E
		[DataMember]
		public CalendarColor CalendarColor { get; set; }

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060048A5 RID: 18597 RVA: 0x00101A97 File Offset: 0x000FFC97
		// (set) Token: 0x060048A6 RID: 18598 RVA: 0x00101A9F File Offset: 0x000FFC9F
		[DataMember]
		public string ParentGroupId { get; set; }

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x060048A7 RID: 18599 RVA: 0x00101AA8 File Offset: 0x000FFCA8
		// (set) Token: 0x060048A8 RID: 18600 RVA: 0x00101AB0 File Offset: 0x000FFCB0
		[DataMember]
		public bool IsGroupMailboxCalendar { get; set; }

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x060048A9 RID: 18601 RVA: 0x00101AB9 File Offset: 0x000FFCB9
		// (set) Token: 0x060048AA RID: 18602 RVA: 0x00101AC1 File Offset: 0x000FFCC1
		[DataMember]
		public CalendarFolderTypeEnum CalendarFolderType { get; set; }
	}
}
