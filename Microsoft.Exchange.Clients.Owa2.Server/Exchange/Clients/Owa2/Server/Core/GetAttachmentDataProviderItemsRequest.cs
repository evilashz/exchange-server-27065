using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003BF RID: 959
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAttachmentDataProviderItemsRequest
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x00076B5A File Offset: 0x00074D5A
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x00076B62 File Offset: 0x00074D62
		[DataMember]
		public string AttachmentDataProviderId { get; set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00076B6B File Offset: 0x00074D6B
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x00076B73 File Offset: 0x00074D73
		[DataMember]
		public AttachmentItemsPagingDetails Paging { get; set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x00076B7C File Offset: 0x00074D7C
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x00076B84 File Offset: 0x00074D84
		[DataMember]
		public AttachmentDataProviderScope Scope { get; set; }
	}
}
