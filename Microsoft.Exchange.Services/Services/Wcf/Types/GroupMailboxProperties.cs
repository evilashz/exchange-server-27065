using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E9 RID: 2537
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GroupMailboxProperties
	{
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x001006A9 File Offset: 0x000FE8A9
		// (set) Token: 0x060047A3 RID: 18339 RVA: 0x001006B1 File Offset: 0x000FE8B1
		[DataMember]
		public int SubscribersCount { get; set; }

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x060047A4 RID: 18340 RVA: 0x001006BA File Offset: 0x000FE8BA
		// (set) Token: 0x060047A5 RID: 18341 RVA: 0x001006C2 File Offset: 0x000FE8C2
		[DataMember]
		public bool CanUpdateAutoSubscribeFlag { get; set; }

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x060047A6 RID: 18342 RVA: 0x001006CB File Offset: 0x000FE8CB
		// (set) Token: 0x060047A7 RID: 18343 RVA: 0x001006D3 File Offset: 0x000FE8D3
		[DataMember]
		public int LanguageLCID { get; set; }
	}
}
