using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003AC RID: 940
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentItemsSort
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00076457 File Offset: 0x00074657
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x0007645F File Offset: 0x0007465F
		[DataMember]
		public AttachmentItemsSortColumn SortColumn { get; set; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x00076468 File Offset: 0x00074668
		// (set) Token: 0x06001DF4 RID: 7668 RVA: 0x00076470 File Offset: 0x00074670
		[DataMember]
		public AttachmentItemsSortOrder SortOrder { get; set; }
	}
}
