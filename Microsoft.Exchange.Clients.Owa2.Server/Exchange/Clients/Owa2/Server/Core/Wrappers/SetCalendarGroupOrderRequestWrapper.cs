using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B1 RID: 689
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarGroupOrderRequestWrapper
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x0005413E File Offset: 0x0005233E
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x00054146 File Offset: 0x00052346
		[DataMember(Name = "groupToPosition")]
		public string GroupToPosition { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0005414F File Offset: 0x0005234F
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x00054157 File Offset: 0x00052357
		[DataMember(Name = "beforeGroup")]
		public string BeforeGroup { get; set; }
	}
}
