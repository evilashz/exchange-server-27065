using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003A3 RID: 931
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GroupAttachmentDataProviderItem : AttachmentDataProviderItem
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x00076352 File Offset: 0x00074552
		public GroupAttachmentDataProviderItem()
		{
			base.Type = AttachmentDataProviderItemType.Group;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x00076361 File Offset: 0x00074561
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x00076369 File Offset: 0x00074569
		[DataMember]
		public AttachmentItemsPagingMetadata PagingMetadata { get; set; }

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x00076372 File Offset: 0x00074572
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x0007637A File Offset: 0x0007457A
		[DataMember]
		public string GroupCategory { get; set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00076383 File Offset: 0x00074583
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0007638B File Offset: 0x0007458B
		[DataMember]
		[DateTimeString]
		public string JoinDate { get; set; }
	}
}
