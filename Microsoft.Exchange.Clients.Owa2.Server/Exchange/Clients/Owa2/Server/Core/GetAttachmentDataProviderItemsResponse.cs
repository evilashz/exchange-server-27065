using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003BE RID: 958
	[DataContract]
	public class GetAttachmentDataProviderItemsResponse
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x00076B0E File Offset: 0x00074D0E
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x00076B16 File Offset: 0x00074D16
		[DataMember]
		public AttachmentResultCode ResultCode { get; set; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x00076B1F File Offset: 0x00074D1F
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x00076B27 File Offset: 0x00074D27
		[DataMember]
		public AttachmentDataProviderItem[] Items { get; set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00076B30 File Offset: 0x00074D30
		// (set) Token: 0x06001EBA RID: 7866 RVA: 0x00076B38 File Offset: 0x00074D38
		[DataMember]
		public AttachmentItemsPagingMetadata PagingMetadata { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00076B41 File Offset: 0x00074D41
		// (set) Token: 0x06001EBC RID: 7868 RVA: 0x00076B49 File Offset: 0x00074D49
		[DataMember]
		public int TotalItemCount { get; set; }
	}
}
