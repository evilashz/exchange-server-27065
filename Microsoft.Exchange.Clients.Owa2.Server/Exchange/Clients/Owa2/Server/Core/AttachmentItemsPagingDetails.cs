using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003AA RID: 938
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentItemsPagingDetails
	{
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x000763F2 File Offset: 0x000745F2
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x000763FA File Offset: 0x000745FA
		[DataMember]
		public IndexedPageView RequestedData { get; set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00076403 File Offset: 0x00074603
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0007640B File Offset: 0x0007460B
		[DataMember]
		public string Location { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x00076414 File Offset: 0x00074614
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0007641C File Offset: 0x0007461C
		[DataMember]
		public AttachmentItemsSort Sort { get; set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00076425 File Offset: 0x00074625
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x0007642D File Offset: 0x0007462D
		[DataMember]
		public AttachmentItemsPagingMetadata PagingMetadata { get; set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x00076436 File Offset: 0x00074636
		// (set) Token: 0x06001DEE RID: 7662 RVA: 0x0007643E File Offset: 0x0007463E
		[DataMember]
		public string ItemsEndpointUrl { get; set; }
	}
}
