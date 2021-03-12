using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A6 RID: 678
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RenameCalendarRequestWrapper
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00053FE7 File Offset: 0x000521E7
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x00053FEF File Offset: 0x000521EF
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00053FF8 File Offset: 0x000521F8
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x00054000 File Offset: 0x00052200
		[DataMember(Name = "newCalendarName")]
		public string NewCalendarName { get; set; }
	}
}
