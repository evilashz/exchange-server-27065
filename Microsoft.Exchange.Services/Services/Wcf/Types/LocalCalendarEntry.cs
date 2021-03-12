using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A30 RID: 2608
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LocalCalendarEntry : CalendarEntry
	{
		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x00102A0C File Offset: 0x00100C0C
		// (set) Token: 0x06004996 RID: 18838 RVA: 0x00102A14 File Offset: 0x00100C14
		[DataMember]
		public bool IsInternetCalendar { get; set; }

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06004997 RID: 18839 RVA: 0x00102A1D File Offset: 0x00100C1D
		// (set) Token: 0x06004998 RID: 18840 RVA: 0x00102A25 File Offset: 0x00100C25
		[DataMember]
		public bool IsDefaultCalendar { get; set; }

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06004999 RID: 18841 RVA: 0x00102A2E File Offset: 0x00100C2E
		// (set) Token: 0x0600499A RID: 18842 RVA: 0x00102A36 File Offset: 0x00100C36
		[DataMember]
		public FolderId CalendarFolderId { get; set; }
	}
}
