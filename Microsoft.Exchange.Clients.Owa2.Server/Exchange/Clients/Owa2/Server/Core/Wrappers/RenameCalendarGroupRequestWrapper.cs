using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A7 RID: 679
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RenameCalendarGroupRequestWrapper
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00054011 File Offset: 0x00052211
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x00054019 File Offset: 0x00052219
		[DataMember(Name = "groupId")]
		public ItemId GroupId { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00054022 File Offset: 0x00052222
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0005402A File Offset: 0x0005222A
		[DataMember(Name = "newGroupName")]
		public string NewGroupName { get; set; }
	}
}
