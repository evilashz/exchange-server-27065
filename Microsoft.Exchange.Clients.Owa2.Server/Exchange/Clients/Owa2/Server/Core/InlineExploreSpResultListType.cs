using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003E1 RID: 993
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InlineExploreSpResultListType
	{
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x0007842A File Offset: 0x0007662A
		// (set) Token: 0x06001FDD RID: 8157 RVA: 0x00078432 File Offset: 0x00076632
		[DataMember]
		public int ResultCount { get; set; }

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x0007843B File Offset: 0x0007663B
		// (set) Token: 0x06001FDF RID: 8159 RVA: 0x00078443 File Offset: 0x00076643
		[DataMember]
		public InlineExploreSpResultItemType[] ResultItems { get; set; }

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x0007844C File Offset: 0x0007664C
		// (set) Token: 0x06001FE1 RID: 8161 RVA: 0x00078454 File Offset: 0x00076654
		[DataMember]
		public string Status { get; set; }
	}
}
