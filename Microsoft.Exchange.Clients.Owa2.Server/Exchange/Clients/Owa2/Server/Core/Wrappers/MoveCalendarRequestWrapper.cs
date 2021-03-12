using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200029C RID: 668
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MoveCalendarRequestWrapper
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00053E87 File Offset: 0x00052087
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x00053E8F File Offset: 0x0005208F
		[DataMember(Name = "calendarToMove")]
		public FolderId CalendarToMove { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00053E98 File Offset: 0x00052098
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x00053EA0 File Offset: 0x000520A0
		[DataMember(Name = "parentGroupId")]
		public string ParentGroupId { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00053EA9 File Offset: 0x000520A9
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x00053EB1 File Offset: 0x000520B1
		[DataMember(Name = "calendarBefore")]
		public FolderId CalendarBefore { get; set; }
	}
}
