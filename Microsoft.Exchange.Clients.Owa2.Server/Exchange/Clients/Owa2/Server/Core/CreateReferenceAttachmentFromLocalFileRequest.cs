using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B7 RID: 951
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateReferenceAttachmentFromLocalFileRequest
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x000768E4 File Offset: 0x00074AE4
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x000768EC File Offset: 0x00074AEC
		[DataMember]
		public ItemId ParentItemId { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x000768F5 File Offset: 0x00074AF5
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x000768FD File Offset: 0x00074AFD
		[DataMember]
		public string FileName { get; set; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00076906 File Offset: 0x00074B06
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x0007690E File Offset: 0x00074B0E
		[DataMember]
		public string FileContentToUpload { get; set; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00076917 File Offset: 0x00074B17
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x0007691F File Offset: 0x00074B1F
		[DataMember]
		public string SubscriptionId { get; set; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00076928 File Offset: 0x00074B28
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x00076930 File Offset: 0x00074B30
		[DataMember]
		public string ChannelId { get; set; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00076939 File Offset: 0x00074B39
		// (set) Token: 0x06001E83 RID: 7811 RVA: 0x00076941 File Offset: 0x00074B41
		[DataMember]
		public string CancellationId { get; set; }
	}
}
